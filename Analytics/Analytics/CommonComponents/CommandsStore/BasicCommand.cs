using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommandsStore
{
    abstract class BasicCommand<TModelsTypeOfResult, TConfigType> : 
        Command<TModelsTypeOfResult, TConfigType>
    {
        protected ModelsState modelsState;
        protected BasicModel<TModelsTypeOfResult, TConfigType> model;

        abstract public void execute();

        public BasicCommand(BasicModel<TModelsTypeOfResult, TConfigType> model)
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

        public void setModel(BasicModel<TModelsTypeOfResult, TConfigType> model)
        {
            this.model = model;
        }
    }
}
