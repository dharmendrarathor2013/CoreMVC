using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rathor.Models
{
    [Table("ApplicationType")]
    public class ApplicationType
    {
        [Key]
        public int ID { get; set; }
       
        public string Name { get; set; }
        
    }
    
}
