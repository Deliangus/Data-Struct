//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Polynomial
//{
//    class Polynomial_Class
//    {
//        private static int sign = -1;

//        public Polynomial_Class()
//        {
//            unsafe
//            {
//                PolynNode f, g;
//            }
//        }

//        private PolynNode creatpolyn()
//        {
//            PolynNode head, inpt;
//            float coef;
//            int expn;
//            head = new PolynNode();//创建链表头 
//            head.Next = null;
//            //printf_s("请输入一元多项式%c:(格式是：系数 指数；以0 0 结束！)\n");
//            //scanf_s_s("%f %d", &coef, &expn);
//            while (coef != 0)
//            {
//                inpt = (PolynNode*)malloc(sizeof(PolynNode));//创建新链节 
//                inpt->coef = coef;
//                inpt->expn = expn;
//                inpt->next = NULL;
//                insert(head, inpt);//不然就查找位置并且插入新链节 
//                                   //printf_s("请输入一元多项式%c的下一项:(以0 0 结束！)\n"); 
//                scanf_s_s("%e %d", &coef, &expn);
//            }
//            return head;
//        }
//    }

//    public class PolynNode
//    {
//        float coef;//系数
//        int expn;//指数
//        public PolynNode Next;

//        public PolynNode()
//        {
//            coef = 0;
//            expn = 0;
//            Next = null;
//        }
//    }

    
//}
