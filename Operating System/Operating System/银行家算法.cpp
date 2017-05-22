#include<iostream>
#include<algorithm>

using namespace std;


int main(void)
{
	int num_P;
	while(cin>>num_P)
	{
		int num_R;
		cin>>num_R;

		int* Available = new int[num_R];

		for(int i = 0;i<num_R;i++)
		{
			cin>>Available[i];
		}

		int**MAX = new int*[num_P];
		for(int i = 0;i<num_P;i++)
			MAX[i] = new int[num_R];

		for(int i = 0;i<num_P;i++)
			for(int j = 0;j<num_R;j++)
				cin >> MAX[i][j];

		int**Alloc = new int*[num_P];
		for(int i = 0;i<num_P;i++)
			Alloc[i] = new int[num_R];

		for(int i = 0;i<num_P;i++)
			for(int j = 0;j<num_R;j++)
				cin>>Alloc[i][j];

		int**Need = new int*[num_P];
		for(int i = 0;i<num_P;i++)
			Need[i] = new int[num_R];

		for(int i = 0;i<num_P;i++)
			for(int j = 0;j<num_R;j++)
				cin>>Need[i][j];

	}

	return 0;
}

