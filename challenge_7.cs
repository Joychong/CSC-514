   
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class AESDecryptor
{
    static void Main()
    {
        // Specify the path to the file containing the Base64-encoded ciphertext
        string filePath = "challenge7.txt";  // Replace with the actual file path

        // Ensure the file exists
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            // Open the file and read the Base64-encoded ciphertext
            string base64CipherText = File.ReadAllText(filePath);

            // The key used for AES encryption/decryption (16 bytes for AES-128)
            string key = "YELLOW SUBMARINE";
            
            // Convert the Base64-encoded cipher text to byte array
            byte[] cipherText = Convert.FromBase64String(base64CipherText);
            
            // Perform the AES decryption
            byte[] decryptedData = DecryptAES(cipherText, key);

            // Convert the decrypted data to a readable string
            string decryptedText = Encoding.UTF8.GetString(decryptedData);
            
            // Output the decrypted text
            Console.WriteLine("Decrypted Text: " + decryptedText);
        }
        catch (FormatException)
        {
            Console.WriteLine("The file contents are not valid Base64 data.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }

    public static byte[] DecryptAES(byte[] cipherText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key); // 16 bytes for AES-128
            aesAlg.IV = new byte[16]; // ECB mode does not use an IV

            aesAlg.Mode = CipherMode.ECB; // Set cipher mode to ECB
            aesAlg.Padding = PaddingMode.PKCS7; // Standard padding for AES

            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
            {
                return PerformCryptography(cipherText, decryptor);
            }
        }
    }

    private static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
    {
        using (MemoryStream ms = new MemoryStream())
        using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
        {
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();
            return ms.ToArray();
        }
    }
}
