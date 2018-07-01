using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler
{
    class ExceptionHandler : ExceptionHandlerInterface
    {
        private List<ConcreteException> exceptions;
        private static ExceptionHandler currentInstanse;

        private ExceptionHandler()
        {
            exceptions = new List<ConcreteException>();
        }

        public static ExceptionHandlerInterface getInstance()
        {
            if(currentInstanse == null)
            {
                currentInstanse = new ExceptionHandler();
            }
            return currentInstanse;
        }

        public void addException(ConcreteException exception)
        {
            //Проверка, возможно, такое исключение было создано ранее
            for (int i = 0; i < currentInstanse.exceptions.Count; i++)
            {
                if (currentInstanse.exceptions.ElementAt(i).GetType() == exception.GetType())
                {
                    //ВЫЗОВ ИСКЛЮЧЕНИЯ-ТАКОЕ ИСКЛЮЧЕНИЕ УЖЕ БЫЛО ДОБАВЛЕННО РАНЕЕ
                    int ikhkhk = 0;
                }
            }
            currentInstanse.exceptions.Add(exception);
        }

        public void processing(Exception exception)
        {
            for(int i=0;i<currentInstanse.exceptions.Count;i++)
            {
                if (currentInstanse.exceptions.ElementAt(i).GetType() == exception.GetType())
                {
                    currentInstanse.exceptions.ElementAt(i).processing();
                    break;
                }
            }
            //Если до сюда дошло, то исключение не найдено
            //ДОБАВИТЬ СЮДА ОБРАБОТКУ ИСКЛЮЧЕНИЯ-НЕИЗВЕСТНОЕ ИСКЛЮЧЕНИЕ
        }
    }
}
