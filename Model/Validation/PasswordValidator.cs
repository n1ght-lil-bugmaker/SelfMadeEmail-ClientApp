using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model.Validation
{
    internal class PasswordValidator : IValidator
    {
        private string _username;
        private string _password;
        public PasswordValidator(string username, string password)
        {
            _username = username;
            _password = password;
        }
        public ValidationInfo Validate()
        {
            bool result= Api.CheckUserData(_username, _password);
            
            if(result)
            {
                return new ValidationInfo(ValidationResult.OK, "Data correct, come in!");
            }
            else
            {
                return new ValidationInfo(ValidationResult.Error, "Wrong email or password");
            }
        }
    }
}
