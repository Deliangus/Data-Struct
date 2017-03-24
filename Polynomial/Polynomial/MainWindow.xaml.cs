using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Polynomial
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow:Window
	{
		private PolynNode HeadX, Result_Add, HeadY,Result_And;

		public MainWindow()
		{
			HeadX=null;
			HeadY=null;
			InitializeComponent();
		}

		private void Click_CreatpolynX(object sender,RoutedEventArgs e)
		{
			Result_Add=new PolynNode(0,-1);
			Result_And=new PolynNode(0,-1);
			if(Int32.Parse(TextBox_XA.Text.Trim())!=0)
			{

				if(HeadX==null)
				{
					HeadX=new PolynNode(Int32.Parse(TextBox_XA.Text.Trim()),Int32.Parse(TextBox_XB.Text.Trim()));
				}
				else
				{
					float coef = Int32.Parse(TextBox_XA.Text.Trim());
					int expn = Int32.Parse(TextBox_XB.Text.Trim());
					PolynNode Temp = Get_ExpnX(expn);
					if(Temp==HeadX)
					{
						if(expn>HeadX.expn)
							HeadX.Next=new PolynNode(coef,expn);
						else
						{
							Temp=new PolynNode(coef,expn,HeadX);
							HeadX=Temp;
						}
					}
					else
					{
						if(Temp.expn==expn)
							System.Windows.MessageBox.Show("x^"+expn.ToString()+"existed");
						else
						{
							PolynNode temp = new PolynNode(coef,expn,Temp.Next);
							Temp.Next=temp;
						}
					}
				}

				TextBlock_X.Text=Accumulate(HeadX);
			}
			TextBox_XA.Text=string.Empty;
			TextBox_XB.Text=string.Empty;
			Operation_Add(HeadX,HeadY,Result_Add);
			TextBlock_Result_Add.Text=Accumulate(Result_Add.Next);
			Operation_And(HeadX,HeadY,Result_And);
			TextBlock_Result_And.Text=Accumulate(Result_And.Next);
		}

		private void Click_CreatpolynY(object sender,RoutedEventArgs e)
		{
			if(Int32.Parse(TextBox_YA.Text.Trim())!=0)
			{
				if(HeadY==null)
				{
					HeadY=new PolynNode(Int32.Parse(TextBox_YA.Text.Trim()),Int32.Parse(TextBox_YB.Text.Trim()));
				}
				else
				{
					float coef = Int32.Parse(TextBox_YA.Text.Trim());
					int expn = Int32.Parse(TextBox_YB.Text.Trim());
					PolynNode Temp = Get_ExpnY(expn);
					if(Temp==HeadY)
					{
						if(expn>HeadY.expn)
							HeadY.Next=new PolynNode(coef,expn);
						else
						{
							Temp=new PolynNode(coef,expn,HeadY);
							HeadY=Temp;
						}
					}
					else
					{
						if(Temp.expn==expn)
							System.Windows.MessageBox.Show("Y^"+expn.ToString()+"existed");
						else
						{
							PolynNode temp = new PolynNode(coef,expn,Temp.Next);
							Temp.Next=temp;
						}
					}
				}
				TextBlock_Y.Text=Accumulate(HeadY);
			}
			TextBox_YA.Text=string.Empty;
			TextBox_YB.Text=string.Empty;
			Operation_Add(HeadX,HeadY,Result_Add);
			TextBlock_Result_Add.Text=Accumulate(Result_Add.Next);
			Operation_And(HeadX,HeadY,Result_And);
			TextBlock_Result_And.Text=Accumulate(Result_And.Next);
		}

		private PolynNode Get_ExpnX(int Expn)
		{
			PolynNode Temp = HeadX;
			PolynNode LastTemp = Temp;
			while(Temp!=null)
			{
				if(Temp.expn>Expn)
					break;
				else
				{
					LastTemp=Temp;
					Temp=Temp.Next;
				}
			}
			return LastTemp;
		}

		private PolynNode Get_ExpnY(int Expn)
		{
			PolynNode Temp = HeadY;
			PolynNode LastTemp = Temp;
			while(Temp!=null)
			{
				if(Temp.expn>Expn)
					break;
				else
				{
					LastTemp=Temp;
					Temp=Temp.Next;
				}
			}
			return LastTemp;
		}

		private void Operation_Add(PolynNode X,PolynNode Y,PolynNode Z)
		{
			if(Z!=null)
			{
				if(X==null&&Y==null)
					;
				else if(Y!=null&&X==null)
				{
					Z.Next=Y.Clone();
					Y=Y.Next;
					Z=Z.Next;
					Operation_Add(X,Y,Z);
				}
				else if(Y==null&&X!=null)
				{
					Z.Next=X.Clone();
					X=X.Next;
					Z=Z.Next;
					Operation_Add(X,Y,Z);
				}
				else
				{
					if(X.expn>Y.expn)
					{
						Z.Next=Y.Clone();
						Y=Y.Next;
						Z=Z.Next;
						Operation_Add(X,Y,Z);
					}
					else if(X.expn==Y.expn)
					{
						Z.Next=X+Y;
						X=X.Next;
						Y=Y.Next;
						Z=Z.Next;
						Operation_Add(X,Y,Z);
					}
					else
					{
						Z.Next=X.Clone();
						X=X.Next;
						Z=Z.Next;
						Operation_Add(X,Y,Z);
					}
				}
			}
		}

		private void Operation_And(PolynNode X,PolynNode Y,PolynNode Z)
		{
			if(X==null||Y==null)
				;
			else
			{
				PolynNode Head = new PolynNode(0,-1);
				Operation_Add(Operation_And(X,Y),null,Head);
				System.Windows.MessageBox.Show(Accumulate(Head.Next));
				X=X.Next;
				while(X!=null)
				{
					PolynNode Temp = new PolynNode(0,-1);
					Operation_Add(Operation_And(X,Y),Head.Next,Temp);
					Head=Temp;
					X=X.Next;
					System.Windows.MessageBox.Show(Accumulate(Head.Next));
				}
				Z.Next = Head.Next;
			}
		}

		private PolynNode Operation_And(PolynNode X,PolynNode Y)
		{
			if(X==null)
				return null;
			else
			{
				if(Y!=null)
				{
					PolynNode Head;
					Head=X*Y;
					Y=Y.Next;
					PolynNode temp = Head;
					while(Y!=null)
					{
						temp.Next=X*Y;
						Y=Y.Next;
						temp=temp.Next;
					}
					return Head;
				}
			}
			return null;
		}
		#region 串联幂项
		private string Accumulate(PolynNode T_Head)
		{
			string temp;
			if(T_Head==null)
				return null;
			else
			{
				temp=T_Head.ToString();
				T_Head=T_Head.Next;
				while(T_Head!=null)
				{
					temp+="+"+T_Head.ToString();
					T_Head=T_Head.Next;
				}
				return temp;
			}
			#endregion
		}
	}

	#region 链表类定义
	public class PolynNode
	{
		public float coef//系数
		{
			get;
			set;
		}
		public int expn//指数
		{
			get;
			set;
		}
		public PolynNode Next
		{
			get;
			set;
		}

		public PolynNode()
		{
			coef=0;
			expn=0;
			Next=null;
		}

		public PolynNode(float Tcoef,int Texpn)
		{
			coef=Tcoef;
			expn=Texpn;
			Next=null;
		}

		public PolynNode(float Tcoef,int Texpn,PolynNode TNext)
		{
			coef=Tcoef;
			expn=Texpn;
			Next=TNext;
		}

		public override string ToString()
		{
			if(expn!=0)
				return coef.ToString()+"x^"+expn.ToString();
			else
				return coef.ToString();
		}

		public PolynNode Clone()
		{
			return new PolynNode(this.coef,this.expn);
		}

		public static PolynNode operator+(PolynNode A,PolynNode B)
		{
			if(A.expn==B.expn)
				return new PolynNode(A.coef+B.coef,A.expn);
			else
				return null;
		}

		public static PolynNode operator *(PolynNode A,PolynNode B)
		{
			if(A.coef*B.coef==0)
				return null;
			else
				return new PolynNode(A.coef+B.coef,A.expn+B.expn);
		}
	}
}
#endregion
