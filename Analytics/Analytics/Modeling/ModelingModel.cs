using Analytics;
using Analytics.CommonComponents.BasicObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class ModelingModel : BasicModel<ModelingState, ModelingState>
    {
        public void setConfiguration(string path_rules_file)
        {
            //чтение файла с конфигурацией модели
            state = new ModelingState();
            //ReadWriteTextFile RWTF = new ReadWriteTextFile();
            state.originalRules = ReadWriteTextFile.Read_from_file(path_rules_file);
            //создание всех очередей, устройств, меток и тд
            RulesParser rules_parser = new RulesParser();
            rules_parser.go_parse(this);
        }

        public ModelingState getState()
        {
            return state;
        }

        //основная функция моделирования, возвращает результат
        //моделирования(ошибка или успех)
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
            notifyObservers();
        }

        public override void loadStore()
        {
            throw new NotImplementedException();
        }
    }
}
/*
 * Модуль управления. Представляет собой реализаию модели из MVC
 */
