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
        void recoverySelf(ModelsState state);
        void loadStore();//загрузка данных из базы данных
        void subscribe(Observer newObserver);
        void notifyObserver(object data);
    }
}