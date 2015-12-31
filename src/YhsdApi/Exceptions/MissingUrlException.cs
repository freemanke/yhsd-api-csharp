using System;

namespace YhsdApi.Exceptions
{
    public class MissingUrlException : Exception
    {
        public override string Message
        {
            get { return "Missing Url."; }
        }
    }
}