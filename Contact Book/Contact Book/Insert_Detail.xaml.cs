using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Contact_Book
{
	/// <summary>
	/// Insert_Detail.xaml 的交互逻辑
	/// </summary>
	public partial class Insert_Detail: Window
	{
		private int Insert_VerifyCode = -1;
		public Insert_Detail(string Title,int Verifycode)
		{
			this.Title=Title;
			this.Insert_VerifyCode=Verifycode;
			InitializeComponent();
			this.ShowDialog();
		}

		private void Button_Insert_Detail_OK_Click(object sender,RoutedEventArgs e)
		{
			string City = TextBox_City.Text;
			string Tel = TextBox_Tel.Text;
			string QQ = TextBox_QQ.Text;

			ContactData.Write_City(City);
			ContactData.Write_QQ(QQ);
			ContactData.Write_Tel(Tel);
			ContactData.Write_Name(this.Title);
			ContactData.Write_VerifyCode(Insert_VerifyCode);
			this.Close();
		}

		private void TextBox_Tel_Pasting(object sender,DataObjectPastingEventArgs e)
	        {
	            if (e.DataObject.GetDataPresent(typeof(String)))
	            {
	                String text = (String)e.DataObject.GetData(typeof(String));
	                if (!isNumberic(text))
	                { e.CancelCommand(); }
	            }
	            else { e.CancelCommand(); }
	        }

	        private void TextBox_Tel_PreviewKeyDown(object sender,System.Windows.Input.KeyEventArgs e)
	        {
	            if (e.Key == Key.Space)
	                e.Handled = true;
	        }
 
	        private void TextBox_Tel_PreviewTextInput(object sender,TextCompositionEventArgs e)
	        {
	            if (!isNumberic(e.Text))
	            {
	                e.Handled = true;
	            }
	            else
	                e.Handled = false;
	        }
	        //isDigit是否是数字
	        public static bool isNumberic(string _string)
	        {
	            if (string.IsNullOrEmpty(_string))
	                return false;
	            foreach (char c in _string)
	            {
	                if (!char.IsDigit(c))
	                    //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右
	                    return false;
	            }
	            return true;
	        }

		private void Button_Insert_Detail_Cancel_Click(object sender,RoutedEventArgs e)
		{
			ContactData.Write_VerifyCode(Insert_VerifyCode+1);
			this.Close();
		}

		private void TextBox_QQ_PreviewTextInput(object sender,TextCompositionEventArgs e)
		{
			if(!isNumberic(e.Text))
			{
				e.Handled = true;
			}
			else
				e.Handled = false;
		}

		private void TextBox_QQ_PreviewKeyDown(object sender,System.Windows.Input.KeyEventArgs e)
		{
			if(e.Key == Key.Space)
				e.Handled = true;
		}

		private void TextBox_QQ_Pasting(object sender,DataObjectPastingEventArgs e)
		{
			if(e.DataObject.GetDataPresent(typeof(String)))
			{
				String text = (String)e.DataObject.GetData(typeof(String));
				if(!isNumberic(text))
				{
					e.CancelCommand();
				}
			}
			else
			{
				e.CancelCommand();
			}
		}
	}
}
