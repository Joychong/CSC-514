// Challenge 2

using System;
using System.Text;

public class Program
{
    // Fixed key for challenge
    private static string key1 = "686974207468652062756c6c277320657965";

    //****************************************************************************************
    //              Function to convert a hex string to binary string
    //****************************************************************************************
    public static string HexToBinary(string hex)
    {
        var binary = new StringBuilder();
        foreach (char c in hex)
        {
            int value = Convert.ToInt32(c.ToString(), 16);
            binary.Append(Convert.ToString(value, 2).PadLeft(4, '0'));
        }
        return binary.ToString();
    }

    //****************************************************************************************
    //              Function to convert binary string to hex string
    //****************************************************************************************
    public static string BinaryToHex(string binary)
    {
        var hex = new StringBuilder();
        for (int i = 0; i < binary.Length; i += 4)
        {
            int value = Convert.ToInt32(binary.Substring(i, 4), 2);
            hex.Append(value.ToString("x"));
        }
        return hex.ToString();
    }

    //****************************************************************************************
    //              Function to XOR two binary strings of equal length
    //****************************************************************************************
    public static string ToXOR(string bin1, string bin2)
    {
        var xorResult = new StringBuilder();
        for (int i = 0; i < bin1.Length; ++i)
        {
            xorResult.Append(bin1[i] == bin2[i] ? '0' : '1'); // XOR operation
        }
        return xorResult.ToString();
    }

    //****************************************************************************************
    //                              MAIN FUNCTION
    //****************************************************************************************
    public static void Main()
    {
        Console.Write("Enter the required input: ");
        string hexInput = Console.ReadLine();

        // Convert hex strings to binary
        string bin1 = HexToBinary(hexInput);
        string keyBin = HexToBinary(key1);

        // XOR the two binary strings
        string xorOutputBin = ToXOR(bin1, keyBin);

        // Convert XORed binary result back to hex
        string xorOutputHex = BinaryToHex(xorOutputBin);

        Console.WriteLine("XOR result (hex): " + xorOutputHex);
    }
}
