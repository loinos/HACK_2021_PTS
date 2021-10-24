class Database {
    struct DBHeader{
        std::vector<Record> data;
        DBHeader(){
            data = std::vector<Record>();
        }
    };
    DBHeader* dbh;
    static const uint16_t IDENTIFICATION = 24565;
    static const uint16_t HEADER = 12;
public:
    Database(){
        dbh = new DBHeader();
    }
    friend class Api;
};