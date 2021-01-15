using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class QRCodeModel
    {
        public byte[] QR { get; set; }
        public string VehicleRegistration { get; set; }
    }
}
