// Programmer: Muhammad Asjad Rehman Hashmi
// Program: Challenge 1 set 1

#include <iostream>
#include <string>
#include <bitset>
#include <sstream>

using namespace std;


// Base64 characters table
const string base64_chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" "abcdefghijklmnopqrstuvwxyz" "0123456789+/";

// Function to convert a hex string to binary string
    string hexToBinary(const string& hex) {
    string binary;

    // Convert each hex digit to a 4-bit binary string
    for (size_t i = 0; i < hex.length(); ++i) {
        binary += bitset<4>(stoi(hex.substr(i, 1), nullptr, 16)).to_string();
    }
    return binary;
}

// Function to convert a binary string to Base64 string
string binaryToBase64(const string& binary) {
    string base64;
    int i = 0;

    // Process 6-bit chunks of the binary string
    while (i + 6 <= binary.length()) {
        // Convert each 6-bit chunk to an integer, then to a Base64 character
        int value = bitset<6>(binary.substr(i, 6)).to_ulong();
        base64 += base64_chars[value];
        i += 6;
    }

    // Handle remaining bits 
    int remaining_bits = binary.length() - i;
    if (remaining_bits > 0) {
        bitset<6> last_chunk(binary.substr(i, remaining_bits));
        base64 += base64_chars[last_chunk.to_ulong()];

        // Add padding based on how many bits were remaining
        base64 += string((6 - remaining_bits) / 2, '=');
    }

    return base64;
}

// Function to convert hex to Base64
string hexToBase64(const std::string& hex) {
    string binary = hexToBinary(hex);
    return binaryToBase64(binary);
}

int main() {
    string hexInput;

    cout << "Enter hexadecimal string: ";
    cin >> hexInput;

    string base64Output = hexToBase64(hexInput);

    cout << "Base64 encoded string: " << base64Output << endl;

    return 0;
}
