using System;
using System.Threading.Tasks;
using Systore.Domain.Abstractions;
using FastReport.Export.PdfSimple;
using System.IO;
using Systore.Domain;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Systore.Report
{
    public class Report : IReport
    {

        private readonly IConfiguration _configuration;

        public Report(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<byte[]> GenerateReport(string reportFile, Dictionary<string, string> parameters)
        {
            return await Task.Factory.StartNew(() => InternalGenerateReport(reportFile, parameters));
        }

        private byte[] InternalGenerateReport(string reportFile, Dictionary<string, string> parameters)
        {
            FastReport.Report report = new FastReport.Report();
            report.Load(Path.Combine("Reports", reportFile));
            if (string.IsNullOrWhiteSpace(_configuration.GetConnectionString("Systore"))){
                throw new NotSupportedException("Connectionstring não informada");
            }
            
            report.Dictionary.Connections[0].ConnectionString = _configuration.GetConnectionString("Systore");
            
            foreach (var parameter in parameters)
            {
                report.SetParameterValue(parameter.Key, parameter.Value);
            }

            report.Prepare();

            PDFSimpleExport export = new PDFSimpleExport();
            using (MemoryStream ms = new MemoryStream())
            {
                report.Export(export, ms);
                ms.Flush();
                return ms.ToArray();
            }
        }
    }
}
