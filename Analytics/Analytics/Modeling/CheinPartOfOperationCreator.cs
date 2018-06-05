using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling
{
    class CheinPartOfOperationCreator
    {
        private Operation operation;
        private CheinPartOfOperationCreator next;

        public CheinPartOfOperationCreator(Operation operation)
        {
            this.operation = operation;
        }

        public void setNext(CheinPartOfOperationCreator operation)
        {
            next = operation;
        }

        public Operation processing(string rule)
        {
            Operation newOperation = operation.check(rule);
            if (newOperation == null)
            {
                if (next != null)
                {
                    return giveOutWorkToNext(rule);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                return newOperation;
            }
        }

        private Operation giveOutWorkToNext(string rule)
        {
            return next.processing(rule);
        }
    }
}
/*
Экзамепляры данного класса используются как части цепочки обязанности при
создании правил в классе RulesParser 
*/