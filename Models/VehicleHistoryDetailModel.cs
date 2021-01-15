using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class VehicleHistoryDetailModel
    {
        public string Depot { get; set; }
        public string VehicleRegistration { get; set; }
        public string Reason { get; set; }
        public string ActionDate { get; set; }
        public string AdditionalComments { get; set; }
        public string ActionUser { get; set; }
        public string FleetOrRent { get; set; }
    }
}
