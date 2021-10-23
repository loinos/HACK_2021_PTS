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
        std::ofstream new_db(path, std::ios::binary);
        if (new_db.is_open()){
            DBapiConverter::DBHeaderEncode(new_db, db->getDBHeader());
        }
        new_db.close();
    }
    void Open() {
        std::ifstream new_db(path, std::ios::binary);
        if (new_db.is_open()){
            DBapiConverter::DatabaseDecode(new_db, db);
        }
    }
    void Add(std::vector<unsigned char> data) {
        std::ofstream new_db(path, std::ios::binary | std::ios::app);
        if (new_db.is_open()){
            DBapiConverter::DBEncodeAppend(new_db, new Record(data));
        }
        new_db.close();
    }

    void Delete(uint16_t id){
        std::ifstream new_db(path, std::ios::binary);
        if (new_db.is_open()){
            DBapiConverter::Find(new_db, id);
        }
        new_db.close();
    }
    void Change();
};