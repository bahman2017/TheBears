using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBears.Models
{
    public class BearModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Habitat is required.")]
        public string Habitat { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public int Sex { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Height is required.")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public int Weight { get; set; }

    }
}
