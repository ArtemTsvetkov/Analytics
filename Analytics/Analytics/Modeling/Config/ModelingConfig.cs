using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.Config
{
    class ModelingConfig
    {
        //Путь до файла с моделью
        private string configData;
        //Флаг, указывающий сброс всего стейта
        private bool resetAllState;

        public ModelingConfig(string configData, bool resetAllState)
        {
            this.configData = configData;
            this.resetAllState = resetAllState;
        }

        public bool getResetAllState()
        {
            return resetAllState;
        }

        public string getConfigData()
        {
            return configData;
        }

        public void setResetAllState(bool resetAllState)
        {
            this.resetAllState = resetAllState;
        }

        public void setConfigData(string configData)
        {
            this.configData = configData;
        }
    }
}
