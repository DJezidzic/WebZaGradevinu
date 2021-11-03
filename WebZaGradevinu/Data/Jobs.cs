using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebZaGradevinu.Data
{
    public class Jobs
    {

        public enum Room
        {
            Bazen,
            Kupaona,
            Ostalo
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string WideDescription { get; set; }
        [Required]
        public DateTime PocetakRadova { get; set; }
        [Required]
        public bool AktivanOglas { get; set; }
        [Required]
        public string Adresa { get; set; }
        [Required]
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [Required]
        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Required]
        public Room Rooms { get; set; }

        public virtual ICollection<AcceptedJobs> AcceptedJobs { get; set; }

    }
}
