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
//	printf("������Ҫִ�е����\n");
//	printf("        P:ѹ��һ����.\n");
//	printf("        D:��������Ԫ��.\n");
//	printf("        G:ȡ������Ԫ��.\n");
//	printf("        S:��ӡջ����.\n");
//	printf("        E:�˳���ǰջ.\n");
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
//			printf("ջ��Ԫ��Ϊ\n");
//			GetTop(S,e);
//			printf("%d\n",e);
//		}
//		if(Poin=='D')
//		{
//			Pop(S,e);
//		}
//		if(Poin=='E')
//		{
//			printf("��ջ\n");
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
