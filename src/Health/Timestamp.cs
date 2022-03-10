using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class Timestamp : HealthObject
    {
        private DateTime? _value;

        // Constructors
        public Timestamp()
            : base(NullFlavor.Default)
        {
            _value = null;
        }
        public Timestamp(NullFlavor nullFlavor)
            : base(nullFlavor)
        {
            _value = null;
        }
        public Timestamp(DateTime value)
        {
            _value = value;
        }
        public Timestamp(DateTime? value, NullFlavor nullFlavor = null)
            : base(nullFlavor)
        {
            _value = value;
            if (nullFlavor == null && value.HasValue == false)
                nullFlavor = NullFlavor.Default;
        }

        public DateTime Value
        {
            get
            {
                return _value.Value;
            }
        }

        protected override void SetNullFlavor(NullFlavor flavor)
        {
            _value = null;
            base.SetNullFlavor(flavor);
        }

        // Operators
        public static explicit operator DateTime(Timestamp timestampValue)
        {
            return timestampValue.Value;
        }
        public static explicit operator DateTime?(Timestamp timestampValue)
        {
            return timestampValue._value;
        }
        public static implicit operator Timestamp(DateTime dateTimeValue)
        {
            return new Timestamp(dateTimeValue);
        }
        public static implicit operator Timestamp(DateTime? dateTimeValue)
        {
            return new Timestamp(dateTimeValue);
        }

        public override string ToString()
        {
            if (!IsNull)
                return Value.ToString("d");

            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            Timestamp other = obj as Timestamp;
            return (
                other != null && 
                this.NullFlavor == other.NullFlavor && 
                this._value == other._value
                );
        }


    }
}
