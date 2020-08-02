using System;
using System.Collections.Generic;
using System.Text;

namespace Registration.Utilities.Exceptions
{
    public class InvalidForeignKeyException : Exception
    {
        private static string Message = "Given foreign key(s) does not exists:";
        public InvalidForeignKeyException(string entity) : base($"{Message} {entity}")
        {
        }
    }
}
