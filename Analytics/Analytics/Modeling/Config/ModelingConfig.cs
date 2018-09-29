using Analytics.CommonComponents.Exceptions;
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
        //Путь до БД
        private string pathOfDataBase;
        //Флаг использования корелляции между запроса на лицензии
        private bool withKovar = false;
        //Модификатор группировки для нализа(по дням/минутам и тд)
        //рассматриваемый промежуток времени
        private GropByType interval = new HourType();
        //Необходимое количество запусков моделирования, если оно больше 1, то в итоговый
        //отчет попадут усредненные значения
        private int numberOfStartsModeling = 1;
        //Восстанавливать report (для поддержки просмотра предыдущих рассчетов и вместе с тем
        //многократного запуска модели)
        private bool rollbackReport = true;
        private string[] unicSoftwareNames;
        private double[] numberOfPurcharedLicenses;
        private double[] percents;
        private bool notifyObservers = true;

        public ModelingConfig(string pathOfDataBase, GropByType interval)
        {
            this.pathOfDataBase = pathOfDataBase;
            this.interval = interval;
            unicSoftwareNames = new string[0];
            numberOfPurcharedLicenses = new double[0];
            percents = new double[0];
        }

        public string[] UnicSoftwareNames
        {
            get
            {
                return unicSoftwareNames;
            }

            set
            {
                unicSoftwareNames = value;
            }
        }

        public double[] NumberOfPurcharedLicenses
        {
            get
            {
                return numberOfPurcharedLicenses;
            }

            set
            {
                numberOfPurcharedLicenses = value;
            }
        }

        public double[] Percents
        {
            get
            {
                return percents;
            }

            set
            {
                percents = value;
            }
        }

        public bool NotifyObservers
        {
            get
            {
                return notifyObservers;
            }

            set
            {
                notifyObservers = value;
            }
        }

        public ModelingConfig copy()
        {
            ModelingConfig copy = new ModelingConfig(pathOfDataBase, interval);
            copy.setWithKovar(withKovar);
            copy.setNumberOfStartsModeling(numberOfStartsModeling);
            copy.setRollbackReport(rollbackReport);
            copy.NotifyObservers = NotifyObservers;
            copy.NumberOfPurcharedLicenses = (double[])NumberOfPurcharedLicenses.Clone();
            copy.Percents = (double[])Percents.Clone();
            copy.UnicSoftwareNames = (string[])UnicSoftwareNames.Clone();

            return copy;
        }

        public int getNumberOfStartsModeling()
        {
            return numberOfStartsModeling;
        }

        public void setNumberOfStartsModeling(int numberOfStartsModeling)
        {
            if (numberOfStartsModeling < 1)
            {
                throw new IncorrectValue("Enter incorrect value: for number of starts modeling");
            }
            else
            {
                this.numberOfStartsModeling = numberOfStartsModeling;
            }
        }

        public GropByType getInterval()
        {
            return interval;
        }

        public void setInterval(GropByType interval)
        {
            this.interval = interval;
        }

        public bool getWithKovar()
        {
            return withKovar;
        }

        public void setWithKovar(bool withKovar)
        {
            this.withKovar = withKovar;
        }

        public bool isRollbackReport()
        {
            return rollbackReport;
        }

        public void setRollbackReport(bool rollbackReport)
        {
            this.rollbackReport = rollbackReport;
        }

        public string getPathOfDataBase()
        {
            return pathOfDataBase;
        }

        public void setPathOfDataBase(string pathOfDataBase)
        {
            this.pathOfDataBase = pathOfDataBase;
        }
    }
}
//Cледуя флагу resetAllState, либо полностью
//откатит все изменения стейта, либо только те объекты, которые участвуют
//непосредственно в моделировании(очереди, устройства и тд), но оставит
//количество запусков моделирования и отчет