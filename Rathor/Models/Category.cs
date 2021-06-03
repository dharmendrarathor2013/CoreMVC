using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rathor.Models
{
    [Table("Category_New")] 
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(1, int.MaxValue, ErrorMessage ="Display Order for Category must be greater than 0")]
        public string DisplayOrder { get; set; }
       // [NotMapped]
        //public int CategoryTypeID { get; set; }
    }
    
}
