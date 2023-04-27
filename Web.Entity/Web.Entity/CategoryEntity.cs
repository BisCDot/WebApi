using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity
{
    public class CategoryEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CourceEntity> CourceEntity { get; set; }
    }
}
