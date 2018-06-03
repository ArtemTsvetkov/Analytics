using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie
{
    class TranzactionsGenerator
    {
        public int time_until_the_next_tranzaction;//время до следующей заявки
        public int count_tranzaktion;//оставшееся кол-во транзактов
        //номер строки в коде или др словами-место, где сейчас будет создан транзакт
        private int my_place;

        private int delays_time_left;//минимальное время задержки транзактов в этом устройстве
        private int delays_time_right;//максимальное время задержки транзактов в этом устройстве

        public TranzactionsGenerator(int delays_time_left, int delays_time_right, 
            int time_until_the_next_tranzaction, int my_place, int count_tranzaktion)
        {
            this.delays_time_left = delays_time_left;
            this.delays_time_right = delays_time_right;
            this.time_until_the_next_tranzaction = time_until_the_next_tranzaction;
            this.count_tranzaktion = count_tranzaktion;
            this.my_place = my_place;
        }

        public int get_my_plase()
        {
            return my_place;
        }

        public int get_deleys_time_left()
        {
            return delays_time_left;
        }

        public int get_deleys_time_right()
        {
            return delays_time_right;
        }
    }
}
/*
 * Класс генератора заявок
 */
