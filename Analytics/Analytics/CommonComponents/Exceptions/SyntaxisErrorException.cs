﻿using Analytics.CommonComponents.ExceptionHandler;
using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.ExceptionHandler.TextJornalist;
using Analytics.CommonComponents.ExceptionHandler.View.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.Exceptions
{
    class SyntaxisErrorException : Exception, ConcreteException
    {
        public SyntaxisErrorException() : base() { }

        public SyntaxisErrorException(string message) : base(message) { }

        public void processing(Exception ex)
        {
            ExceptionViewInterface<ErrorPopupWindowConfig> view = new ErrorPopupWindow();
            ErrorPopupWindowConfig config = new ErrorPopupWindowConfig(
                "Обнаружена синтаксическая ошибка в модели");
            view.setConfig(config);
            view.show();

            TextJornalistConfig jornalistConfig =
                new TextJornalistConfig(ex.Message, ex.StackTrace, ex.Source);
            TextFilesJornalist jornalist = new TextFilesJornalist();
            jornalist.setConfig(jornalistConfig);
            jornalist.write();
        }
    }
}
