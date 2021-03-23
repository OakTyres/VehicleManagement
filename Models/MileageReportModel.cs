using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class MileageReportModel
    {
        public string VehicleRegistration { get; set; }
        public int Mileage { get; set; }
        public DateTime CheckDate { get; set; }
        public string Depot { get; set; }
    }
}
