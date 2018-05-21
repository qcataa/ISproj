using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISproj.Models.CoursesViewModels
{
    public class CreateViewModel
    {
        public int Id;
        public String Name;
        public int Credits;
        public List<Teacher> Teachers;
        public string TeacherId;
        public string Day { get; set; }
        public int Hour { get; set; }
        public int Duration { get; set; }
    }
}
