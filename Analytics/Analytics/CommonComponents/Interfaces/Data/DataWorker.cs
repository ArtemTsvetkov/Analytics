using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.Interfaces.Data
{
    interface DataWorker<TDataType, TStorage>
    {
        void setConfig(string host, TDataType data, StorageForData<TStorage> resultStorage);
        void execute();
        bool connect();
    }
}
/*
 * Позволяет выгрузить данные, например, в БД.
 * Два типа - TDataType1, TDataType2 понадобится, в случае, если, например, при взаимодействии
 * с БД можно слать либо один запрос, либос разу несколько
 */
