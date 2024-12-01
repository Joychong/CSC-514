//Challenge 5

#include <iostream>
#include <string>
#include <vector>
#include <iomanip>
#include <sstream>

using namespace std;

// Convert a string to a vector of bytes
vector<uint8_t> string_to_bytes(const string& str) {
    return vector<uint8_t>(str.begin(), str.end());
}

// XOR the plaintext with the repeating key
vector<uint8_t> repeating_key_xor(const vector<uint8_t>& plaintext, const vector<uint8_t>& key) {
    vector<uint8_t> result;
    size_t key_len = key.size();
    
    for (size_t i = 0; i < plaintext.size(); ++i) {
        // XOR the i-th byte of plaintext with the corresponding key byte
        result.push_back(plaintext[i] ^ key[i % key_len]);
    }
    
    return result;
}

// Convert a vector of bytes to a hex string
string bytes_to_hex(const vector<uint8_t>& bytes) {
    stringstream hex_stream;
    for (auto byte : bytes) {
        hex_stream << hex << setw(2) << setfill('0') << (int)byte;
    }
    return hex_stream.str();
}

int main() {
    // Define the plaintext and the key
    string plaintext = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
    string key = "ICE";

    // Convert the plaintext and key to byte vectors
    vector<uint8_t> plaintext_bytes = string_to_bytes(plaintext);
    vector<uint8_t> key_bytes = string_to_bytes(key);

    // Encrypt the plaintext using the repeating-key XOR
    vector<uint8_t> encrypted = repeating_key_xor(plaintext_bytes, key_bytes);

    // Convert the encrypted result to a hex string and print it
    string encrypted_hex = bytes_to_hex(encrypted);
    cout << "Encrypted (hex): " << encrypted_hex << endl;

    return 0;
}

