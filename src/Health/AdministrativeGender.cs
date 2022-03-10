using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public static class AdministrativeGender
    {
        private const string CodeSystemOid = "2.16.840.1.113883.1.11.1";

        private static CodedValue _male = null;
        public static CodedValue Male
        {
            get
            {
                if (_male == null)
                    _male = new CodedValue(CodeSystemOid, "M", "male", "Male");
                return _male;
            }
        }

        private static CodedValue _female = null;
        public static CodedValue Female
        {
            get
            {
                if (_female == null)
                    _female = new CodedValue(CodeSystemOid, "F", "female", "Female");
                return _female;
            }
        }

        private static CodedValue _undifferentiated = null;
        public static CodedValue Undifferentiated
        {
            get
            {
                if (_undifferentiated == null)
                    _undifferentiated = new CodedValue(CodeSystemOid, "UN", "undifferentiated", "The gender of a person could not be uniquely defined as male or female, such as hermaphrodite.");
                return _undifferentiated;
            }
        }
    }
}
