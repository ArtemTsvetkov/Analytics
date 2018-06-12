using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsModelState : ModelsState
    {
        //Хранит уникальные названия software
        public string[] unicSoftwareNames;
        public double[] avgNumbersUseLicense;//среднее кол-во использований лицензий
        public double[] avgDeviationFromPurchasedNumber;//средняя доходность по каждой из лицензий
        //Хранит данные о использовании software за определенный период
        public List<MarcovitsDataTable> data = new List<MarcovitsDataTable>();
        //Кол-во закупленных лицензий
        public double[] numberBuyLicense;
        //процентное соотношение в общей закупке всех лицензий, двойной массив для унификации 
        //функции перемножения
        public double[,] percents;
        //Рассчитанная доходность
        public double income;
        //Рассчитанный риск, двойной массив для унификации функции перемножения
        public double[,] risk;
    }
}