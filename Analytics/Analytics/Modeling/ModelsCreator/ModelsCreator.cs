using Analytics.CommonComponents.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.ModelsCreator
{
    class ModelsCreator : DataWorker<ModelsCreatorConfigState, List<string>>
    {
        private ModelsCreatorConfigState fields;
        private List<string> result;


        public bool connect()
        {
            return true;
        }

        public void execute()
        {
            //Проверка флага на создание модели с учетом ковариации
            if (fields.withKovar)
            {
                //На каждой итерации создается отдельная, малая модель для конкретной лицензии
                for (int i = 0; i < fields.licenceInfo.Count(); i++)
                {
                    LicenceInfo info = fields.licenceInfo.ElementAt(i);
                    string storageName = info.getName() + "storage";
                    string queueName = info.getName() + "queue";
                    result.Add(storageName + " STORAGE " + info.getCount());
                    result.Add("GENERATE " + info.getAvgRequestedTime() + "," +
                        info.getAvgSquereRequestedTime() + "," +
                        info.getNumberOfTranzactsOnStart());
                    result.Add("ASSIGN 1,0");
                    result.Add("TRANSFER ,SkipIncrement"+ info.getName());
                    result.Add("OnStart" + info.getName() + " ASSIGN 1,1");
                    result.Add("SkipIncrement" + info.getName() + " QUEUE " + queueName);
                    result.Add("TEST NE P1,1,OnStart" + info.getName() + "SkipBoth");
                    result.Add("TRANSFER BOTH,,ExitFrom" + queueName);
                    result.Add("OnStart" + info.getName() + "SkipBoth ENTER " + storageName);
                    result.Add("DEPART " + queueName);
                    result.Add("ADVANCE " + "70" + "," + "12");
                    result.Add("LEAVE " + storageName);
                    result.Add("TRANSFER ,Exit" + info.getName());
                    result.Add("ExitFrom" + queueName + " DEPART " + queueName);
                    for (int m=0; m < fields.licenceInfo.Count(); m++)
                    {
                        if (m != i)
                        {
                            if (fields.korellation[m, i] > 1 | fields.korellation[m, i] < -1)
                            {
                                throw new Exception();
                                //ДОБАВИТЬ ИСКЛЮЧЕНИЕ - ИСПОЛЬЗОВАНИЕ КОВАРИАЦИИ ВМЕСТО КОРЕЛЛЯЦИИ
                            }
                            else
                            {
                                //Если равно -1, то вероятность того, что пользователь
                                //попытается получить лицензию m, равна единице
                                //по-этому ставлю безуслвоный переход
                                if (fields.korellation[m, i] == -1)
                                {
                                    result.Add("TRANSFER ,OnStartLN2");
                                    continue;
                                }
                                //Если больше нуля, но меньше единицы, то для
                                //простоты модели считаю, что нет зависимости
                                //Иначе пришлось бы учитывать, что пользователь может запросить
                                //зависимые лицензии с каким-то неопределенным интервалом и
                                //нужно будет удалять запросы на другие лицензии(зависимые)
                                if (fields.korellation[m, i] < 0)
                                {
                                    //Удаление "-0,"
                                    string percentInRightFormat = "." + fields.korellation[m, i].
                                        ToString().Remove(0,3);
                                    result.Add("TRANSFER "+ percentInRightFormat + ",,"+
                                        "OnStart" + fields.licenceInfo.ElementAt(m).getName());
                                }
                            }
                        }
                    }
                    result.Add("TRANSFER ,OnStart"+ info.getName());
                    result.Add("Exit"+ info.getName() + " TERMINATE 1");
                }
                int fgdgd = 0;
            }
            else
            {
                //На каждой итерации создается отдельная, малая модель для конкретной лицензии
                for (int i = 0; i < fields.licenceInfo.Count(); i++)
                {
                    LicenceInfo info = fields.licenceInfo.ElementAt(i);
                    result.Add(info.getName() + "storage" + " STORAGE " + info.getCount());
                    result.Add("GENERATE " + info.getAvgRequestedTime() + "," +
                        info.getAvgSquereRequestedTime() + "," + 
                        info.getNumberOfTranzactsOnStart());
                    result.Add("QUEUE " + info.getName() + "queue");
                    result.Add("ENTER " + info.getName() + "storage");
                    result.Add("DEPART " + info.getName() + "queue");
                    result.Add("ADVANCE " + info.getAvgDelayTimeInTheProcessing() + "," +
                        info.getAvgSquereDelayTimeInTheProcessing());
                    result.Add("LEAVE " + info.getName() + "storage");
                    result.Add("TERMINATE 1");
                }
            }
        }

        public List<string> getResult()
        {
            return result;
        }

        public void setConfig(ModelsCreatorConfigState fields)
        {
            result = new List<string>();
            this.fields = fields;
        }
    }
}
