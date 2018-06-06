using Analytics.CommandsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    interface CommandsStore<TModelsTypeOfResult, TModelsTypeState>
        where TModelsTypeState : ModelsState
    {
        //Добавление команды в стек
        void push(BasicCommand<TModelsTypeOfResult, TModelsTypeState> command);
        //Извлечение команды из стека
        BasicCommand<TModelsTypeOfResult, TModelsTypeState> pop();
        //Выполнение команды
        void executeCommand(BasicCommand<TModelsTypeOfResult, TModelsTypeState> command);
        void recoveryModel();//Откат изменений модели
    }
}
