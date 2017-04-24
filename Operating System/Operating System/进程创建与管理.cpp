#include<stdio.h>
#include<stdlib.h>

#define running 1 //用running 表示进程处于运行态
#define aready 2 //用aready表示进程处于就绪态
#define blocking 3 //用blocking表示进程处于等待态
#define TimeScaling 5 //用TimeScaling 表示时间片大小
#define Num_Max 10 //假定系统允许进程个数为10 

struct Process
{
	int Identifier;			//进程标识符
	int Status;				//进程状态
	int ax, bx, cx, dx;		//进程现场信息，通用寄存器内容
	int PC;					//进程现场信息，程序计数器内容
	int PSW;				//进程现场信息，程序状态字寄存器内容
	int next;				//下一个进程控制块的位置
}pcbarea[Num_Max]; //定义模拟进程控制块区域的数组

int PSW, AX, BX, CX, DX, PC, TIME; //模拟寄存器
int run; //定义指向正在运行进程的进程控制块的指针

struct
{
	int head;
	int tail;
}Point_Ready; //定义指向就绪队列的头指针head和尾指针tail

int block; //定义指向等待队列的指针
int pfree; //定义指向空闲进程控制块队列的指针

void sheduling()//进程调度函数
{
	int i;
	if (Point_Ready.head == -1) //空闲进程控制块队列为空，退出
	{
		printf("无就绪进程\n");
		return;
	}

	i = Point_Ready.head; //就绪队列头指针赋给i
	Point_Ready.head = pcbarea[Point_Ready.head].next;//就绪队列头指针后移

	if (Point_Ready.head == -1)
		Point_Ready.tail = -1;//就绪队列为空，修正尾指针Point_Ready.tail

	pcbarea[i].Status = running;//修改进程控制块状态
	TIME = TimeScaling; //设置相对时钟寄存器//恢复该进程现场信息：

	run = i;
	//恢复现场以继续运行
	AX = pcbarea[run].ax;
	BX = pcbarea[run].bx;
	CX = pcbarea[run].cx;
	DX = pcbarea[run].dx;
	PC = pcbarea[run].PC;

	PSW = pcbarea[run].PSW;//修改指向运行进程的指针
}

void create(int x)//创建进程
{
	int i;
	if (pfree == -1) //空闲进程控制块队列为空
	{
		printf("无空闲进程控制块，进程创建失败\n");
		return;
	}
	i = pfree;//取空闲进程控制块队列的第一个
	pfree = pcbarea[pfree].next;//pfree后移
	//填写该进程控制块内容：
	pcbarea[i].Identifier = x;//为进程分配唯一标识符
	pcbarea[i].Status = aready;//初始化进程的状态

	pcbarea[i].ax = x;//初始化通用寄存器数据
	pcbarea[i].bx = x;
	pcbarea[i].cx = x;
	pcbarea[i].dx = x;

	pcbarea[i].PC = x;//设置程序状态字内容
	pcbarea[i].PSW = x;//设置程序状态字内容

	if (Point_Ready.head != -1)//就绪队列不空时，挂入就绪队列方式
	{
		pcbarea[Point_Ready.tail].next = i;
		Point_Ready.tail = i;
		pcbarea[Point_Ready.tail].next = -1;
	}
	else//就绪队列空时,挂入就绪队列方式：
	{
		Point_Ready.head = i;
		Point_Ready.tail = i;
		pcbarea[Point_Ready.tail].next = -1;
	}
}

int main(void)
{
	int num,j;
	//初始化OS状态
	run = -1;
	Point_Ready.head = -1;
	Point_Ready.tail = -1;
	block = -1;
	pfree = 0;

	for (j = 0; j<Num_Max - 1; j++)
		pcbarea[j].next = j + 1;
	pcbarea[Num_Max - 1].next = -1;

	printf("输入进程编号(避免编号的冲突,以负数输入结束,最多可以创建10个进程):\n");
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
			printf("进程名 进程状态 寄存器内容:\tax\tbx\tcx\tdx\tpc\tpsw:\n");
			printf("%4d%10d\t\t%3d\t%3d\t%3d\t%3d\t%3d\t%3d\n", pcbarea[run].Identifier, pcbarea[run].Status, pcbarea[run].ax, pcbarea[run].bx, pcbarea[run].cx, pcbarea[run].dx, pcbarea[run].PC, pcbarea[run].PSW);
		}
	}
	system("pause");
	return 0;
}
