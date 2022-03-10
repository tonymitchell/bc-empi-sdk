using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class NullFlavor
    {
        public enum NullFlavorTypeEnum
        {
            NI,
            INV,
            OTH,
            NINF,
            PINF,
            UNC,
            DER,
            UNK,
            ASKU,
            NAV,
            QS,
            NASK,
            TRC,
            MSK,
            NA
        }

        public NullFlavorTypeEnum Type { get; private set; }

        protected NullFlavor(NullFlavorTypeEnum type)
        {
            Type = type;
        }
        public static NullFlavor GetNullFlavor(string hl7Code)
        {
            switch (hl7Code.ToUpper())
            {
                case "NI":
                    return NoInformation;
                case "MSK":
                    return Masked;
                case "INV":
                    return new NullFlavor(NullFlavorTypeEnum.INV);
                case "OTH":
                    return new NullFlavor(NullFlavorTypeEnum.OTH);
                case "NINF":
                    return new NullFlavor(NullFlavorTypeEnum.NINF);
                case "PINF":
                    return new NullFlavor(NullFlavorTypeEnum.PINF);
                case "UNC":
                    return new NullFlavor(NullFlavorTypeEnum.UNC);
                case "DER":
                    return new NullFlavor(NullFlavorTypeEnum.DER);
                case "UNK":
                    return new NullFlavor(NullFlavorTypeEnum.UNK);
                case "ASKU":
                    return new NullFlavor(NullFlavorTypeEnum.ASKU);
                case "NAV":
                    return new NullFlavor(NullFlavorTypeEnum.NAV);
                case "QS":
                    return new NullFlavor(NullFlavorTypeEnum.QS);
                case "NASK":
                    return new NullFlavor(NullFlavorTypeEnum.NASK);
                case "TRC":
                    return new NullFlavor(NullFlavorTypeEnum.TRC);
                case "NA":
                    return new NullFlavor(NullFlavorTypeEnum.NA);

                default:
                    throw new ArgumentOutOfRangeException("hl7Code", hl7Code, "Invalid HL7 NullFlavor code");
            }
        }

        public static NullFlavor Default { get { return NoInformation; } }

        private static NullFlavor _noInformation;
        public static NullFlavor NoInformation { get { if (_noInformation == null) _noInformation = new NullFlavor(NullFlavorTypeEnum.NI); return _noInformation; } }

        private static NullFlavor _masked;
        public static NullFlavor Masked { get { if (_masked == null) _masked = new NullFlavor(NullFlavorTypeEnum.MSK); return _masked; } }

        private static NullFlavor _unknown;
        public static NullFlavor Unknown { get { if (_unknown == null) _unknown = new NullFlavor(NullFlavorTypeEnum.UNK); return _unknown; } }

        //TODO: Expose remaining null flavor types

        public override bool Equals(object obj)
        {
            NullFlavor other = obj as NullFlavor;
            if (other != null)
            {
                return (this.Type == other.Type);
            }
            return false;
        }
    }
}
