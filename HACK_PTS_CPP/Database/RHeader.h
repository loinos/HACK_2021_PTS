class RHeader {
    uint64_t id;
    bool is_free;
    uint16_t size;
public:
    static const uint64_t BASE_START = 11;
    RHeader(uint64_t id, bool is_free, uint16_t size){
        this->id = id;
        this->is_free = is_free;
        this->size = size;
    }
    uint64_t getId(){
        return id;
    }
    uint16_t getSize(){
        return size;
    }
    bool isFree(){
        return is_free;
    }
    friend class Record;
};