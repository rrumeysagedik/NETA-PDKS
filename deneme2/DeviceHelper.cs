using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using zkemkeeper;
namespace deneme2
{
    public class DeviceHelper
    {
        public CZKEMClass _axCZKEM1 = new CZKEMClass();
        public CZKEMClass Context
        {
            get
            {
                _axCZKEM1 = _axCZKEM1 ?? new CZKEMClass();//nul gitmesine engel olmak için bu şekilde.
                return _axCZKEM1;
            }
            set
            {
                _axCZKEM1 = value;
            }
        }
        public DeviceHelper(int m)
        {
            iMachineNumber = m;
        }
        public int iMachineNumber { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public string ComKey { get; set; }
        public bool ConnectState { get; set; }
        public event Action<string> MesajVer;
        public event Action<string, string> Log;
        public List<User> UserList { get; set; }
        public void DisConnect()
        {
            if (ConnectState)
            {
                try
                {
                    Context.Disconnect();
                    ConnectState = false;
                    MesajVer?.Invoke($"{IP}-Cihaz bağlantısı kesildi.");
                }
                catch (Exception)
                {
                    MesajVer?.Invoke($"{IP}-Cihaz bağlantısı kesilemiyor.");
                }
            }
        }
        private string getSysOptions(string option)
        {
            string value = string.Empty;
            Context.GetSysOption(iMachineNumber, option, out value);
            return value;
        }
        public SupportBiometricType getBiometricType()
        {
            SupportBiometricType r = new SupportBiometricType();
            string result = string.Empty;
            result = getSysOptions("BiometricType");
            if (!string.IsNullOrEmpty(result))
            {
                r.fp_available = result[1] == '1';
                r.face_available = result[2] == '1';
                if (result.Length >= 9)
                {
                    r.fingerVein_available = result[7] == '1';
                    r.palm_available = result[8] == '1';
                }

            }
            return r;
        }

        public int ConnectTCP()
        {
            if (IP == "" || Port == "") //|| commKey == "")
            {
                return -1; // ip or port is null
            }

            if (Convert.ToInt32(Port) <= 0 || Convert.ToInt32(Port) > 65535)
            {
                return -1; //Port İllegal!
            }

            //if (Convert.ToInt32(ComKey) < 0 || Convert.ToInt32(ComKey) > 999999)
            //{
            //    return -1; // commKey İllegal!
            //}   
            // Context.SetCommPassword(Convert.ToInt32(commKey));

            // Cihaza daha önce bağlantı açılmış ise bağlantıyı kapatıyor zaten cihaz bağlı diyor.
            if (ConnectState)
            {
                Context.Disconnect();
                ConnectState = false;
            }
            if (Context.Connect_Net(IP, Convert.ToInt32(Port)) == true)
            {
                ConnectState = true;
                MesajVer?.Invoke($"{IP}-Cihaza bağlanıldı.");//soru işareti null kontrolu yapıyor.
                Log?.Invoke($"{IP}-Bağlantı", "Başarılı");

                var bio = getBiometricType();
                if (bio != null)
                {
                    Log?.Invoke($"{IP}-Face Available", bio.face_available.ToString());
                    Log?.Invoke($"{IP}-Finger Print Available", bio.fp_available.ToString());
                    Log?.Invoke($"{IP}-Finger Vein Available", bio.fingerVein_available.ToString());
                    Log?.Invoke($"{IP}-Palm Available", bio.palm_available.ToString());
                }
                else
                {
                    Log?.Invoke($"{IP}-Bio Data Available", "false");

                }
                return 1;
            }
            else
            {
                int idwErrorCode = 1;
                Context.GetLastError(ref idwErrorCode);
                MesajVer?.Invoke($"Cihaz bağlanamıyor! Code:{idwErrorCode}");
                Log?.Invoke($"{IP}-Bağlantı", $"Başarısız:{idwErrorCode}");
                return idwErrorCode;
            }

        }

        public Result<List<UserData>> TumDataGetir()
        {

            //cihaza bağlantı açmadan data çekemeyeceğimiz için bağlantıyı aç komutunu çağırdım.Yani bu kapıları açarki biz içeri girelim.Data alabilelim.

            // ConnectTCP(ip, port, comKey);


            if (!ConnectState)
            {
                //Result r = new Result();
                //r.Code = -1024;
                //r.Message = "Please Login First";
                //return r;
                return new Result<List<UserData>> { Message = "*İlk önce cihaz ile bağlantı kurun!", Code = 1024 };//Object initializer
            }


            //Cihazı devre dışı bırakıyorum ki data okurkenn birileri parmak okutmaya çalışıp engel olmasınlar diye daha sonra satırları okumaya başlıyorum.
            Context.EnableDevice(iMachineNumber, false);//disable the device
            MesajVer?.Invoke($"{IP}-Cihaz devredışı!");


            //bir ihtimal ile users getirme komutlarını buraya taşımak gerekir.
            /*
            users = users ?? new List<User>();

            if (Context.ReadAllUserID(iMachineNumber))
            {
                string sEnrollNumber = "";
                bool bEnabled = false;
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                while (Context.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    User u = new User();
                    u.UserID = Convert.ToInt32(sEnrollNumber);
                    u.UserName = sName;
                    users.Add(u);
                }
            }
            */

            Result<List<UserData>> res = new Result<List<UserData>>();
            if (Context.ReadGeneralLogData(iMachineNumber)) //tüm katılım kayıtlarını belleğe oku
            {
                res.Data = new List<UserData>();//listeyi oluştur
                MesajVer?.Invoke($"{IP}-Reading Genereals");
                Log?.Invoke($"{IP}-ReadGeneralLogData", "Started");
                string sdwEnrollNumber = "";
                int idwVerifyMode = 0;
                int idwInOutMode = 0;
                int idwYear = 0;
                int idwMonth = 0;
                int idwDay = 0;
                int idwHour = 0;
                int idwMinute = 0;
                int idwSecond = 0;
                int idwWorkcode = 0;
                while (Context.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    int ui = Convert.ToInt32(sdwEnrollNumber);
                    var user = UserList?.FirstOrDefault(x => x.UserID == ui);//excelden gelen listeden userId ile bul
                    UserData ud = new UserData();
                    ud.UserID = ui;
                    ud.UserName = user.UserName;
                    ud.VerifyDateRaw = $"{idwYear}-{idwMonth}-{idwDay} {idwHour}:{idwMinute}:{idwSecond}";
                    ud.WorkCode = idwWorkcode;
                    ud.InOutMode = idwInOutMode;
                    ud.VerifyMode = idwVerifyMode;
                    res.Data.Add(ud);
                }
                MesajVer?.Invoke($"{IP}-Read Complated");
                res.Code = 1;
                res.Message = "Read Complated";
            }
            else
            {

                int ec = 1;
                Context.GetLastError(ref ec);
                Log?.Invoke($"{IP}-ReadGeneralLogData", $"{ec}");


                if (ec == -100 || ec == 0)
                {
                    res = new Result<List<UserData>> { Data = null, Message = "Veri yok ", Code = ec };
                   
                    

                }
                else
                {

                    res = new Result<List<UserData>> { Data = null, Code = ec };
                   
                }

            }


            //lblOutputInfo.Items.Add("[func ReadTimeGLogData]Temporarily unsupported");
            Context.EnableDevice(iMachineNumber, true);//enable the device

            return res;
        }

        /// <summary>
        /// DataTable içinde verilen dataları result önceden dolu verilirse içerisini doldurur.Result verilmez ise yeni result listesi oluşturur.
        /// </summary>
        /// <param name="dt">Dönüştürülecek alanlar içerisinde UserID ve VerifyDate olmak zorundadır.</param>
        /// <param name="result">Elimizde UserList var ise içerisini doldurmak amacıyla..</param>
        /// <returns></returns>
        public static List<User> ConvertUser(List<UserData> dt, List<User> result = null)//parametre ile tanımlanan değerlere default değer denir(=null)
        {
            result = result ?? new List<User>();//null ise new user yap
            if (dt != null)
            {
                foreach (UserData row in dt.OrderBy(x => x.UserID).ThenBy(x => x.VerifyDate))
                {

                    var u = result.FirstOrDefault(x => x.UserID == row.UserID);//listenin içerisinden userId si bizim userId değişkenine eşit olanı getirir. (=>) lambda query oluşturur. 
                    if (u == null)
                    {
                        u = new User { UserID = row.UserID, UserName = row.UserName };
                        result.Add(u);
                    }
                    var ud = new UserData
                    {
                        UserID = row.UserID,
                        VerifyDateRaw = row.UserName
                    };
                    u.Logs.Add(ud);

                }
            }

            return result;
        }
        /// <summary>
        /// UserData Listesini alıp tarihe göre filtreleyip User lara gruplar her user datalarını kendi user nesnesi içine koyar
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="bas"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        public static List<User> ConvertUser(List<UserData> dt, DateTime bas, DateTime bit)
        {
            var result = new List<User>();

            if (dt != null)
            {
                foreach (UserData row in dt.Where(x => x.VerifyDate >= bas && x.VerifyDate <= bit).OrderBy(x => x.UserID).ThenBy(x => x.VerifyDate))
                {

                    var u = result.FirstOrDefault(x => x.UserID == row.UserID);
                    if (u == null)
                    {
                        u = new User { UserID = row.UserID, UserName = row.UserName };
                        result.Add(u);
                    }
                    var ud = new UserData
                    {
                        UserID = row.UserID,
                        UserName = row.UserName,
                        VerifyDateRaw = row.VerifyDateRaw
                    };
                    u.Logs.Add(ud);

                }
            }

            return result;
        }

        public List<User> GetAllUser()
        {
            List<User> users = new List<User>();

            Context.EnableDevice(iMachineNumber, false);
            if (Context.ReadAllUserID(iMachineNumber))
            {
                string sEnrollNumber = "";
                bool bEnabled = false;
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                while (Context.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
                {
                    User u = new User();
                    u.UserID = Convert.ToInt32(sEnrollNumber);
                    u.UserName = sName;
                    users.Add(u);
                }
            }
            Context.EnableDevice(iMachineNumber, true);
            return users;
        }

    }

    public class SupportBiometricType
    {
        public bool fp_available { get; set; }
        public bool face_available { get; set; }
        public bool fingerVein_available { get; set; }
        public bool palm_available { get; set; }
    }
    public class Device
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public string ComKey { get; set; }
        public string MachineNumber { get; set; }

    }
    public class Result
    {
        public DataTable Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }

    }
    public class Result<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
    /// <summary>
    /// Giriş çıkış kaydını tutan class
    /// </summary>
    public class UserData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string VerifyDateRaw { get; set; }
        public int VerifyMode { get; set; }
        public int WorkCode { get; set; }
        public int InOutMode { get; set; }

