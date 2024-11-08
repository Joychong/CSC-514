
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class CryptopalsChallenge8
{
    // Function to convert a hex string to a byte array
    static byte[] HexToBytes(string hex)
    {
        int length = hex.Length / 2;
        byte[] bytes = new byte[length];
        for (int i = 0; i < length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return bytes;
    }

    // Function to detect if a ciphertext has been encrypted using ECB mode
    static bool HasRepeatedBlocks(byte[] ciphertext, int blockSize)
    {
        HashSet<string> uniqueBlocks = new HashSet<string>();
        for (int i = 0; i < ciphertext.Length; i += blockSize)
        {
            byte[] block = ciphertext.Skip(i).Take(blockSize).ToArray();
            string blockString = BitConverter.ToString(block);
            if (uniqueBlocks.Contains(blockString))
            {
                return true; // Repeated block found
            }
            uniqueBlocks.Add(blockString);
        }
        return false;
    }

    static void Main()
    {
        string filePath = "challenge8.txt";
        List<string> ciphertexts = new List<string>();

        // Read ciphertexts from the file
        try
        {
            var lines = File.ReadAllLines(filePath);
            ciphertexts.AddRange(lines.Where(line => !string.IsNullOrEmpty(line)));
        }
        catch (Exception)
        {
            Console.WriteLine("Unable to open file");
            return;
        }

        int blockSize = 16; // AES block size (16 bytes)

        // Detect ECB-encrypted ciphertext
        for (int i = 0; i < ciphertexts.Count; i++)
        {
            byte[] ciphertextBytes = HexToBytes(ciphertexts[i]);
            if (HasRepeatedBlocks(ciphertextBytes, blockSize))
            {
                Console.WriteLine($"ECB-encrypted ciphertext found at index {i}");
                Console.WriteLine($"Ciphertext: {ciphertexts[i]}");
            }
        }
    }
}
