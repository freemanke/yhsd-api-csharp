using System;

namespace YhsdApi.Exceptions
{
    public class MissingAppKeyException : Exception
    {
        public override string Message
        {
            get { return "Missing app key."; }
        }
    }
}