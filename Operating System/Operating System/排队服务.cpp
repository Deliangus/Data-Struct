#include<iostream>

using namespace std;

class pcb					//进程类
{
	public:
		char name;			//进程名
		int cometime;		//到达时间
		int runtime;		//进程运行时间
};

void main()
{
	pcb p[10];				//定义十个进程[槽]
	int workstart[10];
	int workend[10];
	int i = 0;				//进程计数
	char PCS_name;					
	int PCS_Come;
	int PCS_Runtime;

	cout << "请输入进程" << i << "的名字，进程的到达时间，进程的运行时间：" << endl;
	cin >> PCS_name >> PCS_Come >> PCS_Runtime;
	while (PCS_name != '#')
	{
		p[i].name = PCS_name;
		p[i].cometime = PCS_Come;
		p[i].runtime = PCS_Runtime;

		if (i == 0)
		{
			workstart[i] = p[i].cometime;
			workend[i] = p[i].runtime + p[i].cometime;
		}
		else
		{
			if (p[i].cometime >= workend[i - 1])
			{
				workstart[i] = p[i].cometime;
				workend[i] = p[i].runtime + p[i].cometime;
			}
			else
			{
				workstart[i] = workend[i - 1];
				workend[i] = workend[i - 1] + p[i].runtime;
			}
		}
		i++;
		cout << "请输入进程" << i << "的名字，进程的到达时间，进程的运行时间：" << endl;
		cin >> PCS_name >> PCS_Come >> PCS_Runtime;
	}

	cout << "先来先服务的调度顺序为：" << endl;
	for (int j = 0; j<i; j++)
		cout << "进程名：" << p[j].name << "开始时间：" << workstart[j] << "   完成时间" << workend[j] << endl;
}

