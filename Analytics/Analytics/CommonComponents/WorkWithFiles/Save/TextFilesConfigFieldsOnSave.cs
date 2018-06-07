using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.CommonComponents.WorkWithFiles
{
    class TextFilesConfigFieldsOnSave
    {
        private string filePath;
        private int action;
        List<string> data;

        public TextFilesConfigFieldsOnSave(List<string> data, string filePath, int action)
        {
            this.data = data;
            this.action = action;
            this.filePath = filePath;
        }

        public string getFilePath()
        {
            return filePath;
        }

        public int getAction()
        {
            return action;
        }

        public List<string> getData()
        {
            return data;
        }
    }
}
