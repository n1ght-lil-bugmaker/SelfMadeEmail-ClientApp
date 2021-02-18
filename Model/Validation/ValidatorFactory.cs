using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model.Validation
{
    public class ValidatorFactory
    {
        private List<IValidator> _validators = new List<IValidator>();
        public bool AllValidationsCompleted { get; private set; }
        public ValidationInfo FailedValidationInfo { get; private set; }

        public void ValidateAll()
        {
            AllValidationsCompleted = true;
            foreach(var validator in _validators)
            {
                var res = validator.Validate();

                if(res.Result == ValidationResult.Error)
                {
                    AllValidationsCompleted = false;
                    FailedValidationInfo = res;
                    break;
                }
            }
        }


        public ValidatorFactory AddBlankLinesValidator(IEnumerable<string> toCheck)
        {
            _validators.Add(new BlankFieldsValidator(toCheck));
            return this;
        }

        public ValidatorFactory AddUserDataValidator(string username, bool needExistance)
        {
            _validators.Add(new UserDataValidator(username, needExistance));
            return this;
        }

        public ValidatorFactory AddPasswordValidator(string username, string password)
        {
            _validators.Add(new PasswordValidator(username, password));
            return this;
        }
    }
}
