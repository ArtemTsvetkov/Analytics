using Analytics.CommonComponents.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.WorkWithMSAccess
{
    class MSAccessStorageForData : StorageForData<DataSet>
    {
        private DataSet data;

        public DataSet getData()
        {
            return data;
        }

        public void setData(DataSet newData)
        {
            data = newData;
        }
    }
}
