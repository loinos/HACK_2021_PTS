class RHeader {
    uint64_t hash;
    uint16_t size;
public:
    RHeader(uint64_t hash, uint16_t size){
        this->hash = hash;
        this->size = size;
    }
};