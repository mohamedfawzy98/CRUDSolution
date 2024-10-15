using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class CounteryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required!!")]
        public string Name { get; set; }
    }
}
