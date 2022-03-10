using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health
{
    public class BcPhn
    {
        /// <summary>
        /// Performs BC PHN validation on the specified input
        /// </summary>
        /// <param name="phn">input to be validated</param>
        /// <returns>true if input is a valid BC PHN, otherwise false</returns>
        public static bool IsValid(string phn)
        {
            // Must 10 digits
            if (phn == null || phn.Length != 10)
                return false;

            // Must start with 9
            if (phn[0] != '9')
                return false;

            // Mod 11 check sum
            if (!IsChecksumValid(phn))
                return false;

            return true;
        }

        /// <summary>
        /// Performs a MOD 11 checksum validation on the PHN
        /// </summary>
        /// <param name="phn">A BC PHN to validate</param>
        /// <returns>true if PHN checksum is correct, false otherwise</returns>
        private static bool IsChecksumValid(string phn)
        {
            // Extract the check digit (the last digit)
            int checkDigit = (int)char.GetNumericValue(phn, phn.Length - 1);

            // Calculate checksum from all digits except first and last
            string digitsToCheck = phn.Substring(1, phn.Length - 2);
            int calculatedCheckDigit = CalculateMod11Checksum(digitsToCheck);

            // Verify the checksum matches
            return (checkDigit == calculatedCheckDigit);
        }
        /// <summary>
        /// Calculate a MOD 11 checksum based on the provided numerical input
        /// </summary>
        /// <param name="input">numerical input string</param>
        /// <returns>MOD 11 checksum</returns>
        private static int CalculateMod11Checksum(string input)
        {
            int calculatedCheckDigit = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                int digit = (int)char.GetNumericValue(input, i);
                calculatedCheckDigit += digit << (i + 1);
            }
            calculatedCheckDigit = 11 - (calculatedCheckDigit % 11);

            return calculatedCheckDigit;
        }

        static Random _rnd = new Random();
        public static string Generate(string startsWith = "")
        {
            if (startsWith.Length > 8) 
                throw new ArgumentOutOfRangeException("startsWith", "Input string is too long.");

            while (true)
            {
                string significantDigits = startsWith;
                for (int i = startsWith.Length; i < 8; ++i)
                    significantDigits += _rnd.Next(10).ToString();

                int checkDigit = CalculateMod11Checksum(significantDigits);
                if (checkDigit > 9) continue;  // Retry if not a valid PHN

                return "9" + significantDigits + checkDigit.ToString();
            }
        }
    }
}
