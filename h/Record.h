#include <utility>

class Record {
    struct HRecord {
        std::vector<unsigned char> data;
        HRecord(){
            data = std::vector<unsigned char>();
        }
    };
    HRecord *hr;
    static const uint16_t HEADER = 6;
public:
    Record(){
        hr = new HRecord();
    }
    friend class Api;
    unsigned char& operator[](const int i) {
        return hr->data[i];
    }
};
