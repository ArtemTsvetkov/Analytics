using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler.View.Question.PopupWindow
{
    class QuestionPopupWindowConfig
    {
        private string message;
        private QuestionPopupWindowObserver observer;

        public QuestionPopupWindowConfig(string message, QuestionPopupWindowObserver observer)
        {
            this.message = message;
            this.observer = observer;
        }

        public string getMessage()
        {
            return message;
        }

        public QuestionPopupWindowObserver getObserver()
        {
            return observer;
        }
    }
}
