//#include<iostream>
//#include<algorithm>
//
//using namespace std;
//
//class pcb					//������
//{
//public:
//	char name;			//������
//	int cometime;		//����ʱ��
//	int runtime;		//��������ʱ��
//};
//
//bool Cometimp_CMP(pcb& a,pcb& b)
//{
//	return a.runtime<b.runtime;
//}
//
//int main(void)
//{
//	pcb p[10];				//����ʮ������[��]
//	int workstart[10];
//	int workend[10];
//	int i = 0;				//���̼���
//	char PCS_name;
//	int PCS_Come;
//	int PCS_Runtime;
//
//	cout<<"���������"<<i<<"�����֣����̵ĵ���ʱ�䣬���̵�����ʱ�䣺"<<endl;
//	cin>>PCS_name>>PCS_Come>>PCS_Runtime;
//
//	while(PCS_name!='#')
//	{
//		p[i].name = PCS_name;
//		p[i].cometime = PCS_Come;
//		p[i].runtime = PCS_Runtime;
//
//		i++;
//		cout<<"���������"<<i<<"�����֣����̵ĵ���ʱ�䣬���̵�����ʱ�䣺"<<endl;
//		cin>>PCS_name>>PCS_Come>>PCS_Runtime;
//	}
//	sort(p,p+i-1,Cometimp_CMP);
//
//	//for(int j = 0;j<i;j++)
//		//cout<<p[j].name<<'\t'<<p[j].cometime<<'\t'<<p[j].runtime<<endl;
//
//	for(int j = 0;j<i;j++)
//	{
//		if(j==0)			//��ʼ���е�һ������
//		{
//			workstart[j] = p[j].cometime;				//��һ�����̵����п�ʼʱ�伴�䵽��ʱ��
//			workend[j] = p[j].runtime+p[j].cometime;	//��һ�����̵Ľ�����ʼʱ�伴�䵽��ʱ��+����ʱ�䡣
//		}
//		else
//		{
//			if(p[j].cometime>=workend[j-1])		//��һ�����̽����󵽴�Ľ���
//			{
//				workstart[j] = p[j].cometime;			//��ʼ����ʱ�伴�䵽��ʱ��
//				workend[j] = p[j].runtime+p[j].cometime;
//			}
//			else										//��һ�����̽���ǰ����Ľ���
//			{
//				workstart[j] = workend[j-1];			//��ʼ����ʱ������һ�����̵Ľ���ʱ��
//				workend[j] = workend[j-1]+p[j].runtime;
//			}
//		}
//	}
//
//	cout<<"�����ȷ���ĵ���˳��Ϊ��"<<endl;
//	for(int j = 0; j<i; j++)
//	{
//		cout<<"��������"<<p[j].name<<"\t����ʱ�䣺"<<p[j].cometime;
//		cout<<"��ʼʱ�䣺"<<workstart[j];
//		cout<<"   ���ʱ��"<<workend[j]<<endl;
//	}
//	system("pause");
//
//	return 0;
//}
//
