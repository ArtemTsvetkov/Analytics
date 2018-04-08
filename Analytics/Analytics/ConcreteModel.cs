using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Analytics
{
    class ConcreteModel : Model
    {
        ConcreteModelsState state = new ConcreteModelsState();

        public ModelsState copySelf()
        {
            return state;
        }

        public void recoverySelf(ModelsState state)
        {
            this.state = (ConcreteModelsState)state;
        }

        public DataSet loadStore()//загрузка данных из базы данных
        {
            DataSet d = new DataSet();
            return d;
        }
    }
}
