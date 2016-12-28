//#include<stdio.h>
//#include<iostream>
//#include <stdlib.h>
//#define OK 1
//#define ERROR 0;
//#define STACK 100
//#define STACKINCREASE 10
//#define TRUE 1
//#define FALSE 0
//using namespace std;
//typedef int TyElme;
//typedef struct
//{
//	TyElme *base;
//	TyElme *top;
//	int stacksize;
//} sqstack;
//typedef int Status;
//int Initstack(sqstack &S)
//{
//	S.base = (TyElme *)malloc(STACK*sizeof(TyElme));
//	if(!S.base)
//		exit(0);
//	S.top = S.base;
//	S.stacksize = STACK;
//	return OK;
//}
//int GetTop(sqstack &S,TyElme &e)
//{
//	if(S.top==S.base)
//		return ERROR;
//	e = *(S.top-1);
//	return OK;
//}
//int Push(sqstack &S,TyElme e)
//{
//	if(S.top-S.base>=S.stacksize)
//	{
//
//		S.base = (TyElme*)realloc(S.base,
//			(S.stacksize+STACKINCREASE)*sizeof(TyElme));
//		if(!S.base)
//			exit(0);
//		S.top = S.base+S.stacksize;
//		S.stacksize += STACKINCREASE;
//	}
//	*S.top++ = e;
//	return OK;
//}
//Status Pop(sqstack &S,TyElme &e)
//{
//	if(S.top==S.base)
//		return ERROR;
//	e = *--S.top;
//	return OK;
//}
//Status StackEmpty(sqstack S)
//{
//
//	if(S.top==S.base)
//		return TRUE;
//	else
//		return FALSE;
//}
//int StackTraverse(sqstack S)
//{
//	if(S.base==S.top)
//		puts("the stack is empty!");
//	else
//	{
//		while(S.top!=S.base)
//		{
//			printf("%d ",*(S.base));
//			S.base++;
//
//
//		}
//	}
//	printf("\n");
//	return OK;
//}
//
//int main()
//{
//	sqstack S;
//	TyElme a,e;
//	Initstack(S);
//	char Poin;
//	printf("请输入要执行的命令：\n");
//	printf("        P:压入一个数.\n");
//	printf("        D:弹出顶个元素.\n");
//	printf("        G:取出顶个元素.\n");
//	printf("        S:打印栈数据.\n");
//	printf("        E:退出当前栈.\n");
//
//	while(cin>>Poin)
//	{
//
//		if(Poin=='P')
//		{
//			cin>>a;
//			if(a!=0)
//				Push(S,a);
//		}
//		if(Poin=='G')
//		{
//			printf("栈顶元素为\n");
//			GetTop(S,e);
//			printf("%d\n",e);
//		}
//		if(Poin=='D')
//		{
//			Pop(S,e);
//		}
//		if(Poin=='E')
//		{
//			printf("退栈\n");
//			while(!StackEmpty(S))
//			{
//				Pop(S,e);
//				cout<<e<<" ";
//			}
//			printf("\n");
//		}
//		if(Poin=='S')
//		{
//			StackTraverse(S);
//		}
//	}
//}
//
//
//
