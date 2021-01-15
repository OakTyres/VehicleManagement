using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class VehicleMaintenanceMeticsModel
    {
        public int TaxDue { get; set; }
        public int MOTDue { get; set; }
        public int MOTOverdue { get; set; }
        public int TaxOverdue { get; set; }
        public int OutOfWarranty { get; set; }
        public int ServicesDue { get; set; }
        public int ServicesOverdue { get; set; }
        public int ActiveVehicles { get; set; }
        public int InactiveVehicles { get; set; }
        public int SoldThisMonth { get; set; }
        public int SornedThisMonth { get; set; }
        public int WriteOffThisMonth { get; set; }
        public int OutForRepair { get; set; }
        public int TotalOnHire { get; set; }
        public int HaydockChecks { get; set; }
        public int LeedsChecks { get; set; }
        public int TraffordChecks { get; set; }
        public int TyneChecks { get; set; }
        public int HaydockNotChecked { get; set; }
        public int LeedsNotChecked { get; set; }
        public int TraffordNotChecked { get; set; }
        public int TyneNotChecked { get; set; }
        public int AllChecks { get; set; }
        public int AllNoneChecks { get; set; }
        public double PercentChecked { get; set; }
        public double PercentNotChecked { get; set; }
    }
}
