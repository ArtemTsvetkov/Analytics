using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class AllDataTableConverter : DataConverter
    {
        public object convert(object data)
        {
            if (data.GetType() == typeof(DataSet))
            {
                DataSet ds = (DataSet)data;
                List<AllDataTable> newData = new List<AllDataTable>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AllDataTable resultTableRow = new AllDataTable(new DateTime(), new DateTime(), ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), "", ds.Tables[0].Rows[i][2].ToString(), "");
                    newData.Add(resultTableRow);
                }
                return newData;
            }
            else
            {
                return null;
            }
        }
    }
}
