using System;
using System.Collections.Generic;
using System.Text;

namespace Registration.Utilities.Exceptions
{
    public class InvalidUserObject : Exception
    {
        private readonly static string Message = "Invalid given object:";
        public InvalidUserObject(string objectName) : base($"{Message} {objectName}")
        {

        }
    }
}
