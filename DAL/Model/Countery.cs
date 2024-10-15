using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Countery<TKey> : BaseEntity<TKey>
    {
        [Required , MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<Player<int>>? Players { get; set; }
    }
}
