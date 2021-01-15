using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models.hireModels
{
    public class EditHireVehicle
    {
        public int Id { get; set; }
        [Display(Name = "PO number")]
        public string OakPONumber { get; set; }
        [Display(Name = "Who was the vehicle for?")]
        public string HiredFor { get; set; }
        [Display(Name = "Hire vehicle registration")]
        public string VehicleRegistration { get; set; }
        [Display(Name = "Vehicle make")]
        public string Make { get; set; }
        [Display(Name = "Vehicle model")]
        public string Model { get; set; }
        [Display(Name = "Hire company")]
        public string HireCompany { get; set; }
        [Display(Name = "Hire provider")]
        public string HireProvider { get; set; }
        [Display(Name = "Hired from")]
        public DateTime HiredFrom { get; set; }
        [Display(Name = "Hired to")]
        public DateTime HiredTo { get; set; }
        [Display(Name = "Reason for hire")]
        public string ReasonForHire { get; set; }
        [Display(Name = "Date added to mid")]
        public DateTime DateAddedToMid { get; set; }
        [Display(Name = "Date removed from mid")]
        public DateTime DateRemovedFromMid { get; set; }
        [Display(Name = "Payload capacity")]
        public int PayloadCapacity { get; set; }
        [Display(Name = "Replacing vehicle")]
        public string VehicleReplacing { get; set; }
    }
}
