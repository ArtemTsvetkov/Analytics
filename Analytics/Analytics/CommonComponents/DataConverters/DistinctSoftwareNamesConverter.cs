using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.DataConverters
{
    class DistinctSoftwareNamesConverter : DataConverter<DataSet, string[]>
    {
        public string[] convert(DataSet data)
        {
            DataSet ds = data;
            string[] newData = new string[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                newData[i] = ds.Tables[0].Rows[i][0].ToString();
                //MS Sql Server дописывает пробелы в конец, их нужно убрать
                bool goNext = false;
                while (goNext == false)
                {
                    if(newData[i].ElementAt(newData[i].Length-1).Equals(' '))
                    {
                        newData[i] = newData[i].Remove(newData[i].Length - 1);
                    }
                    else
                    {
                        goNext = true;
                    }
                }
            }
            return newData;
        }
    }
}