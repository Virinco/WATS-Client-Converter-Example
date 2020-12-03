using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Virinco.WATS.Interface;

namespace XMLConverter
{
    public class XMLConverter : IReportConverter
    {
        public void CleanUp(){} //Nothing to cleanup here

        IDictionary<string, string> arguments; //Save a copy of arguments from Converters.xml
        public XMLConverter(IDictionary<string, string> args)
        {
            arguments = args; //The constructior is called once when client is started
        }

        public Report ImportReport(TDM api, Stream file)
        {
            using (TextReader r = new StreamReader(file))
            {
                XDocument report = XDocument.Load(r);
                XElement testreport = report.Element("TestReport");
                XElement header = testreport.Element("Header");
                UUTReport uut = api.CreateUUTReport(
                    header.Element("Operator").Value,
                    header.Element("PartNumber").Value,
                    header.Element("Revision").Value,
                    header.Element("SerialNumber").Value,
                    header.Element("OperationTypeCode").Value,
                    arguments["sequenceFile"], "1.0");
                uut.StartDateTime = DateTime.ParseExact(header.Element("StartDate").Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                uut.ExecutionTime = double.Parse(header.Element("ExecutionTime").Value, CultureInfo.InvariantCulture);
                foreach (var sequence in testreport.Elements("Sequence"))
                {
                    SequenceCall s = uut.GetRootSequenceCall().AddSequenceCall(sequence.Attribute("Name").Value);
                    foreach (var t in sequence.Elements())
                    {
                        if (t.Name == "NumericTest")
                            s.AddNumericLimitStep(t.Attribute("Name").Value).
                                AddTest(GetValue(t, "Measure"), CompOperatorType.GELE,
                                GetValue(t, "LowLim"),
                                GetValue(t, "HighLim"),
                                t.Attribute("Unit").Value);
                        else if (t.Name == "StringTest")
                            s.AddStringValueStep(t.Attribute("Name").Value).
                                AddTest(CompOperatorType.IGNORECASE, t.Attribute("Measure").Value, t.Attribute("Limit").Value);
                        else if (t.Name == "PassFailTest")
                            s.AddPassFailStep(t.Attribute("Name").Value).
                                AddTest(t.Attribute("Measure").Value.ToLower() == "True" ? true : false);
                    }
                }
                return uut;
            }
        }
        double GetValue(XElement x, string name)
        {
            return double.Parse(x.Attribute(name).Value, NumberStyles.Any, CultureInfo.InvariantCulture);
        }
    }
}
