﻿using Analytics.CommonComponents.ExceptionHandler.Exceptions;
using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.Exceptions;
using Analytics.MarcovitsComponent.Exceptions;
using Analytics.Modeling.ModelingExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler.Concrete
{
    class ConcreteExceptionHandlerInitializer
    {
        public static void initThisExceptionHandler(ExceptionHandlerInterface handler)
        {
            try
            {
                handler.addException(new UnknownTimeIntervalType());
                handler.addException(new NoConfigurationSpecified());
                handler.addException(new InvalidArraySize());
                handler.addException(new NoDataBaseConnection());
                handler.addException(new NoFilesConnection());
                handler.addException(new ErrorWithFile());
                handler.addException(new NotEnoughDataToAnalyze());
                handler.addException(new IncorrectFormatOperation());
                handler.addException(new NotEnoughMemoryInTheModelingElement());
                handler.addException(new UnknownOperation());
                handler.addException(new UsingCovarianceInsteadOfCorrelation());
            }
            catch(Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }
    }
}
