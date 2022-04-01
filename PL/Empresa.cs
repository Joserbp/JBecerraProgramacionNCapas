using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace PL
{
    public class Empresa
    {
        //public static ML.Result ReadExcel()
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0 XML;Data Source=C:\Users\ALIEN11\Documents\Jose Ramon Becerra Perez\JBecerraProgramacionNCapas\PL_MVC\Files\Example.xlsx";
        //        using (OleDbConnection oledbConn = new OleDbConnection(connectionString))
        //        {
        //            DataTable dt = new DataTable();


        //            oledbConn.Open();
        //            using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
        //            {
        //                OleDbDataAdapter oleda = new OleDbDataAdapter();
        //                oleda.SelectCommand = cmd;
        //                DataSet ds = new DataSet();
        //                oleda.Fill(ds);

        //                dt = ds.Tables[0];
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}


        public static void ReadFile()
        {
            string file = @"C:\Users\ALIEN11\Documents\Jose Ramon Becerra Perez\JBecerraProgramacionNCapas\PL_MVC\Files\Example.txt";
            if (File.Exists(file))
            {
                StreamReader Textfile = new StreamReader(file);
                string line;
                while ((line = Textfile.ReadLine()) != null)
                {
                    String[] lines = line.Split('|');
                    ML.Empresa empresa = new ML.Empresa();
                    empresa.Nombre = lines[0];
                    empresa.Telefono = lines[1];
                    empresa.Email = lines[2];
                    empresa.DireccionWeb = lines[3];

                    ML.Result result = BL.Empresa.Add(empresa);
                    if (result.Correct)
                    {
                        Console.WriteLine("Correcto");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
