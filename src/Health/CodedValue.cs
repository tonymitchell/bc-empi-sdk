using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class CodedValue : HealthObject
    {

        public string CodeSystem { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public CodedValue(NullFlavor nullFlavor)
            : base(nullFlavor)
        {
        }
        public CodedValue(NullFlavor nullFlavor, string codeSystem, string code)
            : base(nullFlavor)
        {
            CodeSystem = codeSystem;
            Code = code;
        }
        public CodedValue(string codeSystem, string code, string displayName = "", string description = "")
        {
            CodeSystem = codeSystem;
            Code = code;
            DisplayName = displayName;
            Description = description;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(DisplayName))
                return DisplayName;
            else
                return Code;
        }
    }
}
