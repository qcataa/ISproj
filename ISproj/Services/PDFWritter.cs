using ISproj.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ISproj.Services
{
    public class PDFWritter : IPDFWritter
    {
        public void CreatePDF()
        {
            System.IO.FileStream fs = new FileStream("GeneratedDocument.pdf", FileMode.Create);

            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.AddAuthor("UCV");

            document.AddCreator("UCV");

            document.AddKeywords("Note");

            document.AddSubject("Situatie Scolara");

            document.AddTitle("Situatie scolara");

            document.Open();

            document.Close();

            writer.Close();

            fs.Close();

        }

        public void CreateGradesPDF(List<CourseModel> grades)
        {
            System.IO.FileStream fs = new FileStream("GeneratedDocument.pdf", FileMode.Create);

            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.AddAuthor("UCV");

            document.AddCreator("UCV");

            document.AddKeywords("Note");

            document.AddSubject("Situatie Scolara");

            document.AddTitle("Situatie scolara");

            //document.Add(new Chunk(new LineSeparator(4f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1)));

            document.Open();

            document.Add(new Paragraph("UCV --- Situatie Scolara"));

            if (grades.Count == 0)
            {
                document.Add(new Paragraph("Nu exista situatie scolara."));
            }
            else {
                foreach(var grade in grades)
                {
                    document.Add(new Paragraph(grade.Name + "    " + grade.Teacher + "    " + grade.Credits));
                    //document.Add(new Chunk(new LineSeparator(4f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -1)));
                }
            }
            document.Close();

            writer.Close();

            fs.Close();
        }

    }
}
