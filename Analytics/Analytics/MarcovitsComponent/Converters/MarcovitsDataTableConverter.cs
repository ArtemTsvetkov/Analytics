using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsDataTableConverter : DataConverter<DataSet, List<MarcovitsDataTable>>
    {
        public List<MarcovitsDataTable> convert(DataSet data)
        {
            DataSet ds = data;
            List<MarcovitsDataTable> newData = new List<MarcovitsDataTable>();
            DataTable table = ds.Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                double[] licenses = new double[table.Columns.Count];
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    licenses[j] = convertFromItemToInt(table.Rows[i][j]);
                }

                MarcovitsDataTable resultTableRow = new MarcovitsDataTable(convertFromItemToInt(table.
                    Rows[i][0]), convertFromItemToInt(table.Rows[i][1]),
                    convertFromItemToInt(table.Rows[i][2]), convertFromItemToInt(table.Rows[i][3]),
                    licenses);
                newData.Add(resultTableRow);
            }
            return newData;
        }

        private int convertFromItemToInt(object item)
        {
            return int.Parse(item.ToString());
        }
    }
}