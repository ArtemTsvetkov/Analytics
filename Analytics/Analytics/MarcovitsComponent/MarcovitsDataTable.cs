using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsDataTable//класс для хранения строк результирующей таблицы
    {
        public double[] licenses;//для произволного кол-ва анализируемых лицензий

        public MarcovitsDataTable()
        {

        }

        public MarcovitsDataTable(int year_in, int month_in, int day_in, int hours_in,
            double[] licenses)
        {
            this.licenses = licenses;
        }

        public MarcovitsDataTable copy()
        {
            return new MarcovitsDataTable(0, 0, 0,
                0, licenses);
        }
    }
}
