//#include<stdio.h>
//#include<iostream>
//
//using namespace std;
//
//bool kp(int head,int tail,int *num)
//{
//	int left = head;
//	int right = tail;
//	int key = num[left];
//	bool mode = true;
//	int temp;
//	while(left!=right)
//	{
//		if(mode)
//		{
//			if(num[right]<key)
//			{
//				num[left] = num[right];
//				mode = false;
//			}
//			else
//				right--;
//		}
//		else
//		{
//			if(num[left]>key)
//			{
//				num[right] = num[left];
//				mode = true;
//			}
//			else
//				left++;
//		}
//		for(int i = head;i<=tail;i++)
//			cout<<num[i]<<' ';
//		cout<<endl;
//	}
//	if(left==right)
//		num[left] = key;
//
//	if(head!=left)
//		kp(head,left-1,num);
//	if(tail!=right)
//		kp(right+1,tail,num);
//	return true;
//}
//int main(void)
//{
//	int length;
//
//	while(cin>>length)
//	{
//		int *num = new int[length];
//
//		for(int i = 0;i<length;i++)
//			cin>>num[i];
//
//		kp(0,length-1,num);
//
//		for(int i = 0;i<length;i++)
//			cout<<num[i]<<' ';
//		cout<<endl;
//
//	}
//	return 0;
//}