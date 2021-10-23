class RHeader {
    bool is_free;
    uint16_t size;
public:
    static const uint64_t BASE_START = 10;
    RHeader(bool is_free, uint16_t size){
        this->is_free = is_free;
        this->size = size;
    }
    uint16_t getSize(){
        return size;
    }
    bool isFree(){
        return is_free;
    }
    friend class Record;
};