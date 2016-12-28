#include<stdio.h>
#include<iostream>
#include<string>
#include<algorithm>
using namespace std;


void getroad(int head,int now,int aim,int**graph,int length,int dist)
{
	for(int i = 0;i<length;i++)
	{
		if(graph[now][i]>0)
		{
			if(i!=aim)
			{
				getroad(head,i,aim,graph,length,dist+graph[now][i]);
			}
			else
			{
				if(graph[now][i]+dist<graph[head][aim])
					graph[head][aim] = graph[now][i]+dist;
			}
		}
	}

}

int main(void)
{
	int point;

	while(cin>>point)
	{
		int **graph = new int*[point];

		for(int i = 0;i<point;i++)
		{
			graph[i] = new int[point+1];
			memset(graph[i],INTPTR_MAX,sizeof(graph[i]));
		}

		int side,head,tail,value;

		cin>>side;

		for(int i = 0;i<side;i++)
		{
			cin>>head>>tail>>value;
			graph[head][tail] = value;
			graph[head][point]++;
		}
		int left,right;

		cin>>left>>right;

		getroad(left,left,right,graph,point,0);

		cout<<graph[left][right]<<endl;

	}

	return 0;
}
