using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.ExceptionHandler.Interfaces
{
    interface ExceptionView<TConfigType>
    {
        //Установка конфигурации
        void setConfig(TConfigType config);
        //Отображение вью
        void show();
    }
}
