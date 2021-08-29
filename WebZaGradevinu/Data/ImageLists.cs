using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebZaGradevinu.Data
{
    public class ImageLists
    {
        public enum RoomType
        {
            Bazen,
            Kupaona,
            Ostalo
        }

        [Key]
        public int ID { get; set; }
        public string Path { get; set; }
        public string ImageName { get; set; }
        public RoomType Types { get; set; }

    }
}
