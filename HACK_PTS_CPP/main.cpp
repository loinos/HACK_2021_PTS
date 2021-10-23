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
    dBapi.CreateNew();
    dBapi.Open();
    //dBapi.CreateNew();
    return 0;
}