        public DateTime? VerifyDate //verifydate alanını datetime tipinde dönüştüren property yazdık projede dönüştürmekle uğraşmayacağız hali hazırda içinde dataetime tipinde tutmuş olacak.Faydası ise zamansal büyüklük olarak değerlendirebileceğiz.
        {
            get
            {
                if (string.IsNullOrEmpty(VerifyDateRaw)) return null;
                DateTime d;
                if (DateTime.TryParse(VerifyDateRaw, out d))
                {
                    return d;
                }
                else return null;
            }
            set
            {
                VerifyDateRaw = value.HasValue ? value.Value.ToString("yyyy/MM/dd HH:mm:ss") : string.Empty;
            }
        }
    }

    public class LeftView
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Times { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartTime2 { get; set; }
        public string EndTime2 { get; set; }
        public string StartTime3 { get; set; }
        public string EndTime3 { get; set; }
        public string Elapsed { get; set; }
        public string WeekElapsed { get; set; }
        public UserDay Day { get; set; }


    }

    public class User
    {
        //classı kullnacağmda logs ve days listelerinin null olmamasını garantiliyorum. New user dendiği anda onlarda newlenmiş oluyor.
        public User()
        {
            Logs = new List<UserData>();

        }
        public int UserID { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// Cihazdan gelen datalar
        /// </summary>
        public List<UserData> Logs { get; set; }
        public List<UserDay> Days //property
        {
            get
            {
                var days = new List<UserDay>();

                days = Logs.GroupBy(x => x.VerifyDate.Value.Date).Select(x => new UserDay { Date = x.Key, Logs = x.ToList() }).ToList();
                //groupby metodu vrdiğimz alana göre kayıtları kümeler.Select metodudanher bir küme için userday nesnesi oluşturuyor.Burada grupladığımız alanı x.key ile alıyoruz.
                //Logs içerisndeki kayıtlrı aldı, gün gün gruplayarak gruplu halde verdi.Hesaplamak istedğimizi ogün içersinde hesaplayabliriz.Dolayısıyla saat çıkarımında buradan yapabiliriz.
                return days;
            }
        }
        public string TotalShift
        {
            get
            {
                //günün içinde başlama saatindeki saati alıp. bitiş saatindeki saati alıp. aradaki farkları hesaplayıp toplayabiliriz. 

                /*Seçilebilecek Modeller
                 *1 en erken basılanı giriş al en geç basılanı çıkış al aradaki farkı hesapla
                 *2 ilk basılanı giriş al sonrakini çıkış al sonrakini giriş al vb.
                 *3 sabah saatlerinde basılanı giriş al <12 öğlen saatinde basılanı çıkış al 12>13  öğlenden sonra basılanı giriş al >13 akşam basılanı çıkış al >5
                 */

                //2. Model
                TimeSpan total = new TimeSpan();
                foreach (var day in Days)
                {
                    total = total.Add(day.DayElapsed);
                    //var sabah = day.Sabah;
                    //var oglen = day.Oglen;
                    //var oglen_gir = day.OgleSonra;
                    //var aksam = day.Aksam;

                    ////sabah kendi içinde 
                    //if (sabah != null && oglen != null)
                    //{
                    //    total = total.Add(oglen.Value - sabah.Value);
                    //}

                    ////öğlen kendi içinde 
                    //if (oglen_gir != null && aksam != null)
                    //{
                    //    total = total.Add(aksam.Value - oglen_gir.Value);
                    //}

                    //////////////
                    ///

                    //öğlen değerleri yoksa sabahtan akşama çalışmış kabul edip. öğlen değerleri varsa onları çalışma saatinden çıkartan komut. 
                    /*
                    if (sabah!=null&&aksam!=null)
                    {
                        total = total.Add(aksam.Value - sabah.Value);
                    }
                    if (oglen!=null&&oglen_gir!=null)
                    {
                        total = total.Subtract(oglen_gir.Value - oglen.Value);
                    }*/
                }

                return $"{(int)total.TotalHours}:{total.Minutes}";


            }
        }
        public string Mail { get; set; }
        public bool Izin { get; set; }
        public List<LeftView> DeclareLeftView()
        {
            List<LeftView> result = new List<LeftView>();
            foreach (UserDay item in Days)
            {
                if (item.DayElapsed.TotalHours > 11)
                {

                    var liste = item.Logs.OrderBy(x => x.VerifyDate).ToList();
                    if (liste.Count >= 2)
                    {
                        do
                        {
                            TimeSpan fazlalik = item.DayElapsed.Add(new TimeSpan(-11, 0, 0));
                            int ind = liste.Count - 1;
                            ind -= (1-ind % 2);
                            if (ind < 1) break;
                            var sonuncu = liste[ind];
                            var baslangic = liste[ind-1];

                            if (sonuncu != null && baslangic != null)
                            {
                                var fark = sonuncu.VerifyDate - baslangic.VerifyDate;
                                if (fark.Value.Ticks > fazlalik.Ticks)
                                {
                                    sonuncu.VerifyDate = sonuncu.VerifyDate.Value.Add(new TimeSpan(fazlalik.Ticks * -1));
                                }
                                else
                                {
                                    item.Logs.Remove(baslangic);
                                    item.Logs.Remove(sonuncu);
                                    liste.Remove(baslangic);
                                    liste.Remove(sonuncu);
                                }
                            }
                        } while (item.DayElapsed.TotalHours > 11);
                    }
                }

                /*
                 2:30 5:30  5 saat fazla  12:30
                 12:30 2:30 hala 2 saat fazla
                 */
                LeftView lv = new LeftView();
                lv.Day = item;
                lv.UserID = UserID;
                lv.UserName = UserName;
                lv.Date = item.Date.ToString("dd.MM.yyyy");
                lv.Times = item.Times;
                lv.WeekElapsed = TotalShift;
                //TimeSpan elapsed = new TimeSpan();
                //if (item.Sabah != null && item.Oglen != null)
                //{
                //    lv.StartTime = $"{item.Sabah.Value.Hour}:{item.Sabah.Value.Minute}";
                //    lv.EndTime = $"{item.Oglen.Value.Hour}:{item.Oglen.Value.Minute}";
                //    elapsed = elapsed.Add(item.Oglen.Value - item.Sabah.Value);
                //}
                //if (item.OgleSonra != null && item.Aksam != null)
                //{

                //    lv.StartTime2 = $"{item.OgleSonra.Value.Hour}:{item.OgleSonra.Value.Minute}";
                //    lv.EndTime2 = $"{item.Aksam.Value.Hour}:{item.Aksam.Value.Minute}";
                //    elapsed = elapsed.Add(item.Aksam.Value - item.OgleSonra.Value);

                //}

                lv.Elapsed = $"{(int)item.DayElapsed.TotalHours}:{item.DayElapsed.Minutes}";
                result.Add(lv);

            }

            return result;
        }

        /*
         * Excel oluşturmada 2 model vardır.
         *1) Database olarak yazdırma :sanki database tablosuymuş gibi bağlanıp yazdırılır. OleDbConnection ile bağlanılır. OleDbCommand ile insert komutu gönderilebilr.
         *2) DataSheet olarak yazdırma : sheet row ve cell ler birer nesne olarak oluşturulup eklenir. puzzle gibi 
         * 
         * 1. yöntemi uygulayacağım. 
         */

    }
    public class UserDay
    {
        public UserDay()
        {
            Logs = new List<UserData>();
        }

        const int mesaiBaslangicSaat = 7;
        const int mesaiBaslangicDakika = 30;
        const int YemegeCikisSaat = 11;
        const int YemegeCikisDakika = 30;
        const int YemektenDonusSaat = 14;
        const int yemektenDonusDakika = 30;
        const int mesaiBitisSaat = 17;
        const int mesaiBitisDakika = 30;


        public DateTime Date { get; set; }
        public List<UserData> Logs { get; set; }
        public string Times
        {
            get
            {
                string r = string.Empty;
                foreach (var l in Logs.OrderBy(x => x.VerifyDate))
                {
                    r += $"{l.VerifyDate.Value.Hour}:{l.VerifyDate.Value.Minute} ";
                }
                return r;
            }
        }

        public List<DateTime> InOutList
        {
            get
            {
                return Logs.Where(x => x.VerifyDate.HasValue).OrderBy(x => x.VerifyDate).Select(x => x.VerifyDate.Value).ToList();

            }
        }

        public TimeSpan DayElapsed
        {
            get
            {
                TimeSpan total = new TimeSpan();
                var list = InOutList.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i % 2 == 1)
                    {
                        total = total.Add(list[i] - list[i - 1]);
                    }
                }
                return total;
            }
        }








        //geriye DateTime dönen ve giriş saatini logs kaydının içinden hesaplayıp ilk kaydını getiren bir property orderby ile sıraladık firsordefault ile ilk kaydı getirdim
        public DateTime? Sabah
        {
            get
            {
                var v = Logs?.Where(log =>

                log.VerifyDate.HasValue &&
                log.VerifyDate.Value.Hour < YemegeCikisSaat ||
                (log.VerifyDate.Value.Hour == YemegeCikisSaat && log.VerifyDate.Value.Minute <= YemegeCikisDakika)


                ).OrderBy(log => log.VerifyDate).FirstOrDefault();
                return v?.VerifyDate;
            }
        }

        //Oglen kaydı için saat 12 den büyük olan  ilk kaydı  getir. 
        public DateTime? Oglen
        {

            get
            {
                var b = Logs?.Where(log =>
                log.VerifyDate.HasValue &&

                log.VerifyDate.Value.Hour > YemegeCikisSaat ||

                (log.VerifyDate.Value.Hour == YemegeCikisSaat && log.VerifyDate.Value.Minute >= YemegeCikisDakika)
                ).OrderBy(log => log.VerifyDate).FirstOrDefault();
                return b?.VerifyDate;
            }

        }

        //Ogledensonra için saat 12 ile 13.30 arasında olan SON kaydı getir 
        public DateTime? OgleSonra
        {
            get
            {
                //11:30 - 14:30
                //11:20 
                //12:45 ok
                //14:20 ok
                //14:40 


                var n = Logs?.Where(log =>
                log.VerifyDate.HasValue &&

                (log.VerifyDate.Value.Hour > YemegeCikisSaat ||
                (log.VerifyDate.Value.Hour == YemegeCikisSaat && log.VerifyDate.Value.Minute >= YemegeCikisDakika))
                &&
                (log.VerifyDate.Value.Hour < YemektenDonusSaat
                ||
                (log.VerifyDate.Value.Hour == YemektenDonusSaat && log.VerifyDate.Value.Minute <= yemektenDonusDakika))

                ).OrderBy(log => log.VerifyDate).Take(2);

                if (n.Any() && n.Count() == 2)//kayıt var mı 
                {
                    return n.LastOrDefault()?.VerifyDate;
                }
                else
                {
                    return null;
                }
            }
        }

        //akşam kaydı için saat 13.30 dan sonra olan son kaydı getir.
        public DateTime? Aksam
        {
            ////////////////////////////////////////////////////12:15////////////////////////////////////////////////////////////////////
            get
            {
                /*
                 14:00-21:00 =7

                14.00-17.15
                18.10-23.25

                14:00-23:25
                

                 */
                var m = Logs?.Where(log =>
                log.VerifyDate.HasValue &&

                log.VerifyDate.Value.Hour > YemektenDonusSaat ||
                (log.VerifyDate.Value.Hour == YemektenDonusSaat && log.VerifyDate.Value.Minute > yemektenDonusDakika)

                ).OrderBy(log => log.VerifyDate).LastOrDefault();
                return m?.VerifyDate;
            }
        }

        // var sabah = day.Logs.FirstOrDefault(x => x.VerifyDate.HasValue && x.VerifyDate.Value.Hour < 12)?.VerifyDate;//eğer firstordefault null verirse verifydate hata vermesin diye ?. ile bağladık.
        // var oglen = day.Logs.FirstOrDefault(x => x.VerifyDate.HasValue && x.VerifyDate.Value.Hour >= 12 && x.VerifyDate.Value.Hour < 13)?.VerifyDate;
        //var oglen_gir = day.Logs.FirstOrDefault(x => x.VerifyDate.HasValue && x.VerifyDate.Value.Hour >= 13 && x.VerifyDate.Value.Hour < 17)?.VerifyDate;
        //var a = day.Logs.FirstOrDefault(x => x.VerifyDate.HasValue && x.VerifyDate.Value.Hour > 13);
        //var aksam = a?.VerifyDate;

        //userday nesnesi oluşturulduğu anda logs listesidee oluşturulur.
    }
}
