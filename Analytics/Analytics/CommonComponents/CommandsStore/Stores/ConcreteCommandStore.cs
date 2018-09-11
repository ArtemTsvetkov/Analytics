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
        private List<BasicCommand<TModelsTypeOfResult, TConfigType>> rollbacksHistory =
            new List<BasicCommand<TModelsTypeOfResult, TConfigType>>();

        public void executeCommand(BasicCommand<TModelsTypeOfResult, TConfigType> command)
        {
            command.execute();
            rollbacksHistory.Clear();
            push(command);
        }

        public void recoveryModel()
        {
            BasicCommand<TModelsTypeOfResult, TConfigType> command = getPreviousCommand();
            if (command != null)
            {
                command.recoveryModel();
            }
        }

        public BasicCommand<TModelsTypeOfResult, TConfigType> getPreviousCommand()
        {
            if (history.Count > 0)
            {
                BasicCommand<TModelsTypeOfResult, TConfigType> command = history.Last();
                rollbacksHistory.Add(history.Last());
                history.RemoveAt(history.Count - 1);
                return command;
            }
            else
            {
                return null;
            }
        }

        public BasicCommand<TModelsTypeOfResult, TConfigType> getNextCommand()
        {
            if (rollbacksHistory.Count > 0)
            {
                BasicCommand<TModelsTypeOfResult, TConfigType> command = rollbacksHistory.Last();
                history.Add(rollbacksHistory.Last());
                rollbacksHistory.RemoveAt(rollbacksHistory.Count - 1);
                return command;
            }
            else
            {
                return null;
            }
        }

        public void push(BasicCommand<TModelsTypeOfResult, TConfigType> command)
        {
            history.Add(command);
        }

        public void rollbackRecoveryModel()
        {
            BasicCommand<TModelsTypeOfResult, TConfigType> command = getNextCommand();
            if (command != null)
            {
                command.recoveryModel();
            }
        }
    }
}

