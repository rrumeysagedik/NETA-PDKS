using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace deneme2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MailHelper.SendMail("Deneme1783@gmail.com","Giriş-Çıkış Kontrol","Test Başarı ile sonuçlandı.");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
