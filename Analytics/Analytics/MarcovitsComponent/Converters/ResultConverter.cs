using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.MarcovitsComponent.Converters
{
    class ResultConverter : DataConverter<MarcovitsModelState, MarcovitsModelState>
    {
        public MarcovitsModelState convert(MarcovitsModelState data)
        {
            return data;
        }
    }
}
