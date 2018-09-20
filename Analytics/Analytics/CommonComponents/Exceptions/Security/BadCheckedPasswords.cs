using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.ExceptionHandler.View.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.Exceptions.Security
{
    class BadCheckedPasswords : Exception, ConcreteException
    {
        public BadCheckedPasswords() : base() { }

        public BadCheckedPasswords(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<ErrorPopupWindowConfig> view = new ErrorPopupWindow();
            ErrorPopupWindowConfig config = new ErrorPopupWindowConfig(
                "Указаныe (новые) пароли должны совпадать и быть не пустыми!");
            view.setConfig(config);
            view.show();
        }
    }
}