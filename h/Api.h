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

            // Пропустить блок данных
            iof.seekg(c_size);
        }

        // Если конец фаила - аппенд
        return FillBlock(iof, array, size, id);

    }
    Record Get(uint64_t id){
        std::ifstream in(path, std::ios::binary | std::ios::in);
        in.seekg(Database::HEADER, std::ios_base::beg);

        // Сдвиг, сколько бит пропускать
        // на каждой итерации
        uint16_t shift = 0;

        // Текущее положение каретки
        uint64_t c_id = -1;

        while (in.eof()) {

            // Если текущее положение искомое - возврат
            in.read((char*)&c_id, sizeof(c_id));
            if (c_id == id) return id;

            // Иначе сдвиг == 16 + 64 = 80 бит => 10 байт
            //uint64_t id; 64 = 8
            //uint16_t fill; 16 = 2
            //uint64_t c_size; 64 = 8
            shift = sizeof(uint16_t) + sizeof(uint64_t);
            in.seekg(shift);
        }

        return -1;
    }
    void Update(uint64_t id, unsigned char array[], uint64_t size){
        std::fstream iof(path, std::ios::binary | std::ios::in | std::ios::out);
        iof.seekp(Database::HEADER, std::ios_base::beg);

        uint64_t c_id = 0;
        uint16_t fill = 1;
        uint64_t c_size = size;

        while (iof.eof()) {

            iof.read((char*)&id, sizeof(id));

            // Если нужная запись - обнуляем
            // Меняем флаг заполненности на пусто
            if (c_id == id) {

                // Филлер, чтобы убить флаг
                uint16_t filler = 0;
                iof.write((char*)&filler, sizeof(filler));

                // Сдвиг каретки и филлер для убийства битов
                // А я не знаю как по другому
                iof.read((char*)&c_size, sizeof(c_size));
                uint8_t b = 0;
                for (int i = 0; i < c_size; ++i) {
                    iof.write((char*)&b, sizeof(b));
                }
                return;
            }

            iof.read((char*)&fill, sizeof(fill));
            iof.read((char*)&c_size, sizeof(c_size));

            iof.seekg(c_size);
        }
    }
    void Delete(uint64_t id){

    }
};
