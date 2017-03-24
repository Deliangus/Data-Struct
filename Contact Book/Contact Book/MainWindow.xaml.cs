using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemControls = System.Windows.Controls;
using System.Resources;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Contact_Book
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>

	public partial class MainWindow: Window
	{
		private int Insert_Verifycode = 0;
		System.Random VerifyCode_Generator = new System.Random(new System.DateTime().Millisecond%1000);
		public MainWindow()
		{
            this.Height=SystemParameters.PrimaryScreenHeight;
            this.Width=this.Height*(double)Properties.Resources.Sign_In_BackGround.Width/Properties.Resources.Sign_In_BackGround.Height;
            Sign_in Temp = new Sign_in();
			InitializeComponent();
            this.FillDataGrid();
        }

        private void FillDataGrid()
        {   
            //大概过程---先配置数据库ConString（因为配置文件App.config把数据库名字改成了这个）,作用是说明在配置文件里直接就可以修改数据库的参数而不用麻烦的在代码里改
            //--然后定义一个字符串用作写T-SQL语句,然后初始化数据库连接(使用配置文件ConString)，字符串放T-sql语句
            //--再然后数据库命令，作用是使得上一句的字符串的命令生效
            //--再然后实例化SqlDataAdapter
            //---再然后实例化数据表DataTable
            //---再然后SqlDataAdapter填充数据表DataTable的东西
            //--最后把填充好的数据的基础视图赋值给DataGrid的ItemSource
            using(SqlConnection con = new SqlConnection(Properties.Settings.Default.DataBaseConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Name,City,Tel,QQ,Belong FROM Contactors WHERE Belong='"+Sign_in.Get_Confidential_User_ID().Trim()+"'",con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Contact_Book_View.ItemsSource=dt.DefaultView;
            }
        }
        #region DataGrid数据绑定

        private void KeyDown_Reload(object sender,System.Windows.Input.KeyEventArgs e)
		{
            this.FillDataGrid();
		}

		private void MouseDown_Reload(object sender,System.Windows.Input.MouseButtonEventArgs e)
		{
            this.FillDataGrid();
		}
		#endregion

		#region 初始化联系人列表

		private void Click_Exit(object sender,RoutedEventArgs e)
		{
			this.Close();
		}

        #endregion

        #region 插入数据
        private void Click_Insert(object sender,RoutedEventArgs e)
        {
            if(this.Insert_Name.Text.Equals(string.Empty))
                System.Windows.MessageBox.Show("Empty Name Inserted!");
            else
            {
                using(SqlConnection con = new SqlConnection(Properties.Settings.Default.DataBaseConnectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("SELECT Name,City,Tel,QQ,Belong FROM Contactors WHERE Belong='"+Sign_in.Get_Confidential_User_ID().Trim()+"'",con);
                        SqlDataAdapter AutoSda = new SqlDataAdapter();
						AutoSda.SelectCommand=cmd;
                        SqlCommandBuilder ComBuild = new SqlCommandBuilder(AutoSda);
                        con.Open();
                        DataTable dt = new DataTable();
                        AutoSda.Fill(dt);
                        DataRow []Rows_Selected = dt.Select("Name='"+this.Insert_Name.Text.Trim()+"'");
                        if(Rows_Selected.Count()==0)
                        {
							Insert_Detail Item = new Insert_Detail(this.Insert_Name.Text.Trim(),Insert_Verifycode);
							this.Insert_Name.Text="输入名字";

							Object[] temp = new Object[5];
                            temp[0]=ContactData.Get_Name();
                            temp[1]=ContactData.Get_City();
                            temp[2]=ContactData.Get_Tel();
                            temp[3]=ContactData.Get_QQ();
                            temp[4]=Sign_in.Get_Confidential_User_ID();
                            dt.LoadDataRow(temp,true);
							dt.AcceptChanges();
							AutoSda.Update(dt);
                            Contact_Book_View.ItemsSource=dt.DefaultView;
                        }
                        else
                            System.Windows.MessageBox.Show(Name+" is already existed!");
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

		private void Insert_Name_GotFocus(object sender,RoutedEventArgs e)
		{
			Insert_Name.Text=string.Empty;
		}

		private void Insert_Name_LostFocus(object sender,RoutedEventArgs e)
		{
			if(Insert_Name.Text.Equals(string.Empty))
				Insert_Name.Text="输入名字";
		}

		private void Search_Name_GotFocus(object sender,RoutedEventArgs e)
		{
			Search_Name.Text=string.Empty;
		}

		private void Search_Name_LostFocus(object sender,RoutedEventArgs e)
		{
			if(Search_Name.Text.Equals(string.Empty))
				Search_Name.Text="输入名字";
		}

		#endregion

		#region 搜索数据
		private void Click_Search(object sender,RoutedEventArgs e)
		{
			//System.Windows.MessageBox.Show("Searching "+this.Search_Name.Text);
			if(this.Search_Name.Text.Trim().Equals(string.Empty))
				System.Windows.MessageBox.Show("Empty Name Inserted!");
			else
			{
				using(SqlConnection con = new SqlConnection(Properties.Settings.Default.DataBaseConnectionString))
				{
					try
					{
						SqlCommand cmd = new SqlCommand("SELECT Name,City,Tel,QQ,Belong FROM Contactors WHERE Belong='"+Sign_in.Get_Confidential_User_ID().Trim()+"'",con);
						SqlDataAdapter AutoSda = new SqlDataAdapter();
						AutoSda.SelectCommand=cmd;
						SqlCommandBuilder ComBuild = new SqlCommandBuilder(AutoSda);
						con.Open();
						DataTable dt = new DataTable();
						AutoSda.Fill(dt);
						DataRow[] Rows_Selected = dt.Select("Name='"+this.Search_Name.Text.Trim()+"'");
						if(Rows_Selected.Count()==0)
						{
							System.Windows.MessageBox.Show(this.Search_Name.Text.Trim()+" not Found");
						}
						else
						{
							Searched_Detail temp = new Searched_Detail(Rows_Selected[0]);
							dt.AcceptChanges();
							AutoSda.Update(dt);
							Contact_Book_View.ItemsSource=dt.DefaultView;
						}
						Search_Name.Text="输入名字";
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

		private void Click_Log_Out(object sender,RoutedEventArgs e)
		{
			Sign_in Temp = new Sign_in();
			InitializeComponent();
		}

	}
}
