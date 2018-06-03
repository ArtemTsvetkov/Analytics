using Analytics.CommandsStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class ConcreteCommandStore : CommandsStore<MarcovitsModelState, MarcovitsModelState>
    {
        private List<BasicCommand<MarcovitsModelState, MarcovitsModelState>> history = 
            new List<BasicCommand<MarcovitsModelState, MarcovitsModelState>>();

        public void executeCommand(BasicCommand<MarcovitsModelState, MarcovitsModelState> command)
        {
            command.execute();
            push(command);
        }

        public void recoveryModel()
        {
            BasicCommand<MarcovitsModelState, MarcovitsModelState> command = pop();
            command.recoveryModel();
        }

        public BasicCommand<MarcovitsModelState, MarcovitsModelState> pop()
        {
            return history.Last();
        }

        public void push(BasicCommand<MarcovitsModelState, MarcovitsModelState> command)
        {
            history.Add(command);
        }
    }
}

