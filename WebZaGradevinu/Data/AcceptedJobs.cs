using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebZaGradevinu.Data
{
    public class AcceptedJobs
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Jobs))]
        public int? JobId { get; set; }
        public Jobs Jobs { get; set; }

        [ForeignKey(nameof(Company))]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
        [Required]
        public DateTime AcceptedTime { get; set; }
        public bool JobisFinished { get; set; }
        [Required]
        public DateTime FinishedTime { get; set; }
    }
}
