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

namespace Contect_Book
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
			ContectData temp = new ContectData();
			temp.Write_City(City);
			temp.Write_QQ(QQ);
			temp.Write_Tel(Tel);
			temp.Write_Name(this.Title);
			temp.Write_VerifyCode(Insert_VerifyCode);
			this.Close();
		}

		private void KeyDown_Tel(object sender,System.Windows.Input.KeyEventArgs e)
		{
			if(e.Key>=Key.D0&&e.Key<=Key.D9)
				e.Handled=true;
		}

		private void KeyDown_QQ(object sender,System.Windows.Input.KeyEventArgs e)
		{
			if(e.Key>=Key.D0&&e.Key<=Key.D9)
				e.Handled=true;
		}

		private void Button_Insert_Detail_Cancel_Click(object sender,RoutedEventArgs e)
		{
			ContectData temp = new ContectData();
			temp.Write_VerifyCode(Insert_VerifyCode+1);
			this.Close();
		}
	}
}
