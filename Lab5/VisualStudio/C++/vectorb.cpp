#include "pch.h"
#include "vectorb.h"

vectorb::vectorb(const vectorb& m) {
	int i, j;
	N = m.N;
	blockN = m.blockN;
	vec = new double* [N];
	for (i = 0; i < N; i++)
		vec[i] = new double[blockN];

	for (i = 0; i < N; i++)
		for (j = 0; j < blockN; j++)
			vec[i][j] = m.vec[i][j];

}

vectorb::vectorb() {
	N = 0;
	blockN = 0;
	vec = NULL;
}

vectorb::vectorb(int n, int blockn) {
	int i, j;
	N = n;
	blockN = blockn;
	vec = new double* [N];
	for (i = 0; i < N; i++)
		vec[i] = new double[blockN];

	for (i = 0; i < N; i++)
		for (j = 0; j < blockN; j++)
			vec[i][j] = 0;

}

vectorb::vectorb(int n, int blockn, bool test) {
	int i, j;
	N = n;
	blockN = blockn;
	vec = new double* [N];
	for (i = 0; i < N; i++)
		vec[i] = new double[blockN];

	for (i = 0; i < N; i++)
		for (j = 0; j < blockN; j++)
			vec[i][j] = j;
}
vectorb::vectorb(double* d, int b) {
	int i, j;
	N = 1;
	blockN = b;
	vec = new double* [N];
	vec[0] = new double[blockN];
	for (j = 0; j < blockN; j++)
		vec[0][j] = d[j];
}

//vectorb::~vectorb() {
//	int i;
//	for (i = 0; i < N; i++) delete[] vec[i];
//	if (vec != NULL) delete[] vec;
//}

int vectorb::order() {
	return N;
}

int vectorb::blockOrder() {
	return blockN;
}


std::ostream& operator << (std::ostream& out, vectorb v) {
	int i, j;
	out << "(";
	for (i = 0; i < v.order(); ++i) {
		out << "[";
		for (j = 0; j < v.blockOrder(); ++j) {
			out << v.vec[i][j];
			if (j != v.blockOrder() - 1)
				out << ',';
		}
		out << "]";
		if (i != v.order() - 1)
			out << ',';

	}

	out << ")\n";

	return out;
}

double* vectorb::operator [] (int i) {
	return vec[i];
}


//сложение векторов
vectorb vectorb::operator +(vectorb& v) {
	int i, j;

	if (N != v.N || blockN != v.blockN) throw Exept("vector 1 and vector 2 have different size");

	vectorb out(N, blockN);

	for (i = 0; i < N; i++)
		for (j = 0; j < blockN; j++)
			out.vec[i][j] = vec[i][j] + v.vec[i][j];

	return out;
}


vectorb& vectorb::operator +=(vectorb& v) {
	*this = *this + v;

	return *this;
}

vectorb& vectorb::operator +=(vectorb v) {
	*this = *this + v;

	return *this;
}

//вычитание векторов
vectorb vectorb::operator -(vectorb& v) {
	int i, j;

	if (N != v.N || blockN != v.blockN) throw Exept("vector 1 and vector 2 have different size");

	vectorb out(N, blockN);

	for (i = 0; i < N; i++)
		for (j = 0; j < blockN; j++)
			out.vec[i][j] = vec[i][j] - v.vec[i][j];

	return out;
}


vectorb& vectorb::operator -=(vectorb& v) {
	*this = *this - v;

	return *this;
}

vectorb& vectorb::operator -=(vectorb v) {
	*this = *this - v;

	return *this;
}