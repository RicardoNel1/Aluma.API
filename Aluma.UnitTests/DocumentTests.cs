using DataService.Dto;
using DocumentService.Services;
using NUnit.Framework;
using System;

namespace Aluma.UnitTests
{
    public class DocumentTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestFNADocument()
        {
            IDocumentBaseService _documentService = new DocumentBaseService();

            FNAReportDto dto = new FNAReportDto()
            {
                ClientId = 1,
                ClientModule = true,
                ProvidingOnDisability = true
            };

            try
            {
                var result = _documentService.PDFGeneration(_documentService.FNAHtmlGeneration(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
            }
            finally
            {
                Assert.Pass("Success");
            }

        }
    }
}