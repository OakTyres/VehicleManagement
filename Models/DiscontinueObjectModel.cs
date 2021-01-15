using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class DiscontinueObjectModel
    {
        public string Reg { get; set; }
        public string Comments { get; set; }
        public string Date { get; set; }
        public int Reason { get; set; }
        public string User { get; set; }
    }
}
