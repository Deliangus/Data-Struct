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
//	while(cin>>Frame)						//����Frame������
//	{
//		if(Frame>0)
//		{
//			Stack = new int[Frame] {0};			//��ʼ����ջ����
//			Queue = new int[Maximum] {0};		//��ʼ��ҳ��������
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
//				if(Queue_State[Queue[i]]>0)				//ҳ������
//				{
//					hit = true;
//					count++;
//					temp = Queue_State[Queue[i]]-1;		//��ȡ����ҳ���ڶ�ջ�е�λ��
//					while(temp>0)
//					{
//						Queue_State[Stack[temp-1]]++;	//��ջ�е�ҳ��λ����ջ����һλ
//						Stack[temp] = Stack[temp-1];	//��ջҳ����ջ����һλ
//						temp--;
//					}
//					Stack[temp] = Queue[i];				//������ҳ�����ջ��
//					Queue_State[Queue[i]] = temp+1;		//������ҳλ��Ϊ1
//				}
//				else
//				{
//					hit = false;
//					if(flag<Frame-1)						//��ջδ��
//					{
//						temp = flag;
//						while(temp>0)
//						{
//							Queue_State[Stack[temp-1]]++;	//��ջ�е�ҳ��λ����ջ����һλ
//							Stack[temp] = Stack[temp-1];	//��ջҳ����ջ����һλ
//							temp--;
//						}
//
//						Stack[temp] = Queue[i];				//������ҳ�����ջ��
//						Queue_State[Queue[i]] = temp+1;		//������ҳλ��Ϊ1
//						flag++;
//					}
//					else//��ջ����
//					{
//						temp = flag;
//						Queue_State[Stack[temp]] = 0;
//						while(temp>0)
//						{
//							Queue_State[Stack[temp-1]]++;	//��ջ�е�ҳ��λ����ջ����һλ
//							Stack[temp] = Stack[temp-1];	//��ջҳ����ջ����һλ
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
//				hit ? cout<<"����" : cout<<"����|�滻";
//				cout<<'\t'<<(double)count/length*100<<endl;
//			}
//		}
//		else
//			break;
//	}
//	return 0;
//}