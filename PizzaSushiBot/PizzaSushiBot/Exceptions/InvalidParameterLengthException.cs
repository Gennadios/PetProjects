using System;

namespace PizzaSushiBot.Exceptions
{
    sealed class InvalidParameterLengthException : Exception
    {
        public string CauseOfError { get; set; }

        public InvalidParameterLengthException() { }
        public InvalidParameterLengthException(string message, string cause) 
            : base(message)
        {
            CauseOfError = cause;
        }
    }
}