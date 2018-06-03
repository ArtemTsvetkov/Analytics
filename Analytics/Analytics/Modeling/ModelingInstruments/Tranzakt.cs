using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie
{
    class Tranzakt
    {
        private int my_id;//id транзакта
        public int time_in_system;//время нахождения транзакта в системе
        //номер строки транзакта в коде или др словами-место, где сейчас находится транзакт
        public int my_place;
        public int remaining_time_delay;//оставшееся время задержки транзакта
        //транзакт блокируется(то есть он не может быть перемещен, пока системное время не 
        //проинкрементится) когда попадает в блок задержки(когда не может продвинуться из-за 
        //занятости устройства), пока он не заблокирован он перемещается по всей модели
        //транзакта стоит до порядкового номера транзакта в устройстве, которое стоит после очереди
        public bool blocked;
        private List<string> parameters;//значение параметра
        private List<string> id_parameters;//id параметра

        //функция возвращает значение параметра по его имени
        public string get_parameter(string parameters_name)
        {
            for(int i=0;i<id_parameters.Count;i++)
            {
                if(id_parameters.ElementAt(i) == parameters_name)
                {
                    return parameters.ElementAt(i);
                }
                if(i == id_parameters.Count-1)
                {
                    return "No matches";
                }
            }
            return "Error";
        }

        //функция задает значение параметра
        public void set_parameter(string parameters_name, string parameters_value)
        {
            //проверка, возможно параметр с таким именем уже использолвался ранее и его 
            //можно просто переписать
            for(int i=0;i<id_parameters.Count();i++)
            {
                if(id_parameters.ElementAt(i) == parameters_name)
                {
                    //нельзя просто так заменить значение в списке, по-этому просто вставляю нужное
                    parameters.Insert(i,parameters_value);
                    parameters.RemoveAt(i + 1);//затем удаляю старое(оно сместилось вниз)
                    break;
                }
                if(i == id_parameters.Count()-1)//если ничего не нашли-просто вставляем
                {
                    id_parameters.Add(parameters_name);
                    parameters.Add(parameters_value);
                }
            }
            //на случай, когда еще ни одни параметр не быд задан и проверку выполнить нельзя
            if (id_parameters.Count() == 0)
            {
                id_parameters.Add(parameters_name);
                parameters.Add(parameters_value);
            }
        }


        public Tranzakt(int my_place, int my_id)
        {
            this.my_place = my_place;
            this.blocked = false;
            this.time_in_system = 0;
            this.remaining_time_delay = 0;
            id_parameters = new List<string>();
            parameters = new List<string>();
            this.my_id = my_id;
        }

        public int get_my_id()
        {
            return my_id;
        }
    }
}
/*
 * Класс транзакт, один из основных классов при моделировании, представляет собой один транзакт
 */