using System.Text;

namespace rsa
{
    internal class Program
    {
        public static int GCD(int a, int b)
        {
            while(b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        public static int findPublicKey(int phi)
        {
            int e = 2;
            while(e < phi)
            {
                if(GCD(e, phi) == 1)
                {
                    return e;
                }
                else
                {
                    e++;
                }
            }
            return -1;
        }

        public static int findPrivateKey(int e, int phi)
        {
            int d = 1;
            while((d * e) % phi != 1)
            {
                d++;
            }
            return d;
        }
        public static int ModPow(int a, int b, int n)
        {
            string binaryB = Convert.ToString(b, 2);
            int f = 1;
            for(int i = 0; i< binaryB.Length; i++)
            {
                f = (f * f) % n;
                if (binaryB[i] == '1')
                {
                    f = (f * a) % n;
                }
            }
            return f;
        }
        public static int[] encrypt(string input, int e, int n)
        {
            int[] encrypted = new int[input.Length];
            for(int i = 0; i< input.Length; i++)
            {
                int charValue = input[i];
                encrypted[i] = ModPow(charValue, e, n); 
            }
            return encrypted;
        }
        public static string decrypt(int[] input, int d, int n)
        {
            StringBuilder decrypted = new StringBuilder();
            foreach(int charValue in input)
            {
                int dChar = ModPow(charValue, d, n);
                decrypted.Append((char)dChar);
            }
            return decrypted.ToString();
        }
        static void Main(string[] args)
        {
            int p = 61;
            int q = 53;
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int e = findPublicKey(phi);
            int d = findPrivateKey(e, phi);
            //
            string plainText = "TRUONG DHCNHN";
            Console.WriteLine(plainText);
            int[] eText = encrypt(plainText, e, n);
            string dText = decrypt(eText, d, n);
            //
            Console.WriteLine(string.Join(", ", eText));
            Console.WriteLine(dText);
        }
    }
}
