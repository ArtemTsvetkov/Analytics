using Analytics.CommonComponents.Interfaces.AdwancedModelsInterfaces;
using Analytics.CommonComponents.Interfaces.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.BasicObjects
{
    abstract class BasicModel<TTypeOfResult, TConfigType> : 
        Model<TTypeOfResult, TConfigType>
    {
        private List<Observer> observers = new List<Observer>();
        //Для создания конфигурации, может быть как String, так и 
        //классом с габором полей, например
        protected TConfigType config;

        abstract public void loadStore();
        abstract public void setConfig(TConfigType configData);
        abstract public TTypeOfResult getResult();

        //Уведомление подписчиков о изменении стэйта
        protected void notifyObservers()
        {
            for(int i=0; i < observers.Count; i++)
            {
                observers.ElementAt(i).notify();
            }
        }

        public void subscribe(Observer newObserver)
        {
            observers.Add(newObserver);
        }
    }
}
