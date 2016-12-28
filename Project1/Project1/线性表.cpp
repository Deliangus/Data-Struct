#include<stdio.h>
#include<iostream>

using namespace std;

struct List
{
	int *value = NULL;//线性表头
	const int Floor = 10;//线性表扩张量
	int length = 0;//线性表长length = right-left+1
	int left,right;//线性表头尾
	int MaxLength;

	List(const int& leng)//建立一个大于等于leng的最小FLOOR倍数线性表
	{
		MaxLength = (int)(leng/Floor)*Floor;
		value = new int[MaxLength];
		length = 0;
		left = 0;
		right = left-1;
	}

	List()//建立一个最小FLOOR倍数线性表
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
	static bool InitList(List *L,const int&length)//建立一个大于等于leng的最小FLOOR倍数线性表
	{
		L = new List(length);
		return true;
	}
	static bool InitList(List *L)//建立一个最小FLOOR倍数线性表
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

	static bool ClearList(List&L)//left==right:线性表为空
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

	static int LocateElem(const List&L,int e)//-1为无
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

	bool ListInsert(const int posi,const int val)//在posi位置插入value，posi<=length
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

				cout<<"插入位置"<<posi<<"插入数据"<<val<<endl;

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

				cout<<"插入位置"<<dest<<"插入数据"<<value[dest]<<endl;
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

				cout<<"插入位置 "<<dest<<" 插入数据 "<<value[dest]<<endl;

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

	bool ListDelete(const int posi,int &carry)//删去位置posi上的元素，posi<length，山区的元素放入carry中
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

///////////////////////////////算法2.1/////////////////////

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
//	cout<<"请输入顺序表元素的个数"<<endl;
//
//	while(cin>>num)
//	{
//		List*item_L = new List(num);
//
//		List &L = *item_L;
//
//		int value;
//
//		cout<<"请输入顺序表各元素的值,以空格为间隔"<<endl;
//
//		for(int i = 0;i<num;i++)
//		{
//			cin>>value;
//
//			L.ListInsert(i,value);
//
//			L.GetElem(L,i,value);
//
//			//cout<<"插入位置"<<i<<"插入数据"<<value<<endl;
//		}
//
//		int posi;
//
//		cout<<"请输入要插入元素的位置及元素值，以空格为间隔(以1为起始点)"<<endl;
//
//		cin>>posi>>value;
//
//		L.ListInsert(posi-1,value);
//
//		cout<<"插入后的元素表为"<<endl;
//
//		L.ListToString();
//
//		cout<<"请输入要删除元素的位置(以1为起始点)"<<endl;
//
//		cin>>posi;
//
//		L.ListDelete(posi-1,value);
//
//		cout<<"所删除的元素为 "<<value<<endl;
//
//		cout<<"删除后的顺序表为"<<endl;
//
//		L.ListToString();
//
//		cout<<endl<<"请输入顺序表元素的个数"<<endl;
//	}
//
//	return 0;
//}