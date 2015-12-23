using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{

    /// <summary>
    /// The CSVable class allows strong-typed objects to be lumped together for the sake of exporting to CSV.
    /// </summary>
    public class CSVable
    {
        public virtual string GetHeader()
        {
            return "";
        }

    }
}
