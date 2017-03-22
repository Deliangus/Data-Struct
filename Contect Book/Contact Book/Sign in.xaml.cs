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
using System.IO;
using System.Windows.Forms;

namespace Contact_Book
{
	/// <summary>
	/// Sign_in.xaml 的交互逻辑
	/// </summary>
	public partial class Sign_in: Window
	{
		private string NodeTree = "Config";//Config/
		private string XmlPath = System.Environment.CurrentDirectory+@"\Resources\MeiDiBianPinKongTiao.xml";
		private XmlDocument Doc;
		private static string User_ID;

		#region 建立登陆窗口和载入账户目录
		public Sign_in()
		{
			InitialAccounts();
			//System.Windows.MessageBox.Show(Properties.Resources.Sign_In_BackGround.Width.ToString()+"/"+ Properties.Resources.Sign_In_BackGround.Height.ToString());
			this.Height = SystemParameters.PrimaryScreenHeight;
			this.Width = this.Height *(double)Properties.Resources.Sign_In_BackGround.Width/Properties.Resources.Sign_In_BackGround.Height;
            InitializeComponent();
			this.ShowDialog();
		}
		#endregion
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if(User_ID==null)
				e.Cancel = true;
		}

		#region 读取权限和帐户名
		private void InitialAccounts()
		{
			Doc = new XmlDocument();
			if(File.Exists(XmlPath))
			{
				try
				{
					Doc.Load(XmlPath);
				}
				catch(Exception e)
				{
					System.Windows.MessageBox.Show(e.Message + "####Sign in/Sign_in/Doc.Load(XmlPath)####831vtb");
				}
			}
			else
			{
				Doc.AppendChild(Doc.CreateXmlDeclaration("1.0","utf-8",null));//XML文件声明

				XmlNode temp = Doc.CreateNode(XmlNodeType.Element,"Config",string.Empty);//创建根节点Config

				XmlElement Admin = Doc.CreateElement("Administrator");//创建管理员账户
				Admin.InnerText = "Romantic";

				temp.AppendChild(Admin);//管理员并入根节点
				Doc.AppendChild(temp);//根节点写入文件

				XmlPath = System.Environment.CurrentDirectory + @"\Resources\MeiDiBianPinKongTiao.xml";//刷新XML存储目录
				if(Directory.Exists(System.Environment.CurrentDirectory + @"\Resources"))
				{
					try
					{
						Doc.Save(XmlPath);//用新的存储目录来存放登录信息
					}
					catch(Exception e)
					{
						System.Windows.MessageBox.Show(e.Message + "####Sign in\\Sign_in\\Doc.Save(XmlPath);####890h78y");
					}
				}
				else
				{
					Directory.CreateDirectory(System.Environment.CurrentDirectory + @"\Resources");
					try
					{
						Doc.Save(XmlPath);//用新的存储目录来存放登录信息
					}
					catch(Exception e)
					{
						System.Windows.MessageBox.Show(e.Message + "####Sign in\\Sign_in\\Doc.Save(XmlPath);####890h23w8y");
					}
				}
			}
		}

		public static string Get_Confidential_User_ID()
		{
			return User_ID;
		}
		#endregion

		#region Button Sign In的交互逻辑
		private void Click_Sign_In(object sender,RoutedEventArgs e)
		{
			if(this.TextBox_Password.Password.Equals(string.Empty) || this.TextBox_User_ID.Text.Equals(string.Empty))//账户和密码非空
			{
				System.Windows.MessageBox.Show("Invalid input of User ID and Password");
			}
			else
			{
				if(Doc != null)
				{
					XmlElement Search_Result = Doc.SelectSingleNode(NodeTree + "/" + TextBox_User_ID.Text.Trim()) as XmlElement;//查找帐户名的节点

					if(Search_Result == null)
						System.Windows.MessageBox.Show("The User ID does not exist,please varify and try again, or Sign Up as a new user!");
					else
					{
						if(Search_Result.InnerText.Equals(TextBox_Password.Password))
						{
							User_ID = TextBox_User_ID.Text;
							Doc.Save(XmlPath);
							this.Close();
						}
						else
						{
							System.Windows.MessageBox.Show("Wrong User ID or Password, please varify and try again");
						}
					}
				}
				else
				{
					System.Windows.MessageBox.Show("Constructional Error!!Please Inform Developer By Email Address:Deliangus@Foxmail.com");
					System.Windows.Forms.Application.Exit();
				}
			}
		}
		#endregion

		private void Click_Sign_Up(object sender,RoutedEventArgs e)
		{

			if(this.TextBox_Password.Password.Equals(string.Empty) || this.TextBox_User_ID.Text.Equals(string.Empty))
			{
				System.Windows.MessageBox.Show("Invalid input of User ID and Password");
				return;
			}

			if(Doc != null)
			{
				XmlElement Search_Result = Doc.SelectSingleNode(NodeTree + "/" + TextBox_User_ID.Text) as XmlElement;
				if(Search_Result == null)
				{
					XmlNode Config = Doc.SelectSingleNode(NodeTree);
					Search_Result = Doc.CreateElement(TextBox_User_ID.Text);
					Search_Result.InnerText = TextBox_Password.Password;
					//XmlElement Admin_Element=Doc.SelectSingleNode(NodeTree+"/"+"Administrator") as XmlElement;
					//if(Admin_Element==null)
					//System.Windows.MessageBox.Show("Error:Admin_Element==NULL!!! At Sign in.xaml.cs/Click_Sign up!Please inform Developer by Deliangus@Foxmail.com");
					//else
					//{
					//Confidential_Level_Signing_Up Temp = new Confidential_Level_Signing_Up(TextBox_User_ID.Text,Admin_Element.InnerText);

					//XmlElement temp = Doc.CreateElement("Confidential");
					//temp.InnerText=Temp.Get_Confidential_Level_Value().ToString();
					///Search_Result.AppendChild(temp);
					Config.AppendChild(Search_Result);
					User_ID = TextBox_User_ID.Text;
					try
					{
						Doc.Save(XmlPath);
					}
					catch(Exception ex)
					{
						System.Windows.MessageBox.Show(ex.Message + "####Sign in/Sign_in/Doc.Load(Properties.Resources.MeiDiBianPinKongTiao)####");
					}
					//Temp.Close();

					this.Close();
				}
				else
					System.Windows.MessageBox.Show("The User ID is existed, please try another!");
			}
			else
				System.Windows.MessageBox.Show("Constructional Error!!Please Inform Developer By Email Address:Deliangus@Foxmail.com");
		}
	}
}
