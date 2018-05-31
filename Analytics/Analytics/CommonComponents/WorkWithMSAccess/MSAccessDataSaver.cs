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
    class MSAccessDataSaver : DataSaver<List<string>,string, DataSet>
    {
        private string host;//пример хоста:C:\\Users\\Artem\\Documents\\Database3.accdb
        private string query;//Для выполненения 1 запроса
        private List<string> querys;//Для выполнения сразу нескольких запросов
        private StorageForData<DataSet> resultStorage;

        public void execute()
        {
            List<string> currentQuerys = new List<string>();
            if (query == null)
            {
                currentQuerys = querys;
            }
            else
            {
                currentQuerys.Add(query);
            }


            string connStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + host + 
                ";Persist Security Info=True;");
            resultStorage.setData(runQuery(connStr, currentQuerys));
            //return runQuery(connStr, currentQuerys);
        }

        //Тестирует возможность подключения к БД, в этом классе(он не прокси) всегда есть
        //подключение
        public bool connect()
        {
            return true;
        }

        public void setConfig(string host, string query, StorageForData<DataSet> resultStorage)
        {
            this.host = host;
            this.query = query;
            this.querys = null;
            this.resultStorage = resultStorage;
        }

        public void setConfig(string host, List<string> querys, StorageForData<DataSet> resultStorage)
        {
            this.host = host;
            this.querys = querys;
            this.query = null;
            this.resultStorage = resultStorage;
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
            return "null";
        }

        private DataSet runQuery(string connection_string, List<string> query)
        {
            DataSet dataSet = new DataSet();
            OleDbConnection conn;
            conn = null;

            try
            {
                conn = new OleDbConnection(connection_string);
                conn.Open();
                for (int i = 0; i < query.Count; i++)
                {
                    try
                    {
                        OleDbDataAdapter adapter = new OleDbDataAdapter(query.ElementAt(i), conn);
                        adapter.Fill(dataSet, selectTableNameFromQuery(query.ElementAt(i)));
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
                        buf.Add("Query:" + query);
                        ReadWriteTextFile.Write_to_file(buf, Directory.GetCurrentDirectory() + 
                            "\\Errors.txt", 0);
                    }
                }
                return dataSet;
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
                return null;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
    }
}
