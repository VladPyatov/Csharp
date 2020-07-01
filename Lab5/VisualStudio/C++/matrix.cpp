#include "pch.h"
#include "matrix.h"

double matrix::operator()(int i, int j) { return mat[i][j]; }

matrix::matrix(const matrix& m) {
	int i, j;
	N = m.N;
	mat = new double* [N];
	for (i = 0; i < N; i++)
		mat[i] = new double[N];

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			mat[i][j] = m.mat[i][j];

}

matrix::matrix() {
	N = 0;
	mat = NULL;
}

matrix::~matrix() {
	int i;
	for (i = 0; i < N; i++) delete[] mat[i];
	if (mat != NULL) delete[] mat;
}

matrix::matrix(int ord) {
	int i, j;
	N = ord;
	mat = new double* [N];
	for (i = 0; i < N; i++)
		mat[i] = new double[N];

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			mat[i][j] = 0;
}

matrix::matrix(int k, int ord) {
	int i, j;
	N = ord;
	mat = new double* [N];
	for (i = 0; i < N; i++)
		mat[i] = new double[N];

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			mat[i][j] = i == j ? k : 0;
}

matrix::matrix(double* d, int ord) {
	int i, j;
	N = ord;
	mat = new double* [N];
	for (i = 0; i < N; i++)
		mat[i] = new double[N];

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			mat[i][j] = i == j ? d[i] : 0;

}


int matrix::order() {
	return N;
}

void matrix::set(int i, int j, double val) {
	if (i < 1) throw Exept("wrong first parameter");
	if (j < 1) throw Exept("wrong second parameter");
	mat[i - 1][j - 1] = val;
}

matrix& matrix::operator =(const matrix& m) {
	int i, j;

	for (i = 0; i < N; i++) delete[] mat[i];
	if (mat != NULL) delete[] mat;
	N = m.N;
	mat = new double* [N];
	for (i = 0; i < N; i++)
		mat[i] = new double[N];

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			mat[i][j] = m.mat[i][j];

	return *this;

}

std::ostream& operator << (std::ostream& out, matrix m) {
	int i, j;

	for (i = 0; i < m.order(); ++i) {
		for (j = 0; j < m.order(); ++j)
			out << m(i, j) << ' ';
		out << std::endl;
	}

	return out;
}

//сложение матриц
matrix matrix::operator +(matrix& m) {
	int i, j;

	if (N != m.N) throw Exept("matrix 1 and 2 have different size");

	matrix out(N);

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			out.mat[i][j] = mat[i][j] + m.mat[i][j];

	return out;
}
matrix& matrix::operator +=(matrix& m) {
	*this = *this + m;

	return *this;
}

//вычитание матриц
matrix matrix::operator -(matrix& m) {
	int i, j;

	if (N != m.N) throw Exept("matrix 1 and 2 have different size");

	matrix out(N);

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			out.mat[i][j] = mat[i][j] - m.mat[i][j];

	return out;
}
matrix& matrix::operator -=(matrix& m) {
	*this = *this - m;

	return *this;
}

//умножение матриц
matrix matrix::operator *(matrix& m) {
	int i, j, k;
	if (N != m.N) throw Exept("can't multiply matrix 1 and 2");
	matrix out(N);

	for (i = 0; i < N; i++)
		for (j = 0; j < N; j++)
			for (k = 0; k < N; k++)
				out.mat[i][j] += mat[i][k] * m.mat[k][j];

	return out;
}
matrix& matrix::operator*=(matrix& m) {
	*this = *this * m;

	return *this;
}

//умножение матрицы на вектор
vectorb matrix::operator *(vectorb& v) {
	if (N != v.blockOrder() || v.order() > 1) throw Exept("can't multiply matrix to vector");
	vectorb out(1, v.blockOrder());

	for (int i = 0; i < out.blockOrder(); i++) {
		for (int j = 0; j < out.blockOrder(); j++)
			out[0][i] += mat[i][j] * v[0][j];
	}

	return out;
}
vectorb matrix::operator *(double* v) {
	vectorb out(1, N);

	for (int i = 0; i < out.blockOrder(); i++) {
		for (int j = 0; j < out.blockOrder(); j++)
			out[0][i] += mat[i][j] * v[j];
	}

	return out;
}

double* matrix::operator [] (int i) {
	return mat[i];
};