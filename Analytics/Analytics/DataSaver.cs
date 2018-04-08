using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Analytics
{
    interface DataSaver
    {
        void setConfig(string host, string query);
        void setConfig(string host, List<string> querys);
        DataSet execute();
        bool connect();
    }
}
/*
 * Позволяет выгрузить данные, например, в БД.
 */
