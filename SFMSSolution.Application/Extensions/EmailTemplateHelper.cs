using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.Extensions
{
    public static class EmailTemplateHelper
    {
        public static string ApplyTemplate(string template, Dictionary<string, string> placeholders)
        {
            if (string.IsNullOrWhiteSpace(template)) return string.Empty;

            foreach (var kvp in placeholders)
            {
                var token = $"{{{{{kvp.Key}}}}}"; // {{Key}}
                template = template.Replace(token, kvp.Value);
            }

            return template;
        }
    }
}
