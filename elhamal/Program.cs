using System;
using System.Collections.Generic;

class Program
{
    // Hàm tìm ước chung lớn nhất của hai số nguyên
    static int Gcd(int a, int b)
    {
        if (b == 0)
            return a;
        return Gcd(b, a % b);
    }

    // Hàm sinh số nguyên ngẫu nhiên trong khoảng từ minValue đến maxValue
    static int GenKey(int q)
    {
        Random random = new Random();
        int key = random.Next(1, q);
        while (Gcd(q, key) != 1)
            key = random.Next(1, q);
        return key;
    }

    // Hàm tính lũy thừa theo modulo (a^b mod c)
    static int Power(int a, int b, int c)
    {
        int x = 1;
        int y = a % c;
        while (b > 0)
        {
            if (b % 2 == 1)
                x = (x * y) % c;
            y = (y * y) % c;
            b /= 2;
        }
        return x % c;
    }

    // Hàm mã hóa thông điệp bằng thuật toán ElGamal
    static Tuple<List<int>, int> Encrypt(string msg, int p, int h, int anpha)
    {
        var enMsg = new List<int>();
        Random random = new Random();
        int k = GenKey(p); // Khóa riêng cho người gửi
        int s = Power(h, k, p); // Tính s = h^k mod q
        int q = Power(anpha, k, p); // Tính p = g^k mod q

        // Mã hóa từng ký tự trong thông điệp
        foreach (char c in msg)
            enMsg.Add(s * c);

        return Tuple.Create(enMsg, q);
    }

    // Hàm giải mã thông điệp đã được mã hóa bằng ElGamal
    static string Decrypt(List<int> enMsg, int p, int key, int q)
    {
        var drMsg = new List<char>();
        int h = Power(p, key, q); // Tính h = p^key mod q

        // Giải mã từng phần tử trong danh sách mã hóa
        foreach (int bi in enMsg)
            drMsg.Add((char)(bi / h));

        return new string(drMsg.ToArray());
    }

    static void Main(string[] args)
    {
        string msg = "15";
        Console.WriteLine("Original Message: " + msg);

        // Chọn giá trị cố định cho q và g
        int p = 23; // Số nguyên tố
        int anpha = 11;   // Số nguyên g > 1 và < q

        // Sinh khóa bí mật key
        int a = 6;

        // Tính h = g^key mod q
        int h = Power(anpha, a, p);

        Console.WriteLine("Khoa bi mat: " + a);
        Console.WriteLine("Khoa cong khai: " + p + ", " + anpha + ", " + h);


        // Mã hóa thông điệp
        var encryptionResult = Encrypt(msg, p, h, anpha);
        List<int> enMsg = encryptionResult.Item1;
        int pVal = encryptionResult.Item2;

        // Giải mã thông điệp

        string drMsg = Decrypt(enMsg, pVal, a, p);
        Console.WriteLine("Decrypted Message: " + drMsg);
    }
}
