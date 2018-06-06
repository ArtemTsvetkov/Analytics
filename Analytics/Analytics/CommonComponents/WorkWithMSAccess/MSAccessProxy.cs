using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Analytics.CommonComponents.Interfaces.Data;

namespace Analytics
{
    class MSAccessProxy : DataWorker<List<string>, DataSet>
    {
        private DataWorker<List<string>, DataSet> saver = new MSAccessDataSaver();
        private string host;//пример хоста:C:\\Users\\Artem\\Documents\\Database3.accdb
        private List<string> query;

        public void setConfig(string host, List<string> querys, StorageForData<DataSet> resultStorage)
        {
            this.host = host;
            query = querys;
            saver.setConfig(host, query, resultStorage);
        }

        public void execute()
        {
            if (connect())
            {
                saver.execute();
            }
            else
            {
                //ДОБАВИТЬ СЮДА ВЫЗОВ ИСКЛЮЧЕНИЯ
            }
        }

        //Пока для простоты, будем считать, что если запрос выполнился, значит подключение есть
        public bool connect()
        {
            List<string> currentQuerys = new List<string>();
            currentQuerys.Add("SELECT count(*) FROM Information");
            string connStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + host + 
                ";Persist Security Info=True;");
            DataSet dataSet = new DataSet();
            OleDbConnection conn;
            conn = null;


            try
            {
                conn = new OleDbConnection(connStr);
                conn.Open();
                for (int i = 0; i < currentQuerys.Count; i++)
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter(currentQuerys.ElementAt(i), conn);
                    adapter.Fill(dataSet, selectTableNameFromQuery(currentQuerys.ElementAt(i)));
                }
                return true;
            }
            catch (Exception ex)
            {
                ReadWriteTextFile rwtf = new ReadWriteTextFile();
                List<string> buf = new List<string>();
                buf.Add("-----------------------------------------------");
                buf.Add("Module: Form1");
                DateTime thisDay = DateTime.Now;
                buf.Add("Time: " + thisDay.ToString());
                buf.Add("Exception: " + ex.Message);
                ReadWriteTextFile.Write_to_file(buf, Directory.GetCurrentDirectory() + "\\Errors.txt",
                    0);
                return false;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        //функция поиска названия таблицы базы данных
        private string selectTableNameFromQuery(string query)
        {
            String[] buf_of_substrings = query.Split(new char[] { ' ' }, StringSplitOptions.
                RemoveEmptyEntries);
            if (buf_of_substrings[0].Equals("SELECT", StringComparison.CurrentCultureIgnoreCase) == 
                true)
            {
                return buf_of_substrings[3];
            }
            if (buf_of_substrings[0].Equals("INSERT", StringComparison.CurrentCultureIgnoreCase) == 
                true)
            {
                return buf_of_substrings[2];
            }
            if (buf_of_substrings[0].Equals("UPDATE", StringComparison.CurrentCultureIgnoreCase) == 
                true)
            {
                return buf_of_substrings[1];
            }
            if (buf_of_substrings[0].Equals("DELETE", StringComparison.CurrentCultureIgnoreCase) ==
                true)
            {
                return buf_of_substrings[2];
            }
            return null;
        }
    }
}
/*
 * Контролирует возможность доступа к БД. Реализация паттерна "Посредник"
 */

