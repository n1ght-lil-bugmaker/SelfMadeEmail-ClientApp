using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model.Validation
{
    internal class BlankFieldsValidator : IValidator
    {
        public IEnumerable<string> _parametrs;

        public BlankFieldsValidator(IEnumerable<string> parametrs)
        {
            _parametrs = parametrs;
        }
        public ValidationInfo Validate()
        {
            foreach(var parametr in _parametrs)
            {
                if(parametr == "" || parametr == null)
                {
                    return new ValidationInfo(ValidationResult.Error, "Не оставляйте поля пустыми!");
                }
            }
            return new ValidationInfo(ValidationResult.OK, "все ок");
        }
    }
}