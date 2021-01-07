using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Threading;

namespace deneme2
{
    public class ExcelHelper
    {
        //(Assembly.GetExecutingAssembly().Location) çalışan exenin konumunu getirir.
        //Path.GetDirectoryName verdiğin yoldan klasör adına kadar olan yolu getirir.
        static string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Persist Security Info = False;Extended Properties=\"Excel 8.0\"";
        static string Directory { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }
        public static List<User> GetEmplooyes()
        {
            OleDbConnection con = new OleDbConnection(string.Format(connection, Directory + "\\Employees.xlsx"));
            OleDbDataAdapter adp = new OleDbDataAdapter("SELECT * FROM [Sayfa1$]", con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            List<User> user = new List<User>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {                   
                    string ui = null, un = null, m = null, izin = null;
                    if (!(row["UserID"] is DBNull))//is kullanmı object olan bir değişkene "şu classtan mısın ?" diye sormak için kullanılır. userıd kolununun içindeki değer dbnulltitpinde mi?
                        ui = (string)row["UserID"];
                    if (!(row["UserName"] is DBNull))
                        un = (string)row["UserName"];
                    if (!(row["Mail"] is DBNull))
                        m = (string)row["Mail"];
                    if (!(row["Izin"] is DBNull))
                        izin = (string)row["Izin"];
                    User user1 = new User();
                    user1.UserID = Convert.ToInt32(ui);
                    user1.UserName = un;
                    user1.Mail = m;
                    user1.Izin = !string.IsNullOrEmpty(izin?.Trim());
                    user.Add(user1);
                }
            }
            return user;

        }
    }
}
