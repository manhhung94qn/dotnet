using GasStationData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GasStationWeb.ModelsViews
{
    public class GasStaionMV
    {
        public long GasStationId { get; set; }
        public string GasStationName { get; set; }
        public string Address { get; set; }
        public MDistrict District { get; set; }
        public string OpeningTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Rating Rating { get; set; }
        public DateTime InsertedAt { get; set; }
        public long InsertedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public long? DeletedBy { get; set; }
        public List<Type> Types { get; set; }

    }

    public class Type
    {
        public long GasStationGasTypeId { get; set; }
        public string GasTypeCode { get; set; }
        public string GasTypeName { get; set; }
    }
    public class Rating
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
