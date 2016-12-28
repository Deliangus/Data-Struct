//#include<iostream>
//#include<string>
//#include<queue>
//using namespace std;
//#define ERROR 1
//#define MAX_VERTEX_NUM 100
////////////�ڽӱ�洢��ʾ//////////
//typedef struct ArcNode
//{
//	int adjvex;
//	struct ArcNode *nextarc;
//	string info;
//} ArcNode;
//typedef struct VNode
//{
//	char date;
//	ArcNode * firstarc;
//} VNode, AdjList[MAX_VERTEX_NUM];
//typedef struct
//{
//	AdjList vertices;
//	int vexnum, arcnum;        //��ǰͼ��vexnum��������arcnum����
//	int kind;
//} ALGraph;
////////////////////////////////////////////////
//int LocateVex(ALGraph &G, char &v1)
//{
//	int i;
//	for (i = 0; i<G.vexnum; i++)
//	{
//		if (G.vertices[i].date == v1)
//			return i;
//	}
//	if (i >= G.vexnum)
//		return ERROR;
//	else
//		return 0;
//}
//void CreateDG(ALGraph &G)////��ͼ
//{
//	ArcNode *p, *q;
//	char v1, v2;
//	char v;
//	int i, j, k, n;
//	cout << "������ͼ�Ķ������ͻ�����" << endl;
//	cin >> G.vexnum;
//	cin >> G.arcnum;
//	cout << "�����붥�㣺" << endl;
//
//	for (i = 0; i<G.vexnum; i++)
//	{
//		cin >> v;
//		G.vertices[i].date = v;
//		G.vertices[i].firstarc = NULL;
//	}
//	cout << "�����뻡β�ͻ�ͷ��" << endl;
//	for (k = 0; k<G.arcnum; k++)
//	{
//		cin >> v1;
//		cin >> v2;
//		i = LocateVex(G, v1);
//		j = LocateVex(G, v2);
//
//		if (G.vertices[i].firstarc == NULL)
//		{
//			p = (ArcNode *)new ArcNode;
//			G.vertices[i].firstarc = p;
//			q = G.vertices[i].firstarc;
//		}
//		else
//		{
//			q = G.vertices[i].firstarc;
//			for (n = 0; n<G.arcnum; n++, q = q->nextarc)
//			{
//				if (!q->nextarc)
//					break;
//			}
//			p = (ArcNode *)new ArcNode;
//			q->nextarc = p;
//			q = q->nextarc;
//		}
//		q->adjvex = j;
//		q->nextarc = NULL;
//	}
//	cout << "ͼ�����ɹ���";
//}
////----------------������ȱ���--------------------//
//
//bool visited[MAX_VERTEX_NUM];//���ʱ�־���飬ͨ���������ʾ�����Ƿ��ѷ��ʣ���visited[i]Ϊfalseʱ����ʾ��i��δ�����ʡ�
//int FirstAdjVex(ALGraph &G, int v)//�ҵ���ͼG�еģ��붥��G.vertices[v]���ڵ�δ�������ʵ��ڽӵ�
//{
//	int i;
//	int n = -1;
//	ArcNode*p;
//	p = G.vertices[v].firstarc;
//	if (p)
//	{
//		i = p->adjvex;
//		if (visited[i] == false)
//			n = i;
//	}
//	return n;
//}
//int NextAdjVex(ALGraph &G, int v)
//{
//	int i;
//	int n = -1;
//	ArcNode *p;
//	p = G.vertices[v].firstarc;
//	for (i = p->adjvex; i<G.vexnum, p != NULL;)
//	{
//		i = p->adjvex;
//		if (visited[i] == false)
//		{
//			n = i;
//			break;
//		}
//		else
//			p = p->nextarc;
//	}
//	return n;
//}
//
//void VisitFuc(ALGraph &G, int v)
//{
//	cout << G.vertices[v].date << " ";
//}
//void DFS(ALGraph &G, int v)//��ͼG��������ȱ����������������Ϊv�Ķ��㿪ʼ
//{
//	int w;
//	visited[v] = true;//��������Ϊv�Ķ���Ϊ�ѷ���
//	VisitFuc(G, v);//��������Ϊv�Ķ���
//	for (w = FirstAdjVex(G, v); w >= 0; w = NextAdjVex(G, v))
//		if (!visited[w]) DFS(G, w);
//
//}
//void DFSTraverse(ALGraph &G)//������ȱ�������ʼ���������ô˺�����ʼ������
//{
//	int v;
//	for (v = 0; v<G.vexnum; v++)
//		visited[v] = false;//��ʼ�������е㶼Ϊ�����ʣ�ͳͳ��Ϊfalse
//	cout << "�������������" << endl;
//	for (v = 0; v<G.vexnum; v++)
//	{
//		if (!visited[v])
//			DFS(G, v);
//	}
//}
////-----------------������ȱ���----------------------//
//
//void BFSTraverse(ALGraph &G)
//{
//	int v;
//	int w;
//	queue<int> q; //STL����
//	for (v = 0; v<G.vexnum; v++)
//		visited[v] = false;
//	//  InitQueue(Q);
//	cout << "�������������";
//	for (v = 0; v<G.vexnum; v++)
//	{
//		if (!visited[v])
//		{
//			visited[v] = true;
//			VisitFuc(G, v);
//			q.push(v);   //v����
//			while (q.empty() != true)
//			{
//				v = q.front();
//				q.pop();
//				for (w = FirstAdjVex(G, v); w >= 0; w = NextAdjVex(G, v))
//				{
//					if (!visited[w])
//					{
//						visited[w] = true;
//						VisitFuc(G, w);
//
//						q.push(w);
//					}
//				}
//			}
//		}
//	}
//}
///////////////////////////////////////////////////////////////////
//void menu()
//{
//	cout << "ͼ�Ļ�������" << endl;
//	cout << "1��ͼ�Ĺ���" << endl;
//	cout << "2��������ȱ���" << endl;
//	cout << "3��������ȱ���" << endl;
//	cout << "���������ֽ���ѡ��:" << endl;
//}
//int main()
//{
//	ALGraph G;
//	int i;
//	menu();
//	cin >> i;
//	while (i<4)
//	{
//		switch (i)
//		{
//		case 1:
//			CreateDG(G);
//			break;
//		case 2:
//			DFSTraverse(G);
//			cout << endl;
//			break;
//		case 3:
//			BFSTraverse(G);
//			cout << endl;
//			break;
//		default:
//			return ERROR;
//		}
//		menu();
//		cin >> i;
//	}
//	return 0;
//}
