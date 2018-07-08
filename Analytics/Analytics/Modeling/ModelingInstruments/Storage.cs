using Analytics.Modeling.ModelingExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingInstruments
{
    class Storage : ModelingInstrument<Storage>
    {
        //имя устройства
        private string name;
        //массив очередности обработки заявок устройством(очередь устройства)
        public List<int> id_tranzaktions_in_device_queue;
        //кол-во оставшихся ячеек памяти у устройства
        private int emptyPlaces;
        private int size;

        public Storage(string name, int size)
        {
            this.name = name;
            emptyPlaces = size;
            this.size = size;
            id_tranzaktions_in_device_queue = new List<int>();
        }

        //возвращает имя устройства
        public string Get_name()
        {
            return name;
        }

        public bool checkEmptyPlaces(int numberOfPlaces)
        {
            if (emptyPlaces >= numberOfPlaces)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void enterStorage(int numberOfPlaces)
        {
            if (size >= numberOfPlaces)
            {
                emptyPlaces -= numberOfPlaces;
            }
            else
            {
                throw new NotEnoughMemoryInTheModelingElement(
                    "Not enough memory in the modeling element:"+ name);
            }
        }

        public void leaveStorage(int numberOfPlaces)
        {
            emptyPlaces += numberOfPlaces;
        }

        public string getType()
        {
            return "Storage";
        }

        public Storage clone()
        {
            Storage storage = new Storage(name, size);
            storage.emptyPlaces = emptyPlaces;
            int[] copy = new int[0];
            id_tranzaktions_in_device_queue.CopyTo(copy);
            for(int i=0; i<copy.Length;i++)
            {
                storage.id_tranzaktions_in_device_queue.Add(copy[i]);
            }

            return storage;
        }
    }
}
/*
 * Класс устройства, моделирует многоканальное устройство
 */
