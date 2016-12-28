#include<stdio.h>
#include<iostream>
#include<string>
#include<algorithm>
using namespace std;

class spot
{
public:
	int son;
	spot**next;
	int *value;

	spot()
	{
		son = 0;
		next = NULL;
		value = NULL;
	}
	void set(int point)
	{
		son = 0;
		next = new spot*[point];
		value = new int[point];;
	}
	void insert(spot*item,int v)
	{
		next[son] = item;
		value[son++] = v;
	}

};
int main(void)
{
	int point;

	while(cin>>point)
	{
		int **graph = new int*[point];

		for(int i = 0;i<point;i++)
		{
			graph[i] = new int[point+1];
			memset(graph[i],0,sizeof(graph[i]));
		}

		int side,head,tail,value;

		cin>>side;

		for(int i = 0;i<side;i++)
		{
			cin>>head>>tail>>value;
			graph[head][tail] = value;
			graph[head][point]++;
		}

		bool **trail = new bool*[point];
		for(int i = 0;i<point;i++)
			trail[i] = new bool[point]{0};

		for(int i = 0;i<point;i++)
		{
			for(int j = 0;j<point;j++)
			{
				cout<<graph[i][j]<<' ';
				if(graph[i][j]>0)
					trail[i][j] = true;
			}
			cout<<endl;
		}

	}

	return 0;
}