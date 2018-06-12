﻿using Analytics.Modeling.IntermediateStates;
using Analytics.Modeling.IntermediateStates.ModelsCreatorConfig;
using Analytics.Modeling.ModelsCreator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Modeling.Converters
{
    class ModelCreatorConfigCreator : DataConverter<StateForConverterOfModelCreatorConfig,
        ReturnStateForConverterOfModelCreatorConfig>
    {
        public ReturnStateForConverterOfModelCreatorConfig convert(
            StateForConverterOfModelCreatorConfig data)
        {
            ReturnStateForConverterOfModelCreatorConfig answer= 
                new ReturnStateForConverterOfModelCreatorConfig();
            //Так как bufOftimeBetweenQueryToGetLicenses в формате DataSet хранит
            //не время между заявками, я время самих заявок(так было проще сделать запрос)
            //То для получения времени между заявками нужно из n+1 вычитать n
            for (int i=0; i<data.bufOftimeBetweenQueryToGetLicenses.Count();i++)
            {
                DataSet ds = data.bufOftimeBetweenQueryToGetLicenses.ElementAt(i);
                MappingLicenseResult licence = 
                    new MappingLicenseResult(data.unicNames.ElementAt(i), ds.Tables[0].Rows.Count);
                for (int m = 0; m < (ds.Tables[0].Rows.Count-1); m++)
                {
                    licence.characteristic[m] = int.Parse(ds.Tables[0].Rows[m+1][0].ToString()) - 
                        int.Parse(ds.Tables[0].Rows[m][0].ToString());
                }
                answer.bufOftimeBetweenQueryToGetLicenses.Add(licence);
            }

            for (int i = 0; i < data.bufOfTimesOfInBetweenOutLicenses.Count(); i++)
            {
                DataSet ds = data.bufOfTimesOfInBetweenOutLicenses.ElementAt(i);
                MappingLicenseResult licence =
                    new MappingLicenseResult(data.unicNames.ElementAt(i), ds.Tables[0].Rows.Count);
                for (int m = 0; m < ds.Tables[0].Rows.Count; m++)
                {
                    licence.characteristic[m]=int.Parse(ds.Tables[0].Rows[m][0].ToString());
                }
                answer.bufOfTimesOfInBetweenOutLicenses.Add(licence);
            }

            DataSet numberOfBuyLicensesds = data.numberBuyLicenses;
            answer.numberBuyLicenses = new int[numberOfBuyLicensesds.Tables[0].Rows.Count];
            for (int m = 0; m < numberOfBuyLicensesds.Tables[0].Rows.Count; m++)
            {
                answer.numberBuyLicenses[m] = 
                    int.Parse(numberOfBuyLicensesds.Tables[0].Rows[m][1].ToString());
            }

            return answer;
        }
    }
}
