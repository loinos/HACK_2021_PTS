class DBapiConverter {
public:
    static void DBHeaderEncode(std::ofstream &ofstream, DBHeader* dbh){
        uint64_t size = dbh->getSize();
        uint16_t identifier = DBHeader::IDENTIFIER;
        ofstream.write((char*)&identifier, sizeof(identifier));
        ofstream.write((char*)&size, sizeof(size));
    }
    static void DBEncode(std::ofstream &ofstream, Database* db){
        std::vector<Record> array = db->GetDb();
        for (auto r : array){
            RHeader *rh = r.getRHeader();
            uint16_t size = rh->getSize();
            ofstream.write((char*)&size, sizeof(size));
            for (int i = 0; i < r.size(); ++i) {
                unsigned char b = r[i];
                ofstream.write((char*)&b, sizeof(b));
            }
        }
    }
    static void DBEncodeAppend(std::ofstream &ofstream, Record* r){
        uint16_t size = r->getRHeader()->getSize();
        bool is_free = r->getRHeader()->isFree();
        ofstream.write((char*)&is_free, sizeof(is_free));
        ofstream.write((char*)&size, sizeof(size));
        for (int i = 0; i < r->size(); ++i) {
            unsigned char b = (*r)[i];
            ofstream.write((char*)&b, sizeof(b));
        }
    }

    static int DBEncodeRecord(std::ifstream &in, std::ofstream &out, std::string path, Record* r){
        in.seekg(DBHeader::BASE_START - 1);
        uint16_t size;
        bool is_free;
        int i = 0;
        while (!out.eof()) {
            in.read((char*)&is_free, sizeof(is_free));
            in.read((char*)&size, sizeof(size));
            if (is_free){
                in.seekg(size);
                ++i;
            }
            if (r->size() < size) {
                in.close();
                out.open(path);
                for (int i = 0; i < r->size(); ++i) {
                    unsigned char b = (*r)[i];
                    out.write((char*)&b, sizeof(b));
                }
                out.close();
                return i;
            }
            in.seekg(size);
            ++i;
        }
    }
    static int DatabaseDecode(std::ifstream &ifstream, Database* &db){
        uint16_t identifier;
        ifstream.read((char*)&identifier, sizeof(identifier));
        uint64_t size_;
        ifstream.read((char*)&size_, sizeof(size_));
        db->getDBHeader()->setSize(size_);

        bool is_free = true;
        uint16_t size;
        unsigned char b;
        while (ifstream.read((char*)&is_free, sizeof(is_free))) {
            //ifstream.read((char*)&is_free, sizeof(is_free));
            ifstream.read((char*)&size, sizeof(size));
            std::vector<unsigned char> data;
            for (int i = 0; i < size; ++i) {
                ifstream.read((char*)&b, sizeof(b));;
                data.push_back(b);
            }
            RHeader *rHeader = new RHeader(is_free, size);
            Record record(rHeader, data);
            db->RIn(record);
        }
    }
    ///////////////////
    static Record* Find(std::ifstream &ifstream, int i){
        uint8_t point = 1;
        ifstream.seekg(DBHeader::BASE_START - 1);
        uint16_t size;
        while (!ifstream.eof()) {
            ifstream.read((char*)&point, sizeof(point));
            ifstream.read((char*)&size, sizeof(size));
            if (i < 0) {
                unsigned char data[size];
                ifstream.read((char*)&data, sizeof(data));
                std::vector<unsigned char> v_data;
                for (int j = 0; j < size; ++j) {
                    v_data.push_back(data[j]);
                }
                Record* r = new Record(v_data);
                return r;
            }
            ifstream.seekg(point + size);
        }
        return nullptr;
    }
    static uint64_t FindFree(std::ifstream &ifstream, int r_size){
        uint64_t point = DBHeader::BASE_START;
        bool is_free = true;
        uint16_t size;
        ifstream.seekg(DBHeader::BASE_START);
        while (ifstream) {
            ifstream.read((char*)&point, sizeof(point));
            ifstream.read((char*)&is_free, sizeof(is_free));
            ifstream.read((char*)&size, sizeof(size));
            if (is_free && size > r_size) {
                return point;
            }
            ifstream.seekg(point + size);
        }
    }



    static unsigned char* toByteArray(uint64_t item){
        unsigned char r[sizeof(item)];
        for (int i = 0; i < sizeof(item); ++i) {
            r[i] = item;
            item ^= r[i];
            item >>= 1;
        }
        for (int i = sizeof(item) - 1; i >= 0; --i) {
            item |= r[i];
            if (i != 0) item <<= 1;
        }
    }
    template <typename T>
    static T DecodeVariable(std::fstream &ifstream, const T &t, int size){
        unsigned char b = 0;
        T item = 0;
        for (int i = 0; i < size; ++i) {
            item <<= 8;
            ifstream >> b;
            item |= b;
        }
        return item;
    }
};
