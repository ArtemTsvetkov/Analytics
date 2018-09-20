using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.DataConverters
{
    class FromDataSetToString : DataConverter<DataSet, string>
    {
        public string convert(DataSet data)
        {
            try
            {
                DataSet ds = data;
                DataTable table = ds.Tables[0];
                return table.Rows[0][0].ToString();
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
