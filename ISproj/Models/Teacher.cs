using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISproj.Models
{
    public class Teacher
    {
        [Key]
        public string CNP { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public IEnumerable<CourseModel> Courses { get; set; }
    }
}
