using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class Device
    {
        //имя устройства
        private string name;
        //массив очередности обработки заявок устройством(очередь устройства)
        public List<int> id_tranzaktions_in_device_queue;
        //если true, то устройство свободно и его можно занять
        public bool device_empty;
        //возвращает имя устройства
        public string Get_name()
        {
            return name;
        }

        public Device(string name)
        {
            this.name = name;
            device_empty = true;
            id_tranzaktions_in_device_queue = new List<int>();
        } 
    }
}
/*
 * Класс устройства, моделирует какое-либо устройство
 */