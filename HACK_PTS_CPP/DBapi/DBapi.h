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
        std::ofstream new_db(path, std::ios::binary | std::ios::trunc);
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
        new_db.close();
    }
    void Add(std::vector<unsigned char> data) {
        std::ofstream new_db(path, std::ios::binary | std::ios::app);
        if (new_db.is_open()){
            DBapiConverter::DBEncodeAppend(new_db, new Record(data));
        }
        new_db.close();
    }
    int Add_2(std::vector<unsigned char> data) {
        std::ifstream in(path, std::ios::binary | std::ios::out);
        if (in.is_open()){
            return DBapiConverter::DBEncodeRecord(in, path, new Record(data));
        }
        in.close();
    }
    void Delete(uint64_t id){
        std::ifstream new_db(path, std::ios::binary | std::ios::out);
        if (new_db.is_open()){
            DBapiConverter::Delete(new_db, path, id);
        }
        new_db.close();
    }
    void Change(uint64_t id, std::vector<unsigned int> array){
        std::ifstream new_db(path, std::ios::binary | std::ios::out);
        if (new_db.is_open()){
            DBapiConverter::Find(new_db, id);
        }
        new_db.close();
    }
};