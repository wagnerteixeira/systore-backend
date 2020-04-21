using System.Collections.Generic;
using System.Threading.Tasks;

namespace Systore.Domain.Abstractions
{
    public interface IReport
    {
        Task<byte[]> GenerateReport(string reportFile, Dictionary<string, string> parameters);
    }
}
