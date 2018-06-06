using Analytics.Modeling.ModelingInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class Device : ModelingInstrument<Device>
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

        public string getType()
        {
            return "Device";
        }

        public Device clone()
        {
            Device device = new Device(Get_name());
            int[] copy = new int[0];
            id_tranzaktions_in_device_queue.CopyTo(copy);
            for(int i=0; i<copy.Length; i++)
            {
                device.id_tranzaktions_in_device_queue.Add(copy[0]);
            }
            device.device_empty = device_empty;

            return device;
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