using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.WorkWithFiles.Load;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.InitialyzerComponent
{
    class ConfigReader
    {
        private static ConfigReader reader;
        private string connectionString;
        
        private ConfigReader()
        {

        }

        public static ConfigReader getInstance()
        {
            if (reader == null)
            {
                reader = new ConfigReader();
            }

            return reader;
        }

        public void read()
        {
            TextFilesConfigFieldsOnLoad proxyConfig =
                    new TextFilesConfigFieldsOnLoad(Directory.GetCurrentDirectory() +
                    "\\Config.txt");

            DataWorker<TextFilesConfigFieldsOnLoad, List<string>> saver = 
                new TextFilesDataLoader();
            saver.setConfig(proxyConfig);
            saver.connect();
            saver.execute();
            List<string> configList = saver.getResult();
            reader.connectionString = configList.ElementAt(0);
        }

        public string getConnectionString()
        {
            return reader.connectionString;
        }
    }
}
