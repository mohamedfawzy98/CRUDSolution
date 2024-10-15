using DAL.Model;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class PlayersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearBirth { get; set; }
        [Range(1,10)]
        public double Rate { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Display(Name = "Choose Poster..")]
        public string? Poster { get; set; }
        public IFormFile? PosterImage { get; set; }
        [Display(Name ="Countrey")]
        public int? CounteryId { get; set; }

        public  Countery<int>? Countery { get; set; }
    }
}
