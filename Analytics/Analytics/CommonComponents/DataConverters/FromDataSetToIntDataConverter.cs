using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.DataConverters
{
    class FromDataSetToIntDataConverter : DataConverter<DataSet, int>
    {
        public int convert(DataSet data)
        {
            DataSet ds = data;
            DataTable table = ds.Tables[0];
            return convertFromItemToInt(table.Rows[0][0]);
        }

        private int convertFromItemToInt(object item)
        {
            return int.Parse(item.ToString());
        }
    }
}
