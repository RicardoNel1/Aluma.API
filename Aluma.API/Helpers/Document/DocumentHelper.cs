using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aluma.API.Helpers
{
    public class DocumentHelper
    {
        public byte[] PopulateDocument(string templateName, Dictionary<string, string> formData, IWebHostEnvironment host)
        {
            
            char slash = Path.DirectorySeparatorChar;
            string templatePath = $"{host.WebRootPath}{slash}pdf{slash}{templateName}";

            var ms = new MemoryStream();
            var pdf = new PdfDocument(new PdfReader(templatePath), new PdfWriter(ms));
            var form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            foreach (var d in formData)
            {
                try
                {
                    fields.TryGetValue(d.Key, out toSet);
                    toSet.SetValue(d.Value);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Form Field Error:  {d.Key}, {d.Value}");
                }
            }

            form.FlattenFields();
            pdf.Close();

            byte[] file = ms.ToArray();
            ms.Close();

            return file;
        }
    }
}