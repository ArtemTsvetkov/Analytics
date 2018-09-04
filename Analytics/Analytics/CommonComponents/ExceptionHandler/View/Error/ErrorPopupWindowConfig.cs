using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler.View.Error
{
    class ErrorPopupWindowConfig
    {
        private string message;

        public ErrorPopupWindowConfig(string message)
        {
            this.message = message;
        }

        public string getMessage()
        {
            return message;
        }
    }
}
