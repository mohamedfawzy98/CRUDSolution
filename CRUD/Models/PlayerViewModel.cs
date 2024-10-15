using DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class PlayerViewModel
    {
        public string Name { get; set; }
        public int YearBirth { get; set; }
        public double Rate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string? Poster { get; set; }
        public IFormFile PosterImage { get; set; }
        public int? CounteryId { get; set; }
        public Countery<int>? Countery { get; set; }
    }
}
