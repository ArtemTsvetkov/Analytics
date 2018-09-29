using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.MarcovitsComponent.Config
{
    class MarcovitsConfig
    {
        private string pathOfDataBase;
        //Рассматриваемый промежуток времени
        private GropByType interval;
        private string[] unicSoftwareNames;
        private double[] numberOfPurcharedLicenses;
        private double[] percents;
        private bool notifyObservers = true;

        public MarcovitsConfig(string pathOfDataBase, GropByType interval)
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

        public GropByType getInterval()
        {
            return interval;
        }

        public void setInterval(GropByType interval)
        {
            this.interval = interval;
        }

        public string getPathOfDataBase()
        {
            return pathOfDataBase;
        }

        public void setPathOfDataBase(string pathOfDataBase)
        {
            this.pathOfDataBase = pathOfDataBase;
        }

        public MarcovitsConfig copy()
        {
            MarcovitsConfig copy = new MarcovitsConfig(pathOfDataBase, interval);
            copy.NotifyObservers = NotifyObservers;
            copy.NumberOfPurcharedLicenses = (double[])NumberOfPurcharedLicenses.Clone();
            copy.Percents = (double[])Percents.Clone();
            copy.UnicSoftwareNames = (string[])UnicSoftwareNames.Clone();
            return copy;
        }
    }
}
