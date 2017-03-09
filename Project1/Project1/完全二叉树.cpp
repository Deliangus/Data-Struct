//#include<stdio.h>
//#include<iostream>
//
//using namespace std;
//
//struct Node
//{
//	int num;
//	int posi;
//	Node*signel[2];
//	bool exist;
//
//	Node(const int p)
//	{
//		num = 0;
//		posi = p;
//		exist = 0;
//		signel[0] = signel[1] = NULL;
//	}
//};
//class Tree
//{
//private:
//	int depth;
//	Node*root;
//
//	void HeadTravel(Node*temp)
//	{
//		cout<<temp->num<<' ';
//		if(temp->signel[false]!=NULL)
//			HeadTravel(temp->signel[false]);
//		if(temp->signel[true]!=NULL)
//			HeadTravel(temp->signel[true]);
//	}
//
//	void MidTravel(Node*temp)
//	{
//		if(temp->signel[false]!=NULL)
//			MidTravel(temp->signel[false]);
//
//		cout<<temp->num<<' ';
//
//		if(temp->signel[true]!=NULL)
//			MidTravel(temp->signel[true]);
//	}
//
//	void AfterTravel(Node*temp)
//	{
//		if(temp->signel[false]!=NULL)
//			AfterTravel(temp->signel[false]);
//
//		if(temp->signel[true]!=NULL)
//			AfterTravel(temp->signel[true]);
//
//		cout<<temp->num<<' ';
//	}
//public:
//	Tree()
//	{
//		depth = 0;
//		root = NULL;
//	}
//
//	void TreeClear()
//	{
//		depth = 0;
//		root = NULL;
//	}
//
//	void DestoryTree()
//	{
//		depth = 0;
//		root = NULL;
//	}
//
//	bool TreeEmpty()
//	{
//		return root!=NULL;
//	}
//
//	int TreeDepth()
//	{
//		return depth;
//	}
//
//	Node* TreeRoot()
//	{
//		return root;
//	}
//
//	int GetElem(const int posi)
//	{
//		Node*temp = GetNode(posi);
//
//		if(temp==NULL)
//			return -1;
//		else
//		{
//			if(temp->exist)
//				return temp->num;
//			else
//				return -1;
//		}
//	}
//
//	Node* GetNode(const int posi)
//	{
//		bool sign = posi%2==1;
//		if(posi==1)
//			return root;
//
//		if(posi>1)
//		{
//			Node*temp = GetNode(posi/2);
//			if(temp!=NULL)
//				return temp->signel[sign];
//			else
//				return NULL;
//		}
//		return NULL;
//	}
//
//	bool InsertNode(const int posi,const int value)
//	{
//		if(posi==1)
//		{
//			if(root==NULL)
//			{
//				root = new Node(1);
//				root->num = value;
//				root->exist = true;
//				cout<<"Inserted "<<root->num<<" at root"<<endl;
//				depth = 1;
//				return true;
//			}
//			else
//			{
//				if(root->exist)
//				{
//					cout<<"不能在已有节点插入"<<endl;
//					return false;
//				}
//				else
//				{
//					root->num = value;
//					root->exist = true;
//					cout<<"Inserted "<<root->num<<" at root"<<endl;
//					depth = 1;
//					return true;
//				}
//			}
//		}
//		else
//		{
//			bool sign = posi%2==1;
//
//			Node* temp = GetNode(posi/2);
//
//			if(temp==NULL)
//			{
//				cout<<"不允许插入无双亲节点"<<endl;
//				return false;
//			}
//			else
//			{
//				if(temp->signel[sign]==NULL)
//				{
//					temp->signel[sign] = new Node(posi);
//					temp = temp->signel[sign];
//					temp->num = value;
//					temp->exist = true;
//					cout<<"Inserted "<<temp->num<<" at "<<temp->posi<<endl;
//
//					int temp = 0;
//					int floor = posi;
//					while(floor>0)
//					{
//						floor /= 2;
//						temp++;
//					}
//					depth = depth>temp?depth:temp;
//
//					return true;
//				}
//				else
//				{
//					if(temp->exist)
//					{
//						cout<<"不允许插入无双亲节点"<<endl;
//						return false;
//					}
//					else
//					{
//						temp->num = value;
//						temp->exist = true;
//
//						int temp = 0;
//						int floor = posi;
//						while(floor>0)
//						{
//							floor /= 2;
//							temp++;
//						}
//						depth = depth>temp?depth:temp;
//						return false;
//					}
//				}
//			}
//		}
//	}
//
//	bool NodeAssign(const int posi,const int value)
//	{
//		Node*temp = GetNode(posi);
//
//		if(temp==NULL)
//			return false;
//		else
//		{
//			temp->num = value;
//			temp->exist = true;
//			return true;
//		}
//	}
//
//	Node* GetParent(const int posi)
//	{
//		int parent = (int)(posi/2);
//
//		return GetNode(parent);
//	}
//
//	Node* GetLeftNode(const int posi)
//	{
//		Node*temp = GetNode(posi*2);
//
//		return temp;
//	}
//
//	Node* GetRightNode(const int posi)
//	{
//		Node*temp = GetNode(posi*2+1);
//
//		return temp;
//	}
//
//	Node* GetLeftSibling(const int posi)
//	{
//		bool sign = posi%2==1;
//
//		if(!sign)
//			return NULL;
//		else
//			return GetNode(posi-1);
//	}
//
//	Node* GetRightSibling(const int posi)
//	{
//		bool sign = posi%2==1;
//
//		if(sign)
//			return NULL;
//		else
//			return GetNode(posi+1);
//	}
//
//	bool DeleteNode(const int posi,const bool left)
//	{
//		Node*temp = GetNode(posi);
//
//		if(temp==NULL)
//			return false;
//		else
//		{
//			temp->signel[left] = NULL;
//			return true;
//		}
//	}
//
//	void ToStringPreOrder()
//	{
//		if(root!=NULL)
//			HeadTravel(root);
//		cout<<"Done!"<<endl;
//	}
//
//	void ToStringInOrder()
//	{
//		if(root!=NULL)
//			MidTravel(root);
//		cout<<"Done!"<<endl;
//	}
//
//	void ToStringPostOrder()
//	{
//		if(root!=NULL)
//			AfterTravel(root);
//		cout<<"Done!"<<endl;
//	}
//};
//
//int main(void)
//{
//	cout<<"输入节点数"<<endl;
//	
//	int node;
//	
//	Tree item;
//
//	while(cin>>node)
//	{
//		int posi,value;
//
//		cout<<"从根节点出发，输入节点在完全二叉树中的位置和其值 以空格为间隔，根节点位置为0"<<endl;
//
//		for(int i = 0;i<node;)
//		{
//			cin>>posi>>value;
//			if(item.InsertNode(posi,value))
//				i++;
//		}
//
//		cout<<"先序输出："<<endl;
//
//		item.ToStringPreOrder();
//
//		cout<<endl;
//
//		cout<<"中序输出："<<endl;
//
//		cout<<endl;
//
//		item.ToStringInOrder();
//
//		cout<<"后序输出："<<endl;
//
//		item.ToStringPostOrder();
//
//		cout<<endl<<endl;
//
//		cout<<"输入节点数"<<endl;
//	}
//	return 0;
//}