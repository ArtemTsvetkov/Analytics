using Analytics.CommandsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class ConcreteCommandStore<TModelsTypeOfResult, TConfigType> : 
        CommandsStore<TModelsTypeOfResult, TConfigType>
    {
        private List<BasicCommand<TModelsTypeOfResult, TConfigType>> history = 
            new List<BasicCommand<TModelsTypeOfResult, TConfigType>>();

        public void executeCommand(BasicCommand<TModelsTypeOfResult, TConfigType> command)
        {
            command.execute();
            push(command);
        }

        public void recoveryModel()
        {
            BasicCommand<TModelsTypeOfResult, TConfigType> command = pop();
            command.recoveryModel();
        }

        public BasicCommand<TModelsTypeOfResult, TConfigType> pop()
        {
            return history.Last();
        }

        public void push(BasicCommand<TModelsTypeOfResult, TConfigType> command)
        {
            history.Add(command);
        }
    }
}

