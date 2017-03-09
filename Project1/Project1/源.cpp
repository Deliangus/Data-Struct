#include<stdio.h>
#include<iostream>
#include<string>
#include<algorithm>
using namespace std;
//struct Node
//{
//	int value;
//	Node*next;
//
//	Node()
//	{
//		next = NULL;
//	}
//};
//int main(void)
//{
//	int num;
//
//	while(cin>>num)
//	{
//		Node*head;
//		int value;
//		head = new Node;
//		cin>>head->value;
//		head->next = NULL;
//		Node *tail = head;
//		for(int i = 1;i<num;i++)
//		{
//			cin>>value;
//			tail->next = new Node;
//			tail = tail->next;
//			tail->value = value;
//			tail->next = NULL;
//		}
//
//		int num2;
//
//		Node *head2 = new Node;
//		tail = head2;
//
//		cin>>num2;
//
//		cin>>head2->value;
//
//		for(int i = 1;i<num2;i++)
//		{
//			tail->next = new Node;
//			tail = tail->next;
//			cin>>tail->value;
//			tail->next = NULL;
//		}
//
//		Node*c3 = NULL;
//
//		bool he = true;
//
//		while(head!=NULL&&head2!=NULL)
//		{
//			if(head->value<head2->value)
//			{
//				if(he)
//				{
//					c3 = head;
//					tail = c3;
//					he = false;
//				}
//				else
//				{
//					tail->next = head;
//					tail = tail->next;
//				}
//				head = head->next;
//			}
//			else
//			{
//				if(he)
//				{
//					c3 = head2;
//					tail = c3;
//					he = false;
//				}
//				else
//				{
//					tail->next = head2;
//					tail = tail->next;
//				}
//				head2 = head2->next;
//			}
//		}
//
//		tail->next = head==NULL?head2:head;
//
//		tail = c3;
//		cout<<tail->value;
//		tail = tail->next;
//		int last = c3->value;
//		while(tail!=NULL)
//		{
//			if(last!=tail->value)
//			{
//				cout<<' '<<tail->value;
//				last = tail->value;
//			}			
//			tail = tail->next;
//		}
//		//cout<<endl;
//
//		while(getchar()!='\n');
//	}
//	return 0;
//
//}

int main(void)
{
	int n1;
	int n2;

	int s1[1000];
	int s2[1000];

	cin>>n1;

	for(int i = 0;i<n1;i++)
		cin>>s1[i];
	
	cin>>n2;

	for(int i = 0;i<n2;i++)
	{
		cin>>s2[i];
	}

	int last;

	if(s1[0]>s2[0])
	{
		cout<<s2[0];
		last = s2[0];
		for(int j = 0,i = 1;;)
		{
			if(j<n1&&i<n2)
			{
				if(s1[j]<s2[i])
				{
					if(s1[j]!=last)
					{
						printf(" %d",s1[j]);
						last = s1[j];
					}
					j++;
				}
				else
				{
					if(s2[i]!=last)
					{
						printf(" %d",s2[i]);
						last = s2[i];
					}
					i++;
				}
			}
			else
			{
				if(i<n2)
				{
					while(i<n2)
					{
						if(s2[i]!=last)
						{
							printf(" %d",s2[i]);
							last = s2[i];
						}
						i++;
					}
				}
				else if(j<n1)
				{
					while(j<n1)
					{
						if(s1[j]!=last)
						{
							printf(" %d",s1[j]);
							last = s1[j];
						}
						j++;
					}
				}
				else
					break;
			}
		}
	}
	else
	{
		cout<<s1[0];
		last = s1[0];
		for(int j = 1,i = 0;;)
		{
			if(j<n1&&i<n2)
			{
				if(s1[j]<s2[i])
				{
					if(s1[j]!=last)
					{
						printf(" %d",s1[j]);
						last = s1[j];
					}
					j++;
				}
				else
				{
					if(s2[i]!=last)
					{
						printf(" %d",s2[i]);
						last = s2[i];
					}
					i++;
				}
			}
			else
			{
				if(i<n2)
				{
					while(i<n2)
					{
						if(s2[i]!=last)
						{
							printf(" %d",s2[i]);
							last = s2[i];
						}
						i++;
					}
				}
				else if(j<n1)
				{
					while(j<n1)
					{
						if(s1[j]!=last)
						{
							printf(" %d",s1[j]);
							last = s1[j];
						}
						j++;
					}
				}
				else
					break;
			}
		}
	}
	return 0;
}