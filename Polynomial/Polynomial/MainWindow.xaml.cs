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
    public partial class MainWindow : Window
    {
        private PolynNode HeadX,Input,HeadY;

        public MainWindow()
        {
            HeadX = null;
            Input = null;
            HeadY = null;
            InitializeComponent();
        }

        private void Click_CreatpolynX(object sender, RoutedEventArgs e)
        {
            if (HeadX == null)
            {
                HeadX = new PolynNode(Int32.Parse(TextBox_XA.Text.Trim()), Int32.Parse(TextBox_XB.Text.Trim()));
            }
            else
            {
                float coef = Int32.Parse(TextBox_XA.Text.Trim());
                int expn = Int32.Parse(TextBox_XB.Text.Trim());
                PolynNode Temp = Get_ExpnX(expn);
                if(Temp==HeadX)
                {
                    Temp = new PolynNode(coef, expn, HeadX);
                    HeadX = Temp;
                }
                else
                {
                    if (Temp.expn == expn)
                        System.Windows.MessageBox.Show("x^" + expn.ToString() + "existed");
                    else
                    {
                        PolynNode temp = new PolynNode(coef, expn, Temp.Next);
                        Temp.Next = temp;
                    }
                }
            }

            TextBlock_X.Text = Accumulate(HeadX);
        }

        private void Click_CreatpolynY(object sender, RoutedEventArgs e)
        {
            if (HeadY == null)
            {
                HeadY = new PolynNode(Int32.Parse(TextBox_YA.Text.Trim()), Int32.Parse(TextBox_YB.Text.Trim()));
            }
            else
            {
                float coef = Int32.Parse(TextBox_YA.Text.Trim());
                int expn = Int32.Parse(TextBox_YB.Text.Trim());
                PolynNode Temp = Get_ExpnY(expn);
                if (Temp == HeadY)
                {
                    Temp = new PolynNode(coef, expn, HeadY);
                    HeadY = Temp;
                }
                else
                {
                    if (Temp.expn == expn)
                        System.Windows.MessageBox.Show("Y^" + expn.ToString() + "existed");
                    else
                    {
                        PolynNode temp = new PolynNode(coef, expn, Temp.Next);
                        Temp.Next = temp;
                    }
                }
            }
            TextBlock_Y.Text = Accumulate(HeadY);
        }

        private PolynNode Get_ExpnX(int Expn)
        {
            PolynNode Temp = HeadX;
            PolynNode LastTemp = Temp;
            while(Temp!=null)
            {
                if (Temp.expn > Expn)
                    break;
                else
                {
                    LastTemp = Temp;
                    Temp = Temp.Next;
                }
            }
            return LastTemp;
        }

        private PolynNode Get_ExpnY(int Expn)
        {
            PolynNode Temp = HeadY;
            PolynNode LastTemp = Temp;
            while (Temp != null)
            {
                if (Temp.expn > Expn)
                    break;
                else
                {
                    LastTemp = Temp;
                    Temp = Temp.Next;
                }
            }
            return LastTemp;
        }

        private string Accumulate(PolynNode T_Head)
        {
            string temp;
            if (T_Head == null)
                return null;
            else
            {
                temp = T_Head.ToString();
                T_Head = T_Head.Next;
                while (T_Head != null)
                {
                    temp += "+" + T_Head.ToString();
                    T_Head = T_Head.Next;
                }
                return temp;
            }
        }
    }

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
            coef = 0;
            expn = 0;
            Next = null;
        }

        public PolynNode(float Tcoef,int Texpn)
        {
            coef = Tcoef;
            expn = Texpn;
            Next = null;
        }

        public PolynNode(float Tcoef, int Texpn,PolynNode TNext)
        {
            coef = Tcoef;
            expn = Texpn;
            Next = TNext;
        }

        public override string ToString()
        {
            return coef.ToString() + "x^" + expn.ToString();
        }
    }
}
