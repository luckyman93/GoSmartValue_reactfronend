using System.Collections.Generic;

namespace AV.Common.DTOs
{
    public class EmailTemplate
    {
        public string Template { get; set; }
        public Dictionary<string,string> Data { get; set; }
    }
}
