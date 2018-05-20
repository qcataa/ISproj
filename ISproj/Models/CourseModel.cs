using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISproj.Models
{
    public class CourseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public virtual string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}