using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebZaGradevinu.Data
{
    public class AppUser : IdentityUser
    {

        [RegularExpression("[0-9]{11}", ErrorMessage = "OIB mora sadržavat 11 cjelobrojnih brojeva.")]
        [Required(ErrorMessage = "OIB is required.")]
        public string OIB { get; set; }
        [Required(ErrorMessage = "Naziv tvrtke je obavezno polje.")]
        public string NazivTvrtke { get; set; }

        public string Adress { get; set; }
        public string Grad { get; set; }
    }
}
