using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISproj.Models;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ISproj.Services
{
    public interface IPDFWritter
    {
        void CreatePDF();

        void CreateGradesPDF(List<CourseModel> grades);
    }
}
