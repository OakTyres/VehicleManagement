using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace VehicleManagement.Models
{
    public class AddVehicleModel
    {

        public int Id { get; set; }
        public int Depot { get; set; }
        [Display(Name = "Vehicle Reg")]
        [RegularExpression(@"(?<Current>^[A-Z]{2}[0-9]{2}[A-Z]{3}$)|(?<Prefix>^[A-Z][0-9]{1,3}[A-Z]{3}$)|(?<Suffix>^[A-Z]{3}[0-9]{1,3}[A-Z]$)|(?<DatelessLongNumberPrefix>^[0-9]{1,4}[A-Z]{1,2}$)|(?<DatelessShortNumberPrefix>^[0-9]{1,3}[A-Z]{1,3}$)|(?<DatelessLongNumberSuffix>^[A-Z]{1,2}[0-9]{1,4}$)|(?<DatelessShortNumberSufix>^[A-Z]{1,3}[0-9]{1,3}$)", ErrorMessage = "You must specify a valid vehicle reg!")]
//|(^[A-Z][0-9]{1,3} [A-Z]{3}$)|(^[A-Z]{3} [0-9]{1,3}[A-Z]$)|(^[0-9]{1,4} [A-Z]{1,2}$)|(^[0-9]{1,3} [A-Z]{1,3}$)|(^[A-Z]{1,2} [0-9]{1,4}$)|(^[A-Z]{1,3} [0-9]{1,3}$)")]
        public string VehicleRegistration { get; set; }
        [Required(ErrorMessage = "Vehicle make must'nt be blank")]
        [Display(Name = "Vehicle Make")]
        public string Make { get; set; }
        [Required(ErrorMessage = "Vehicle model must'nt be blank")]
        [Display(Name = "Vehicle Model")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Please provide the warranty mileage")]
        [Display(Name = "Warranty Mileage")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a mileage greater than 0")]
        public int WarrantyMileage { get; set; }
        [Required(ErrorMessage = "Please give a starting date for this vehicle")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please give an end date for this vehicle")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Mileage Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime MileageDate { get; set; }
        [DefaultValue(true)]
        [Display(Name = "Current Mileage")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a mileage greater than 0")]
        public int CurrentMileage { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Tax Due")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TaxDue { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "MOT Due")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime MotDue { get; set; }
        [Display(Name = "In Warranty?")]
        public string InWarranty { get; set; }
        [Display(Name = "Service Interval")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a interval greater than 0")]
        public int ServiceInterval { get; set; }
        [Display(Name = "Driver")]
        public string Driver { get; set; }
        [Display(Name = "Livery")]
        public string Livery { get; set; }
        [Display(Name = "Tyre Size")]
        public string TyreSize { get; set; }
        [Display(Name = "Camera")]
        public string Camera { get; set; }
        [Display(Name = "Masternaught?")]
        public string Masternaught { get; set; }
        public int PayLoadCapacity { get; set; }
    }
}
