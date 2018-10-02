using Analytics.CommonComponents.InitialyzerComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.WorkWithDataBase.MsSqlServer
{
    class MsSQLServerStateFields
    {
        private string connectionString;
        private List<string> query;

        public MsSQLServerStateFields(List<string> query)
        {
            connectionString = ConfigReader.getInstance().getConnectionString();
            this.query = query;
        }

        public string getConnectionString()
        {
            return connectionString;
        }

        public List<string> getQuery()
        {
            return query;
        }
    }
}
