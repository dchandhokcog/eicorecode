using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Manager
{
    public class ImportComponentsEventArgs
    {

        public List<DataTable> DataTables { get; set; }
        public int? PlantId { get; set; }
        public int? ProcessUnitId { get; set; }

    }
}
