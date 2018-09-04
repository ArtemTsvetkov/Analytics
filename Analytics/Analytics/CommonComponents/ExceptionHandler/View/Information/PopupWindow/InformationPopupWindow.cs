using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using Analytics.CommonComponents.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.ExceptionHandler.View.Information.PopupWindow
{
    class InformationPopupWindow : ExceptionViewInterface<InformationPopupWindowConfig>
    {
        private InformationPopupWindowConfig config;

        public void setConfig(InformationPopupWindowConfig config)
        {
            this.config = config;
        }

        public void show()
        {
            try
            {
                if(config == null)
                {
                    throw new NoConfigurationSpecified("No configuration specified");
                }
                MessageBox.Show(
                    config.getMessage(),
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
            catch(Exception ex)
            {
                ExceptionHandler.getInstance().processing(ex);
            }
        }
    }
}
