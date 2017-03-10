#include<stdio.h>
#include<iostream>
#include<string>
#include<algorithm>
#include<opencv.hpp>

using namespace std;
const int orien[8][2]
{
	{-1,-1},
	{0,-1},
	{1,-1},
	{-1,0},
	{1,0},
    {-1,+1},
    {0,1},
    {1,1}
};
void floodfill(int**graph,int&value,int x,int y)
{
	if(graph[x][y]==1)
	{
		graph[x][y] = value;
		int px,py;
		for(int i = 0;i<8;i++)
		{
			px = x+orien[i][0];
			py = y+orien[i][1];
			if(px>=0&&py>=0&&graph[px][py]==1)
				floodfill(graph,value,x+orien[i][0],y+orien[i][1]);
		}
	}
}