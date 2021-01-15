using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class EditVehicle
    {
        public int Id { get; set; }
        public int Depot { get; set; }
        [Display(Name = "Vehicle Reg")]
        [RegularExpression(@"(?<Current>^[A-Z]{2}[0-9]{2}[A-Z]{3}$)|(?<Prefix>^[A-Z][0-9]{1,3}[A-Z]{3}$)|(?<Suffix>^[A-Z]{3}[0-9]{1,3}[A-Z]$)|(?<DatelessLongNumberPrefix>^[0-9]{1,4}[A-Z]{1,2}$)|(?<DatelessShortNumberPrefix>^[0-9]{1,3}[A-Z]{1,3}$)|(?<DatelessLongNumberSuffix>^[A-Z]{1,2}[0-9]{1,4}$)|(?<DatelessShortNumberSufix>^[A-Z]{1,3}[0-9]{1,3}$)", ErrorMessage = "You must specify a valid vehicle reg!")]
        //|(^[A-Z][0-9]{1,3} [A-Z]{3}$)|(^[A-Z]{3} [0-9]{1,3}[A-Z]$)|(^[0-9]{1,4} [A-Z]{1,2}$)|(^[0-9]{1,3} [A-Z]{1,3}$)|(^[A-Z]{1,2} [0-9]{1,4}$)|(^[A-Z]{1,3} [0-9]{1,3}$)")]
        public string VanRegistration { get; set; }
        [Display(Name = "Vehicle Make")]
        public string Make { get; set; }
        [Display(Name = "Vehicle Model")]
        public string Model { get; set; }
        [Display(Name = "Warranty Mileage")]
        public int WarrantyMileage { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Mileage Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime MileageDate { get; set; }
        [Display(Name = "Current Mileage")]
        public int CurrentMileage { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Tax Due")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TaxDue { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "MOT Due")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime MotDue { get; set; }
        [Display(Name = "In Warranty?")]
        public bool InWarranty { get; set; }
        [Display(Name = "Service Interval")]
        public int ServiceInterval { get; set; }
        [Display(Name = "Driver")]
        public string Driver { get; set; }
        [Display(Name = "Livery")]
        public string Livery { get; set; }
        [Display(Name = "Tyre Size")]
        public string TyreSize { get; set; }
        [Display(Name = "Camera")]
        public bool Camera { get; set; }
        [Display(Name = "Masternaught?")]
        public string Masternaught { get; set; }
        public int PayLoadCapacity { get; set; }
    }
}
