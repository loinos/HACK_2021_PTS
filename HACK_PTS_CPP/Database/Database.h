class Database {
    DBHeader *dbh;
    std::vector<Record> db;
public:
    Database(){
        dbh = new DBHeader(0);
        db = std::vector<Record>();
    }
    ~Database(){
        delete dbh;
    }
    void RIn(std::vector<unsigned char> array){
        Record r(array);
        db.push_back(r);
    }
    void ROut();
};