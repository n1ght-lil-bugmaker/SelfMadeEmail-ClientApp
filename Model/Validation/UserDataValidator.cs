using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model.Validation
{
    internal class UserDataValidator : IValidator
    {
        private string _username;
        private bool _needExistance;
        public ValidationInfo Validate()
        {
            var response = Api.GetUser(_username);

            if(response == null && !_needExistance)
            {
                return new ValidationInfo(ValidationResult.OK, "Existance doesn't need, user hasn't found");
            }
            if(response == null && _needExistance)
            {
                return new ValidationInfo(ValidationResult.Error, "User hasn't found, check data!");
            }
            if(response != null && !_needExistance)
            {
                return new ValidationInfo(ValidationResult.Error, "User already exists!");
            }
            else
            {
                return new ValidationInfo(ValidationResult.OK, "Existance is required, and user has been found");
            }

        }

        public UserDataValidator(string username, bool needExistance)
        {
            _username = username;
            _needExistance = needExistance;
        }
    }
}
