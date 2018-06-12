using Analytics.Modeling;
using Analytics.Modeling.ModelingInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class ModelingState : ModelsState
    {
        public int last_tranzaktions_id;//хранение последнего id транзакта
        public List<Queue> queues = new List<Queue>();
        //здесь могут писаться ошибка(например, синтаксическая в какой-либо функции) или 
        //сообщение об успешном моделировании
        public string result;
        public List<Tranzakt> tranzakts = new List<Tranzakt>();
        public List<Lable> lables = new List<Lable>();
        public List<Device> devices = new List<Device>();
        public List<Storage> storages = new List<Storage>();
        public List<Variable> variables = new List<Variable>();
        public List<TranzactionsGenerator> tranzation_generators = 
            new List<TranzactionsGenerator>();
        public List<string> originalRules = new List<string>();//исходные правила модели
        public int time_of_modeling;//итоговое время моделирования(нереальное)
        //Правила модели, по ним двиагаются транзакты
        public List<Operation> newRules = new List<Operation>();
        public int idProcessingTranzact;//Индекс текущего обрабатываемого транзакта
        //Он должен создаваться один раз, иначе распределение не то получится
        public Random rand = new Random();
        //Количество прогонов модели, объявлено здесь, только чтобы в 
        //конвертор результата пересылать
        public int numberOfStartsModel;
        //Текущий отчет по моделированию, объявлено здесь, только чтобы в 
        //конвертор результата пересылать
        public ModelingReport report;
    }
}
/*
 * Класс для хранения всех объектов моделирования(очередей, меток, устройств и тд)
 */
