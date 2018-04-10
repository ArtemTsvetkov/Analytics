using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Analytics
{
    class AllLoadModelState : ModelsState
    {
        public string pathOfDataBase = "";
        public string tableOfDataBase = "";
        public List<AllDataTable> data = new List<AllDataTable>();
    }
}
