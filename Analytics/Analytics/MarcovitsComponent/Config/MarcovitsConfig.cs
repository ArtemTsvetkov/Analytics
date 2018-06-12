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
        private string tableOfDataBase;

        public MarcovitsConfig(string pathOfDataBase, string tableOfDataBase)
        {
            this.pathOfDataBase = pathOfDataBase;
            this.tableOfDataBase = tableOfDataBase;
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
    }
}
