#include <iostream>
#include <string>
#include <sstream>
#include <vector>
#include <cctype>
#include <map>
#include <iomanip>
#include <fstream>

using namespace std;

// Frequency of letters in English (normalized values)
map<char, double> english_frequencies = {
    {'a', 0.0651738}, {'b', 0.0124248}, {'c', 0.0217339}, {'d', 0.0349835},
    {'e', 0.1041442}, {'f', 0.0197881}, {'g', 0.0158610}, {'h', 0.0492888},
    {'i', 0.0558094}, {'j', 0.0009033}, {'k', 0.0050529}, {'l', 0.0331490},
    {'m', 0.0202124}, {'n', 0.0564513}, {'o', 0.0596302}, {'p', 0.0137645},
    {'q', 0.0008606}, {'r', 0.0497563}, {'s', 0.0515760}, {'t', 0.0729357},
    {'u', 0.0225134}, {'v', 0.0082903}, {'w', 0.0171272}, {'x', 0.0013692},
    {'y', 0.0145984}, {'z', 0.0007836}, {' ', 0.1918182}
};

// Convert a hex string to a vector of bytes
vector<uint8_t> hex_to_bytes(const string& hex) {
    vector<uint8_t> bytes;
    for (size_t i = 0; i < hex.length(); i += 2) {
        string byte_string = hex.substr(i, 2);
        uint8_t byte = (uint8_t)strtol(byte_string.c_str(), nullptr, 16);
        bytes.push_back(byte);
    }
    return bytes;
}

// XOR the byte vector with a single character key
string xor_with_key(const vector<uint8_t>& bytes, char key) {
    string result;
    for (auto byte : bytes) {
        result += (char)(byte ^ key);
    }
    return result;
}

// Score the plaintext based on English letter frequencies
double score_text(const std::string& text) {
    double score = 0;
    for (char c : text) {
        c = tolower(c);
        if (english_frequencies.find(c) != english_frequencies.end()) {
            score += english_frequencies[c];
        }
    }
    return score;
}

int main() {
    ifstream file("/Users/jaylawoods/Desktop/Projects/challenge problem 4 redo/challenge problem 4 redo/file.txt");
    string hex_input;
    double overall_best_score = -1;
    string overall_best_plaintext;
    char overall_best_key;
    string best_hex;

    // Read each line from the file
    while (getline(file, hex_input)) {
        vector<uint8_t> bytes = hex_to_bytes(hex_input);

        double best_score = -1;
        string best_plaintext;
        char best_key;

        // Try every possible single character (byte) as the XOR key
        for (int key = 0; key < 256; ++key) {
            string decrypted = xor_with_key(bytes, (char)key);
            double score = score_text(decrypted);

            // Keep track of the highest-scoring (best) result for the current string
            if (score > best_score) {
                best_score = score;
                best_plaintext = decrypted;
                best_key = key;
            }
        }

        // Keep track of the highest-scoring (best) result across all strings
        if (best_score > overall_best_score) {
            overall_best_score = best_score;
            overall_best_plaintext = best_plaintext;
            overall_best_key = best_key;
            best_hex = hex_input;
        }
    }

    // Output the best result
    cout << "Best key: " << hex << setw(2) << setfill('0') << (int)overall_best_key << endl;
    cout << "Hex input: " << best_hex << endl;
    cout << "Decrypted message: " << overall_best_plaintext << endl;

    return 0;
}
