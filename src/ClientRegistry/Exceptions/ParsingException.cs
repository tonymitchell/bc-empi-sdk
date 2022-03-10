using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Services.Exceptions
{
    public class ParsingException : ApplicationException
    {
        public ParsingException() { }

        public ParsingException(string message)
            : base(message)
        {
        }
        public ParsingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
