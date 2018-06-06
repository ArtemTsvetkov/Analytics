using Analytics.CommandsStore;
using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class GetMarcovitsStatistcCommand<TConfigType>
        : BasicCommand<MarcovitsModelState, MarcovitsModelState, TConfigType>
    {

        public GetMarcovitsStatistcCommand(BasicModel<MarcovitsModelState, MarcovitsModelState, TConfigType>
            model) : base(model)
        {
            
        }

        public override void execute()
        {
            modelsState = model.copySelf();
            model.calculationStatistics();
        }
    }
}
