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
    void CreateNew();
    void Open();
    void Add();
    void Delete();
};