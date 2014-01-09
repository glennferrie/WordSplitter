using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWordSplitter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ValidateArgs(args)) return;

            var term = args[0];
            var maxLength = 3; // term.Length;
            const int minLength = 3;

            var set = new HashSet<string>();

            for (var l = minLength; l <= maxLength; l++)
            {
                FindTermsOfLength(set, term, l);
            }

            Console.WriteLine("Terms found: {0}", set.Count);
                                     

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void FindTermsOfLength(HashSet<string> set, string term, int length)
        {

            for (var q = 0; q < term.Length; q++ )
            {
                // find all words starting with each letter
                var letter = term[q].ToString();

                var otherLetters = GetOtherLetters(term, q);
                
                GetPermutations(set, letter, otherLetters, length - 1);
            }
        }

        private static string GetOtherLetters(string term, int removeIndex)
        {
            var sb = new StringBuilder();

            for (var w = 0; w < term.Length; w++)
            {
                if (w == removeIndex) continue;
                sb.Append(term[w]);
            }

            return sb.ToString();
        }

        private static void GetPermutations(HashSet<string> set, string startsWith, string term, int length)
        {
            if (length == 0)
            {
                if (!set.Contains(startsWith)) set.Add(startsWith);
                return;
            }
            for (var f = 0; f < term.Length; f++)
            {
                var nextLetter = term[f];
                GetPermutations(set, startsWith + nextLetter, GetOtherLetters(term, f), length - 1);
            }
        }

        private static bool ValidateArgs(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Term expected!");
                Console.ReadLine();
                return false;
            }

            var term = args[0];
            if (term.Length < 3)
            {
                Console.WriteLine("Term is too short! 3 letter minimum");
                Console.ReadLine();
                return false;
            }

            return true;
        }
    }
}
