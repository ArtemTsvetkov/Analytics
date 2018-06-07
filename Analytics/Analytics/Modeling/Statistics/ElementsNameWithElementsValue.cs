using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.Statistics
{
    class ElementsNameWithElementsValue
    {
        public string name;
        public double value;

        public ElementsNameWithElementsValue(string name, double value)
        {
            this.name = name;
            this.value = value;
        }

        public ElementsNameWithElementsValue copy()
        {
            return new ElementsNameWithElementsValue(name, value);
        }
    }
}
/*
 * Позволяет хранить имя, например, очереди и , например, максимальное количество
 * заявок в этой очереди. Параметр value может хранить значение как одного 
 * прогона модели, так и суммарное нескольких прогонов.
 */
