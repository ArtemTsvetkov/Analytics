using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.Interfaces.AdwancedModelsInterfaces
{
    interface RecoveredModel
    {
        ModelsState copySelf();//Создание копии модели для возможного ее восстановления
        void recoverySelf(ModelsState state);//восстановление модели
    }
}
