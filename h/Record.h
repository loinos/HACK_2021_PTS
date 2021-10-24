#include <utility>

class Record {
    struct HRecord {
        uint64_t id;
        uint16_t fill;
        uint64_t size;
        std::vector<unsigned char> data;
        HRecord(uint64_t size){
            data = std::vector<unsigned char>();
            this->size = size;
        }
    };
    HRecord *hr;
    static const uint16_t HEADER = 14;
public:
    Record(uint64_t size){
        hr = new HRecord(size);
    }
    friend class Api;
    unsigned char& operator[](const int i) {
        return hr->data[i];
    }
};
