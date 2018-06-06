using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommandsStore
{
    abstract class BasicCommand<TModelsTypeOfResult, TModelsTypeState> : 
        Command<TModelsTypeOfResult, TModelsTypeState>
        where TModelsTypeState : ModelsState
    {
        protected TModelsTypeState modelsState;
        protected BasicModel<TModelsTypeOfResult, TModelsTypeState> model;

        abstract public void execute();

        public BasicCommand(BasicModel<TModelsTypeOfResult, TModelsTypeState> model)
        {
            setModel(model);
        }

        public TModelsTypeState getModelState()
        {
            return modelsState;
        }

        public void recoveryModel()
        {
            model.recoverySelf(modelsState);
        }

        public void setModel(BasicModel<TModelsTypeOfResult, TModelsTypeState> model)
        {
            this.model = model;
        }
    }
}
