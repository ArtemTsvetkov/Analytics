using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.ExceptionHandler.TextJornalist;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.WorkWithFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler
{
    class TextFilesJornalist : ExceptionsJornalistInterface<TextJornalistConfig>
    {
        private TextJornalistConfig config;

        public void setConfig(TextJornalistConfig config)
        {
            this.config = config;
        }

        public void write()
        {
            if(config == null)
            {
                //ДОБАВИТЬ ВЫЗОВ ИСКЛЮЧЕНИЯ-КОНФИГ НЕ ЗАДАН
            }
            else
            {
                List<string> buf = new List<string>();
                buf.Add("-----------------------------------------------");
                buf.Add("Module: "+ config.getExeptionsSourse());
                buf.Add("Trace: " + config.getExceptionTrace());
                DateTime thisDay = DateTime.Now;
                buf.Add("Time: " + thisDay.ToString());
                buf.Add("Exception: " + config.getExceptionMessage());

                TextFilesConfigFieldsOnSave proxyConfig = 
                    new TextFilesConfigFieldsOnSave(buf, Directory.GetCurrentDirectory() + 
                    "\\Errors.txt", 0);


                DataWorker<TextFilesConfigFieldsOnSave, bool> saver = new TextFilesDataSaver();
                saver.setConfig(proxyConfig);
                saver.connect();
                saver.execute();
            }
        }
    }
}
