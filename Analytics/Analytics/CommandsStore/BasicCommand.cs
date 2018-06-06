using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommandsStore
{
    abstract class BasicCommand<TModelsTypeOfResult, TModelsTypeState, TConfigType> : 
        Command<TModelsTypeOfResult, TModelsTypeState, TConfigType>
        where TModelsTypeState : ModelsState
    {
        protected TModelsTypeState modelsState;
        protected BasicModel<TModelsTypeOfResult, TModelsTypeState, TConfigType> model;

        abstract public void execute();

        public BasicCommand(BasicModel<TModelsTypeOfResult, TModelsTypeState, TConfigType> model)
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

        public void setModel(BasicModel<TModelsTypeOfResult, TModelsTypeState, TConfigType> model)
        {
            this.model = model;
        }
    }
}
