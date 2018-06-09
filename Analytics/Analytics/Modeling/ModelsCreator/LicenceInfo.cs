using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelsCreator
{
    class LicenceInfo
    {
        //Название
        private string name;
        //Производитель
        private string vendor;
        //Количество
        private int count;
        //Среднее время обработки
        private int avgDelayTimeInTheProcessing;
        //Среднее квадратическое отклонение от времени обработки
        private int avgSquereDelayTimeInTheProcessing;
        //Количество транзактов у генераторов транзактов
        private int numberOfTranzactsOnStart;
        //Среднее время генератора транзактов
        private int avgRequestedTime;
        //Среднее квадратичное отклонение генаратора транзактов
        private int avgSquereRequestedTime;

        public LicenceInfo(string name, string vendor, int count, 
            int avgDelayTimeInTheProcessing, int avgSquereDelayTimeInTheProcessing,
            int numberOfTranzactsOnStart, int avgRequestedTime, int avgSquereRequestedTime)
        {
            this.name = name;
            this.vendor = vendor;
            this.count = count;
            this.avgDelayTimeInTheProcessing = avgDelayTimeInTheProcessing;
            this.avgSquereDelayTimeInTheProcessing = avgSquereDelayTimeInTheProcessing;
            this.numberOfTranzactsOnStart = numberOfTranzactsOnStart;
            this.avgRequestedTime = avgRequestedTime;
            this.avgSquereRequestedTime = avgSquereRequestedTime;
        }

        public string getName()
        {
            return name;
        }

        public string getVendor()
        {
            return vendor;
        }

        public int getCount()
        {
            return count;
        }

        public int getAvgDelayTimeInTheProcessing()
        {
            return avgDelayTimeInTheProcessing;
        }

        public int getAvgSquereDelayTimeInTheProcessing()
        {
            return avgSquereDelayTimeInTheProcessing;
        }

        public int getNumberOfTranzactsOnStart()
        {
            return numberOfTranzactsOnStart;
        }

        public int getAvgRequestedTime()
        {
            return avgRequestedTime;
        }

        public int getAvgSquereRequestedTime()
        {
            return avgSquereRequestedTime;
        }
    }
}
