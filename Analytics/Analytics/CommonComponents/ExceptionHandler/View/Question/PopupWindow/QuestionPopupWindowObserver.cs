﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler.View.Question.PopupWindow
{
    interface QuestionPopupWindowObserver
    {
        void saidYes();
        void saidNo();
    }
}
