using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelingRules
{
    abstract class BasicOperation : Operation
    {
        protected ModelingModel model;
        protected string[] parameters;//Хранит параметры конкретной операции
        abstract public Operation check(string rule);
        abstract public void processing();

        public BasicOperation(ModelingModel model)
        {
            this.model = model;
        }
    }
}
/*
 Базовый класс для хранения конкретных реализаций Operation
 отличается от Operation наличием поля parameters реализаиция команды из одноименного паттерна
*/
