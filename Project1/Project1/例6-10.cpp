//#include<stdio.h>
//#include<iostream>
//#include<string>
//#include<algorithm>
//using namespace std;
//int le,ri;
//void count(int*list,int value)
//{
//	int left,right;
//
//	cin>>left;
//
//	//list[value] += mid;
//	if(left!=-1)
//	{
//		le = le<value-1?le:value-1;
//		list[value-1] += left;
//		count(list,value-1);
//	}
//	cin>>right;
//	if(right!=-1)
//	{
//		ri = ri>value+1?ri:value+1;
//		list[value+1] += right;
//		count(list,value+1);
//	}
//}
//int main(void)
//{
//	int mid;
//	while(cin>>mid)
//	{
//		le = ri = 1000;
//		int val[2000];
//
//		memset(val,0,sizeof(val));
//
//		val[le] = mid;
//
//		count(val,le);
//
//		for(int i = le;i<=ri;i++)
//			cout<<val[i]<<' ';
//		cout<<endl;
//	}
//	return 0;
//}