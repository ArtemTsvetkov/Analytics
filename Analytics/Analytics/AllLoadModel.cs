using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace Analytics
{
    class AllLoadModel : Model
    {
        AllLoadModelState state = new AllLoadModelState();
        Observer observer;
        AllDataTableConverter converter = new AllDataTableConverter();

        public AllLoadModel(string pathOfDataBase, string tableOfDataBase)
        {
            state.pathOfDataBase = pathOfDataBase;
            state.tableOfDataBase = tableOfDataBase;
        }

        public void calculationStatistics()
        {
            throw new Exception();//Эта модель просто грузит все данные и все, она не считает 
                                  //статистику, ее сделал просто для примера
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
            this.state = (AllLoadModelState)state;
        }

        public void loadStore()//загрузка данных из базы данных
        {
            MSAccessProxy accessProxy = new MSAccessProxy();
            //получение значения id
            accessProxy.setConfig(state.pathOfDataBase, "SELECT user_name, user_host, software FROM " + state.tableOfDataBase);
            DataSet ds = accessProxy.execute();
            state.data = (List<AllDataTable>)converter.convert(ds);
            notifyObserver();
        }

        public void notifyObserver()
        {
            observer.notify(state.data);
        }
    }
}
