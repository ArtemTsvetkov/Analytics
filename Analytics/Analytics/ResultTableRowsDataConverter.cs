using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Analytics
{
    class ResultTableRowsDataConverter : DataConverter
    {
        public object convert(object data)
        {
            if (data.GetType() == typeof(DataSet))
            {
                DataSet ds = (DataSet)data;
                List<ResultTableRows> newData = new List<ResultTableRows>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ResultTableRows resultTableRow = new ResultTableRows(new DateTime(), new DateTime(), ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), "", ds.Tables[0].Rows[i][2].ToString(), "");
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

