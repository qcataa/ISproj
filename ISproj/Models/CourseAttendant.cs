using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ISproj.Models
{
    public class CourseAttendant
    {
        [Key]
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public int StudentId { get; set; }
        public virtual CourseModel Course { get; set; }
        public int CourseId { get; set; }
        public int? Grade { get; set; }
        public int Attendances { get; set; }
    }
}
