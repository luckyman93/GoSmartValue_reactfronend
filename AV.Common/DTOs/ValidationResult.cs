using System.Collections.Generic;

namespace AV.Common.DTOs
{
    public class ValidationResult
    {
        public bool IsValid => Messages.Count == 0;
        public List<string> Messages { get; set; }
        public ValidationResult()
        {
            Messages = new List<string>();
        }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}
