using Analytics.CommonComponents.ExceptionHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CommonComponents.ExceptionHandler.View.Question.PopupWindow
{
    class QuestionPopupWindow : ExceptionViewInterface<QuestionPopupWindowConfig>
    {
        private QuestionPopupWindowConfig config;

        public void setConfig(QuestionPopupWindowConfig config)
        {
            this.config = config;
        }

        public void show()
        {
            DialogResult result = MessageBox.Show(
                config.getMessage(),
                "Вопрос",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                config.getObserver().saidYes();
            }
            else
            {
                config.getObserver().saidNo();
            }
        }
    }
}