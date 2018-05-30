using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsDataTable//класс для хранения строк результирующей таблицы
    {
        //public int requireNumber;//покажет, сколько требовалось в определенный срок
        //public int currentNumber = 0;//Покажет, сколько планировается закупить
        public double[] licenses;//для произволного кол-ва анализируемых лицензий
        public int year_in;
        public int month_in;
        public int day_in;
        public int hours_in;

        public MarcovitsDataTable()
        {

        }

        public MarcovitsDataTable(int year_in, int month_in, int day_in, int hours_in, double[] licenses)
        {
            this.year_in = year_in;
            this.month_in = month_in;
            this.day_in = day_in;
            this.hours_in = hours_in;
            this.licenses = licenses;
        }
    }
}
