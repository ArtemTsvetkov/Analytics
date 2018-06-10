using Analytics.Modeling.ModelingInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class Lable : ModelingInstrument<Lable>
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

        public void decrementEntriesNumber()
        {
            entries_number--;
        }

        public void set_entries_number(int entries_number)
        {
            this.entries_number = entries_number;
        }

        public int get_entries_number()
        {
            return entries_number;
        }

        public string getType()
        {
            return "Lable";
        }

        public Lable clone()
        {
            Lable lable = new Lable(my_plase, lable_name);
            lable.set_entries_number(get_entries_number());

            return lable;
        }
    }
}
/*
 *Класс-метка 
 */