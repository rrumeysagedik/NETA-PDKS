using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


using zkemkeeper;

namespace deneme2
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            dgvLogs.AutoGenerateColumns = true;
            timer.Interval = 1000; 
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        List<User> CollectAllDeviceUsers()
        {
            var xUsers = ExcelHelper.GetEmplooyes();
            List<UserData> result = new List<UserData>();
            foreach (var d in devices)
            {
                DeviceHelper h1 = new DeviceHelper(Convert.ToInt32(d.MachineNumber));
                h1.MesajVer += H_MesajVer;
                h1.Log += H_Log;
                h1.IP = d.IP;
                h1.Port = d.Port;
                h1.ComKey = d.ComKey;
                h1.ConnectTCP();
                var usr = h1.GetAllUser();
                UserKaynastir(xUsers, usr);
            }
            return xUsers;
        }

        //bool varmi(User x)
        //{
        //    return x.UserID == item.UserID;
        //}
        /// <summary>
        /// Cihazda excel de tanımlı olmayan bir kullanıcı gelirse onu da listeye eklemeyi sağlar
        /// Cihazdaki liste ile exceldeki listeyi birleştirir.
        /// </summary>
        /// <param name="kaynak"></param>
        /// <param name="ekler"></param>
        void UserKaynastir(List<User> kaynak, List<User> ekler)
        {
            if (kaynak == null || ekler == null) return;
            foreach (var item in ekler)
            {                
                if (!kaynak.Any(x => x.UserID == item.UserID))
                {
                    kaynak.Add(item);
                }
            }
        }
        List<UserData> CollectAllDevices()
        {
            var xUsers = ExcelHelper.GetEmplooyes();

            List<UserData> result = new List<UserData>();
            foreach (var d in devices)
            {
                DeviceHelper h1 = new DeviceHelper(Convert.ToInt32(d.MachineNumber));
                h1.MesajVer += H_MesajVer;
                h1.Log += H_Log;
                h1.IP = d.IP;
                h1.Port = d.Port;
                h1.ComKey = d.ComKey;

                h1.ConnectTCP();

                if (!h1.ConnectState)
                {
                    H_Log($"{d.IP} Cihazına Bağlantı", "Başarısız");
                    continue;
                }
                var users = h1.GetAllUser();
                UserKaynastir(xUsers, users);//xusers excel listesi ile cihazdan gelenleri birleştirdik.
                h1.UserList = xUsers;
                var r = h1.TumDataGetir();

                h1.DisConnect();
                lblDurum.Text = "Cihaz Bağlantısı kapatıldı.";
                if(r!=null&& r.Data!=null)                  
                result.AddRange(r.Data);  //tüm cihazlardaki dataları tutan result değişkenine bu cihazdan gelen bilgileri ekle
                lblDurum.Text = "Cihazda veri yok";
                H_Log($"{d.IP} Cihazında veri yok", "Başarısız");


            }
            //source.OrderBy(order => order.OrderDate).ThenBy(order => order.OrderId).ToList();
            return result.OrderBy(x => x.UserID).ThenBy(x => x.VerifyDate).ToList();

        }
        bool sabah = false, oglen = false, aksam = false;
        List<User> sabahMailGiden = new List<User>();
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime ti = DateTime.Now;
            lblSaat.Text = ti.ToString("HH:mm:ss");
            #region Sabah Kontrol
            if (ti.Hour == 8 && ti.Minute == 30 && !sabah)
            {
                sabah = true;
                oglen = aksam = false;
                var logs = CollectAllDevices();
                if (logs != null)
                {
                    var bugun = logs.Where(
                       x => x.VerifyDate != null &&  //verifyDate boş değilse
                       x.VerifyDate.Value.Date == ti.Date &&  //bu günün kaydı ise 
                       (
                       x.VerifyDate.Value.Hour < 8 || (x.VerifyDate.Value.Hour == 8 && x.VerifyDate.Value.Minute <= 30) //saat 8:30 dan önce ise
                       )
                       ).ToList();//verifydate null olmayan ve bu güne ait olan kayıtları filtrele
                    foreach (var item in FromExcel.Where(x => x.Izin == false))
                    {
                        var kontrol = bugun.FirstOrDefault(x => x.UserID == item.UserID);
                        if (kontrol == null)
                        {
                            sabahMailGiden.Add(item);
                            if (!string.IsNullOrEmpty(item.Mail))
                            {
                                H_Log("Mail Gönderildi.", item.Mail);
                                MailHelper.SendMail(item.Mail, "Giriş-Çıkış Kontrolü", "Sabah 8.30'a kadar parmak haraketiniz gözlemlenmedi, lütfen parmağınızı okutunuz.");

                            }

                        }
                    }
                }
                else
                {
                }
            }
            #endregion
            #region Öğlen Kontrol
            if (ti.Hour == 14 && ti.Minute == 30 && !oglen)
            {
                oglen = true;
                sabah = aksam = false;
                var logs = CollectAllDevices();
                if (logs != null)
                {
                    var sabah = logs.Where(
                         x => x.VerifyDate != null &&  //verifyDate boş değilse
                         x.VerifyDate.Value.Date == ti.Date &&  //bu günün kaydı ise 
                         (x.VerifyDate.Value.Hour < 11 || (x.VerifyDate.Value.Hour == 11 && x.VerifyDate.Value.Minute <= 55)) //saat 8:30 dan önce ise

                         ).ToList();

                    var oglen = logs.Where(
                        x => x.VerifyDate != null &&  //verifyDate boş değilse
                        x.VerifyDate.Value.Date == ti.Date &&  //bu günün kaydı ise 

                        (x.VerifyDate.Value.Hour < 14 || (x.VerifyDate.Value.Hour == 14 && x.VerifyDate.Value.Minute <= 30)) &&
                        (x.VerifyDate.Value.Hour > 11 || (x.VerifyDate.Value.Hour == 11 && x.VerifyDate.Value.Minute > 55))
                        ).ToList();//verifydate null olmayan ve bu güne ait olan kayıtları filtrele

                    foreach (var item in FromExcel.Where(x => x.Izin == false))
                    {
                        var sabahKontrol = sabah.Any(x => x.UserID == item.UserID);
                        var oglenKontrol = oglen.Where(x => x.UserID == item.UserID);
                        if ((sabahKontrol && oglenKontrol.Count() < 2)) //sabah giriş yapmışsa ve öğlen 2 giriş çıkış yapmamışsa 

                        {

                            if (!string.IsNullOrEmpty(item.Mail))
                            {
                                MailHelper.SendMail(item.Mail, "Giriş-Çıkış Kontrolü", " 11.55-14.30 saatleri arasında giriş veya çıkış haraketiniz gözlemlenmedi,lütfen parmağınızı okutunuz.");
                            }


                        }
                    }
                }
                else
                {

                }
            }
            #endregion
            #region Akşam Kontrol
            if (ti.Hour == 16 && ti.Minute == 45 && !aksam)
            {
                aksam = true;
                sabah = oglen = false;
                var logs = CollectAllDevices();
                if (logs != null)
                {

                    var sabah = logs.Where(
                                         x => x.VerifyDate != null &&  //verifyDate boş değilse
                                         x.VerifyDate.Value.Date == ti.Date &&  //bu günün kaydı ise 
                                         (x.VerifyDate.Value.Hour < 11 || (x.VerifyDate.Value.Hour == 11 && x.VerifyDate.Value.Minute <= 55)) //saat 8:30 dan önce ise

                                         ).ToList();

                    var oglen = logs.Where(
                        x => x.VerifyDate != null &&  //verifyDate boş değilse
                        x.VerifyDate.Value.Date == ti.Date &&  //bu günün kaydı ise 

                        (x.VerifyDate.Value.Hour > 11 || (x.VerifyDate.Value.Hour == 11 && x.VerifyDate.Value.Minute > 55)) &&
                        (x.VerifyDate.Value.Hour < 16 || (x.VerifyDate.Value.Hour == 16 && x.VerifyDate.Value.Minute <= 30))
                        ).ToList();//verifydate null olmayan ve bu güne ait olan kayıtları filtrele


                    foreach (var item in FromExcel.Where(x => x.Izin == false))
                    {
                        //işe gelmiş personele çıkış için hatırlatma maili at.
                        var sabahKontrol = sabah.Any(x => x.UserID == item.UserID);
                        var oglenKontrol = oglen.Where(x => x.UserID == item.UserID);
                        if ((sabahKontrol && oglenKontrol.Count() == 2) ||   //sabah gelmiş öğlen de yemeğe çıkmış yemekten dönmüş.
                            (!sabahKontrol && oglenKontrol.Count() >= 1)    //sabah gelmemiş öğlen gelmiş
                            )
                        {
                            if (!string.IsNullOrEmpty(item.Mail))
                            {
                                MailHelper.SendMail(item.Mail, "Giriş-Çıkış Kontolü ", "Mesai bitiminde çıkış haraketinizi cihaza bildirmeyi unutmayınız.");
                            }
                        }
                    }
                }
                else
                {
                }
            }

            #endregion
        }
        //8:30 dan önce basmamışlara mail attım
        //14:30 da 11:55 den önce giriş yapıp 11:55 ile 14:30 arasında parmağını okutmayanlara ve 1 kere okutanlara mail attım
        //16:45 de  11:55 de girişi olup öğlen çıkış giriş kaydı olanlara veya sadece öğlen girişi olanlara hatırlatma maili attım.


        List<User> FromExcel = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvLogs.AutoGenerateColumns = false;
            dateBaslama.Value = DateTime.Now.Date;
            dateBitis.Value = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            FromExcel = ExcelHelper.GetEmplooyes();
            dgvUsers.DataSource = FromExcel;
            dgvDevices.DataSource = devices;
        }
        DeviceHelper h;
        private void H_Log(string arg1, string arg2)
        {
            ListViewItem li = new ListViewItem();
            li.Text = arg1;
            li.SubItems.Add(arg2);
            lstLogs.Items.Add(li);
        }
        private void H_MesajVer(string obj)
        {
            lblDurum.Text = obj;
            Application.DoEvents();// arayüz yeniliyor.
        }
        private void BtnGeneralAttData_Click(object sender, EventArgs e)
        {
            var logs = CollectAllDevices();
            if (logs != null)
            {
                // dgvLogs.DataSource = DeviceHelper.ConvertUserData(r.Data).Where(x => x.VerifyDate != null && x.VerifyDate >= dateBaslama.Value && x.VerifyDate <= dateBitis.Value);
                List<User> islenmis = DeviceHelper.ConvertUser(logs, dateBaslama.Value, dateBitis.Value);
               //islenmis içinde sadece userName var ancak diğer bilgiler excel listesinde
                List<LeftView> lf = new List<LeftView>();
                foreach (var item in islenmis)
                {
                    var fe = FromExcel.FirstOrDefault(x => x.UserID == item.UserID);
                    if (fe != null)
                    {
                        fe.Logs.AddRange(item.Logs);
                    }
                    lf.AddRange(item.DeclareLeftView());
                }

                dgvLogs.DataSource = lf;

            }
            else
            {
            }
        }
        List<Device> devices = new List<Device>()
        {
           // new Device{IP="192.168.1.3",Port="4370",ComKey="",MachineNumber="1"}

        };



        private void BtnTestData_Click(object sender, EventArgs e)
        {
            List<UserData> testData = new List<UserData>();
            Random rnd = new Random();
            foreach (var item in FromExcel)
            {

                for (int i = 1; i < 7; i++)
                {
                    for (int j = 0; j < rnd.Next(1, 11); j++)
                    {
                        UserData d = new UserData();
                        d.UserID = item.UserID;
                        d.UserName = item.UserName;
                        d.VerifyDateRaw = $"2019-08-{i} {rnd.Next(1, 24)}:{rnd.Next(0, 60)}:00";
                        testData.Add(d);
                        User u = FromExcel?.FirstOrDefault(x => x.UserID == d.UserID);
                        u?.Logs?.Add(d);
                    }
                }

            }
            List<LeftView> dayCol = new List<LeftView>();
            foreach (var item in FromExcel)
            {
                dayCol.AddRange(item.DeclareLeftView());
            }
            dgvLogs.DataSource = dayCol.OrderBy(x => x.UserID).ThenBy(x => x.Date).ToList();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var l = dgvLogs.CurrentRow.DataBoundItem as LeftView;
            var t = l.Day.DayElapsed;
            //var t1 = l.Day.Sabah;
            //var t2 = l.Day.Oglen;
            //var t3 = l.Day.OgleSonra;
            //var t4 = l.Day.Aksam;

        }

        private void Chcbx_CheckStateChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvUsers.Rows.Count; i++)
            {
                dgvUsers.Rows[i].Cells[4].Value = chcbx.Checked ? true : false;
            }
        }

        private void BtnExportMultiLineExcel_Click(object sender, EventArgs e)
        {
            if (FromExcel == null)
            {
                lblDurum.Text = "Sağ liste dolu olmalıdır.";
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Dosyası|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {


                OleDbConnection con = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={sfd.FileName};Extended Properties='Excel 12.0'");
                OleDbCommand cmd = new OleDbCommand("create table sheet1(KullaniciID int , KullaniciAd char(50) , Depatman char(50) , Tarih char(50) , Saatler char(100),Giris char(100),Cikis char(100))", con);
                try
                {

                    con.Open();
                    cmd.ExecuteNonQuery();
                    foreach (var u in FromExcel)
                    {
                        foreach (var d in u.Days)
                        {
                            for (int i = 0; i+1 < d.InOutList.Count; i += 2)
                            {
                                DateTime g = d.InOutList[i];
                                DateTime c = d.InOutList[i + 1];
                                OleDbCommand iicmd = new OleDbCommand("Insert Into sheet1(KullaniciID,KullaniciAd,Depatman,Tarih,Saatler,Giris,Cikis) Values(@p1,@p2,@p3,@p4,@p5,@p6,@p7)", con);
                                iicmd.Parameters.AddWithValue("@p1", u.UserID);
                                iicmd.Parameters.AddWithValue("@p2", u.UserName);
                                iicmd.Parameters.AddWithValue("@p3", "AR-GE");
                                iicmd.Parameters.AddWithValue("@p4", d.Date.ToString("dd/MM/yyyy"));
                                iicmd.Parameters.AddWithValue("@p5", d.Times);
                                iicmd.Parameters.AddWithValue("@p6", g.ToString("HH:mm"));
                                iicmd.Parameters.AddWithValue("@p7", c.ToString("HH:mm"));
                                iicmd.ExecuteNonQuery();
                            }

                        }



                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        //private void DgvDevices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        private void BtnAddDevice_Click(object sender, EventArgs e)
        {
            Device d = new Device { IP = txtIp.Text, Port = txtPort.Text, ComKey = txtCmdKey.Text, MachineNumber = txtMac.Text };
            devices.Add(d);
            dgvDevices.DataSource = null;
            dgvDevices.DataSource = devices;
        }
        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            List<LeftView> data = (List<LeftView>)dgvLogs.DataSource;
            if (data == null)
            {
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Dosyası|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                int? gcCount = data.Max(x => x?.Day?.InOutList?.Count);
                StringBuilder csb = new StringBuilder();
                csb.Append("create table sheet1(KullaniciID int , KullaniciAd char(50) , Depatman char(50) , Tarih char(50) , Saatler char(100)");
                if (gcCount.HasValue)
                {
                    for (int i = 1; i <= gcCount.Value / 2; i++)
                    {
                        csb.Append($",Giris{i} char(50),Cikis{i} char(50)");
                    }
                }
                csb.Append(",Sure char(50),HaftalikSure char(50))");


                OleDbConnection con = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={sfd.FileName};Extended Properties='Excel 12.0'");
                OleDbCommand cmd = new OleDbCommand(csb.ToString(), con);
                try
                {
                    StringBuilder isb = new StringBuilder();
                    isb.Append("Insert Into sheet1(KullaniciID,KullaniciAd,Depatman,Tarih,Saatler,Sure,HaftalikSure");
                    if (gcCount.HasValue)
                    {
                        for (int i = 1; i <= gcCount.Value / 2; i++)
                        {
                            isb.Append($",Giris{i},Cikis{i}");
                        }
                    }
                    isb.Append(") Values(@p1,@p2,@p3,@p4,@p5,@p6,@p7");
                    if (gcCount.HasValue)
                    {
                        gcCount = gcCount.Value - gcCount.Value % 2;

                        for (int i = 1; i <= gcCount.Value; i += 2)
                        {
                            isb.Append($",@p{i + 7},@p{i + 8}");
                        }
                    }
                    isb.Append(")");


                    con.Open();
                    cmd.ExecuteNonQuery();
                    foreach (var item in data)
                    {
                        OleDbCommand iicmd = new OleDbCommand(isb.ToString(), con);
                        iicmd.Parameters.AddWithValue("@p1", item.UserID);
                        iicmd.Parameters.AddWithValue("@p2", item.UserName);
                        iicmd.Parameters.AddWithValue("@p3", "AR-GE");
                        iicmd.Parameters.AddWithValue("@p4", item.Date);
                        iicmd.Parameters.AddWithValue("@p5", item.Times);
                        iicmd.Parameters.AddWithValue("@p6", item.Elapsed);
                        iicmd.Parameters.AddWithValue("@p7", item.WeekElapsed);

                        if (gcCount.HasValue)
                        {
                            gcCount = gcCount.Value - gcCount.Value % 2;

                            for (int i = 0; i < gcCount.Value; i += 1)
                            {
                                if (i < item.Day.InOutList.Count)
                                {

                                    var d = item?.Day?.InOutList[i];
                                    if (d != null)
                                    {
                                        var s = $"{d.Value.Hour}:{d.Value.Minute}";
                                        iicmd.Parameters.AddWithValue($"@p{i + 8}", s);
                                    }
                                    else
                                    {
                                        iicmd.Parameters.AddWithValue($"@p{i + 8}", "");
                                    }
                                }
                                else
                                {
                                    iicmd.Parameters.AddWithValue($"@p{i + 8}", "");

                                }
                            }
                        }

                        iicmd.ExecuteNonQuery();


                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}



