using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models.hireModels
{
    public class AddHireVehicleModel
    {
        public int Id { get; set; }
        [Display(Name = "Enter a PO Number")]
        public string OakPONumber { get; set; }
        [Display(Name = "Who is the vehicle for?")]
        [Required(ErrorMessage = "State who the vehicle is for")]
        public string HiredFor { get; set; }
        [Display(Name = "Enter the vehicles registration")]
        [Required(ErrorMessage = "Please enter a regisatration for this vehicle")]
        [RegularExpression(@"(?<Current>^[A-Z]{2}[0-9]{2}[A-Z]{3}$)|(?<Prefix>^[A-Z][0-9]{1,3}[A-Z]{3}$)|(?<Suffix>^[A-Z]{3}[0-9]{1,3}[A-Z]$)|(?<DatelessLongNumberPrefix>^[0-9]{1,4}[A-Z]{1,2}$)|(?<DatelessShortNumberPrefix>^[0-9]{1,3}[A-Z]{1,3}$)|(?<DatelessLongNumberSuffix>^[A-Z]{1,2}[0-9]{1,4}$)|(?<DatelessShortNumberSufix>^[A-Z]{1,3}[0-9]{1,3}$)", ErrorMessage = "You must specify a valid vehicle reg!")]
        public string VehicleRegistration { get; set; }
        [Display(Name = "Vehicle Make")]
        public string Make { get; set; }
        [Display(Name = "Vehicle Model")]
        public string Model { get; set; }
        [Display(Name = "Hire Company")]
        public string HireCompany { get; set; }
        [Display(Name = "Hire Provider")]
        public string HireProvider { get; set; }
        [Display(Name = "Date hired from")]
        public DateTime HiredFrom { get; set; }
        [Display(Name = "Date hired to")]
        public DateTime HiredTo { get; set; }
        [Display(Name = "Provide a reason for hiring this vehicle")]
        public string ReasonForHire { get; set; }
        [Display(Name = "Date added to MID")]
        public DateTime DateAddedToMid { get; set; }
        [Display(Name = "Date removed from MID")]
        public DateTime DateRemovedFromMid { get; set; }
        [Display(Name = "Vehicle payload capacity (vans and artics only)")]
        public int PayLoadCapacity { get; set; }
        [Display(Name = "Which van are you supplementing?")]
        public string VehicleReplacing { get; set; }
    }
}
