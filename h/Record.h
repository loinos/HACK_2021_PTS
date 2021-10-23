class Record {
    struct HRecord {
        std::vector<unsigned char> data;
        HRecord(){
            data = std::vector<unsigned char>();
        }
    };
    HRecord *hr;
public:
    Record(){
        hr = new HRecord();
    }
};