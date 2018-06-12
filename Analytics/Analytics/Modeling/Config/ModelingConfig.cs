using Analytics.Modeling.GroupByTypes;
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
        private bool resetAllState = false;
        //Путь до БД
        private string pathOfDataBase;
        //Название теблицы в БД
        private string tableOfDataBase;
        //Флаг использования корелляции между запроса на лицензии
        private bool withKovar = false;
        //Модификатор группировки для нализа(по дням/минутам и тд)
        private GropByType groupType = new HourType();

        public ModelingConfig(string configData, string pathOfDataBase, string tableOfDataBase)
        {
            this.configData = configData;
            this.pathOfDataBase = pathOfDataBase;
            this.tableOfDataBase = tableOfDataBase;
        }

        public void setGroupType(GropByType groupType)
        {
            this.groupType = groupType;
        }

        public GropByType getGroupType()
        {
            return groupType;
        }

        public bool getWithKovar()
        {
            return withKovar;
        }

        public void setWithKovar(bool withKovar)
        {
            this.withKovar = withKovar;
        }

        public string getPathOfDataBase()
        {
            return pathOfDataBase;
        }

        public string getTableOfDataBase()
        {
            return tableOfDataBase;
        }

        public void setPathOfDataBase(string pathOfDataBase)
        {
            this.pathOfDataBase = pathOfDataBase;
        }

        public void setTableOfDataBase(string tableOfDataBase)
        {
            this.tableOfDataBase = tableOfDataBase;
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
//Cледуя флагу resetAllState, либо полностью
//откатит все изменения стейта, либо только те объекты, которые участвуют
//непосредственно в моделировании(очереди, устройства и тд), но оставит
//количество запусков моделирования и отчет