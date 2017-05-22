//#include<iostream>
//#include<algorithm>
//
//using namespace std;
//
//class pcb					//进程类
//{
//public:
//	char name;			//进程名
//	int cometime;		//到达时间
//	int runtime;		//进程运行时间
//};
//
//bool Cometimp_CMP(pcb& a,pcb& b)
//{
//	return a.runtime<b.runtime;
//}
//
//int main(void)
//{
//	pcb p[10];				//定义十个进程[槽]
//	int workstart[10];
//	int workend[10];
//	int i = 0;				//进程计数
//	char PCS_name;
//	int PCS_Come;
//	int PCS_Runtime;
//
//	cout<<"请输入进程"<<i<<"的名字，进程的到达时间，进程的运行时间："<<endl;
//	cin>>PCS_name>>PCS_Come>>PCS_Runtime;
//
//	while(PCS_name!='#')
//	{
//		p[i].name = PCS_name;
//		p[i].cometime = PCS_Come;
//		p[i].runtime = PCS_Runtime;
//
//		i++;
//		cout<<"请输入进程"<<i<<"的名字，进程的到达时间，进程的运行时间："<<endl;
//		cin>>PCS_name>>PCS_Come>>PCS_Runtime;
//	}
//	sort(p,p+i-1,Cometimp_CMP);
//
//	//for(int j = 0;j<i;j++)
//		//cout<<p[j].name<<'\t'<<p[j].cometime<<'\t'<<p[j].runtime<<endl;
//
//	for(int j = 0;j<i;j++)
//	{
//		if(j==0)			//开始运行第一个进程
//		{
//			workstart[j] = p[j].cometime;				//第一个进程的运行开始时间即其到达时间
//			workend[j] = p[j].runtime+p[j].cometime;	//第一个进程的结束开始时间即其到达时间+运行时间。
//		}
//		else
//		{
//			if(p[j].cometime>=workend[j-1])		//上一个进程结束后到达的进程
//			{
//				workstart[j] = p[j].cometime;			//开始运行时间即其到达时间
//				workend[j] = p[j].runtime+p[j].cometime;
//			}
//			else										//上一个进程结束前到达的进程
//			{
//				workstart[j] = workend[j-1];			//开始运行时间是上一个进程的结束时间
//				workend[j] = workend[j-1]+p[j].runtime;
//			}
//		}
//	}
//
//	cout<<"先来先服务的调度顺序为："<<endl;
//	for(int j = 0; j<i; j++)
//	{
//		cout<<"进程名："<<p[j].name<<"\t到达时间："<<p[j].cometime;
//		cout<<"开始时间："<<workstart[j];
//		cout<<"   完成时间"<<workend[j]<<endl;
//	}
//	system("pause");
//
//	return 0;
//}
//
