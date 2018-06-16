using Analytics.Modeling.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling
{
    class ModelingReport
    {
        //Максимальное количество транзактов в очереди
        List<ElementsNameWithElementsValue> maxTranzactsInQueue;
        //Среднее количество транзактов в очереди
        List<ElementsNameWithElementsValue> avgTranzactsInQueue;
        //Количество переходов по метке
        List<ElementsNameWithElementsValue> numberRunTranzactsOnLable;
        //Значение переменных
        List<ElementsNameWithElementsValue> variablesValue;

        //В конструкторе только создается нужная структура, с именами элементов, но во всех
        //значения стоит 0
        public ModelingReport(ModelingState state)
        {
            reset(state);
        }

        public List<ElementsNameWithElementsValue> getMaxTranzactsInQueue()
        {
            return maxTranzactsInQueue;
        }

        public List<ElementsNameWithElementsValue> getAvgTranzactsInQueue()
        {
            return avgTranzactsInQueue;
        }

        public List<ElementsNameWithElementsValue> getNumberRunTranzactsOnLable()
        {
            return numberRunTranzactsOnLable;
        }

        public List<ElementsNameWithElementsValue> getVariablesValue()
        {
            return variablesValue;
        }

        //Сброс отчета
        public void reset(ModelingState state)
        {
            maxTranzactsInQueue = new List<ElementsNameWithElementsValue>();
            avgTranzactsInQueue = new List<ElementsNameWithElementsValue>();
            numberRunTranzactsOnLable = new List<ElementsNameWithElementsValue>();
            variablesValue = new List<ElementsNameWithElementsValue>();

            if (state.report != null && state.report.maxTranzactsInQueue.Count != 0
                & state.report.avgTranzactsInQueue.Count != 0
                & state.report.numberRunTranzactsOnLable.Count != 0
                & state.report.numberRunTranzactsOnLable.Count != 0)
            {
                for (int i = 0; i < state.report.getMaxTranzactsInQueue().Count(); i++)
                {
                    maxTranzactsInQueue.Add(
                        new ElementsNameWithElementsValue(
                            state.report.getMaxTranzactsInQueue().ElementAt(i).name, 0));
                }

                for (int i = 0; i < state.report.getAvgTranzactsInQueue().Count(); i++)
                {
                    avgTranzactsInQueue.Add(
                        new ElementsNameWithElementsValue(
                            state.report.getAvgTranzactsInQueue().ElementAt(i).name, 0));
                }

                for (int i = 0; i < state.report.numberRunTranzactsOnLable.Count(); i++)
                {
                    numberRunTranzactsOnLable.Add(
                        new ElementsNameWithElementsValue(
                            state.report.numberRunTranzactsOnLable.ElementAt(i).name, 0));
                }

                for (int i = 0; i < state.report.getVariablesValue().Count(); i++)
                {
                    variablesValue.Add(
                        new ElementsNameWithElementsValue(
                            state.report.getVariablesValue().ElementAt(i).name, 0));
                }
            }
            else
            {
                for (int i = 0; i < state.queues.Count(); i++)
                {
                    maxTranzactsInQueue.Add(
                        new ElementsNameWithElementsValue(state.queues.ElementAt(i).get_name(), 0));
                }

                for (int i = 0; i < state.queues.Count(); i++)
                {
                    avgTranzactsInQueue.Add(
                        new ElementsNameWithElementsValue(state.queues.ElementAt(i).get_name(), 0));
                }

                for (int i = 0; i < state.lables.Count(); i++)
                {
                    numberRunTranzactsOnLable.Add(
                        new ElementsNameWithElementsValue(state.lables.ElementAt(i).get_name(), 0));
                }

                for (int i = 0; i < state.variables.Count(); i++)
                {
                    variablesValue.Add(
                        new ElementsNameWithElementsValue(state.variables.ElementAt(i).get_name(), 0));
                }
            }
        }

        public ModelingReport copyReport(ModelingState state)
        {
            ModelingReport copy = new ModelingReport(state);
            
            for(int i=0; i<maxTranzactsInQueue.Count(); i++)
            {
                copy.maxTranzactsInQueue.RemoveAt(i);
                copy.maxTranzactsInQueue.Insert(i, maxTranzactsInQueue.ElementAt(i).copy());
            }

            for (int i = 0; i < avgTranzactsInQueue.Count(); i++)
            {
                copy.avgTranzactsInQueue.RemoveAt(i);
                copy.avgTranzactsInQueue.Insert(i, avgTranzactsInQueue.ElementAt(i).copy());
            }

            for (int i = 0; i < numberRunTranzactsOnLable.Count(); i++)
            {
                copy.numberRunTranzactsOnLable.RemoveAt(i);
                copy.numberRunTranzactsOnLable.Insert(i, numberRunTranzactsOnLable.ElementAt(i).copy());
            }

            for (int i = 0; i < variablesValue.Count(); i++)
            {
                copy.variablesValue.RemoveAt(i);
                copy.variablesValue.Insert(i, variablesValue.ElementAt(i).copy());
            }

            return copy;
        }

        public void updateReport(ModelingState state)
        {
            for (int i = 0; i < state.queues.Count(); i++)
            {
                maxTranzactsInQueue.ElementAt(i).value +=
                    state.queues.ElementAt(i).get_max_tranzacts_in_queue();
            }

            for (int i = 0; i < state.queues.Count(); i++)
            {
                avgTranzactsInQueue.ElementAt(i).value +=
                    state.queues.ElementAt(i).get_sum_tranzacts_in_queue()/
                    (double)state.time_of_modeling;
            }

            for (int i = 0; i < state.lables.Count(); i++)
            {
                numberRunTranzactsOnLable.ElementAt(i).value +=
                    state.lables.ElementAt(i).get_entries_number();
            }

            for (int i = 0; i < state.variables.Count(); i++)
            {
                if(!state.variables.ElementAt(i).value.Equals(""))
                {
                    variablesValue.ElementAt(i).value +=
                        double.Parse(state.variables.ElementAt(i).value);
                }
            }
        }
    }
}
/*
 * Отчет по моделированию, хранит в себе реультаты одного или 
 * нескольких запусков моделирования
 */