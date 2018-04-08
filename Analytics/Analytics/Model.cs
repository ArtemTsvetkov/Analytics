using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Analytics
{
    interface Model
    {
        ModelsState copySelf();
        void recoverySelf(ModelsState state);
        DataSet loadStore();//загрузка данных из базы данных
    }
}
