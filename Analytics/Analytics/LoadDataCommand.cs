using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class LoadDataCommand : Command
    {
        private Model model;
        private ModelsState state;

        public LoadDataCommand(Model model)
        {
            this.model = model;
            state = this.model.copySelf();
        }

        public ModelsState getModelState()
        {
            return state;
        }

        public void recoveryModel()
        {
            model.recoverySelf(state);
        }

        public void execute()
        {
            model.loadStore();
        }
    }
}