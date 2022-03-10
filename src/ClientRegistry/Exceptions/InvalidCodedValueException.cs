using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Services.Exceptions
{
    public class InvalidCodedValueException : ParsingException
    {
        private string _codeSystem;
        private string _actualValue;

        public InvalidCodedValueException() { }
        public InvalidCodedValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        public InvalidCodedValueException(string codesSystem, string message)
            : base(message)
        {
            _codeSystem = codesSystem;
            _actualValue = null;
        }
        public InvalidCodedValueException(string codesSystem, string actualValue, string message)
            : base(message)
        {
            _codeSystem = codesSystem;
            _actualValue = actualValue;
        }

        public virtual string CodeSystem { get { return _codeSystem; } }
        public virtual string ActualValue { get { return _actualValue; } }
        public override string Message
        {
            get
            {
                return string.Format("{0}\nCodeset: {1}\nActual value: {2}", Message, CodeSystem, ActualValue);
            }
        }

    }
}
