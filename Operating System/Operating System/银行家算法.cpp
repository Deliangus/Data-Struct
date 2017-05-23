#include<iostream>
#include<algorithm>

using namespace std;


int main(void)
{
	cout<<"输入进程数和资源种数"<<endl;
	int num_P;
	int num_R;
	while(cin>>num_P>>num_R)
	{
		int* Available = new int[num_R];		//各种资源的有效数量

		cout<<"依次输入各种资源的有效数量"<<endl;
		for(int i = 0;i<num_R;i++)
			cin>>Available[i];

		int**MAX = new int*[num_P];				//各进程使用的最大资源数量
		for(int i = 0;i<num_P;i++)
			MAX[i] = new int[num_R];

		cout<<"依次输入各进程使用的各种资源的最大数量"<<endl;
		for(int i = 0;i<num_P;i++)
		{
			cout<<"进程 "<<i<<": "<<endl;
			for(int j = 0;j<num_R;j++)
				cin>>MAX[i][j];
		}

		int**Alloc = new int*[num_P];			//各进程已分配的资源数量
		for(int i = 0;i<num_P;i++)
			Alloc[i] = new int[num_R];

		cout<<"依次输入各进程已分配资源的数量"<<endl;
		for(int i = 0;i<num_P;i++)
		{
			cout<<"进程 "<<i<<": "<<endl;
			for(int j = 0;j<num_R;j++)
				cin>>Alloc[i][j];
		}

		int**Need = new int*[num_P];			//各进程仍需的资源数量
		for(int i = 0;i<num_P;i++)
			Need[i] = new int[num_R];

		for(int i = 0;i<num_P;i++)
			for(int j = 0;j<num_R;j++)
			{
				Need[i][j] = MAX[i][j]-Alloc[i][j];
			}
		
		cout<<"当前资源分配情况"<<endl;
		for(int i = 0;i<num_P;i++)
		{
			cout<<"进程 "<<i<<":\t";
			for(int j = 0;j<num_R;j++)
			{
				cout<<Alloc[i][j]<<'\t';
			}
			cout<<endl;
		}

		cout<<endl<<"资源剩余"<<endl;
		for(int j = 0;j<num_R;j++)
		{
			cout<<Available[j]<<'\t';
		}
		cout<<endl<<endl;

		cout<<"输入进程号及其申请资源类和数量"<<endl;

		bool*Flag = new bool[num_P] {0};

		int pro,res,num;
		
		while(cin>>pro>>res>>num)
		{
			if(pro+res+num==0)
				break;
			else
			{
				if(num<=Need[pro][res])
				{
					if(num<=Available[res])
					{
						Need[pro][res] -= num;
						Available[res] -= num;
						Alloc[pro][res] += num;

						for(int i = 0;i<num_P;i++)
						{
							if(Flag[i])
								continue;
							for(int j = 0;j<num_R;j++)
							{
								if(Need[i][j]>Available[j])
								{
									Flag[i] = true;
									break;
								}
							}

							if(Flag[i])
							{
								if(i==num_P-1)
								{
									Need[pro][res] += num;
									Available[res] += num;
									Alloc[pro][res] -= num;
									cout<<"非安全申请被拒绝"<<endl;
								}
								continue;
							}
							else
							{
								cout<<"安全申请"<<endl;
								cout<<"当前资源分配情况"<<endl;
								for(int i = 0;i<num_P;i++)
								{
									cout<<"进程 "<<i<<":\t";
									for(int j = 0;j<num_R;j++)
									{
										cout<<Alloc[i][j]<<'\t';
									}
									cout<<endl;
								}

								cout<<endl<<"资源剩余"<<endl;
								for(int j = 0;j<num_R;j++)
								{
									cout<<Available[j]<<'\t';
								}
								cout<<endl<<endl;

								break;
							}
						}
					}
					else
						cout<<"非安全申请被拒绝"<<endl;
				}
				else
					cout<<"非安全申请被拒绝"<<endl;

				cout<<"输入进程号及其申请资源类和数量"<<endl;
			}
		}
	}

	return 0;
}
