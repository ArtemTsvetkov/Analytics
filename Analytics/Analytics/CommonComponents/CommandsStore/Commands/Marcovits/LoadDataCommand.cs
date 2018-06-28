using Analytics.CommandsStore;
using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class LoadDataCommand<TConfigType> : BasicCommand<MarcovitsModelState, TConfigType>
    {
        //private BasicModel<MarcovitsModelState, MarcovitsModelState> model;
        private ModelsState state;

        public LoadDataCommand(BasicModel<MarcovitsModelState, TConfigType> model)
            : base(model)
        {
        }

        public override void execute()
        {
            state = model.copySelf();
            //model.loadStore();
        }
    }
}