using Analytics.CommandsStore;
using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class GetMarcovitsStatistcCommand : BasicCommand<MarcovitsModelState, MarcovitsModelState>
    {
        //private BasicModel<MarcovitsModelState, MarcovitsModelState> model;
        private ModelsState state;

        public GetMarcovitsStatistcCommand(BasicModel<MarcovitsModelState, MarcovitsModelState>
            model) : base(model)
        {
            
        }

        public override void execute()
        {
            state = model.copySelf();
            model.calculationStatistics();
        }
    }
}
