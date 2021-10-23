class Record {
    RHeader *rh;
    std::vector<unsigned char> data;
public:
    Record(const std::vector<unsigned char>& data) {
        rh = new RHeader(1, data.size(), false);
        this->data = data;
    }
    Record(RHeader *rh, const std::vector<unsigned char>& data) {
        this->rh = rh;
        this->data = data;
    }
    ~Record(){
        //delete rh;
    }
    RHeader* getRHeader(){
        return rh;
    }
    int size(){
        return data.size();
    }
    unsigned char operator[](int i){
        return data[i];
    }
    friend class Database;
};