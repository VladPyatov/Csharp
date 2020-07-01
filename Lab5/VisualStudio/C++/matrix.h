#pragma once
#include <iostream>
#include "vectorb.h"
class matrix
{
	int N;
	double** mat;
public:
	class Exept : public std::exception {
	public:
		virtual const char* what() const throw () { return msg; }
		char msg[256];
		Exept(const char* msg) {
			strcpy_s(this->msg, msg);
		}
	};
	double operator()(int, int);
	matrix(const matrix&);
	matrix();
	~matrix();
	matrix(int);
	matrix(int, int);
	matrix(double*, int);

	static matrix inverse(matrix m) {//обратная
		int i;
		matrix M(m.N);

		for (i = 0; i < m.N; i++) M.mat[i][i] = 1 / m.mat[i][i];

		return M;
	}

	int order();

	void set(int, int, double);
	matrix& operator =(const matrix&);
	friend std::ostream& operator << (std::ostream&, matrix);
	matrix operator +(matrix&);
	matrix& operator +=(matrix&);
	matrix operator -(matrix&);
	matrix& operator -=(matrix&);
	matrix operator *(matrix&);
	matrix& operator *=(matrix&);
	vectorb operator *(vectorb&);
	vectorb operator *(double*);
	double* operator [] (int);
};



