using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie
{
    class Lable
    {
        //номер строки метки в коде или др словами-место, куда отправится транзакт по этой метке
        private int my_plase;
        private string lable_name;//имя метки
        private int entries_number;//кол-во транзактов, прошедших через метку
        public Lable(int my_plase, string lable_name)
        {
            this.my_plase = my_plase;
            this.lable_name = lable_name;
        }
        public string get_name()
        {
            return lable_name;
        }
        public int get_my_plase()
        {
            return my_plase;
        }
        public void upper_entries_number()
        {
            entries_number++;
        }
        public int get_entries_number()
        {
            return entries_number;
        }
    }
}
/*
 *Класс-метка 
 */