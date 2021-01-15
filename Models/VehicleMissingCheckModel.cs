using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class VehicleMissingCheckModel
    {
        public int Depot { get; set; }
        public string VanRegistration { get; set; }
        public string UsualDriver { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
