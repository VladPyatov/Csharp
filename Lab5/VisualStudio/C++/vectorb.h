#pragma once
#include <iostream>

class vectorb
{
	int N, blockN;
	double** vec;
public:
	class Exept : public std::exception {
	public:
		virtual const char* what() const throw () { return msg; }
		char msg[256];
		Exept(const char* msg) {
			strcpy_s(this->msg, msg);
		}
	};
	vectorb(const vectorb&);
	vectorb();
	//~vectorb();
	vectorb(int, int);
	vectorb(int, int, bool);
	vectorb(double* d, int);
	int order();
	int blockOrder();
	friend std::ostream& operator << (std::ostream&, vectorb);
	double* operator [] (int i);
	vectorb operator +(vectorb&);
	vectorb& operator +=(vectorb&);
	vectorb& operator +=(vectorb);
	vectorb operator -(vectorb&);
	vectorb& operator -=(vectorb&);
	vectorb& operator -=(vectorb);
};

