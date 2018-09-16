using Analytics.CommandsStore;
using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.BasicObjects.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class GetMarcovitsStatistcCommand<TConfigType>
        : BasicCommand
    {
        new BasicStatisticsModel<MarcovitsModelState, TConfigType> model;

        public GetMarcovitsStatistcCommand(BasicStatisticsModel<MarcovitsModelState, TConfigType>
            model) : base(model)
        {
            
        }

        public override void execute()
        {        
            model.calculationStatistics();
            modelsState = model.copySelf();
        }
    }
}
