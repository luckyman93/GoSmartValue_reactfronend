using System.Collections.Generic;
using System.Text;
using AV.Common.Interfaces;

namespace GoSmartValue.Web.Services
{
    public class DocumentService : IDocumentService
    {
        public string PerformSubstitution(IDictionary<string, string> data, string htmlTemplate)
        {
            var template = new StringBuilder(htmlTemplate);
            foreach (var dataItem in data)
            {
                template.Replace($"<%#{dataItem.Key}#%>", dataItem.Value);
            }

            return template.ToString();
        }
    }
}