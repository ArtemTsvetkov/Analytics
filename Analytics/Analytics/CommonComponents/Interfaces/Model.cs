using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Analytics.CommonComponents.Interfaces.Data;

namespace Analytics
{
    interface Model<TTypeOfResult, TConfigType>
    {
        void setConfig(TConfigType configData);
        void loadStore();//загрузка исходных данных откуда-то
        void subscribe(Observer newObserver);//подписка на модель
        TTypeOfResult getResult();
    }
}