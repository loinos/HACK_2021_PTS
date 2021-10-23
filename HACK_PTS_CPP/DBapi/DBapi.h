class DBapi {
Database *db;
std::string path;
public:
    DBapi(std::string path){
        db = new Database();
        this->path = path;
    }
    ~DBapi(){
        delete db;

    }
    void CreateNew(){
        std::ofstream new_db;
        new_db.open(path);
        if (new_db.is_open()){
            DBapiConverter::DBHeaderEncode(new_db, db->getDBHeader());
        }
        new_db.close();
    }
    void Open() {
        std::ifstream new_db;
        new_db.open(path);
        if (new_db.is_open()){
            DBapiConverter::DatabaseDecode(new_db, db);
        }
    }
    void Add();
    void Delete();
};