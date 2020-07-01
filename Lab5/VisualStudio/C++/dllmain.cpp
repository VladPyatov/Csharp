// dllmain.cpp : Определяет точку входа для приложения DLL.
#include "pch.h"
#include "matrix.h"
#include "blockmatrix.h"
#include "vectorb.h"
#include <ctime>
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C" void _declspec(dllexport)  F(double*A, double* x, double*b, double N, double blockN, double& time) {
	
	blockmatrix blocktest = blockmatrix(N, blockN);
	vectorb vec = vectorb(N, blockN);

	//перевод массива в вектор правой части
	int n = 0;
	for (int i = 0; i < N; i++)
		for (int j = 0; j < blockN; j++)
			 vec[i][j]= b[n++];

	//перевод массива в блочную 3-диагональную матрицу
	int mat_index = 0;

	for (int j = 0; j < N; j++)
	{
		if (j != 0)
			for (int i = 0; i < blockN; i++)
			{
				blocktest.A[j][i][i]= A[mat_index];
				mat_index++;
			}
		for (int i = 0; i < blockN; i++)
		{
			blocktest.C[j][i][i]= A[mat_index];
			mat_index++;
		}
		if (j != N - 1)
			for (int i = 0; i < blockN; i++)
			{
				blocktest.B[j][i][i]=A[mat_index];
				mat_index++;
			}
	}

	double start = clock();
	vectorb out = blocktest.SLAU(vec);
	double end = clock();

	blocktest.save(out, vec);
	//перевод вектора решения в массив
	n = 0;
	for (int i = 0; i < N; i++)
		for (int j = 0; j < blockN; j++)
			x[n++] = out[i][j];
	

	time = (end - start)/*/ CLOCKS_PER_SEC*/;


}

