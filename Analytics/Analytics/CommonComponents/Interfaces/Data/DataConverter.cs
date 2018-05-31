using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics
{
    interface DataConverter<T>
    {
        object convert(T data);
    }
}