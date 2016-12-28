#include<stdio.h>
#include<iostream>

using namespace std;

struct List
{
	int *value = NULL;//���Ա�ͷ
	const int Floor = 10;//���Ա�������
	int length = 0;//���Ա�length = right-left+1
	int left,right;//���Ա�ͷβ
	int MaxLength;

	List(const int& leng)//����һ�����ڵ���leng����СFLOOR�������Ա�
	{
		MaxLength = (int)(leng/Floor)*Floor;
		value = new int[MaxLength];
		length = 0;
		left = 0;
		right = left-1;
	}

	List()//����һ����СFLOOR�������Ա�
	{
		value = new int[Floor];
		MaxLength = Floor;
		length = 0;
		left = 0;
		right = left-1;
	}

	void ListConfig()
	{
		cout<<"length "<<length<<endl;
		cout<<"left->right "<<left<<"->"<<right<<endl;
		cout<<"MaxLength "<<MaxLength<<endl;
		cout<<"value ";
		for(int i = 0;i<MaxLength;i++)
		{
			cout<<value[i]<<' ';
		}
		cout<<endl;

		return;
	}
	///////////////////////////////////////////////////////////////
	static bool InitList(List *L,const int&length)//����һ�����ڵ���leng����СFLOOR�������Ա�
	{
		L = new List(length);
		return true;
	}
	static bool InitList(List *L)//����һ����СFLOOR�������Ա�
	{
		L = new List();
		return true;
	}

	static bool DestroyList(List *L)
	{
		delete L;
		return true;
	}
	static bool DestroyList(List &L)
	{
		return DestroyList(&L);
	}

	static bool ClearList(List&L)//left==right:���Ա�Ϊ��
	{
		L.left = 0;
		L.right = -1;
		L.length = 0;
		return true;
	}
	static bool ClearList(List*L)
	{
		return ClearList(*L);
	}

	static bool ListEmpty(List *L)
	{
		return L->length==0;
	}
	static bool ListEmpty(List &L)
	{
		return L.length==0;
	}

	static bool GetElem(const List&L,const int i,int&e)
	{
		if(L.length>=i)
		{
			e = (L.left+i)%L.MaxLength;
			e = L.value[e];
			return true;
		}
		return false;
	}

	static int LocateElem(const List&L,int e)//-1Ϊ��
	{
		if(L.length)
		{
			int i = L.left;
			while(e!=L.value[i])
			{
				if(i==L.right)
					break;
				i++;
				if(i>=L.MaxLength)
					i %= L.MaxLength;
			}
			if(i==L.right&&e==L.value[L.right])
				return i;

			if(i==L.right)
				return -1;
			else
				return i;
		}
		else
			return -1;
	}

	void ListExpand()
	{
		MaxLength += Floor;
		int*temp = new int[MaxLength];

		for(int i = 0;i<length;i++)
		{
			temp[i] = value[(left+i)%MaxLength];
			i++;
		}

		delete value;

		value = temp;
		left = 0;
		right = length-1;
	}

	bool ListInsert(const int posi,const int val)//��posiλ�ò���value��posi<=length
	{
		if(posi<=length)
		{
			if(length+Floor>MaxLength)
				ListExpand();

			if(length==posi)
			{
				value[posi] = val;
				right = (right+1)%MaxLength;
				length++;

				cout<<"����λ��"<<posi<<"��������"<<val<<endl;

				return true;
			}
			else if(posi>=length/2)
			{
				int move = (right+1)%MaxLength;
				int dest = (left+posi)%MaxLength;
				for(int i = 0;i < length-posi;i++)
				{
					value[(move-i+MaxLength)%MaxLength] = value[(move-i+MaxLength-1)%MaxLength];
				}
				value[dest] = val;
				right = (right+1)%MaxLength;

				cout<<"����λ��"<<dest<<"��������"<<value[dest]<<endl;
			}
			else
			{
				int move = (left-2+MaxLength)%MaxLength;
				int dest = (left+posi-1+MaxLength)%MaxLength;

				for(int i = 0;i<=posi;i++)
				{
					value[(move+i)%MaxLength] = value[(move+i+1)%MaxLength];
				}
				value[dest] = val;
				left = (left-1+MaxLength)%MaxLength;

				cout<<"����λ�� "<<dest<<" �������� "<<value[dest]<<endl;

			}
			length++;

			return true;
		}
		else
			return false;
	}

