using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    interface Command<TModelsTypeOfResult, TConfigType>
    {
        ModelsState getModelState();//Получить сохраненное состояние модели
        void execute();//Выполнить некоторые действия с моделью
        void recoveryModel();//Восстановить модель, отправив в нее ModelsState
        //Установить модель, с которой работает команда
        void setModel(BasicModel<TModelsTypeOfResult, TConfigType> model);
    }
}
/*
 * Представляет собой объект команды, выполняет некоторые действия и хранит стейт модели на 
 * случай отката
 */
