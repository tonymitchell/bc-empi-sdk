using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class PersonName : HealthObject, IComparable
    {
        public PersonName(string nameType)
        {
            Type = nameType;
        }
        //public PersonName(NullFlavor nullFlavor, string nameType)
        //    : base(nullFlavor)
        //{
        //    Type = nameType;
        //}
        //public PersonName(string nameType, string surname, string firstPreferred, string firstGiven, string secondGiven, string thirdGiven)
        //    : base()
        //{
        //    Type = nameType;
        //    Surname = surname;
        //    FirstPreferredName = firstPreferred;
        //    FirstGivenName = firstGiven;
        //    SecondGivenName = secondGiven;
        //    ThirdGivenName = thirdGiven;
        //}

        //Name Type
        public string Type { get; set; }
        //Surname
        public string Surname { get; set; }
        //First Given Name
        public string FirstGivenName { get; set; }
        //First Preferred Name
        public string FirstPreferredName { get; set; }
        //Second Given Name
        public string SecondGivenName { get; set; }
        //Third Given Name
        public string ThirdGivenName { get; set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            int result = 0;
            PersonName other = obj as PersonName;
            if (other == null)
                result = string.Compare(this.Surname, null, true);
            else
            {
                result = string.Compare(this.Surname, other.Surname, true);
                if (result == 0) result = string.Compare(this.Surname, other.Surname, true);
                if (result == 0) result = string.Compare(this.FirstGivenName, other.FirstGivenName, true);
                if (result == 0) result = string.Compare(this.SecondGivenName, other.SecondGivenName, true);
                if (result == 0) result = string.Compare(this.ThirdGivenName, other.ThirdGivenName, true);
            }
            return result;
        }

        #endregion


    }
}
