using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Contact_Book
{
	/// <summary>
	/// Sign_in.xaml 的交互逻辑
	/// </summary>
	public partial class Sign_in:Window
	{
		private static string User_ID;

		#region 建立登陆窗口
		public Sign_in()
		{
			this.Height=SystemParameters.PrimaryScreenHeight;	//获取设备的屏幕分辨率高度并作为程序高度
			this.Width=											//计算出对应的符合背景图片比例的宽度
				this.Height*(double)Properties.Resources.Sign_In_BackGround.Width/Properties.Resources.Sign_In_BackGround.Height;
			InitializeComponent();
			this.ShowDialog();
		}
		#endregion

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if(User_ID==null)//在输入的用户名为空时关闭窗口将导致整个通讯录系统退出
				System.Windows.Forms.Application.Exit();
		}

		#region 读取权限和帐户名

		public static string Get_Confidential_User_ID()
		{
			return User_ID;
		}
		#endregion

#region Button Sign In的交互逻辑
private void Click_Sign_In(object sender,RoutedEventArgs e)
{
	if(this.TextBox_Password.Password.Equals(string.Empty)||this.TextBox_User_ID.Text.Equals(string.Empty))//账户和密码非空
	{
		System.Windows.MessageBox.Show("Invalid input of User ID and Password");
	}
	else
	{
		using(SqlConnection con = new SqlConnection(Properties.Settings.Default.DataBaseConnectionString))
		{
			try
			{
				SqlCommand cmd = new SqlCommand("SELECT ID,Password FROM Accounts WHERE ID='"+this.TextBox_User_ID.Text.Trim()+"'",con);
				SqlDataAdapter AutoSda = new SqlDataAdapter();
				AutoSda.SelectCommand=cmd;
				SqlCommandBuilder ComBuild = new SqlCommandBuilder(AutoSda);
				con.Open();
				DataTable dt = new DataTable();
				AutoSda.Fill(dt);
				DataRow []Rows_Selected = dt.Select("ID='"+this.TextBox_User_ID.Text.Trim()+"'");
				if(Rows_Selected.Count()==0)
				{
					System.Windows.MessageBox.Show("The User ID does not exist,please varify and try again, or Sign Up as a new user!");
				}
				else
				{
					if(TextBox_Password.Password.Equals(Rows_Selected[0]["Password"].ToString().Trim()))
					{
						User_ID=TextBox_User_ID.Text;
						this.Close();
					}
				}
			}
			catch(Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
			finally
			{
				con.Close();
			}
		}
	}
}

		#endregion

#region Button Sign Up的交互逻辑
		private void Click_Sign_Up(object sender,RoutedEventArgs e)
		{

			if(this.TextBox_Password.Password.Equals(string.Empty)||this.TextBox_User_ID.Text.Equals(string.Empty))
			{
				System.Windows.MessageBox.Show("Invalid input of User ID and Password");
				return;
			}
			else
			{
				using(SqlConnection con = new SqlConnection(Properties.Settings.Default.DataBaseConnectionString))
				{
					try
					{
						SqlCommand cmd = new SqlCommand("SELECT ID,Password FROM Accounts WHERE ID='"+this.TextBox_User_ID.Text.Trim()+"'",con);
						SqlDataAdapter AutoSda = new SqlDataAdapter();
						AutoSda.SelectCommand=cmd;
						SqlCommandBuilder ComBuild = new SqlCommandBuilder(AutoSda);
						con.Open();
						DataTable dt = new DataTable();
						AutoSda.Fill(dt);
						DataRow[] Rows_Selected = dt.Select("ID='"+this.TextBox_User_ID.Text.Trim()+"'");
						if(Rows_Selected.Count()==0)
						{
							object[] temp = new object[2];
							temp[0]=this.TextBox_User_ID.Text.Trim();
							temp[1]=this.TextBox_Password.Password.Trim();
							dt.LoadDataRow(temp,true);
							dt.AcceptChanges();
							AutoSda.Update(dt);
							User_ID=this.TextBox_User_ID.Text.Trim();
							this.Close();
						}
						else
						{
							System.Windows.MessageBox.Show("The User ID is existed, please try another!");
						}
					}
					catch(Exception ex)
					{
						System.Windows.MessageBox.Show(ex.Message);
					}
					finally
					{
						con.Close();
					}
				}
			}
		}
		#endregion
	}
}
