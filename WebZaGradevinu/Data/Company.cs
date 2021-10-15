using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebZaGradevinu.Data
{
    public class Company
    {
        
        public int ID { get; set; }
        public string Adresa { get; set; }
        public string NazivTvrtke { get; set; }
        public string Email { get; set; }
        public string OIB { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey(nameof(City))]
        public int? CityId { get; set; }
        public City City { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
        
    }
}