	int ListTruePosi(int posi)
	{
		return (left+posi)%MaxLength;
	}

	bool ListDelete(const int posi,int &carry)//ɾȥλ��posi�ϵ�Ԫ�أ�posi<length��ɽ����Ԫ�ط���carry��
	{
		if(posi<length)
		{
			if(posi>=length/2)
			{
				int dest = (right-1+MaxLength)%MaxLength;
				int move = (left+posi)%MaxLength;
				carry = value[move];
				for(int i = 0;i<=length-posi-2;i++)
				{
					value[(move+i)%MaxLength] = value[(move+i+1)%MaxLength];
				}
				right = (right-1+MaxLength)%MaxLength;
			}
			else
			{
				int move = (left+posi)%MaxLength;
				int dest = (left+1)%MaxLength;

				carry = value[move];

				for(int i = 0;i<=posi;i++)
				{
					value[(move-i+MaxLength)%MaxLength] = value[(move-i-1+MaxLength)%MaxLength];
				}
				left = (left+1)%MaxLength;
			}
			length--;
			return true;
		}
		else
			return false;
	}

	static bool ListTraverse(List&L,bool* func(int&))
	{
		bool judge = true;
		for(int i = L.left;i<=L.right&&judge;i++)
		{
			judge=judge&&func(L.value[i]);
		 }

		return judge;
	}

///////////////////////////////�㷨2.1/////////////////////

	static bool MergeList(const List& La,const List& Lb,List&Lc)
	{
		ClearList(Lc);
		for(int i = La.left,j = Lb.left,k=0;;)
		{
			if(La.value[i]<Lb.value[j]&&i>=0)
			{

			}
			else
			{
				Lc.ListInsert(k,Lb.value[j]);
				if(j==Lb.right)
					break;
				else
					j = (j+1)%La.MaxLength;
			}
		}
		return true;
	}

	void ListToString()
	{
		cout<<value[left];

		for(int i = 1;i<length;i++)
		{
			cout<<' '<<value[(i+left)%MaxLength];
		}

		cout<<endl;

		return;
	}

};

//int main(void)
//{
//	int num;
//
//	cout<<"������˳���Ԫ�صĸ���"<<endl;
//
//	while(cin>>num)
//	{
//		List*item_L = new List(num);
//
//		List &L = *item_L;
//
//		int value;
//
//		cout<<"������˳����Ԫ�ص�ֵ,�Կո�Ϊ���"<<endl;
//
//		for(int i = 0;i<num;i++)
//		{
//			cin>>value;
//
//			L.ListInsert(i,value);
//
//			L.GetElem(L,i,value);
//
//			//cout<<"����λ��"<<i<<"��������"<<value<<endl;
//		}
//
//		int posi;
//
//		cout<<"������Ҫ����Ԫ�ص�λ�ü�Ԫ��ֵ���Կո�Ϊ���(��1Ϊ��ʼ��)"<<endl;
//
//		cin>>posi>>value;
//
//		L.ListInsert(posi-1,value);
//
//		cout<<"������Ԫ�ر�Ϊ"<<endl;
//
//		L.ListToString();
//
//		cout<<"������Ҫɾ��Ԫ�ص�λ��(��1Ϊ��ʼ��)"<<endl;
//
//		cin>>posi;
//
//		L.ListDelete(posi-1,value);
//
//		cout<<"��ɾ����Ԫ��Ϊ "<<value<<endl;
//
//		cout<<"ɾ�����˳���Ϊ"<<endl;
//
//		L.ListToString();
//
//		cout<<endl<<"������˳���Ԫ�صĸ���"<<endl;
//	}
//
//	return 0;
//}