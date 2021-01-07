using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace deneme2
{
   public class TextHelper
    {
        public static void DataToText(DataTable data, string FilePath)
        {
            if (data == null) return;
            using (FileStream fs= new FileStream(FilePath, FileMode.OpenOrCreate))
            {
                StreamWriter sw = new StreamWriter(fs);
                foreach(DataRow item in data?.Rows)
                {
                    string satir= $"{item["UserID"]} \t{item["VerifyDate"]} \t{item["VerifyType"]} \t{ item["VerifyState"]} \t{item["WorkCode"] }";
                    sw.WriteLine(satir);
                }
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }
    }
}
 