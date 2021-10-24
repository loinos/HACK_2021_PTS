#include <iostream>
#include <vector>
#include <fstream>
#include "Record.h"
#include "Database.h"
#include "Api.h"

int main() {
    unsigned char a[150];
    unsigned char b[100];
    unsigned char c[300];
    for (int i = 0; i < 300; ++i) {
        if (i < 150) a[i] = rand();
        if (i < 100) b[i] = rand();
        if (i < 300) c[i] = rand();
    }
    Api api("db.db");
    api.New();
    api.Set(a, 150);
    api.Update(0, b, 100);
    api.Set(b, 100);
    //api.Set(c, 300);
    return 0;
}
