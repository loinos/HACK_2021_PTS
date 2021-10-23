class Record {
    RHeader *rh;
    std::vector<unsigned char> data;
public:
    Record(const std::vector<unsigned char>& data) {
        rh = new RHeader(1, data.size());
        this->data = data;
    }
    ~Record(){
        delete rh;
    }
    RHeader* getRHeader(){
        return rh;
    }
};