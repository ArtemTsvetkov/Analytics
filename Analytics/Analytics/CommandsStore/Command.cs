using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    interface Command<TModelsTypeOfResult, TModelsTypeState, TConfigType> where TModelsTypeState : ModelsState
    {
        TModelsTypeState getModelState();//Получить сохраненное состояние модели
        void execute();//Выполнить некоторые действия с моделью
        void recoveryModel();//Восстановить модель, отправив в нее ModelsState
        //Установить модель, с которой работает команда
        void setModel(BasicModel<TModelsTypeOfResult, TModelsTypeState, TConfigType> model);
    }
}
/*
 * Представляет собой объект команды, выполняет некоторые действия и хранит стейт модели на 
 * случай отката
 */
