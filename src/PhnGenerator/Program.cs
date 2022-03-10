using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Health;

namespace PhnGenerator
{
    /// <summary>
    /// Generates a list of 1 or more valid BC PHNs for use in testing.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // If no number specified, just generate 1 by default
            int numPhnsToGenerate;
            if (args.Length < 1 || !int.TryParse(args[0], out numPhnsToGenerate))
                numPhnsToGenerate = 1;

            // If a prefix is provided, generate a random PHN with those starting characters
            string startsWith = "";
            if (args.Length >= 2)
                startsWith = args[1].Trim();

            for (int i = 0; i < numPhnsToGenerate; ++i)
            {
                string phn = BcPhn.Generate(startsWith);
                Console.WriteLine(phn);
            }
        }
    }
}
