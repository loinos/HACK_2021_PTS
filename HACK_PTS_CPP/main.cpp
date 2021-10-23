#include <iostream>
#include <vector>
#include "fstream"
#include "Database/DBHeader.h"
#include "Database/RHeader.h"
#include "Database/Record.h"
#include "Database/Database.h"
#include "DBapi/DBapiConverter.h"
#include "DBapi/DBapi.h"

int main() {
    DBapi dBapi("test.ff");
    std::vector<unsigned char> a;
    dBapi.CreateNew();
    for (int i = 0; i < 100; ++i) {
        a.push_back(rand() % 255);
    }
    dBapi.Add(a);
    dBapi.Open();
    return 0;
}

