using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class Variable
    {
        private string name;//имя переменной
        public string value;//значение переменной, для случая использования команды savevalue
        //функция переменной, по которой она рассчитывается, это происходит в момент ее вызова, 
        //если не используется команды savevalue, то переменаня не запоминает свое значение
        //если функция пуста, значит переменная определена как ячейка табдицы, она всегда помнит 
        //свое знчение, для его изменения необходимо использовать savevalue
        private string function;
        public Variable(string name, string function, string value)
        {
            this.name = name;
            this.value = value;
            this.function = function;
        }
        public string get_name()
        {
            return name;
        }
        public string get_function()
        {
            return function;
        }
    }
}
/*
 * Класс-переменная
 */