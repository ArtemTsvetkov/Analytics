using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelirovanie
{
    interface Operation
    {
        //Проверка, может ли эта Operation работать с такой строкой, если
        //может, то происходит инициализация необходимых параметров и вернет
        //экземпляр класса
        Operation check(string rule);
        void processing();//Обработка транзакта правилом
    }
}
/*
 * Интерфейс, описывающий конкретные операции, реализаиция команды из одноименного паттерна
 */