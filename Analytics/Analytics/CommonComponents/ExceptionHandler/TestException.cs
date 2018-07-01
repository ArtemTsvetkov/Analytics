using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler
{
    class TestException : Exception, ConcreteException
    {
        public void processing()
        {
            bool isProcessing = true;
        }
    }
}
