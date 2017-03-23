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
using System.Windows.Shapes;
using System.Xml;

namespace Contact_Book
{
	/// <summary>
	/// Confidential_Level_Signing_Up.xaml 的交互逻辑
	/// </summary>
	public partial class Confidential_Level_Signing_Up: Window
	{
		private int Confidential_Level_Value = -1;
		private string Admin_Pass;

		public Confidential_Level_Signing_Up(string User_Id,string Testify_Confidential)
		{
			this.Title=User_Id;
			Admin_Pass=Testify_Confidential;
			InitializeComponent();
			this.ShowDialog();
		}

		private void Button_Confidential_Level_Confirm(object sender,RoutedEventArgs e)
		{
			if(ComboBox_Confidential_Level.Text.Equals("User"))
			{
				Confidential_Level_Value=0;
			}
			else
			{
				if(TextBox_Admin_Password.Text.Equals(Admin_Pass))
				{
					if(ComboBox_Confidential_Level.Text.Equals("Administrator"))
						Confidential_Level_Value=1;
					else
						Confidential_Level_Value=2;
				}
			}
			this.Hide();
		}

		public int Get_Confidential_Level_Value()
		{
			return Confidential_Level_Value;
		}

		private void Button_Confidential_Level_Help(object sender,RoutedEventArgs e)//The reaction operation of Help Button
		{
			System.Windows.MessageBox.Show("In order to get help, please Contact Administrator or Developer by Deliangus@Foxmail.com!");
		}

		private void ComboBox_Confidential_Level_GotFocus(object sender,RoutedEventArgs e)
		{
			if(ComboBox_Confidential_Level.Text.Equals("User"))
			{
				TextBox_Admin_Password.IsEnabled=false;
			}
			else
			{
				TextBox_Admin_Password.IsEnabled=true;
			}
		}
	}
}
