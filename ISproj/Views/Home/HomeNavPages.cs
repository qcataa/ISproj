﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ISproj.Views.Home
{
    public static class HomeNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string Teacher => "Teacher";

        public static string Student => "Student";

        public static string Courses => "Cursuri";

        public static string Scholarship => "Burse";

        public static string Faculties => "Facultati";

        public static string Timetable => "Timetable";

        public static string Grades => "Grades";

        public static string Attendant => "Attendant";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string TeacherNavClass(ViewContext viewContext) => PageNavClass(viewContext, Teacher);

        public static string StudentNavClass(ViewContext viewContext) => PageNavClass(viewContext, Student);

        public static string CoursesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Courses);

        public static string ScholarshipNavClass(ViewContext viewContext) => PageNavClass(viewContext, Scholarship);

        public static string FacultiesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Faculties);

        public static string TimetableNavClass(ViewContext viewContext) => PageNavClass(viewContext, Timetable);

        public static string StudentGradesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Grades);

        public static string AttendantNavClass(ViewContext viewContext) => PageNavClass(viewContext, Attendant);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
