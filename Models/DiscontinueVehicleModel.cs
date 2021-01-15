using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class DiscontinueVehicleModel
    {
        public int Id { get; set; }
        public int Depot { get; set; }
        public string DepotName { get; set; }
        public string VanRegistration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int WarrantyMileage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime MileageDate { get; set; }
        public int CurrentMileage { get; set; }
        public DateTime TaxDue { get; set; }
        public DateTime MotDue { get; set; }
        public string InWarranty { get; set; }
        public int ServiceInterval { get; set; }
        public string Driver { get; set; }
        public string Livery { get; set; }
        public string TyreSize { get; set; }
        public bool Camera { get; set; }
        public string Masternaught { get; set; }
    }
}
