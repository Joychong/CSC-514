//Challenge 8 

#include <iostream>
#include <fstream>
#include <vector>
#include <set>
#include <string>

using namespace std;

// Function to convert a hex string to a vector of bytes
vector<uint8_t> hexToBytes(const string& hex) {
    vector<uint8_t> bytes;
    for (size_t i = 0; i < hex.length(); i += 2) {
        string byteString = hex.substr(i, 2);
        uint8_t byte = static_cast<uint8_t>(stoi(byteString, nullptr, 16));
        bytes.push_back(byte);
    }
    return bytes;
}

// Function to detect if a ciphertext has been encrypted using ECB mode
bool hasRepeatedBlocks(const vector<uint8_t>& ciphertext, size_t blockSize) {
    set<vector<uint8_t>> uniqueBlocks;
    for (size_t i = 0; i < ciphertext.size(); i += blockSize) {
        vector<uint8_t> block(ciphertext.begin() + i, ciphertext.begin() + i + blockSize);
        if (uniqueBlocks.find(block) != uniqueBlocks.end()) {
            return true; // Repeated block found
        }
        uniqueBlocks.insert(block);
    }
    return false;
}

int main() {
    ifstream file("Cryptopals8.txt");
    vector<string> ciphertexts;
    string line;

    // Read ciphertexts from the file
    if (file.is_open()) {
        while (getline(file, line)) {
            if (!line.empty()) {
                ciphertexts.push_back(line);
            }
        }
        file.close();
    }
    else {
        cerr << "Unable to open file" << endl;
        return 1;
    }

    size_t blockSize = 16; // AES block size (16 bytes)

    // Detect ECB-encrypted ciphertext
    for (size_t i = 0; i < ciphertexts.size(); ++i) {
        vector<uint8_t> ciphertextBytes = hexToBytes(ciphertexts[i]);
        if (hasRepeatedBlocks(ciphertextBytes, blockSize)) {
            cout << "ECB-encrypted ciphertext found at index " << i << endl;
            cout << "Ciphertext: " << ciphertexts[i] << endl;
        }
    }

    return 0;
}
