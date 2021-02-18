using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model.Validation
{
    public struct ValidationInfo
    {
        public ValidationResult Result { get; }
        public string Message { get; }

        public ValidationInfo(ValidationResult result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
