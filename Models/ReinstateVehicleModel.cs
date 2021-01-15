using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class ReinstateVehicleModel
    {
        public int Depot { get; set; }
        public string VanRegistration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int CurrentMileage { get; set; }
        public int WarrantyMileage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime MileageLastChecked { get; set; }
        public string LastAction { get; set; }
        public DateTime LastActionDate { get; set; }
    }
}
