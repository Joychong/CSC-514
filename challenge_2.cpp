//Challenge 2

#include <iostream>
#include <string>
#include <bitset>
#include <sstream>
using namespace std;

// Fixed key for challenge
string key1 = "686974207468652062756c6c277320657965";

//****************************************************************************************
//              Function to convert a hex string to binary string
//****************************************************************************************
string hexToBinary(const string& hex) {
    string binary;
    for (size_t i = 0; i < hex.length(); ++i) {
        binary += bitset<4>(stoi(hex.substr(i, 1), nullptr, 16)).to_string();
    }
    return binary;
}

//****************************************************************************************
//              Function to convert binary string to hex string
//****************************************************************************************
string binaryToHex(const string& binary) {
    stringstream hexStream;
    for (size_t i = 0; i < binary.length(); i += 4) {
        hexStream << hex << stoi(binary.substr(i, 4), nullptr, 2);
    }
    return hexStream.str();
}

//****************************************************************************************
//              Function to XOR two binary strings of equal length
//****************************************************************************************
string toXOR(const string& bin1, const string& bin2) {
    string xorResult;
    for (size_t i = 0; i < bin1.length(); ++i) {
        xorResult += (bin1[i] == bin2[i]) ? '0' : '1';  // XOR operation
    }
    return xorResult;
}

//****************************************************************************************
//                              MAIN FUNCTION
//****************************************************************************************
int main() {
    string hexInput;
    cout << "Enter the required input: ";
    cin >> hexInput;

    // Convert hex strings to binary
    string bin1 = hexToBinary(hexInput);
    string keyBin = hexToBinary(key1);

    // XOR the two binary strings
    string xorOutputBin = toXOR(bin1, keyBin);

    // Convert XORed binary result back to hex
    string xorOutputHex = binaryToHex(xorOutputBin);

    cout << "XOR result (hex): " << xorOutputHex << endl;

    return 0;
}
