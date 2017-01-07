//#include<stdio.h>
//#include<iostream>
//#include<string>
//#include<algorithm>
//using namespace std;
//
//const int LIMINATION = 1111;
//void getroad(int head,int now,int aim,int**graph,int length,int dist)
//{
//	for(int i = 0;i<length;i++)
//	{
//		if(graph[now][i]<LIMINATION)
//		{
//			if(i!=aim)
//			{
//				getroad(head,i,aim,graph,length,dist+graph[now][i]);
//			}
//			else
//			{
//				if(graph[now][i]+dist<graph[head][aim])
//					graph[head][aim] = graph[now][i]+dist;
//			}
//		}
//	}
//
//}
//
//int main(void)
//{
//	int point;
//
//	while(cin>>point)
//	{
//		int **graph = new int*[point];
//
//		for(int i = 0;i<point;i++)
//			graph[i] = new int[point];
//
//		for(int i = 0;i<point;i++)
//			for(int j = 0;j<point;j++)
//				graph[i][j] = LIMINATION;
//
//		int side,head,tail,value;
//
//		cin>>side;
//
//		for(int i = 0;i<side;i++)
//		{
//			cout<<"±ß"<<i<<endl;
//			cin>>head>>tail>>value;
//			graph[head][tail] = value;
//		}
//
//		for(int i = 0;i<point;i++)
//		{
//			for(int j = 0;j<point;j++)
//			{
//				cout<<graph[i][j]<<' ';
//			}
//			cout<<endl;
//		}
//
//		int left,right;
//		while(cin>>left>>right)
//		{
//			getroad(left,left,right,graph,point,0);
//
//			cout<<graph[left][right]<<endl;
//			for(int i = 0;i<point;i++)
//			{
//				for(int j = 0;j<point;j++)
//				{
//					cout<<graph[i][j]<<' ';
//				}
//				cout<<endl;
//			}
//		}
//	}
//
//	return 0;
//}
