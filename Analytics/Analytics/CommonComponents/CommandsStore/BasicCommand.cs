using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.Interfaces.AdwancedModelsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommandsStore
{
    abstract class BasicCommand : Command
    {
        protected ModelsState modelsState;
        protected RecoveredModel model;

        abstract public void execute();

        public BasicCommand(RecoveredModel model)
        {
            setModel(model);
        }

        public ModelsState getModelState()
        {
            return modelsState;
        }

        public void recoveryModel()
        {
            model.recoverySelf(modelsState);
        }

        public void setModel(RecoveredModel model)
        {
            this.model = model;
        }
    }
}
