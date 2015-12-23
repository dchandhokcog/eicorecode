using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Common.Containers
{
    public class ExtendedResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }

    public class ExtendedResult : ExtendedResult<object>
    {

    }
}
