using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ISproj.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Surname { get; set; }
        public string CNP { get; set; }
        public string Email { get; set; }

        public string FullName { get { return Surname + ' ' + Name; } }
    }
}
