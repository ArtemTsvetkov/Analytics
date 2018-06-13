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
        //Флаг, указывающий сброс всего стейта
        private bool resetAllState = false;
        //Путь до БД
        private string pathOfDataBase;
        //Флаг использования корелляции между запроса на лицензии
        private bool withKovar = false;
        //Модификатор группировки для нализа(по дням/минутам и тд)
        //рассматриваемый промежуток времени
        private GropByType interval = new HourType();

        public ModelingConfig(string pathOfDataBase, GropByType interval)
        {
            this.pathOfDataBase = pathOfDataBase;
            this.interval = interval;
        }

        public GropByType getInterval()
        {
            return interval;
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

        public void setPathOfDataBase(string pathOfDataBase)
        {
            this.pathOfDataBase = pathOfDataBase;
        }

        public bool getResetAllState()
        {
            return resetAllState;
        }

        public void setResetAllState(bool resetAllState)
        {
            this.resetAllState = resetAllState;
        }
    }
}
//Cледуя флагу resetAllState, либо полностью
//откатит все изменения стейта, либо только те объекты, которые участвуют
//непосредственно в моделировании(очереди, устройства и тд), но оставит
//количество запусков моделирования и отчет