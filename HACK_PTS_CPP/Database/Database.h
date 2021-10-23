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

    DBHeader** GetDBHeader(){
        return &dbh;
    }

    std::vector<Record> GetDb(){
        return db;
    }
    void RIn(const Record& r){
        db.push_back(r);
    }
    void ROut();
    DBHeader* getDBHeader(){
        return dbh;
    }
};