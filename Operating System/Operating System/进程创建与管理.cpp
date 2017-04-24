#include<stdio.h>
#include<stdlib.h>

#define running 1 //��running ��ʾ���̴�������̬
#define aready 2 //��aready��ʾ���̴��ھ���̬
#define blocking 3 //��blocking��ʾ���̴��ڵȴ�̬
#define TimeScaling 5 //��TimeScaling ��ʾʱ��Ƭ��С
#define Num_Max 10 //�ٶ�ϵͳ������̸���Ϊ10 

struct Process
{
	int Identifier;			//���̱�ʶ��
	int Status;				//����״̬
	int ax, bx, cx, dx;		//�����ֳ���Ϣ��ͨ�üĴ�������
	int PC;					//�����ֳ���Ϣ���������������
	int PSW;				//�����ֳ���Ϣ������״̬�ּĴ�������
	int next;				//��һ�����̿��ƿ��λ��
}pcbarea[Num_Max]; //����ģ����̿��ƿ����������

int PSW, AX, BX, CX, DX, PC, TIME; //ģ��Ĵ���
int run; //����ָ���������н��̵Ľ��̿��ƿ��ָ��

struct
{
	int head;
	int tail;
}Point_Ready; //����ָ��������е�ͷָ��head��βָ��tail

int block; //����ָ��ȴ����е�ָ��
int pfree; //����ָ����н��̿��ƿ���е�ָ��

void sheduling()//���̵��Ⱥ���
{
	int i;
	if (Point_Ready.head == -1) //���н��̿��ƿ����Ϊ�գ��˳�
	{
		printf("�޾�������\n");
		return;
	}

	i = Point_Ready.head; //��������ͷָ�븳��i
	Point_Ready.head = pcbarea[Point_Ready.head].next;//��������ͷָ�����

	if (Point_Ready.head == -1)
		Point_Ready.tail = -1;//��������Ϊ�գ�����βָ��Point_Ready.tail

	pcbarea[i].Status = running;//�޸Ľ��̿��ƿ�״̬
	TIME = TimeScaling; //�������ʱ�ӼĴ���//�ָ��ý����ֳ���Ϣ��

	run = i;
	//�ָ��ֳ��Լ�������
	AX = pcbarea[run].ax;
	BX = pcbarea[run].bx;
	CX = pcbarea[run].cx;
	DX = pcbarea[run].dx;
	PC = pcbarea[run].PC;

	PSW = pcbarea[run].PSW;//�޸�ָ�����н��̵�ָ��
}

void create(int x)//��������
{
	int i;
	if (pfree == -1) //���н��̿��ƿ����Ϊ��
	{
		printf("�޿��н��̿��ƿ飬���̴���ʧ��\n");
		return;
	}
	i = pfree;//ȡ���н��̿��ƿ���еĵ�һ��
	pfree = pcbarea[pfree].next;//pfree����
	//��д�ý��̿��ƿ����ݣ�
	pcbarea[i].Identifier = x;//Ϊ���̷���Ψһ��ʶ��
	pcbarea[i].Status = aready;//��ʼ�����̵�״̬

	pcbarea[i].ax = x;//��ʼ��ͨ�üĴ�������
	pcbarea[i].bx = x;
	pcbarea[i].cx = x;
	pcbarea[i].dx = x;

	pcbarea[i].PC = x;//���ó���״̬������
	pcbarea[i].PSW = x;//���ó���״̬������

	if (Point_Ready.head != -1)//�������в���ʱ������������з�ʽ
	{
		pcbarea[Point_Ready.tail].next = i;
		Point_Ready.tail = i;
		pcbarea[Point_Ready.tail].next = -1;
	}
	else//�������п�ʱ,����������з�ʽ��
	{
		Point_Ready.head = i;
		Point_Ready.tail = i;
		pcbarea[Point_Ready.tail].next = -1;
	}
}

int main(void)
{
	int num,j;
	//��ʼ��OS״̬
	run = -1;
	Point_Ready.head = -1;
	Point_Ready.tail = -1;
	block = -1;
	pfree = 0;

	for (j = 0; j<Num_Max - 1; j++)
		pcbarea[j].next = j + 1;
	pcbarea[Num_Max - 1].next = -1;

	printf("������̱��(�����ŵĳ�ͻ,�Ը����������,�����Դ���10������):\n");
	scanf_s("%d", &num);
	while (num>0)
	{
		create(num);
		scanf_s("%d", &num);
	}

	while (Point_Ready.head >= 0)
	{
		sheduling();
		if (run != -1) {
			printf("������ ����״̬ �Ĵ�������:\tax\tbx\tcx\tdx\tpc\tpsw:\n");
			printf("%4d%10d\t\t%3d\t%3d\t%3d\t%3d\t%3d\t%3d\n", pcbarea[run].Identifier, pcbarea[run].Status, pcbarea[run].ax, pcbarea[run].bx, pcbarea[run].cx, pcbarea[run].dx, pcbarea[run].PC, pcbarea[run].PSW);
		}
	}
	system("pause");
	return 0;
}
