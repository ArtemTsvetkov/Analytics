﻿using Analytics.CommonComponents.Exceptions;
using Analytics.Modeling.GroupByTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.Config
{
    class ModelingConfig
    {
        //Путь до БД
        private string pathOfDataBase;
        //Флаг использования корелляции между запроса на лицензии
        private bool withKovar = false;
        //Модификатор группировки для нализа(по дням/минутам и тд)
        //рассматриваемый промежуток времени
        private GropByType interval = new HourType();
        //Необходимое количество запусков моделирования, если оно больше 1, то в итоговый
        //отчет попадут усредненные значения
        private int numberOfStartsModeling = 1;

        public ModelingConfig(string pathOfDataBase, GropByType interval)
        {
            this.pathOfDataBase = pathOfDataBase;
            this.interval = interval;
        }

        public int getNumberOfStartsModeling()
        {
            return numberOfStartsModeling;
        }

        public void setNumberOfStartsModeling(int numberOfStartsModeling)
        {
            if (numberOfStartsModeling < 1)
            {
                throw new IncorrectValue("Enter incorrect value: for number of starts modeling");
            }
            else
            {
                this.numberOfStartsModeling = numberOfStartsModeling;
            }
        }

        public GropByType getInterval()
        {
            return interval;
        }

        public bool getWithKovar()
        {
            return withKovar;
        }

        public void setWithKovar(bool withKovar)
        {
            this.withKovar = withKovar;
        }

        public string getPathOfDataBase()
        {
            return pathOfDataBase;
        }

        public void setPathOfDataBase(string pathOfDataBase)
        {
            this.pathOfDataBase = pathOfDataBase;
        }
    }
}
//Cледуя флагу resetAllState, либо полностью
//откатит все изменения стейта, либо только те объекты, которые участвуют
//непосредственно в моделировании(очереди, устройства и тд), но оставит
//количество запусков моделирования и отчет