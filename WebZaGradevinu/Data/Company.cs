using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebZaGradevinu.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Adresa { get; set; }
        [Required]
        public string NazivTvrtke { get; set; }
        
        public string Email { get; set; }
        [Required]
        public string OIB { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey(nameof(City))]
        public int? CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<AcceptedJobs> AcceptedJobs { get; set; }
        
        public virtual ICollection<Jobs> Jobs { get; set; }
        
    }
}
