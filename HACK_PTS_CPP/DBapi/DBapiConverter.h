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
            uint8_t hash = rh->getHash();
            uint16_t size = rh->getSize();
            ofstream.write((char*)&hash, sizeof(hash));
            ofstream.write((char*)&size, sizeof(size));
            ofstream << rh->getHash() << rh->getSize();
            for (int i = 0; i < r.size(); ++i) {
                unsigned char b = r[i];
                ofstream.write((char*)&b, sizeof(b));
            }
        }
    }
    static void DBEncodeAppend(std::ofstream &ofstream, Record* r){
        uint64_t hash = r->getRHeader()->getHash();
        uint16_t size = r->getRHeader()->getSize();
        ofstream.write((char*)&hash, sizeof(hash));
        ofstream.write((char*)&size, sizeof(size));
        for (int i = 0; i < r->size(); ++i) {
            unsigned char b = (*r)[i];
            ofstream.write((char*)&b, sizeof(b));
        }
    }
    static int DatabaseDecode(std::ifstream &ifstream, Database* &db){
        uint16_t identifier;
        ifstream.read((char*)&identifier, sizeof(identifier));
        uint64_t size_;
        ifstream.read((char*)&size_, sizeof(size_));
        db->getDBHeader()->setSize(size_);

        uint64_t hash;
        uint16_t size;
        unsigned char b;
        while (!ifstream.eof()) {
            ifstream.read((char*)&hash, sizeof(hash));
            ifstream.read((char*)&size, sizeof(size));
            std::vector<unsigned char> data;
            for (int i = 0; i < size; ++i) {
                ifstream.read((char*)&b, sizeof(b));;
                data.push_back(b);
            }
            ///////// заглушка
            Record record(new RHeader(hash, size), data);
            db->RIn(record);
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
