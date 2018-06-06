﻿using Analytics.CommonComponents.BasicObjects;
using Analytics.MarcovitsComponent.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.Views
{
    class MarcovitsView : Observer
    {
        private Form1 form;
        private BasicModel<MarcovitsModelState, MarcovitsModelState> model;

        public MarcovitsView(Form1 form)
        {
            this.form = form;
        }

        public void button2_Click()
        {
            CommandsStore<MarcovitsModelState, MarcovitsModelState> commandsStore =
                new ConcreteCommandStore<MarcovitsModelState, MarcovitsModelState>();
            ResultConverter resultConverter = new ResultConverter();
            model = new MarcovitsModel("D:\\Files\\MsVisualProjects\\Diplom\\Логи\\testlogs\\Database3.accdb", "Information");
            model.setResultConverter(resultConverter);
            model.subscribe(this);
            commandsStore.executeCommand(new GetMarcovitsStatistcCommand(model));
            //model.calculationStatistics();
        }

        public void notify()
        {
            MarcovitsModelState state = model.getResult();
            //Вывод данных о доходности
            form.chart1Elem.Series[0].Points.Clear();
            form.chart1Elem.Series[0].Points.AddXY(0, (state.income * 100));
            form.chart1Elem.Series[0].Points.AddXY(0, 100 - (state.income * 100));
            form.chart1Elem.Series[0].Points.ElementAt(1).Color = Color.Black;
            double plus = state.income * 100;
            form.label5Elem.Visible = true;
            form.label5Elem.Text = "Доходность:" + plus.ToString();
            //Вывод данных о риске
            form.chart2Elem.Series[0].Points.Clear();
            form.chart2Elem.Series[0].Points.AddXY(0, (state.risk[0, 0] * 100));
            form.chart2Elem.Series[0].Points.AddXY(0, 100 - (state.income * 100));
            form.chart2Elem.Series[0].Points.ElementAt(0).Color = Color.Red;
            form.chart2Elem.Series[0].Points.ElementAt(1).Color = Color.Black;
            double mines = state.risk[0, 0] * 100;
            form.label6Elem.Visible = true;
            form.label6Elem.Text = "Риск:" + mines.ToString();
            //Вывод данных о распределении бюджета по лицензиям
            form.chart3Elem.Series[0].Points.Clear();
            for (int i = 0; i < state.unicSoftwareNames.Length; i++)
            {
                form.chart3Elem.Series[0].Points.AddXY(0, (state.percents[i, 0] * 100));
                form.chart3Elem.Series[0].Points.ElementAt(i).Label = state.unicSoftwareNames[i];
                form.chart3Elem.Legends.ElementAt(0).Title = "Лицензии:";
            }
            //Вывод данных о средней доходности по каждой из лицензий
            form.chart4Elem.Series[0].Points.Clear();
            for (int i = 0; i < state.unicSoftwareNames.Length; i++)
            {
                form.chart4Elem.Series[0].Points.AddXY(0, (state.avgDeviationFromPurchasedNumber[i] * 100));
                form.chart4Elem.Series[0].Points.ElementAt(i).Label = state.unicSoftwareNames[i];
            }
        }
    }
}