using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISproj.Models.CoursesViewModels
{
    public class CreateViewModel
    {
        public String Name;
        public int Credits;
        public List<Teacher> Teachers;
        public string TeacherId;
    }
}
