class DBapiConverter {
public:
    static void DBHeaderEncode(std::ofstream &ofstream, DBHeader* dbh){
        uint64_t size = dbh->getSize();
        ofstream << std::hex << DBHeader::IDENTIFIER;
        ofstream << std::hex << size;
    }
    static void DBEncode(std::ofstream &ofstream, Database* db){
        DBHeaderEncode(ofstream, db->getDBHeader());
        std::vector<Record> array = db->GetDb();
        for (auto r : array){
            RHeader *rh = r.getRHeader();
            ofstream << rh->getHash() << rh->getSize();
        }
    }
    static int DatabaseDecode(std::ifstream &ifstream, Database* &db){
        unsigned char b;
        uint16_t identifier;
        ifstream >> identifier;
        if (identifier != DBHeader::IDENTIFIER) {
            return -1;
        }
        uint64_t size_;
        ifstream >> size_;
        db->getDBHeader()->setSize(size_);

        uint64_t hash;
        uint16_t size;
        while (ifstream >> hash) {
            ifstream >> size;
            std::vector<unsigned char> data(size);
            for (int i = 0; i < size; ++i) {
                ifstream >> b;
                data.push_back(b);
            }
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
};