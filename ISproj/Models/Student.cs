using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISproj.Models
{
    public class Student
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string CNP { get; set; }
    }
}
