#pragma once
#include <iostream>
#include "vectorb.h"
#include "matrix.h"
#include <fstream>
class blockmatrix
{
	int N, blockN;
	
public:
	matrix* A;
	matrix* C;
	matrix* B;
	class Exept : public std::exception {
	public:
		virtual const char* what() const throw () { return msg; }
		char msg[256];
		Exept(const char* msg) {
			strcpy_s(this->msg, msg);
		}
	};
	blockmatrix(const blockmatrix&);
	blockmatrix();
	~blockmatrix();
	friend std::ostream& operator << (std::ostream&, blockmatrix);
	blockmatrix(int, int);
	vectorb vecMul(vectorb);
	vectorb SLAU(vectorb);
	void save(vectorb, vectorb);

};


