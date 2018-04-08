using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analytics
{
    class ConcreteModelsState : ModelsState
    {
        public string pathOfDataBase = "";
        public string tableOfDataBase = "";      
        public List<ResultTableRows> data = new List<ResultTableRows>();
    }
}
