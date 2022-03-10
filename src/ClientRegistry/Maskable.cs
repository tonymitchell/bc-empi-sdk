using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Services
{
#if false
    public class Maskable<T>
    {
        private bool _isMasked;
        private T _value;

        public Maskable()
        {
            _isMasked = true;
        }

        public Maskable(T value)
        {
            _value = value;
            _isMasked = false;
        }

        public static explicit operator T(Maskable<T> value)
        {
            return value.Value;
        }
        public static implicit operator Maskable<T>(T value)
        {
            return new Maskable<T>(value);
        }


        public T Value { 
            get 
            {
                if (IsMasked)
                    throw new InvalidOperationException("Attempted to access a masked value.");
                return _value;
            } 
        }
        public bool IsMasked { get { return _isMasked; } }

        public T GetValueOrDefault()
        {
            return GetValueOrDefault(default(T));
        }
        public T GetValueOrDefault(T defaultValue)
        {
            if (IsMasked)
                return defaultValue;        
            else
                return Value;
        }

        public override bool Equals(object o)
        {
            Maskable<T> other = o as Maskable<T>;
            if (other == null)
                return false;
            else if (this.IsMasked == false && other.IsMasked == false)
                return this.Value.Equals(other.Value);
            else if (this.IsMasked && other.IsMasked)
                return true;
            else
                return false;
        }
        public override int GetHashCode()
        {
            if (IsMasked == false)
                return Value.GetHashCode();
            else
                return 0;
        }
        public override string ToString()
        {
            if (IsMasked)
                return "confidential";
            else
                return Value.ToString();
        }
    }
#endif
}
