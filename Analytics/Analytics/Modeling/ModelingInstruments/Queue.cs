using Analytics.Modeling.ModelingInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class Queue : ModelingInstrument<Queue>
    {
        private string name;//имя очереди
        public int count_of_tranzactions_in_queue;//кол-во транзактов в очереди
        private int max_tranzacts_in_queue;
        private int sum_tranzacts_in_queue;

        public string get_name()//возвращает имя очереди
        {
            return name;
        }
        public Queue(string name)
        {
            this.name = name;
            this.sum_tranzacts_in_queue = 0;
            this.max_tranzacts_in_queue = 0;
        }
        public void update_tranzacts_in_queue()
        {
            if(count_of_tranzactions_in_queue > max_tranzacts_in_queue)
            {
                max_tranzacts_in_queue = count_of_tranzactions_in_queue;
            }
            sum_tranzacts_in_queue++;
        }
        public int get_max_tranzacts_in_queue()
        {
            return max_tranzacts_in_queue;
        }
        public int get_sum_tranzacts_in_queue()
        {
            return sum_tranzacts_in_queue;
        }

        public string getType()
        {
            return "Queue";
        }

        public Queue clone()
        {
            Queue queue = new Queue(name);
            queue.count_of_tranzactions_in_queue = count_of_tranzactions_in_queue;
            queue.max_tranzacts_in_queue = max_tranzacts_in_queue;
            queue.sum_tranzacts_in_queue = sum_tranzacts_in_queue;

            return queue;
        }
    }
}
/*
 * Класс очереди, моделирует какую-либо очередь
 */
