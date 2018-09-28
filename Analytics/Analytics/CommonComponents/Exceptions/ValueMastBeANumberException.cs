using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.ExceptionHandler.View.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.Exceptions
{
    class ValueMastBeANumberException : Exception, ConcreteException
    {
        public ValueMastBeANumberException() : base() { }

        public ValueMastBeANumberException(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<ErrorPopupWindowConfig> view = new ErrorPopupWindow();
            ErrorPopupWindowConfig config = new ErrorPopupWindowConfig(
                "Значение должно быть числом!");
            view.setConfig(config);
            view.show();
        }
    }
}