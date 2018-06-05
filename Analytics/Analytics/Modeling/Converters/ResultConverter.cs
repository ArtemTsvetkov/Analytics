using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.Converters
{
    class ResultConverter : DataConverter<ModelingState, ModelingState>
    {
        public ModelingState convert(ModelingState data)
        {
            return data;
        }
    }
}
