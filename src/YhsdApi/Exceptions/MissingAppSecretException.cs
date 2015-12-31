using System;

namespace YhsdApi.Exceptions
{
    public class MissingAppSecretException : Exception
    {
        public override string Message
        {
            get { return "Missing app secret."; }
        }
    }
}