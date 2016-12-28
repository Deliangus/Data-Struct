#include<stdio.h>
#include<stdlib.h>
#include<iostream>

using namespace std;

struct Lnode
{
	int value;
	Lnode *next;

	Lnode()
	{
		value = 0;

		next = NULL;
	}

	Lnode(const int& val)
	{
		value = val;
		next = NULL;
	}

	Lnode(const int& val, Lnode*Next)
	{
		value = val;
		next = Next;
	}
};

class Link
{
private:
	Lnode* head;
	int length;
	Lnode* tail;
public:
	Link()
	{
		head = NULL;
		length = 0;
		tail = NULL;
	}

	void ListClear()
	{
		head = tail = NULL;
		length = 0;
	}

	bool ListEmpty()
	{
		return head == tail&&tail == NULL&&length == 0;
	}

	int ListLength()
	{
		return length;
	}

	Lnode* GetElem(const int&posi, int&carry)
	{
		if (posi >= length)
			return NULL;

		int count = 0;
		Lnode *temp = head;

		while (count<posi)
		{
			if (temp->next == NULL)
			{
				cout << "GetElem exceeded at " << count + 1 << endl;
				return NULL;
			}
			temp = temp->next;
		}

		if (temp == NULL)
		{
			cout << "GetElem exceeded at " << count + 1 << endl;
			return NULL;
		}
		else
			carry = temp->value;

		return temp;
	}

	Lnode* GetElem(const int&posi)
	{
		if (posi >= length)
			return NULL;

		int count = 0;
		Lnode *temp = head;

		while (count<posi)
		{
			if (temp->next == NULL)
			{
				cout << "GetElem exceeded at " << count + 1 << endl;
				return NULL;
			}
			temp = temp->next;
			count++;
		}

		return temp;
	}

	int LocateElem(const int aim)
	{
		Lnode *temp = head;

		int count = 0;

		while (temp != NULL)
		{
			if (temp->value == aim)
				return count;
			temp = temp->next;
			count++;
		}
		return -1;
	}

	Lnode* PriorElem(const Lnode*L)
	{
		Lnode*temp = head;

		while (temp->next != NULL)
		{
			if (temp->next == L)
				return temp;
			else
				temp = temp->next;
		}
		return NULL;
	}

	Lnode* PriorElem(const Lnode&L)
	{
		PriorElem(&L);
	}

	Lnode* NextElem(const Lnode*L)
	{
		Lnode*temp = PriorElem(L);

		if (temp == NULL)
			return NULL;
		else
			return temp->next->next;
	}

	Lnode* NextElem(const Lnode&L)
	{
		return NextElem(&L);
	}

	void ListToString()
	{
		if (length>0)
		{
			Lnode*temp = head->next;
			cout << head->value;

			while (temp != NULL)
			{
				cout << ' ' << temp->value;
				temp = temp->next;
			}
			cout << endl;
		}
		else
			cout << "List empty" << endl;
	}

	bool ListInsert(const int&posi, const int&val)
	{
		if (posi>length)
			return false;
		else if (posi == length)
		{
			if (length == 0)
			{
				head = new Lnode(val);
				tail = head;
			}
			else
			{
				tail->next = new Lnode(val);
				tail = tail->next;
			}
			cout << "Inserted " << tail->value << endl;
			length++;
			return true;
		}
		else
		{
			if (posi == 0)
			{
				Lnode*temp = head;
				head = new Lnode(val);
				head->next = temp;
				cout << "Inserted " << head->value << " at head" << endl;
				length++;
				return true;
			}
			else
			{
				Lnode *temp = GetElem(posi - 1);

				if (temp == NULL)
				{
					cout << "Insert error at " << posi << " getting NULL temp" << endl;
					return false;
				}
				else
				{
					temp->next = new Lnode(val, temp->next);
					cout << "Inserted " << temp->next->value << " at " << posi+1 << endl;
					length++;
					return true;
				}
			}
		}
	}

	Lnode* ListDelete(const int&posi)
	{
		if (posi >= length)
			return NULL;

		if (posi == 0)
		{
			Lnode*temp = head;
			head = head->next;

			length--;

			return temp;
		}
		else
		{
			Lnode*temp = GetElem(posi - 1);

			Lnode*temp2 = temp->next;
			temp->next = temp2->next;

			length--;

			return temp2;
		}
	}
};

//int main(void)
//{
//	int num;
//
//	cout << "������˳���Ԫ�صĸ���" << endl;
//
//	Link L;
//
//	while (cin >> num)
//	{
//		if (num>0)
//		{
//			int value;
//
//			cout << "������˳����Ԫ�ص�ֵ,�Կո�Ϊ���" << endl;
//
//			for (int i = 0; i<num; i++)
//			{
//				cin >> value;
//
//				L.ListInsert(i, value);
//			}
//
//			int posi;
//
//			cout << "������Ҫ����Ԫ�ص�λ�ü�Ԫ��ֵ���Կո�Ϊ���(��1Ϊ��ʼ��)" << endl;
//
//			cin >> posi >> value;
//
//			L.ListInsert(posi - 1, value);
//
//			cout << "������Ԫ�ر�Ϊ" << endl;
//
//			L.ListToString();
//
//			cout << "������Ҫɾ��Ԫ�ص�λ��(��1Ϊ��ʼ��)" << endl;
//
//			cin >> posi;
//
//			Lnode*temp = L.ListDelete(posi - 1);
//
//			cout << "��ɾ����Ԫ��Ϊ " << temp->value << endl;
//
//			cout << "ɾ�����˳���Ϊ" << endl;
//
//			L.ListToString();
//
//			cout << endl<<"������˳���Ԫ�صĸ���" << endl;
//
//			L.ListClear();
//		}
//	}
//
//	return 0;
//}
