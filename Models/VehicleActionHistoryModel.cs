using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class VehicleActionHistoryModel
    {
        public string DepotName { get; set; }
        public string VanRegistration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string FleetOrRent { get; set; }
        public string VehicleStatus { get; set; }
    }
}
