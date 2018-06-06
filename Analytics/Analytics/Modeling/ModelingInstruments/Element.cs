using Analytics.Modeling.ModelingInstruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    class Element : ModelingInstrument<Element>
    {
        public int x_angle;
        public int y_angle;
        public int with;
        public int height;
        public string type;

        public Element(int x_angle, int y_angle, int with, int height, string type)
        {
            this.x_angle = x_angle;
            this.y_angle = y_angle;
            this.with = with;
            this.height = height;
            this.type = type;
        }

        public string getType()
        {
            return "Element";
        }

        public Element clone()
        {
            return new Element(x_angle, y_angle, with, height, type);
        }
    }
}
/*
 * Класс шестерни, нужен для визуализирования моделирования
 */