using System;

namespace YhsdApi.Exceptions
{
    public class MissingScopeException : Exception
    {
        public override string Message
        {
            get { return "Missing scope."; }
        }
    }
}