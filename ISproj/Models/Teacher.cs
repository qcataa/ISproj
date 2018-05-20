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
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<CourseModel> Courses { get; set; }

        public string FullName { get { return LastName + " " + FirstName; } }
    }
}
