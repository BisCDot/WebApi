using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity
{
    public class CourceEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        public DateTime CreateDatatime { get; set; }
        public long CategorysId { get; set; }
        [ForeignKey(nameof(CategorysId))]
        public CategoryEntity Category { get; set; }
    }
}
