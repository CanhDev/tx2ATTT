using System.Diagnostics.Metrics;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;

namespace affine
{
    internal class Program
    {
         
        public static class Affine
        {
            public static string encrypt(string input, int a, int b)
            {
                string rs = "";
                foreach (char c in input)
                {
                    if (char.IsLetter(c))
                    {
                        var shiftedChar = (char)(((a * (c - 'A') + b) % 26) + 'A');
                        rs += shiftedChar;
                    }
                    else
                    {
                        rs += c;
                    }
                }
                return rs;
            }
            public static string decrypt(string input, int a, int b)
            {
                string rs = "";
                int m = 26;
                int aInverse = 0;
                for(int i = 0; i < m; i++)
                {
                    if((a*i) % m == 1)
                    {
                        aInverse = i;
                        break;
                    }
                }
                foreach (char c in input)
                {
                    if (char.IsLetter(c))
                    {
                        var shiftedChar = (char)(((aInverse * (c - 'A' - b + 26)) % 26) + 'A');
                        rs += shiftedChar;
                    }
                    else
                    {
                        rs += c;
                    }
                }
                return rs;
            }
        }
        static void Main(string[] args)
        {
            int a = 5; int b = 3;
            string plainText = "HELLO";
            string eText = Affine.encrypt(plainText, a, b);
            string dText = Affine.decrypt(eText, a, b);
            Console.WriteLine(plainText);
            Console.WriteLine(eText);
            Console.WriteLine(dText);
        }
    }
}
