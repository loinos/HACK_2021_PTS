class DBHeader {
uint64_t size;
public:
    static const uint16_t IDENTIFIER = 24565;
    DBHeader(uint64_t size){
        this->size = size;
    }
    uint64_t getSize() const{
        return size;
    }
};