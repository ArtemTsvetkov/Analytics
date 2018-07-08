using Analytics;
using Analytics.CommonComponents;
using Analytics.CommonComponents.BasicObjects;
using Analytics.CommonComponents.DataConverters;
using Analytics.CommonComponents.Interfaces.Data;
using Analytics.CommonComponents.Math;
using Analytics.CommonComponents.MsSqlServersQueryConfigurator;
using Analytics.CommonComponents.WorkWithDataBase.MsSqlServer;
using Analytics.CommonComponents.WorkWithFiles.Load;
using Analytics.Modeling;
using Analytics.Modeling.Config;
using Analytics.Modeling.Converters;
using Analytics.Modeling.GroupByTypes;
using Analytics.Modeling.IntermediateStates;
using Analytics.Modeling.IntermediateStates.ModelsCreatorConfig;
using Analytics.Modeling.ModelsCreator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class ModelingModel : BasicModel<ModelingReport, ModelingConfig>
    {
        private ModelingState state;
        //Отчет по моделированию
        private ModelingReport report;
        private int numberOfStartsModel = 0;

        public ModelingState getState()
        {
            return state;
        }

        //Данная функция, следуя флагу resetAllState, либо полностью
        //откатит все изменения стейта, либо только те объекты, которые участвуют
        //непосредственно в моделировании(очереди, устройства и тд), но оставит
        //количество запусков моделирования и отчет
        public override void recoverySelf(ModelsState backUpState)
        {
            ModelingState oldState = (ModelingState)backUpState;
            state = new ModelingState();
            state.last_tranzaktions_id = oldState.last_tranzaktions_id;
            state.result = oldState.result;
            state.time_of_modeling = oldState.time_of_modeling;
            state.idProcessingTranzact = oldState.idProcessingTranzact;
            state.numberOfStartsModel = oldState.numberOfStartsModel;
            state.rand = oldState.rand;
            state.interval = oldState.interval;

            string[] originalRulesC = new string[oldState.originalRules.Count];
            oldState.originalRules.CopyTo(originalRulesC);
            for (int i = 0; i < originalRulesC.Length; i++)
            {
                state.originalRules.Add(originalRulesC[i]);
            }

            for (int i = 0; i < oldState.queues.Count; i++)
            {
                state.queues.Add(oldState.queues.ElementAt(i).clone());
            }

            for (int i = 0; i < oldState.tranzakts.Count; i++)
            {
                state.tranzakts.Add(oldState.tranzakts.ElementAt(i).clone());
            }

            for (int i = 0; i < oldState.lables.Count; i++)
            {
                state.lables.Add(oldState.lables.ElementAt(i).clone());
            }

            for (int i = 0; i < oldState.devices.Count; i++)
            {
                state.devices.Add(oldState.devices.ElementAt(i).clone());
            }

            for (int i = 0; i < oldState.storages.Count; i++)
            {
                state.storages.Add(oldState.storages.ElementAt(i).clone());
            }

            for (int i = 0; i < oldState.variables.Count; i++)
            {
                state.variables.Add(oldState.variables.ElementAt(i).clone());
            }

            for (int i = 0; i < oldState.tranzation_generators.Count; i++)
            {
                state.tranzation_generators.Add(oldState.tranzation_generators.ElementAt(i).clone());
            }

            for (int i = 0; i < oldState.newRules.Count; i++)
            {
                state.newRules.Add(oldState.newRules.ElementAt(i).clone());
            }

            if(config.getResetAllState())
            {
                state.report = oldState.report.copyReport(oldState);
                report = oldState.report.copyReport(oldState);
            }
            else
            {
                state.numberOfStartsModel = numberOfStartsModel;
                state.report = report.copyReport(state);
            }

            notifyObservers();
        }

        public override ModelsState copySelf()
        {
            ModelingState copy = new ModelingState();
            copy.last_tranzaktions_id = state.last_tranzaktions_id;
            copy.result = state.result;
            copy.time_of_modeling = state.time_of_modeling;
            copy.idProcessingTranzact = state.idProcessingTranzact;
            copy.numberOfStartsModel = state.numberOfStartsModel;
            copy.rand = state.rand;
            copy.interval = state.interval;
            copy.report = state.report.copyReport(state);

            string[] originalRulesC = new string[state.originalRules.Count];
            state.originalRules.CopyTo(originalRulesC);
            for(int i=0;i<originalRulesC.Length;i++)
            {
                copy.originalRules.Add(originalRulesC[i]);
            }

            for (int i = 0; i < state.queues.Count; i++)
            {
                copy.queues.Add(state.queues.ElementAt(i).clone());
            }

            for (int i = 0; i < state.tranzakts.Count; i++)
            {
                copy.tranzakts.Add(state.tranzakts.ElementAt(i).clone());
            }

            for (int i = 0; i < state.lables.Count; i++)
            {
                copy.lables.Add(state.lables.ElementAt(i).clone());
            }

            for (int i = 0; i < state.devices.Count; i++)
            {
                copy.devices.Add(state.devices.ElementAt(i).clone());
            }

            for (int i = 0; i < state.storages.Count; i++)
            {
                copy.storages.Add(state.storages.ElementAt(i).clone());
            }

            for (int i = 0; i < state.variables.Count; i++)
            {
                copy.variables.Add(state.variables.ElementAt(i).clone());
            }

            for (int i = 0; i < state.tranzation_generators.Count; i++)
            {
                copy.tranzation_generators.Add(state.tranzation_generators.ElementAt(i).clone());
            }

            for (int i = 0; i < state.newRules.Count; i++)
            {
                copy.newRules.Add(state.newRules.ElementAt(i).clone());
            }

            return copy;
        }

        //основная функция моделирования
        private void run_simulation()
        {
            //начало моделировния, генерация транзактов и "перемещение" их от 
            //точки создания до точки удаления из системы
            int system_time = 0;//системное время, оно приращается на единицу, когда какой-либо 
                                //тразакт войдет в блок задержки
            //итерации заканчиваются, когда у генераторов транзактов в поле кол-ва оставшихся 
            //транзактов появляется ноль и систеимные транзакты закончились
            //выход по кол-ву удаленных транзактов в этой версии не предусмотрен
            state.last_tranzaktions_id = 1;//транзактов еще не было и по-этому номер первого 
            //транзакта равен 1
            bool modeling_complete = false;//флаг завершения моделирования
            while (modeling_complete == false)
            {
                bool all_tranzakts_was_blocked = false;


                //движение осуществляется последовательно всеми транзактами(для незаблокированных) 
                //пока все транзакты не станут заблокированными
                while (all_tranzakts_was_blocked == false)
                {
                    //"перемещение" транзактов,уже находящихся в системе
                    //проверка, возможно транзакт заблокирован и с ним пока нельзя совершать 
                    //никаких действий
                    for (int i = 0; i < state.tranzakts.Count; i++)
                    {
                        if (state.tranzakts.ElementAt(i).blocked == false)
                        {
                            //проверка, возможно транзакт прошел метку и необходимо инкрементировать
                            //ее параметр, подсчитывающий СЧА-кол-во транзактов, прошедших через 
                            //нее
                            state.idProcessingTranzact = i;
                            for (int a = 0; a < state.lables.Count(); a++)
                            {
                                //сравнил текущее место транзактов местом метки
                                if (state.lables.ElementAt(a).get_my_plase() == 
                                    state.tranzakts.ElementAt(i).my_place)
                                {
                                    state.lables.ElementAt(a).upper_entries_number();
                                    break;
                                }
                            }

                            //Обработка транзакта соответствующей Operation
                            state.newRules.ElementAt(state.tranzakts.ElementAt(i).my_place).
                                processing();
                        }
                    }

                    all_tranzakts_was_blocked = true;
                    //проверка на заблокированность транзактов, если хотя бы один транзакт 
                    //останется не блокированным, то флаг изменится
                    for (int i = 0; i < state.tranzakts.Count; i++)
                    {
                        if (state.tranzakts.ElementAt(i).blocked == false)
                        {
                            all_tranzakts_was_blocked = false;
                            break;
                        }
                    }
                }


                //добавление транзактов в систему
                for (int i = 0; i < state.tranzation_generators.Count; i++)
                {
                    //если в генераторе еще есть транзакты
                    if (state.tranzation_generators.ElementAt(i).count_tranzaktion != 0)
                    {
                        //если наступило время создания транзакта
                        if (state.tranzation_generators.ElementAt(i).
                            time_until_the_next_tranzaction == 0)
                        {
                            Tranzakt tranzact = new Tranzakt(state.tranzation_generators.
                                ElementAt(i).get_my_plase() + 1, state.last_tranzaktions_id);
                            state.tranzakts.Add(tranzact);
                            //обновление последнего номера транзакта
                            state.last_tranzaktions_id++;
                            //после создания транзакта необходимо сгененировать время до следующего
                            state.tranzation_generators.ElementAt(i).
                                time_until_the_next_tranzaction = 
                                create_assign(state.tranzation_generators.ElementAt(i).
                                get_deleys_time_left(), 
                                state.tranzation_generators.ElementAt(i).get_deleys_time_right(), 
                                state.rand);
                            state.tranzation_generators.ElementAt(i).count_tranzaktion--;
                        }
                        else
                        {
                            //иначе отнимаем единицу от времени дос ледующего транзакта
                            state.tranzation_generators.ElementAt(i).
                                time_until_the_next_tranzaction--;
                        }
                    }
                    
                }
                //инкремент системного времени
                system_time++;
                //декремент оставшегося времени задержки в транзактах
                for (int j = 0; j < state.tranzakts.Count; j++)
                {
                    //инкремент времени нахождения транзакта в системе
                    state.tranzakts.ElementAt(j).time_in_system++;
                    if (state.tranzakts.ElementAt(j).remaining_time_delay > 0)
                    {
                        state.tranzakts.ElementAt(j).remaining_time_delay--;
                        //проверка, возможно, транзакт можно снова разблокировать
                        if (state.tranzakts.ElementAt(j).remaining_time_delay == 0)
                        {
                            state.tranzakts.ElementAt(j).blocked = false;
                            state.tranzakts.ElementAt(j).my_place++;
                        }
                    }
                }

                //проверка на завершенность моделирования
                //изменится на true, в случае, если хотя бы
                //один генератор еще может генерировать транзакты
                bool generators_is_not_empty = false;                          
                for (int i = 0; i < state.tranzation_generators.Count; i++)
                {
                    if (state.tranzation_generators.ElementAt(i).count_tranzaktion > 0)
                    {
                        generators_is_not_empty = true;
                    }
                }
                //если все генераторы пустые и в системе больше нет транзактов
                if (generators_is_not_empty == false & state.tranzakts.Count == 0)
                {
                    modeling_complete = true;
                }
            }
            state.time_of_modeling = system_time;
            state.result = "Успех!";
        }

        //функция создания времени задержки, принимает разброс возможных значений
        private int create_assign(int min_number, int max_number, Random rand)
        {
            int a = rand.Next(min_number, max_number);
            return rand.Next(min_number, max_number);
        }

        public override void calculationStatistics()
        {
            run_simulation();

            state.numberOfStartsModel++;
            numberOfStartsModel = state.numberOfStartsModel;
            if (state.numberOfStartsModel == 1)
            {
                state.report = new ModelingReport(state);
            }
            state.report.updateReport(state);
            report = state.report.copyReport(state);

            notifyObservers();
        }

        public override void loadStore()
        {
            //чтение файла с конфигурацией модели
            /*TextFilesDataLoader loader = new TextFilesDataLoader();
            TextFilesConfigFieldsOnLoad loadersConfig =
                new TextFilesConfigFieldsOnLoad(config.getConfigData());
            loader.setConfig(loadersConfig);
            loader.execute();
            state.originalRules = loader.getResult();*/
            //Создание модели в реалтайме
            DataWorker<ModelsCreatorConfigState, List<string>> loader = 
                new ModelsCreatorProxy();
            ModelsCreatorConfigState creatorsConfig = new ModelsCreatorConfigState();
            //Сбор необходимых данных
            DataSet unicNamesDS = configProxyForLoadDataFromNewBDAndExecute(
                MsSqlServersQueryConfigurator.getUnicLicensesName());
            DataConverter<DataSet, string[]> unicNamesConverter = 
                new DistinctSoftwareNamesConverter();
            string[] unicNames = unicNamesConverter.convert(unicNamesDS);
            StateForConverterOfModelCreatorConfig stateForConverter = 
                new StateForConverterOfModelCreatorConfig();

            stateForConverter.unicNames = unicNames;

            stateForConverter.numberBuyLicenses = configProxyForLoadDataFromNewBDAndExecute(
                MsSqlServersQueryConfigurator.getNumberOfPurchasedLicenses());

            for (int i=0;i<unicNames.Length;i++)
            {
                stateForConverter.bufOftimeBetweenQueryToGetLicenses.Add(
                    configProxyForLoadDataFromNewBDAndExecute(MsSqlServersQueryConfigurator.getTimesGiveLicense(
                    unicNames[i], config.getInterval())));
                stateForConverter.bufOfTimesOfInBetweenOutLicenses.Add(
                    configProxyForLoadDataFromNewBDAndExecute(MsSqlServersQueryConfigurator.getInBetweenOutLicenses(
                    unicNames[i], config.getInterval())));
                stateForConverter.numberOfGetingLicensesPerTime.Add(
                    configProxyForLoadDataFromNewBDAndExecute(MsSqlServersQueryConfigurator.getNumberOfLicenesForTime(
                        unicNames[i], config.getInterval())));
                stateForConverter.avgLicensePerTime.Add(
                    configProxyForLoadDataFromNewBDAndExecute(MsSqlServersQueryConfigurator.getAvgLicesensePerTime(
                        unicNames[i], config.getInterval())));
        }

            //Перевод типа DataSet к нужному формату
            DataConverter<StateForConverterOfModelCreatorConfig,
                ReturnStateForConverterOfModelCreatorConfig> convertData = 
                new ModelCreatorConfigCreator();
            ReturnStateForConverterOfModelCreatorConfig licencesInfo = 
                convertData.convert(stateForConverter);
            //Создание конфига
            creatorsConfig.licenceInfo = new List<LicenceInfo>();
            for (int i=0; i<unicNames.Length; i++)
            {
                if(licencesInfo.bufOfTimesOfInBetweenOutLicenses.ElementAt(i).
                    characteristic.Count() > 1 & 
                    licencesInfo.bufOftimeBetweenQueryToGetLicenses.ElementAt(i).
                    characteristic.Count() > 1)
                {
                    int numberBuyLicense = licencesInfo.numberBuyLicenses[i];
                    int avgDelayTimeInTheProcessing = Convert.ToInt32(MathWorker.avg(
                        licencesInfo.bufOfTimesOfInBetweenOutLicenses.ElementAt(i).characteristic));
                    int avgSquereDelayTimeInTheProcessing = Convert.ToInt32(MathWorker.
                        standardDeviation(licencesInfo.bufOfTimesOfInBetweenOutLicenses.
                        ElementAt(i).characteristic));
                    if(avgSquereDelayTimeInTheProcessing> avgDelayTimeInTheProcessing)
                    {
                        avgDelayTimeInTheProcessing = (avgSquereDelayTimeInTheProcessing + 
                            avgDelayTimeInTheProcessing) / 2;
                        avgSquereDelayTimeInTheProcessing = avgDelayTimeInTheProcessing;
                    }
                    int avgRequestedTime = Convert.ToInt32(MathWorker.avg(
                        licencesInfo.bufOftimeBetweenQueryToGetLicenses.ElementAt(i).characteristic));
                    int avgSquereRequestedTime = Convert.ToInt32(MathWorker.
                        standardDeviation(licencesInfo.bufOftimeBetweenQueryToGetLicenses.
                        ElementAt(i).characteristic));
                    if(avgSquereRequestedTime > avgRequestedTime)
                    {
                        avgRequestedTime = (avgSquereRequestedTime + avgRequestedTime) / 2;
                        avgSquereRequestedTime = avgRequestedTime;
                    }
                    creatorsConfig.licenceInfo.Add(new LicenceInfo(unicNames[i], numberBuyLicense,
                        avgDelayTimeInTheProcessing, avgSquereDelayTimeInTheProcessing, 
                        licencesInfo.avgLicensePerTime[i], avgRequestedTime, 
                        avgSquereRequestedTime));
                }  
            }
            //Вычисление матрицы корреляций
            int size = creatorsConfig.licenceInfo.Count;
            creatorsConfig.korellation = new double[size, size];
            for (int i=0; i<size; i++)
            {
                for (int m=i; m<size; m++)
                {
                    if (licencesInfo.numberOfGetingLicensesPerTime.ElementAt(i).
                    characteristic.Count() != 0 &
                    licencesInfo.numberOfGetingLicensesPerTime.ElementAt(m).
                    characteristic.Count() != 0)
                    {
                        //Перед вычислением корелляции необходимо выяснить, не состоят ли
                        //проверяемые массивы из одинаковых элементов
                        bool countCorell = true;
                        if(MathWorker.standardDeviation(licencesInfo.
                            numberOfGetingLicensesPerTime.ElementAt(i).characteristic) == 0 ||
                            MathWorker.standardDeviation(licencesInfo.
                            numberOfGetingLicensesPerTime.ElementAt(m).characteristic) == 0)
                        {
                            countCorell = false;
                        }
                        if (countCorell)
                        {
                            double currentCorellation = MathWorker.corellation(
                            licencesInfo.numberOfGetingLicensesPerTime.ElementAt(i).characteristic,
                            licencesInfo.numberOfGetingLicensesPerTime.ElementAt(m).characteristic);
                            creatorsConfig.korellation[i, m] = currentCorellation;
                            creatorsConfig.korellation[m, i] = currentCorellation;
                        }
                        else
                        {
                            creatorsConfig.korellation[i, m] = 0;
                            creatorsConfig.korellation[m, i] = 0;
                        }
                    }
                }
            }
            creatorsConfig.withKovar = true;
            loader.setConfig(creatorsConfig);
            loader.execute();
            state.originalRules = loader.getResult();

            //создание всех очередей, устройств, меток и тд
            RulesParser rules_parser = new RulesParser();
            rules_parser.go_parse(this);
        }

        //Получение исходных данных для дальнейшей конфигурации конфигуратора 
        //модели для моделирования
        public DataSet configProxyForLoadDataFromNewBDAndExecute(string query)
        {
            DataWorker<MsSQLServerStateFields, DataSet> accessProxy = new MsSQLServerProxy();
            List<string> list = new List<string>();
            list.Add(query);
            MsSQLServerStateFields configProxy =
                new MsSQLServerStateFields(list);
            accessProxy.setConfig(configProxy);
            accessProxy.execute();
            list.Clear();
            return accessProxy.getResult();
        }

        public override void setConfig(ModelingConfig configData)
        {
            config = configData;
            if (configData.getResetAllState())
            {
                state = new ModelingState();
                state.report = new ModelingReport(state);
            }
            report = state.report.copyReport(state);
            state.interval = config.getInterval();
            notifyObservers();
        }

        public override ModelingReport getResult()
        {
            DataConverter<ModelingState, ModelingReport> converter =
                new ResultConverter();
            return converter.convert(state);
        }
    }
}
/*
 * Модуль управления. Представляет собой реализаию модели из MVC
 */
