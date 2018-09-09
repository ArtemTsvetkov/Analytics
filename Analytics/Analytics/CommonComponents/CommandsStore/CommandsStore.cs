using Analytics.CommandsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    interface CommandsStore<TModelsTypeOfResult, TConfigType>
    {
        //Добавление команды в стек
        void push(BasicCommand<TModelsTypeOfResult, TConfigType> command);
        //Извлечение команды
        BasicCommand<TModelsTypeOfResult, TConfigType> getNextCommand();
        BasicCommand<TModelsTypeOfResult, TConfigType> getPreviousCommand();
        //Выполнение команды
        void executeCommand(BasicCommand<TModelsTypeOfResult, TConfigType> command);
        void recoveryModel();//Откат изменений модели
        void rollbackRecoveryModel();//Действие, обраьное откату изменений модели
    }
}
