using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Analytics.CommonComponents.Interfaces.Data;

namespace Analytics
{
    interface DataSaver<T>
    {
        void setConfig(string host, string query, StorageForData<T> resultStorage);
        void setConfig(string host, List<string> querys, StorageForData<T> resultStorage);
        void execute();
        bool connect();
    }
}
/*
 * Позволяет выгрузить данные, например, в БД.
 */
