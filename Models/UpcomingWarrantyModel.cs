using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class UpcomingWarrantyModel
    {
        public int Depot { get; set; }
        public string DepotName { get; set; }
        public string VanRegistration { get; set; }
        public DateTime EndDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int CurrentMileage { get; set; }
        public string Discontinued { get; set; }
    }
}
