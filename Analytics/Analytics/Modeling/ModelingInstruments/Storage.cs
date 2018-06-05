using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingInstruments
{
    class Storage
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
                //ДОБАВИТЬ ВЫЗОВ ИСКЛЮЧЕНИЯ
            }
        }

        public void leaveStorage(int numberOfPlaces)
        {
            emptyPlaces += numberOfPlaces;
        }
    }
}
/*
 * Класс устройства, моделирует многоканальное устройство
 */
