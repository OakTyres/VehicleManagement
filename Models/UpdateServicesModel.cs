using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class UpdateServicesModel
    {
        public string VanReg { get; set; }
        public int ReasonCode { get; set; }
        public IFormFile Doc { get; set; }
    }
}
