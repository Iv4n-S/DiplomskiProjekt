using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHrambaTest.Controllers
{
    public class PdfFileController
    {
        private string _pdfPath;
        public PdfFileController(string pdfPath) { 
            _pdfPath = pdfPath;
        }
        public string GetFileName()
        {
            return _pdfPath.Split("\\").Last();
        }

        public string GetTitleFromFileName(string extension)
        {
            string fileName = GetFileName();
            return fileName.Substring(0, fileName.Length - extension.Length);
        }

        public byte[] LoadPdf()
        {
            return File.ReadAllBytes(_pdfPath);
        }

        public string GetBase64Pdf(byte[] pdfFile)
        {
            return Convert.ToBase64String(pdfFile);
        }

        public byte[] FromBase64Pdf(string base64Pdf)
        {
            return Convert.FromBase64String(base64Pdf);
        }
    }
}
