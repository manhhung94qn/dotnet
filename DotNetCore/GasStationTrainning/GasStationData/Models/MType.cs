using System;
using System.Collections.Generic;

namespace GasStationData.Models
{
    public partial class MType
    {
        public long TypeId { get; set; }
        public string TypeCode { get; set; }
        public string TypeText { get; set; }
        public int TypeType { get; set; }
    }
}
