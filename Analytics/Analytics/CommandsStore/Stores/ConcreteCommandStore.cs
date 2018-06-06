using Analytics.CommandsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class ConcreteCommandStore<TModelsTypeOfResult, TModelsTypeState, TConfigType> : 
        CommandsStore<TModelsTypeOfResult, TModelsTypeState, TConfigType> where TModelsTypeState : ModelsState
    {
        private List<BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType>> history = 
            new List<BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType>>();

        public void executeCommand(BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> command)
        {
            command.execute();
            push(command);
        }

        public void recoveryModel()
        {
            BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> command = pop();
            command.recoveryModel();
        }

        public BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> pop()
        {
            return history.Last();
        }

        public void push(BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> command)
        {
            history.Add(command);
        }
    }
}

