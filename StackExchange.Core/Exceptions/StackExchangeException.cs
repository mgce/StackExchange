using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchange.Core.Exceptions
{
    public class StackExchangeException : Exception
    {
        public string Code { get; }

        protected StackExchangeException()
        {
        }

        protected StackExchangeException(string code)
        {
            Code = code;
        }

        protected StackExchangeException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected StackExchangeException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected StackExchangeException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected StackExchangeException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
