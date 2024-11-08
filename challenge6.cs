using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class RepeatingKeyXOR
{
    // Step 1: Calculate Hamming distance between two byte arrays
    public static int HammingDistance(byte[] a, byte[] b)
    {
        int distance = 0;
        for (int i = 0; i < a.Length; i++)
        {
            byte xor = (byte)(a[i] ^ b[i]);
            while (xor != 0)
            {
                distance += xor & 1;
                xor >>= 1;
            }
        }
        return distance;
    }

    // Step 2: Normalize the Hamming distance
    public static double NormalizedHammingDistance(byte[] cipherText, int keySize)
    {
        int blocks = cipherText.Length / keySize;
        double totalDistance = 0;

        for (int i = 0; i < blocks - 1; i++)
        {
            byte[] block1 = cipherText.Skip(i * keySize).Take(keySize).ToArray();
            byte[] block2 = cipherText.Skip((i + 1) * keySize).Take(keySize).ToArray();
            totalDistance += HammingDistance(block1, block2);
        }

        return totalDistance / (keySize * (blocks - 1));
    }

    // Step 3: Find the best key size
    public static int FindBestKeySize(byte[] cipherText)
    {
        double bestDistance = double.MaxValue;
        int bestKeySize = 0;

        for (int keySize = 2; keySize <= 40; keySize++)
        {
            double distance = NormalizedHammingDistance(cipherText, keySize);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestKeySize = keySize;
            }
        }
        return bestKeySize;
    }

    // Step 4: Break ciphertext into blocks of key size and transpose
    public static byte[][] TransposeBlocks(byte[] cipherText, int keySize)
    {
        int blockCount = cipherText.Length / keySize;
        byte[][] transposedBlocks = new byte[keySize][];

        for (int i = 0; i < keySize; i++)
        {
            transposedBlocks[i] = new byte[blockCount];
            for (int j = 0; j < blockCount; j++)
            {
                transposedBlocks[i][j] = cipherText[i + j * keySize];
            }
        }
        return transposedBlocks;
    }

    // Step 5: Solve each block as a single-byte XOR problem
    public static byte FindSingleByteXORKey(byte[] block)
    {
        int bestScore = int.MaxValue;
        byte bestKey = 0;
        for (byte key = 0; key < 256; key++)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in block)
            {
                sb.Append((char)(b ^ key));
            }

            int score = ScorePlaintext(sb.ToString());
            if (score < bestScore)
            {
                bestScore = score;
                bestKey = key;
            }
        }
        return bestKey;
    }

    // Simple scoring function based on character frequency
    public static int ScorePlaintext(string text)
    {
        int score = 0;
        string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 \t\n\r";
        foreach (char c in text)
        {
            if (!validChars.Contains(c))
                score++;
        }
        return score;
    }

    // Decrypt the ciphertext
    public static string Decrypt(byte[] cipherText, int keySize)
    {
        byte[][] transposedBlocks = TransposeBlocks(cipherText, keySize);
        byte[] key = new byte[keySize];

        // Find the key for each block
        for (int i = 0; i < keySize; i++)
        {
            key[i] = FindSingleByteXORKey(transposedBlocks[i]);
        }

        // Decrypt the entire ciphertext using the key
        StringBuilder decryptedText = new StringBuilder();
        for (int i = 0; i < cipherText.Length; i++)
        {
            decryptedText.Append((char)(cipherText[i] ^ key[i % keySize]));
        }
        return decryptedText.ToString();
    }

    public static void Main(string[] args)
    {
        // Read the Base64-encoded ciphertext from a file
        string base64Text = System.IO.File.ReadAllText("challenge6.txt");
        byte[] cipherText = Convert.FromBase64String(base64Text);

        // Step 6: Find the best key size
        int bestKeySize = FindBestKeySize(cipherText);
        Console.WriteLine($"Best Key Size: {bestKeySize}");

        // Step 7: Decrypt the ciphertext using the found key size
        string decryptedText = Decrypt(cipherText, bestKeySize);
        Console.WriteLine($"Decrypted Text: {decryptedText}");
    }
}

