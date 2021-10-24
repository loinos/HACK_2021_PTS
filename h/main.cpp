#include <iostream>
#include <vector>
#include <fstream>
#include "Record.h"
#include "Database.h"
#include "Api.h"

int main() {
    Api api("db.db");
    api.New();
    unsigned char arr[]{1,2,3,4,5,6,7,8,9};
    api.Set(arr);
    return 0;
}
