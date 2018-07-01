using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler.Interfaces
{
    interface ExceptionHandler
    {
        //Обработка исключения
        void processing(Exception exception);
        //Добавление нового исключения
        void addException(ConcreteException exception);
    }
}
