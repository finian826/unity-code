#include <mysql/mysql.h>
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <fstream>
#include <string>
#include "c_tokenizer.h"
using namespace std;

void DoSetup() 
{
	// Table Setup


}

void ReadConfig() 
{
	string line;
	const char* token;
	unsigned    n;
	
	ifstream myfile ("config.ini");
	if (myfile.is_open())
	{
		while ( getline (myfile,line) )
		{
			tokenizer_t tok = tokenizer( line, ",", TOKENIZER_EMPTIES_OK );
		}
		myfile.close();
	}
}