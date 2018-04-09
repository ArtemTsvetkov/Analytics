using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace Analytics
{
    class ConcreteModel : Model
    {
        ConcreteModelsState state = new ConcreteModelsState();
        Observer observer;
        ResultTableRowsDataConverter converter = new ResultTableRowsDataConverter();

        public ConcreteModel(string pathOfDataBase, string tableOfDataBase)
        {
            state.pathOfDataBase = pathOfDataBase;
            state.tableOfDataBase = tableOfDataBase;
        }

        public void subscribe(Observer newObserver)
        {
            observer = newObserver;
        }

        public ModelsState copySelf()
        {
            return state;
        }

        public void recoverySelf(ModelsState state)
        {
            this.state = (ConcreteModelsState)state;
        }

        public void loadStore()//загрузка данных из базы данных
        {
            MSAccessProxy accessProxy = new MSAccessProxy();
            //получение значения id
            accessProxy.setConfig(state.pathOfDataBase, "SELECT user_name, user_host, software FROM " + state.tableOfDataBase);
            DataSet ds = accessProxy.execute();
            notifyObserver(ds);
        }

        public void notifyObserver(object data)
        {
            observer.notify(converter.convert(data));
        }
    }
}
