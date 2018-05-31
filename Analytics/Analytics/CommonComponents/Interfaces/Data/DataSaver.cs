using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Analytics.CommonComponents.Interfaces.Data;

namespace Analytics
{
    interface DataSaver<TDataType1, TDataType2, TStorage>
    {
        void setConfig(string host, TDataType1 data, StorageForData<TStorage> resultStorage);
        void setConfig(string host, TDataType2 data, StorageForData<TStorage> resultStorage);
        void execute();
        bool connect();
    }
}
/*
 * Позволяет выгрузить данные, например, в БД.
 * Два типа - TDataType1, TDataType2 понадобится, в случае, если, например, при взаимодействии
 * с БД можно слать либо один запрос, либос разу несколько
 */
