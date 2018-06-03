using Analytics.CommandsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class ConcreteCommandStore<TModelsTypeOfResult, TModelsTypeState> : 
        CommandsStore<TModelsTypeOfResult, TModelsTypeState> where TModelsTypeState : ModelsState
    {
        private List<BasicCommand<TModelsTypeOfResult, TModelsTypeState>> history = 
            new List<BasicCommand<TModelsTypeOfResult, TModelsTypeState>>();

        public void executeCommand(BasicCommand<TModelsTypeOfResult, TModelsTypeState> command)
        {
            command.execute();
            push(command);
        }

        public void recoveryModel()
        {
            BasicCommand<TModelsTypeOfResult, TModelsTypeState> command = pop();
            command.recoveryModel();
        }

        public BasicCommand<TModelsTypeOfResult, TModelsTypeState> pop()
        {
            return history.Last();
        }

        public void push(BasicCommand<TModelsTypeOfResult, TModelsTypeState> command)
        {
            history.Add(command);
        }
    }
}

