using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    // Convert a hex string to a list of bytes
    static List<byte> HexToBytes(string hex)
    {
        List<byte> bytes = new List<byte>();
        for (int i = 0; i < hex.Length; i += 2)
        {
            string byteValue = hex.Substring(i, 2);
            bytes.Add(Convert.ToByte(byteValue, 16));
        }
        return bytes;
    }

    // Convert a plain text string to a list of bytes (hex format)
    static List<byte> StringToBytes(string text)
    {
        return Encoding.UTF8.GetBytes(text).ToList();
    }

    // XOR the plaintext with the repeating key
    static List<byte> RepeatingKeyXor(List<byte> plaintext, List<byte> key)
    {
        List<byte> result = new List<byte>();
        int keyLen = key.Count;

        for (int i = 0; i < plaintext.Count; i++)
        {
            result.Add((byte)(plaintext[i] ^ key[i % keyLen]));
        }

        return result;
    }

    // Convert a list of bytes to a hex string
    static string BytesToHex(List<byte> bytes)
    {
        StringBuilder hex = new StringBuilder(bytes.Count * 2);
        foreach (byte b in bytes)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }

    static void Main()
    {
        // Get the text input from the user
        Console.Write("Enter the text (plain text or hex): ");
        string input = Console.ReadLine();

        // Get the key from the user
        Console.Write("Enter the key: ");
        string key = Console.ReadLine();

        // Check if input is hex or plain text
        List<byte> plaintextBytes;
        if (input.All(c => "0123456789abcdefABCDEF".Contains(c)) && input.Length % 2 == 0)
        {
            // Treat input as hex
            plaintextBytes = HexToBytes(input);
        }
        else
        {
            // Treat input as plain text
            plaintextBytes = StringToBytes(input);
        }

        // Convert key to byte list
        List<byte> keyBytes = Encoding.UTF8.GetBytes(key).ToList();

        // Encrypt the plaintext using the repeating-key XOR
        List<byte> encrypted = RepeatingKeyXor(plaintextBytes, keyBytes);

        // Convert the encrypted result to a hex string and print it
        string encryptedHex = BytesToHex(encrypted);
        Console.WriteLine("Encrypted (hex): " + encryptedHex);
    }
}
