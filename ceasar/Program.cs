namespace ceasar
{
    internal class Program
    {
        public class Caesar
        {
            public static string encrypt(string input, int shift)
            {
                string rs = "";
                foreach (char c in input)
                {
                    if (char.IsLetter(c))
                    {
                        var shiftedChar = (char)(((c - 'A' + shift) % 26) + 'A');
                        rs += shiftedChar;
                    }
                    else
                    {
                        rs += c;    
                    }
                }
                return rs;
            }
            public static string decrypt(string input, int shift)
            {
                return encrypt(input, 26 -  shift);
            }
        }
        static void Main(string[] args)
        {
            int shift = 3;
            string plainText = "HELLO";
            string eText = Caesar.encrypt(plainText, shift);
            string dText = Caesar.decrypt(eText, shift);
            Console.WriteLine(plainText);
            Console.WriteLine(eText);
            Console.WriteLine(dText);
        }
    }
}
