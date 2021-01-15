using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class UpcomingEventsModel
    {
        public int Depot { get; set; }
        public string DepotName { get; set; }
        public string VanRegistration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Due { get; set; }
        public string DueText { get; set; }
    }
}
