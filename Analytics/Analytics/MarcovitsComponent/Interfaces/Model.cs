using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Analytics
{
    interface Model
    {
        ModelsState copySelf();
        void recoverySelf(ModelsState state);//восстановление модели
        void loadStore();//загрузка данных из базы данных
        void subscribe(Observer newObserver);//подписка на модель
        void notifyObserver();//Уведомление подписчиков о изменении стэйта
        void calculationStatistics();//рассчет статистических параметров
    }
}