using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.BasicObjects
{
    abstract class BasicModel<TTypeOfResult, TTypeState, TConfigType> : 
        Model<TTypeOfResult, TTypeState, TConfigType> 
        where TTypeState : ModelsState
    {
        protected TTypeState state;
        private DataConverter<TTypeState, TTypeOfResult> dataConverter;
        private List<Observer> observers = new List<Observer>();
        //Для создания конфигурации, может быть как String, так и 
        //классом с габором полей, например
        protected TConfigType config;

        abstract public void calculationStatistics();
        abstract public void loadStore();
        abstract public TTypeState copySelf();
        abstract public void recoverySelf(TTypeState state);
        abstract public void setConfig(TConfigType configData);

        public TTypeOfResult getResult()
        {
            return dataConverter.convert(state);
        }

        //Уведомление подписчиков о изменении стэйта
        protected void notifyObservers()
        {
            for(int i=0; i < observers.Count; i++)
            {
                observers.ElementAt(i).notify();
            }
        }

        public void setResultConverter(DataConverter<TTypeState, TTypeOfResult> dataConverter)
        {
            this.dataConverter = dataConverter;
        }

        public void subscribe(Observer newObserver)
        {
            observers.Add(newObserver);
        }
    }
}
