class Database {
    struct DBHeader{
        std::vector<Record> data;
        DBHeader(){
            data = std::vector<Record>();
        }
    };
    DBHeader* dbh;
public:
    Database(){
        dbh = new DBHeader();
    }
};