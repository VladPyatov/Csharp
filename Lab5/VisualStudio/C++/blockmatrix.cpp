#include "pch.h"
#include "blockmatrix.h"
#include <direct.h>

blockmatrix::blockmatrix(const blockmatrix& m) {
	int i;
	N = m.N;
	blockN = m.blockN;
	A = new matrix[N];
	C = new matrix[N];
	B = new matrix[N];
	for (i = 0; i < N; i++) {
		A[i] = m.A[i];
		C[i] = m.C[i];
		B[i] = m.B[i];
	}
}

blockmatrix::blockmatrix() {
	N = 0;
	blockN = 0;
	A = NULL;
	C = NULL;
	B = NULL;
}

blockmatrix::~blockmatrix() {
	delete[] A;
	delete[] C;
	delete[] B;
}

blockmatrix::blockmatrix(int ord, int blockord) {
	int i;
	N = ord;
	blockN = blockord;
	A = new matrix[N];
	C = new matrix[N];
	B = new matrix[N];

	for (i = 0; i < N; i++) {
		A[i] = matrix(1, blockN);
		C[i] = matrix(2, blockN);
		B[i] = matrix(3, blockN);
	}
	A[0] = NULL;
	B[N - 1] = NULL;
}

std::ostream& operator << (std::ostream& out, blockmatrix m) {
	int i, j, k;

	for (i = 0; i < m.N; ++i) {
		for (j = 0; j < m.blockN; j++) {

			for (int g = 1; g < i; g++) {
				for (k = 0; k < m.blockN; k++)
					out << 0 << " ";
				out << " ";
			}
			if (i != 0) {
				for (k = 0; k < m.blockN; k++)
					out << m.A[i][j][k] << " ";
				out << " ";
			}
			for (k = 0; k < m.blockN; k++)
				out << m.C[i][j][k] << " ";
			out << " ";
			if (i != m.N - 1) {
				for (k = 0; k < m.blockN; k++)
					out << m.B[i][j][k] << " ";
				out << " ";
			}
			for (int g = i + 2; g < m.N; g++) {
				for (k = 0; k < m.blockN; k++)
					out << 0 << " ";
				out << " ";
			}
			out << std::endl;
		}
		out << std::endl;

	}

	return out;
}

vectorb blockmatrix::vecMul(vectorb v) {
	int i, j;
	if (N != v.order() || blockN != v.blockOrder()) throw Exept("can't multiply matrix to vector");
	vectorb out(N, blockN);
	vectorb sum;

	for (i = 0; i < N; i++) {

		if (i == 0)
		{
			sum = vectorb(1, blockN);
			sum += C[0] * v[0];
			sum += B[0] * v[1];

		}
		else if (i == N - 1)
		{
			sum = vectorb(1, blockN);
			sum += A[N - 1] * v[N - 2];
			sum += C[N - 1] * v[N - 1];

		}
		else
		{
			sum = vectorb(1, blockN);
			sum += A[i] * v[i - 1];
			sum += C[i] * v[i];
			sum += B[i] * v[i + 1];

		}
		for (j = 0; j < blockN; j++) {
			out[i][j] = sum[0][j];
		}
	}
	return out;
}

vectorb blockmatrix::SLAU(vectorb v) {
	int i, j;
	matrix* a, m;
	vectorb* b, w;
	a = new matrix[N];
	b = new vectorb[N + 1];
	vectorb out = vectorb(N, blockN);

	a[1] = matrix::inverse(C[0]) * B[0];
	for (i = 1; i <= N - 2; i++) {
		m = A[i] * a[i];
		a[i + 1] = matrix::inverse(C[i] - m) * B[i];
	}

	b[1] = matrix::inverse(C[0]) * v[0];
	for (i = 1; i <= N - 1; i++) {
		m = A[i] * a[i];
		w = vectorb(v[i], blockN);
		w -= A[i] * b[i];
		b[i + 1] = matrix::inverse(C[i] - m) * w;
	}

	for (i = 0; i < blockN; i++) {
		out[N - 1][i] = b[N][0][i];
	}

	for (i = N - 2; i >= 0; i--) {
		w = b[i + 1];
		w -= a[i + 1] * out[i + 1];
		for (j = 0; j < blockN; j++) {
			out[i][j] = w[0][j];
		}
	}

	return out;
}

void blockmatrix::save(vectorb ans, vectorb right) {
	std::ofstream f;
	try {
		_mkdir("c:\\Lab5_Pyatov");
	}
	catch (...) {
		std::cout << "Unable to create directory";
	}

	try 
	{
		f.open("c:\\Lab5_Pyatov\\C++_test.txt", std::ios::app);
		f << "Matrix:" << std::endl;
		f << *this;
		f << "Right side:" << std::endl;
		f << right << std::endl;
		f << "Answer:" << std::endl;
		f << ans << std::endl;
		f.close();
	}
	catch (...) {
		std::cout << "Save error";
	}
}
