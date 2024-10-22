//Challenge 6

#include <iostream>
#include <string>
#include <vector>
#include <tuple>
#include <limits>



int hamming_dist(const std::string& b1, const std::string& b2) {
    int diff = 0;
    size_t min_length = std::min(b1.size(), b2.size());
    for (size_t i = 0; i < min_length; ++i) {
        unsigned char xor_result = b1[i] ^ b2[i];
        int distance = 0;
        while (xor_result) {
            distance += xor_result & 1;
            xor_result >>= 1;
        }
        diff += distance;
    }
    return diff;
}

int main() {
    int min_hamming = 10000;
    int guessed_size = 0;
    std::vector<std::tuple<double, int>> distances;

    for (int keysize = 2; keysize < 40; ++keysize) {
        std::string g1 = ct.substr(0, keysize);
        std::string g2 = ct.substr(keysize, keysize);
        std::string g3 = ct.substr(keysize * 2, keysize);
        std::string g4 = ct.substr(keysize * 3, keysize);

        double dist = (hamming_dist(g1, g2) +
            hamming_dist(g2, g3) +
            hamming_dist(g3, g4)) / (3.0 * keysize);
        if (dist < min_hamming) {
            min_hamming = dist;
            guessed_size = keysize;
        }

        distances.emplace_back(dist, keysize);
    }

    std::cout << guessed_size << std::endl;

    return 0;
}

std::vector<std::vector<char>> divide_chunks(const std::vector<char>& c, size_t n) {
    std::vector<std::vector<char>> chunks;
    for (size_t i = 0; i < c.size(); i += n) {
        std::vector<char> chunk(c.begin() + i, c.begin() + std::min(i + n, c.size()));
        chunks.push_back(chunk);
    }
    return chunks;
}

int score(const std::string& s) {
    int num_valid = 0;
    for (char j : s) {
        // space or lowercase character
        if (static_cast<int>(j) == 32 || (static_cast<int>(j) >= 97 && static_cast<int>(j) <= 122)) {
            num_valid += 1;
        }
    }
    return num_valid;
}
