using System;

namespace YhsdApi.Exceptions
{
    public class MissingTokenException : Exception
    {
        public override string Message
        {
            get { return "Missing token."; }
        }
    }
}