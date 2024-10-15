using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Player<TKey> : BaseEntity<TKey>
    {
        public string Name { get; set; }
        public int YearBirth { get; set; }
        public double Rate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string? Poster { get; set; }
        public int? CounteryId { get; set; }
        public Countery<int>? Countery { get; set; }

    }
}
