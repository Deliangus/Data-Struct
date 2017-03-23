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
		public string XmlPath = System.Environment.CurrentDirectory + @"\Resources\HaoKongTiaoGeLiZao.xml";
		private XmlDocument Doc = null;
		private int Insert_Verifycode = 0;
		System.Random VerifyCode_Generator = new System.Random(new System.DateTime().Millisecond%1000);
		public MainWindow()
		{
            this.Height=SystemParameters.PrimaryScreenHeight;
            this.Width=this.Height*(double)Properties.Resources.Sign_In_BackGround.Width/Properties.Resources.Sign_In_BackGround.Height;
            Sign_in Temp = new Sign_in();
			InitializeComponent();
            this.FillDataGrid();
            //InitialXmlDocument();
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
                SqlCommand cmd = new SqlCommand("SELECT Name,City,Tel,QQ FROM Contactors WHERE Belong='"+Sign_in.Get_Confidential_User_ID().Trim()+"'",con);
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
			//if(Doc!=null && !Sign_in.Get_Confidential_User_ID().Equals(string.Empty))
			//	this.Contact_Book_View.ItemsSource=Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
		}

		private void MouseDown_Reload(object sender,System.Windows.Input.MouseButtonEventArgs e)
		{
            this.FillDataGrid();
			//if(Doc != null && !Sign_in.Get_Confidential_User_ID().Equals(string.Empty))
			//	this.Contact_Book_View.ItemsSource = Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
		}
		#endregion

		#region 初始化联系人列表
		private string NodeTree = "Config";//Config/?
		private void InitialXmlDocument()
		{
			if(File.Exists(XmlPath))
			{
				Doc = new XmlDocument();

				Doc.Load(XmlPath);
				if(Sign_in.Get_Confidential_User_ID() != null)
				{
					XmlNode User_Node = Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim());
					if(User_Node == null)
					{
						User_Node = Doc.CreateNode(XmlNodeType.Element,Sign_in.Get_Confidential_User_ID(),string.Empty);
						Doc.SelectSingleNode(NodeTree).AppendChild(User_Node);
					}
                    //System.Windows.MessageBox.Show(Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes.ToString());
                    try
                    {
                        this.Contact_Book_View.ItemsSource = Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
                    }
                    catch(Exception e)
                    {
                        System.Windows.MessageBox.Show(e.Message);
                    }
				}
				else
					System.Windows.MessageBox.Show("Sign_in.Get_Confidential_User_ID()==null|98aus0d8fh");
			}
			else
			{
				New_Document();
			}
		}

		private void Click_Save(object sender,RoutedEventArgs e)
		{
			if(Doc!=null)
			{
				Doc.Save(XmlPath);
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Not yet load a Contact Book");
			}
			return;
		}

		//private void Click_New_Save(object sender,RoutedEventArgs e)
		//{
		//	if(Doc!=null)
		//	{
		//		SaveFileDialog fbd = new SaveFileDialog();
		//		fbd.Filter="数据表|*.xml";
		//		fbd.InitialDirectory=System.Environment.CurrentDirectory;
		//		if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
		//		{
		//			this.XmlPath=fbd.FileName;;
		//			while(File.Exists(XmlPath))
		//			{
		//				System.Windows.Forms.MessageBox.Show("A file of same name exisited!");
		//				if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
		//					XmlPath=fbd.FileName;
		//			}
		//			Doc.Save(fbd.FileName);
		//		}
		//	}
		//	else
		//	{
		//		System.Windows.Forms.MessageBox.Show("Not yet load a Contact Book");
		//	}
		//	return;
		//}

		private void Click_Exit(object sender,RoutedEventArgs e)
		{
			if(Doc==null)
				this.Close();
			else
			{
				Exit_Message_Box temp = new Exit_Message_Box();
				temp.Activate();
				temp.Topmost=true;
				temp.ShowDialog();
				int temp_State = temp.Get_State();
				if(temp_State==1)
				{
					this.Click_Save(temp,new RoutedEventArgs());
					temp.Close();
					this.Close();
				}
				else if(temp_State==2)
				{
					temp.Close();
					this.Close();
				}
				else
					temp.Close();
			}
			return;
		}

		private void New_Document()
		{
			if(this.XmlPath != null)
			{
				Doc = new XmlDocument();
				XmlNode Type_Node = Doc.CreateXmlDeclaration("1.0","utf-8",null);
				Doc.AppendChild(Type_Node);

				XmlNode Config = Doc.CreateNode(XmlNodeType.Element,"Config",string.Empty);
				XmlNode User_Node = Doc.CreateNode(XmlNodeType.Element,Sign_in.Get_Confidential_User_ID().Trim(),string.Empty);
				Config.AppendChild(User_Node);
				Doc.AppendChild(Config);

				Doc.Save(XmlPath);
				Contact_Book_View.ItemsSource = Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
			}
			else
				System.Windows.MessageBox.Show("New_Document()|8i9awuf2");
		}


        #endregion

        #region 插入数据
        private void Click_Insert(object sender,RoutedEventArgs e)
        {
            if(this.Insert_Name.Text.Equals(string.Empty))
                System.Windows.MessageBox.Show("Empty Name Inserted!");
            //else if(Doc==null)
            //System.Windows.MessageBox.Show("No Contact Book Loaded!");
            /*else
			{
				string Name = this.Insert_Name.Text;
				XmlElement Contact_Data = Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim()+"/"+Name) as XmlElement;
				Insert_Verifycode=VerifyCode_Generator.Next();
				if(Contact_Data==null)
				{
					Insert_Detail Item = new Insert_Detail(Name,Insert_Verifycode);

					if(ContactData.Get_VerifyCode()==Insert_Verifycode)
					{

						XmlNode Contactor = Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim());
						Contact_Data=Doc.CreateElement(ContactData.Get_Name());

						XmlElement Temp;

						Temp=Doc.CreateElement("Name");
						Temp.InnerText=ContactData.Get_Name();
						Contact_Data.AppendChild(Temp);

						Temp=Doc.CreateElement("City");
						Temp.InnerText=ContactData.Get_City();
						Contact_Data.AppendChild(Temp);

						Temp=Doc.CreateElement("Tel");
						Temp.InnerText=ContactData.Get_Tel();
						Contact_Data.AppendChild(Temp);

						Temp=Doc.CreateElement("QQ");
						Temp.InnerText=ContactData.Get_QQ();
						Contact_Data.AppendChild(Temp);

						Contactor.AppendChild(Contact_Data);
						Doc.Save(XmlPath);

						this.Contact_Book_View.ItemsSource=Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
						Contact_Book_View.UpdateLayout();
						this.Insert_Name.Text="输入姓名";

					}
				}
				else
					System.Windows.MessageBox.Show(Name+" is already existed!");
			}
            */
            else
            {
                using(SqlConnection con = new SqlConnection(Properties.Settings.Default.DataBaseConnectionString))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("SELECT Name,City,Tel,QQ FROM Contactors WHERE Belong='"+Sign_in.Get_Confidential_User_ID().Trim()+"'",con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        SqlDataAdapter AutoSda = new SqlDataAdapter();
                        AutoSda.SelectCommand=new SqlCommand("select*From Contactors",con);
                        SqlCommandBuilder ComBuild = new SqlCommandBuilder(AutoSda);
                        con.Open();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        DataRow Rows_Selected = dt.Select("Name='"+this.Insert_Name.Text.Trim()+"'")[0];
                        if(Rows_Selected==null)
                        {
                            Object[] temp = new Object[5];
                            temp[0]=ContactData.Get_Name();
                            temp[1]=ContactData.Get_City();
                            temp[2]=ContactData.Get_Tel();
                            temp[3]=ContactData.Get_QQ();
                            temp[4]=Sign_in.Get_Confidential_User_ID();
                            dt.LoadDataRow(temp,true);
                            sda.Update(dt);
                            dt.AcceptChanges();
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
			Insert_Name.Text="";
		}

		private void Insert_Name_LostFocus(object sender,RoutedEventArgs e)
		{
			if(Insert_Name.Text.Equals(string.Empty))
				Insert_Name.Text="输入名字";
		}

		private void Search_Name_GotFocus(object sender,RoutedEventArgs e)
		{
			Search_Name.Text="";
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
			if(this.Search_Name.Text.Trim().Equals(string.Empty))
				System.Windows.MessageBox.Show("Empty Name Inserted!");
			else
			{
				//XmlElement Search_Result = Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim()+"/"+this.Search_Name.Text.Trim()) as XmlElement;
				//if(Search_Result==null)
				//	System.Windows.MessageBox.Show(this.Search_Name.Text.Trim()+"未找到");
				//else
				//{
				//	Searched_Detail Search_Info = new Searched_Detail(Search_Result,this.Doc);
				//	Doc.Save(XmlPath);
				//	this.Contact_Book_View.ItemsSource=Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
				//	Contact_Book_View.UpdateLayout();

				//	Search_Name.Text="输入姓名";
				//}
				using(SqlConnection con = new SqlConnection(Properties.Settings.Default.DataBaseConnectionString))
				{
					try
					{
						SqlCommand cmd = new SqlCommand("SELECT Name,City,Tel,QQ FROM Contactors WHERE Belong='"+Sign_in.Get_Confidential_User_ID().Trim()+"'",con);
						SqlDataAdapter sda = new SqlDataAdapter(cmd);
						SqlDataAdapter AutoSda = new SqlDataAdapter();fefsef
						AutoSda.SelectCommand=new SqlCommand("select*From Contactors",con);
						SqlCommandBuilder ComBuild = new SqlCommandBuilder(AutoSda);
						con.Open();
						DataTable dt = new DataTable();
						sda.Fill(dt);
						DataRow Rows_Selected = dt.Select("Name='"+this.Insert_Name.Text.Trim()+"'")[0];
						if(Rows_Selected==null)
						{
							Object[] temp = new Object[5];
							temp[0]=ContactData.Get_Name();
							temp[1]=ContactData.Get_City();
							temp[2]=ContactData.Get_Tel();
							temp[3]=ContactData.Get_QQ();
							temp[4]=Sign_in.Get_Confidential_User_ID();
							dt.LoadDataRow(temp,true);
							sda.Update(dt);
							dt.AcceptChanges();
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

			//System.Windows.MessageBox.Show("No Contact Book Loaded!");
		}

		#endregion

		private void Click_Log_Out(object sender,RoutedEventArgs e)
		{

            Doc.Save(XmlPath);
			Sign_in Temp = new Sign_in();
			InitialXmlDocument();
			InitializeComponent();
		}

        private void DataGrid_Selected_Cells_Changed(object sender, SystemControls.SelectedCellsChangedEventArgs e)
        {
            //System.Windows.MessageBox.Show(this.Contact_Book_View.SelectedCells.)
            //System.Windows.MessageBox.Show(this.Contact_Book_View.SelectionUnit);
        }

        private void CellsBeginningEdit(object sender,SystemControls.DataGridBeginningEditEventArgs e)
        {

        }

    }
}
