using DocumentService.Interfaces;
using DocumentService.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aluma.UnitTests.Documents
{
    public class DocumentTests
    {
        [Test]
        public void TestSMS()
        {
            IDocumentBaseService documentService = new DocumentBaseService();

            try
            {
                _functionService.SendSms("Test OTP", "0614262803");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message, ex);
            }
            finally
            {
                Assert.Pass("Success");
                context.Dispose();
            }

        }
    }
}
