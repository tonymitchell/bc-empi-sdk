using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class HealthObject
    {
        private NullFlavor _nullFlavor;

        public HealthObject(NullFlavor flavor = null)
        {
            _nullFlavor = flavor;
        }

        public NullFlavor NullFlavor 
        { 
            get 
            {
                return GetNullFlavor();
            } 
            set 
            {
                SetNullFlavor(value);
            } 
        }

        public bool IsNull
        {
            get { return (NullFlavor != null); }
        }

        protected virtual NullFlavor GetNullFlavor()
        {
            return _nullFlavor;
        }
        protected virtual void SetNullFlavor(NullFlavor flavor)
        {
            _nullFlavor = flavor;
        }


        public override string ToString()
        {
            if (NullFlavor == NullFlavor.Masked)
                return "<confidential>";
            else if (NullFlavor == NullFlavor.Unknown)
                return "<unknown>";
            else
                return "";
        }

    }
}
