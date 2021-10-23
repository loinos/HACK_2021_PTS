class Record {
    RHeader *rh;
    std::vector<unsigned char> data;
public:
    Record(const std::vector<unsigned char>& data) {
        this->data = data;
        rh = new RHeader(false, data.size());
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