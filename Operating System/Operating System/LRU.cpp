//#include<cstdlib>
//#include<iostream>
//
//#define Maximum 100
//
//int main(void)
//{
//	using namespace std;
//
//	int Frame;
//	int *Queue;
//	int *Queue_State;
//	int *Stack;
//
//	cout<<"Input number of frames : ";
//
//	while(cin>>Frame)						//输入Frame的数量
//	{
//		if(Frame>0)
//		{
//			Stack = new int[Frame] {0};			//初始化堆栈数组
//			Queue = new int[Maximum] {0};		//初始化页面流数组
//			Queue_State = new int[Maximum] {0};
//
//			cout<<"Input Pages In order, end with -1"<<endl;
//
//			int length = 0;
//			while(cin>>Queue[length])
//			{
//				if(length<100)
//				{
//					if(Queue[length]>=0)
//						length++;
//					else
//					{
//						length--;
//						for(int i = 0;i<=length;i++)
//							cout<<Queue[i]<<' ';
//						cout<<endl<<"Page number: "<<length<<endl;
//						break;
//					}
//				}
//				else
//					cout<<"Overflow"<<endl;
//			}
//
//			int flag = 0;
//			int temp;
//			bool hit;
//			int count = 0;
//
//			for(int i = 0;i<=length;i++)
//			{
//				if(Queue_State[Queue[i]]>0)				//页面命中
//				{
//					hit = true;
//					count++;
//					temp = Queue_State[Queue[i]]-1;		//获取命中页面在堆栈中的位置
//					while(temp>0)
//					{
//						Queue_State[Stack[temp-1]]++;	//堆栈中的页面位置向栈底推一位
//						Stack[temp] = Stack[temp-1];	//堆栈页面向栈底推一位
//						temp--;
//					}
//					Stack[temp] = Queue[i];				//把命中页抽调到栈顶
//					Queue_State[Queue[i]] = temp+1;		//置命中页位置为1
//				}
//				else
//				{
//					hit = false;
//					if(flag<Frame-1)						//堆栈未满
//					{
//						temp = flag;
//						while(temp>0)
//						{
//							Queue_State[Stack[temp-1]]++;	//堆栈中的页面位置向栈底推一位
//							Stack[temp] = Stack[temp-1];	//堆栈页面向栈底推一位
//							temp--;
//						}
//
//						Stack[temp] = Queue[i];				//把命中页抽调到栈顶
//						Queue_State[Queue[i]] = temp+1;		//置命中页位置为1
//						flag++;
//					}
//					else//堆栈已满
//					{
//						temp = flag;
//						Queue_State[Stack[temp]] = 0;
//						while(temp>0)
//						{
//							Queue_State[Stack[temp-1]]++;	//堆栈中的页面位置向栈底推一位
//							Stack[temp] = Stack[temp-1];	//堆栈页面向栈底推一位
//							temp--;
//						}
//						Stack[temp] = Queue[i];
//						Queue_State[Queue[i]] = temp+1;
//					}
//				}
//
//				cout<<Queue[i]<<"|\t";
//				for(int i = 0;i<=flag;i++)
//					cout<<Stack[i]<<'\t';
//				hit ? cout<<"命中" : cout<<"调入|替换";
//				cout<<'\t'<<(double)count/length*100<<endl;
//			}
//		}
//		else
//			break;
//	}
//	return 0;
//}