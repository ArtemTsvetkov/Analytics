using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class MarcovitsModelState : ModelsState
    {
        public string pathOfDataBase = "";
        public string tableOfDataBase = "";
        //Хранит уникальные названия software
        public string[] unicSoftwareNames;
        public double[] avgNumbersUseLicense;//среднее кол-во использований лицензий
        //Хранит данные о использовании software за определенный период
        public List<MarcovitsDataTable> data = new List<MarcovitsDataTable>();



        public double[] numberBuyLicense;//Кол-во закупленных лицензий
        public double[,] percents;//процентное соотношение в общей закупке всех лицензий, двойной массив для унификации функции перемножения
        public double yield;//Рассчитанная доходность
        public double[,] risk;//Рассчитанный риск, двойной массив для унификации функции перемножения
    }
}