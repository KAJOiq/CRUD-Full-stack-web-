﻿using System.ComponentModel.DataAnnotations;

namespace mohammed_hazim.Model.DTOs
{
    public class CarsDTO
    {
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string OwnerSurename { get; set; }
        [Required]
        public string CarPlateNum { get; set; }
        [Required]
        public string CarBrandName { get; set; }

        [RegularExpression(@"^\d{4}$", ErrorMessage = "Car model has 4 numbers.")]
        public int CarModel { get; set; }
        [Required]
        public string ChassisNumber { get; set; }
    }
}
