// Programmer: Muhammad Asjad Rehman Hashmi
// Program: Challenge 1 Set 1

using System;
using System.Text;

public class Program
{
    // Base64 characters table
    private const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

    // Function to convert a hex string to a binary string
    public static string HexToBinary(string hex)
    {
        var binary = new StringBuilder();

        // Convert each hex digit to a 4-bit binary string
        foreach (char c in hex)
        {
            int value = Convert.ToInt32(c.ToString(), 16);
            binary.Append(Convert.ToString(value, 2).PadLeft(4, '0'));
        }

        return binary.ToString();
    }

    // Function to convert a binary string to a Base64 string
    public static string BinaryToBase64(string binary)
    {
        var base64 = new StringBuilder();
        int i = 0;

        // Process 6-bit chunks of the binary string
        while (i + 6 <= binary.Length)
        {
            int value = Convert.ToInt32(binary.Substring(i, 6), 2);
            base64.Append(base64Chars[value]);
            i += 6;
        }

        // Handle remaining bits
        int remainingBits = binary.Length - i;
        if (remainingBits > 0)
        {
            int value = Convert.ToInt32(binary.Substring(i, remainingBits).PadRight(6, '0'), 2);
            base64.Append(base64Chars[value]);

            // Add padding based on how many bits were remaining
            base64.Append(new string('=', (6 - remainingBits) / 2));
        }

        return base64.ToString();
    }

    // Function to convert hex to Base64
    public static string HexToBase64(string hex)
    {
        string binary = HexToBinary(hex);
        return BinaryToBase64(binary);
    }

    public static void Main()
    {
        Console.Write("Enter hexadecimal string: ");
        string hexInput = Console.ReadLine();

        string base64Output = HexToBase64(hexInput);

        Console.WriteLine("Base64 encoded string: " + base64Output);
    }
}
