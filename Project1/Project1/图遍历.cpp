//#include<iostream>
//#include<string>
//#include<queue>
//using namespace std;
//#define ERROR 1
//#define MAX_VERTEX_NUM 100
////////////邻接表存储表示//////////
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
//	int vexnum, arcnum;        //当前图的vexnum顶点数和arcnum弧数
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
//void CreateDG(ALGraph &G)////创图
//{
//	ArcNode *p, *q;
//	char v1, v2;
//	char v;
//	int i, j, k, n;
//	cout << "请输入图的顶点数和弧数：" << endl;
//	cin >> G.vexnum;
//	cin >> G.arcnum;
//	cout << "请输入顶点：" << endl;
//
//	for (i = 0; i<G.vexnum; i++)
//	{
//		cin >> v;
//		G.vertices[i].date = v;
//		G.vertices[i].firstarc = NULL;
//	}
//	cout << "请输入弧尾和弧头：" << endl;
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
//	cout << "图构建成功！";
//}
////----------------深度优先遍历--------------------//
//
//bool visited[MAX_VERTEX_NUM];//访问标志数组，通过该数组表示顶点是否已访问，当visited[i]为false时，表示点i并未被访问。
//int FirstAdjVex(ALGraph &G, int v)//找到在图G中的，与顶点G.vertices[v]相邻的未曾被访问的邻接点
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
//void DFS(ALGraph &G, int v)//对图G做深度优先遍历，遍历点从索引为v的顶点开始
//{
//	int w;
//	visited[v] = true;//设置索引为v的顶点为已访问
//	VisitFuc(G, v);//访问索引为v的顶点
//	for (w = FirstAdjVex(G, v); w >= 0; w = NextAdjVex(G, v))
//		if (!visited[w]) DFS(G, w);
//
//}
//void DFSTraverse(ALGraph &G)//深度优先遍历的起始函数，调用此函数开始遍历。
//{
//	int v;
//	for (v = 0; v<G.vexnum; v++)
//		visited[v] = false;//初始化，所有点都为被访问，统统设为false
//	cout << "深度优先搜索：" << endl;
//	for (v = 0; v<G.vexnum; v++)
//	{
//		if (!visited[v])
//			DFS(G, v);
//	}
//}
////-----------------广度优先遍历----------------------//
//
//void BFSTraverse(ALGraph &G)
//{
//	int v;
//	int w;
//	queue<int> q; //STL队列
//	for (v = 0; v<G.vexnum; v++)
//		visited[v] = false;
//	//  InitQueue(Q);
//	cout << "广度优先搜索：";
//	for (v = 0; v<G.vexnum; v++)
//	{
//		if (!visited[v])
//		{
//			visited[v] = true;
//			VisitFuc(G, v);
//			q.push(v);   //v进队
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
//	cout << "图的基本操作" << endl;
//	cout << "1、图的构建" << endl;
//	cout << "2、深度优先遍历" << endl;
//	cout << "3、广度优先遍历" << endl;
//	cout << "请输入数字进行选择:" << endl;
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
