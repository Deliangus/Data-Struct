#include<iostream>

using namespace std;

class pcb					//������
{
	public:
		char name;			//������
		int cometime;		//����ʱ��
		int runtime;		//��������ʱ��
};

void main()
{
	pcb p[10];				//����ʮ������[��]
	int workstart[10];
	int workend[10];
	int i = 0;				//���̼���
	char PCS_name;					
	int PCS_Come;
	int PCS_Runtime;

	cout << "���������" << i << "�����֣����̵ĵ���ʱ�䣬���̵�����ʱ�䣺" << endl;
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
		cout << "���������" << i << "�����֣����̵ĵ���ʱ�䣬���̵�����ʱ�䣺" << endl;
		cin >> PCS_name >> PCS_Come >> PCS_Runtime;
	}

	cout << "�����ȷ���ĵ���˳��Ϊ��" << endl;
	for (int j = 0; j<i; j++)
		cout << "��������" << p[j].name << "��ʼʱ�䣺" << workstart[j] << "   ���ʱ��" << workend[j] << endl;
}

