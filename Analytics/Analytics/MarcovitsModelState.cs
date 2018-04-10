using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsModelState : ModelsState
    {
        public string pathOfDataBase = "";
        public string tableOfDataBase = "";
        public List<MarcovitsDataTable> data = new List<MarcovitsDataTable>();
    }
}