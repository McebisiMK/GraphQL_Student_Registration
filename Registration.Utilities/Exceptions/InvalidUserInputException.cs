using System;

namespace Registration.Utilities.Exceptions
{
    public class InvalidUserInputException : Exception
    {
        private static string message = "Invalid given user input:";
        public InvalidUserInputException(string input) : base($"{message} {input}")
        {
        }
    }
}
