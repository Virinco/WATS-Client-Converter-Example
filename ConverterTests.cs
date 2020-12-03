using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using Virinco.WATS.Interface;
using System.IO;

namespace XMLConverter
{
    [TestClass]
    public class ConverterTests : TDM
    {
        [TestMethod]
        public void SetupClient()
        {
            SetupAPI(null, "", "Test", true);
            RegisterClient("your wats", "username", "password");
            InitializeAPI(true);
        }

        [TestMethod]
        public void TestXMLConverter()
        {
            InitializeAPI(true);
            var arguments = new Dictionary<string, string> { { "sequenceFile", "sequence.seq" } };
            var converter = new XMLConverter(arguments);
            using (var file = new FileStream(@"Examples\XMLReport.xml", FileMode.Open))
            {
                var report = converter.ImportReport(this, file);
                Submit(report);
            }            
        }
    }
}
