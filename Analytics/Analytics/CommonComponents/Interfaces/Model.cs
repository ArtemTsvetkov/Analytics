using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Analytics.CommonComponents.Interfaces.Data;

namespace Analytics
{
    interface Model<TTypeOfResult, TTypeState, TConfigType> where TTypeState : ModelsState
    {
        TTypeState copySelf();//Создание копии модели для возможного ее восстановления
        void setConfig(TConfigType configData);
        void recoverySelf(TTypeState state);//восстановление модели
        void loadStore();//загрузка данных откуда-то
        void subscribe(Observer newObserver);//подписка на модель
        void calculationStatistics();//рассчет статистических параметров
        //Конвертер для получения результата
        void setResultConverter(DataConverter<TTypeState, TTypeOfResult> dataConverter);
        TTypeOfResult getResult();
        //void setDataLoader(DataWorker dataWorker);
    }
}