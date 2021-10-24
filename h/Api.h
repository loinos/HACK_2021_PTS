class Api {
    Database *db;
    std::string path;

    // Заполнение блока
    int FillBlock(std::fstream &iof, unsigned char array[], int size, int id) {
        uint16_t fill = 1;

        iof.write((char*)&id, sizeof(id));
        iof.write((char*)&fill, sizeof(fill));
        iof.write((char*)&size, sizeof(size));
        for (int i = 0; i < size; ++i) {
            unsigned char b = array[i];
            iof.write((char*)&b, sizeof(b));
        }
        iof.close();
        return id;
    }
public:

    // Говори адрес
    Api(std::string path) {
        this->path = path;
        db = new Database();
    }

    // Обязательно перед заполнением базы. Задает метаданные базы
    void New() {
        std::ofstream out(path, std::ios::binary | std::ios::out);
        uint16_t identification = Database::IDENTIFICATION;
        uint16_t header = Database::HEADER;
        out.write((char*)&identification, sizeof(identification));
        out.seekp(10, std::ios_base::beg);
        out.write((char*)&header, sizeof(header));
        out.close();
    }
    int Set(unsigned char array[], int size){
        std::fstream iof(path, std::ios::binary | std::ios::out | std::ios::in);
        iof.seekp(Database::HEADER, std::ios_base::beg);

        // В порядке нахождения в базе
        uint64_t id = 0;
        uint16_t fill = 1;
        uint64_t c_size = size;

        // Заполнение первого элемента
        if (iof.eof()) {
            return FillBlock(iof, array, size, id);
        }

        // Блин опасно
        while (iof.eof()) {

            // Получаем текущий коммит
            iof.read((char*)&id, sizeof(id));
            iof.read((char*)&fill, sizeof(fill));
            iof.read((char*)&c_size, sizeof(c_size));

            // Ошибка или блок пуст
            if(fill == 0)
                // Ошибка или свободный блок
                // Если свободен - заполняем
                if (size < c_size) {
                    return FillBlock(iof, array, size, id);
                }
        }

        // Если конец фаила - аппенд
        return FillBlock(iof, array, size, id);

    }
    Record Get(uint64_t id){

    }
    void Update(uint64_t id){

    }
    void Delete(uint64_t id){

    }
};
