using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Models
{
    public class PostIt
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(150)]
        public string Text { get; set; }
    }
}
