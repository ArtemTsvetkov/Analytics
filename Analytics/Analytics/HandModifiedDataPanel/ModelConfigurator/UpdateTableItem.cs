using Analytics.CommonComponents.ExceptionHandler;
using Analytics.CommonComponents.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.HandModifiedDataPanel.ModelConfigurator
{
    class UpdateTableItem : ModelConfiguratorInterface<HandModifiedDataState>
    {
        string newValue;
        int row;
        bool isTableWithLicensesCount;

        public UpdateTableItem(string newValue, int row, bool isTableWithLicensesCount)
        {
            this.newValue = newValue;
            this.row = row;
            this.isTableWithLicensesCount = isTableWithLicensesCount;
        }

        public HandModifiedDataState configureMe(HandModifiedDataState oldState)
        {
            string text = checkStringOnIsMustBeDouble(newValue);
            if (text != null)
            {
                if (isTableWithLicensesCount)
                {
                    oldState.numberOfPurcharedLicenses[row] = double.Parse(text);
                }
                else
                {
                    oldState.percents[row] = double.Parse(text);
                }
            }
            return oldState;
        }

        private string checkStringOnIsMustBeDouble(string text)
        {
            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i].Equals('.'))
                    {
                        text = text.Remove(i, 1);
                        text = text.Insert(i, ",");
                    }
                }

                double value = double.Parse(text);
                return text;
            }
            catch (Exception ex)
            {
                ExceptionHandler.getInstance().processing(
                    new ValueMastBeANumberException());
                return null;
            }
        }
    }
}
