﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Systore.Domain.Abstractions;

namespace Systore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PrintController : ControllerBase
    {
        private readonly IReport _report;

        public PrintController(IReport report)
        {
            _report = report;
        }

        //[Authorize]
        [HttpPost("printer-test-body")]
        public async Task<IActionResult> PrinterTestBody([FromBody]JToken jsonbody)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            foreach(JToken children in jsonbody.Children())
            {
                if (children is JProperty)
                {
                    var ch = (children as JProperty);
                    var key = ch.Name;
                    var value = ch.Value.ToString();
                    
                    parameters.Add(key, value);
                }

            }            
            
            var res = await _report.GenerateReport("RelatoriosInadimplentes.frx", parameters);
            return File(res, "application/pdf", "");
        }
        // TODO check this notation
        //[Authorize]
        [HttpGet("printer")]
        public async Task<IActionResult> Printer()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            var queryParams = HttpContext.Request.Query;
            
            foreach(var param in queryParams)
            {
                parameters.Add(param.Key, param.Value);
            }            
            
            var res = await _report.GenerateReport($"{queryParams.First().Value}.frx", parameters);
            return File(res, "application/pdf", "");
        }
    }
}
