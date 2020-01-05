using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ParseLogs
{
    public class UsageValidationRule : ValidationRule
    {
        public UsageValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Usage usage = (Usage) value;
                Debug.Assert(usage.Examples != null, "Examples should not be null.");
                return ValidationResult.ValidResult;
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
            
        }
    }
}
