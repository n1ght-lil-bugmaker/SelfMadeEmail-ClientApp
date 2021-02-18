using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Model.Validation
{
    internal interface IValidator
    {
        ValidationInfo Validate();
    }
}
