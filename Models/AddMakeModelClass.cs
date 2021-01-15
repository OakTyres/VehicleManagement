using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Models
{
    public class AddMakeModelClass
    {
        [Required(ErrorMessage = "You must provide a make")]
        public string Make { get; set; }
        [Required(ErrorMessage = "You must provide a model")]
        public string Model { get; set; }
    }
}
