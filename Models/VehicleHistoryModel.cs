using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class VehicleHistoryModel
    {
        public string VehicleRegistration { get; set; }
        public string ActionReason { get; set; }
        public string ActionDate { get; set; }
        public string AdditionalComments { get; set; }
        public string FilePath { get; set; }
    }
}
