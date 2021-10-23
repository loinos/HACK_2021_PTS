class DBHeader {
    uint64_t size;
public:
    static const uint64_t BASE_START = 10;
    static const uint16_t IDENTIFIER = 24565;
    DBHeader(uint64_t size){
        this->size = size;
    }
    uint64_t getFreePlace(uint16_t size){

        return sizeof(uint64_t) - 1;
    }
    uint64_t getSize() const{
        return size;
    }
    uint64_t setSize(uint64_t size) {
        this->size = size;
    }

    friend class Database;
};
