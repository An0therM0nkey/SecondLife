using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This class for using validations and transfer errors to presentation layer 

namespace BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        //saving prop of model, which is incorrect and does not pass validation
        public string Property { get; protected set; } 
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
