#include<iostream>
#include<algorithm>

using namespace std;


int main(void)
{
	cout<<"�������������Դ����"<<endl;
	int num_P;
	int num_R;
	while(cin>>num_P>>num_R)
	{
		int* Available = new int[num_R];		//������Դ����Ч����

		cout<<"�������������Դ����Ч����"<<endl;
		for(int i = 0;i<num_R;i++)
			cin>>Available[i];

		int**MAX = new int*[num_P];				//������ʹ�õ������Դ����
		for(int i = 0;i<num_P;i++)
			MAX[i] = new int[num_R];

		cout<<"�������������ʹ�õĸ�����Դ���������"<<endl;
		for(int i = 0;i<num_P;i++)
		{
			cout<<"���� "<<i<<": "<<endl;
			for(int j = 0;j<num_R;j++)
				cin>>MAX[i][j];
		}

		int**Alloc = new int*[num_P];			//�������ѷ������Դ����
		for(int i = 0;i<num_P;i++)
			Alloc[i] = new int[num_R];

		cout<<"��������������ѷ�����Դ������"<<endl;
		for(int i = 0;i<num_P;i++)
		{
			cout<<"���� "<<i<<": "<<endl;
			for(int j = 0;j<num_R;j++)
				cin>>Alloc[i][j];
		}

		int**Need = new int*[num_P];			//�������������Դ����
		for(int i = 0;i<num_P;i++)
			Need[i] = new int[num_R];

		for(int i = 0;i<num_P;i++)
			for(int j = 0;j<num_R;j++)
			{
				Need[i][j] = MAX[i][j]-Alloc[i][j];
			}
		
		cout<<"��ǰ��Դ�������"<<endl;
		for(int i = 0;i<num_P;i++)
		{
			cout<<"���� "<<i<<":\t";
			for(int j = 0;j<num_R;j++)
			{
				cout<<Alloc[i][j]<<'\t';
			}
			cout<<endl;
		}

		cout<<endl<<"��Դʣ��"<<endl;
		for(int j = 0;j<num_R;j++)
		{
			cout<<Available[j]<<'\t';
		}
		cout<<endl<<endl;

		cout<<"������̺ż���������Դ�������"<<endl;

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
									cout<<"�ǰ�ȫ���뱻�ܾ�"<<endl;
								}
								continue;
							}
							else
							{
								cout<<"��ȫ����"<<endl;
								cout<<"��ǰ��Դ�������"<<endl;
								for(int i = 0;i<num_P;i++)
								{
									cout<<"���� "<<i<<":\t";
									for(int j = 0;j<num_R;j++)
									{
										cout<<Alloc[i][j]<<'\t';
									}
									cout<<endl;
								}

								cout<<endl<<"��Դʣ��"<<endl;
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
						cout<<"�ǰ�ȫ���뱻�ܾ�"<<endl;
				}
				else
					cout<<"�ǰ�ȫ���뱻�ܾ�"<<endl;

				cout<<"������̺ż���������Դ�������"<<endl;
			}
		}
	}

	return 0;
}
