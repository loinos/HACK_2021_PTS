class Api {
    Database *db;
    std::string path;
    
public:
    Api(std::string path) {
        this->path = path;
        db = new Database();
    }
    void New() {
        std::ofstream out(path, std::ios::binary | std::ios::out);
        uint16_t identification = Database::IDENTIFICATION;
        uint16_t header = Database::HEADER;
        out.write((char*)&identification, sizeof(identification));
        out.seekp(10, std::ios_base::beg);
        out.write((char*)&header, sizeof(header));
        out.close();
    }
    int Set(unsigned char array[], int size){
        std::fstream iof(path, std::ios::binary | std::ios::out | std::ios::in);
        iof.seekp(Database::HEADER, std::ios_base::beg);
        if (iof.eof()) {
            iof.seekp(Record::HEADER);
            for (int i = 0; i < size; ++i) {
                unsigned char b = array[i];
                iof.write((char*)&b, sizeof(b));
            }
        }
        while (iof.eof()) {

        }
        return 0;
    }
    Record Get(uint64_t id){

    }
    void Update(uint64_t id){

    }
    void Delete(uint64_t id){

    }
};
