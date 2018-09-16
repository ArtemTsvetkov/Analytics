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
        private List<BasicCommand> history = 
            new List<BasicCommand>();
        private List<BasicCommand> rollbacksHistory =
            new List<BasicCommand>();

        public void executeCommand(BasicCommand command)
        {
            command.execute();
            rollbacksHistory.Clear();
            push(command);
        }

        public void recoveryModel()
        {
            BasicCommand command = getPreviousCommand();
            if (command != null)
            {
                command.recoveryModel();
            }
        }

        public BasicCommand getPreviousCommand()
        {
            if (history.Count > 0)
            {
                BasicCommand command = history.Last();
                rollbacksHistory.Add(history.Last());
                history.RemoveAt(history.Count - 1);
                return command;
            }
            else
            {
                return null;
            }
        }

        public BasicCommand getNextCommand()
        {
            if (rollbacksHistory.Count > 0)
            {
                BasicCommand command = rollbacksHistory.Last();
                history.Add(rollbacksHistory.Last());
                rollbacksHistory.RemoveAt(rollbacksHistory.Count - 1);
                return command;
            }
            else
            {
                return null;
            }
        }

        public void push(BasicCommand command)
        {
            history.Add(command);
        }

        public void rollbackRecoveryModel()
        {
            BasicCommand command = getNextCommand();
            if (command != null)
            {
                command.recoveryModel();
            }
        }
    }
}

