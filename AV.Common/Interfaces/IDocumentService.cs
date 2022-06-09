using System.Collections.Generic;

namespace AV.Common.Interfaces
{
    public interface IDocumentService
    {
        string PerformSubstitution(IDictionary<string, string> data, string htmlTemplate);
    }
}
