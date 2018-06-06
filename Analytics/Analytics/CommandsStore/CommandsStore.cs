using Analytics.CommandsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    interface CommandsStore<TModelsTypeOfResult, TModelsTypeState, TConfigType>
        where TModelsTypeState : ModelsState
    {
        //Добавление команды в стек
        void push(BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> command);
        //Извлечение команды из стека
        BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> pop();
        //Выполнение команды
        void executeCommand(BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> command);
        void recoveryModel();//Откат изменений модели
    }
}
