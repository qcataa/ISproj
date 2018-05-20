using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ISproj.Models
{
    public class Scholarship
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public Student Student { get; set; }
        public virtual int StudentId { get; set; }
    }
}
