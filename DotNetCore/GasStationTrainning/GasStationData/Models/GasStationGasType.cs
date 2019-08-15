﻿using System;
using System.Collections.Generic;

namespace GasStationData.Models
{
    public partial class GasStationGasType
    {
        public long GasStationGasTypeId { get; set; }
        public long? GasStationId { get; set; }
        public string GasType { get; set; }

        public virtual GasStation GasStation { get; set; }
    }
}
