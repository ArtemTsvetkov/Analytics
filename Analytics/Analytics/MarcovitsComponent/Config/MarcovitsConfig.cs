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

        public MarcovitsConfig(string pathOfDataBase, GropByType interval)
        {
            this.pathOfDataBase = pathOfDataBase;
            this.interval = interval;
        }
        
        public GropByType getInterval()
        {
            return interval;
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
            return new MarcovitsConfig(pathOfDataBase, interval);
        }
    }
}
