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

namespace Contact_Book
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>

	public partial class MainWindow: Window
	{
		public string Xmlpath = System.Environment.CurrentDirectory + @"\Resources\HaoKongTiaoGeLiZao.xml";
		private XmlDocument Doc = null;
		private int Insert_Verifycode = 0;
		System.Random VerifyCode_Generator = new System.Random(new System.DateTime().Millisecond%1000);
		public MainWindow()
		{
			Sign_in Temp = new Sign_in();
			InitializeComponent();
            InitialXmlDocument();
        }

		#region DataGrid数据绑定

		private void KeyDown_Reload(object sender,System.Windows.Input.KeyEventArgs e)
		{
			if(Doc!=null && !Sign_in.Get_Confidential_User_ID().Equals(string.Empty))
				this.Contact_Book_View.ItemsSource=Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
		}

		private void MouseDown_Reload(object sender,System.Windows.Input.MouseButtonEventArgs e)
		{
			if(Doc != null && !Sign_in.Get_Confidential_User_ID().Equals(string.Empty))
				this.Contact_Book_View.ItemsSource = Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
		}
		#endregion

		#region 初始化联系人列表
		private string NodeTree = "Config";//Config/?
		private void InitialXmlDocument()
		{
			if(File.Exists(Xmlpath))
			{
				Doc = new XmlDocument();

				Doc.Load(Xmlpath);
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
				Doc.Save(Xmlpath);
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
		//			this.Xmlpath=fbd.FileName;;
		//			while(File.Exists(Xmlpath))
		//			{
		//				System.Windows.Forms.MessageBox.Show("A file of same name exisited!");
		//				if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
		//					Xmlpath=fbd.FileName;
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
			if(this.Xmlpath != null)
			{
				Doc = new XmlDocument();
				XmlNode Type_Node = Doc.CreateXmlDeclaration("1.0","utf-8",null);
				Doc.AppendChild(Type_Node);

				XmlNode Config = Doc.CreateNode(XmlNodeType.Element,"Config",string.Empty);
				XmlNode User_Node = Doc.CreateNode(XmlNodeType.Element,Sign_in.Get_Confidential_User_ID().Trim(),string.Empty);
				Config.AppendChild(User_Node);
				Doc.AppendChild(Config);

				Doc.Save(Xmlpath);
				Contact_Book_View.ItemsSource = Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
			}
			else
				System.Windows.MessageBox.Show("New_Document()|8i9awuf2");
		}
		

		#endregion

		#region 插入数据
		private ContactData Insert_Read = new ContactData();

		private void Click_Insert(object sender,RoutedEventArgs e)
		{
			if(this.Insert_Name.Text.Equals(string.Empty))
				System.Windows.MessageBox.Show("Empty Name Inserted!");
			else if(Doc==null)
				System.Windows.MessageBox.Show("No Contact Book Loaded!");
			else
			{
				string Name = this.Insert_Name.Text;
				XmlElement Contact_Data = Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim()+"/"+Name) as XmlElement;
				Insert_Verifycode=VerifyCode_Generator.Next();
				if(Contact_Data==null)
				{
					Insert_Detail Item = new Insert_Detail(Name,Insert_Verifycode);

					if(Insert_Read.Get_VerifyCode()==Insert_Verifycode)
					{

						XmlNode Contactor = Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim());
						Contact_Data=Doc.CreateElement(Insert_Read.Get_Name());

						XmlElement Temp;

						Temp=Doc.CreateElement("Name");
						Temp.InnerText=Insert_Read.Get_Name();
						Contact_Data.AppendChild(Temp);

						Temp=Doc.CreateElement("City");
						Temp.InnerText=Insert_Read.Get_City();
						Contact_Data.AppendChild(Temp);

						Temp=Doc.CreateElement("Tel");
						Temp.InnerText=Insert_Read.Get_Tel();
						Contact_Data.AppendChild(Temp);

						Temp=Doc.CreateElement("QQ");
						Temp.InnerText=Insert_Read.Get_QQ();
						Contact_Data.AppendChild(Temp);

						Contactor.AppendChild(Contact_Data);
						Doc.Save(Xmlpath);

						this.Contact_Book_View.ItemsSource=Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
						Contact_Book_View.UpdateLayout();
						this.Insert_Name.Text="输入姓名";

					}
				}
				else
					System.Windows.MessageBox.Show(Name+" is already existed!");
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
			if(Doc!=null)
			{
				if(this.Search_Name.Text.Trim().Equals(string.Empty))
					System.Windows.MessageBox.Show("Empty Name Inserted!");
				else
				{
					XmlElement Search_Result = Doc.SelectSingleNode(NodeTree+"/"+Sign_in.Get_Confidential_User_ID().Trim()+"/"+ this.Search_Name.Text.Trim()) as XmlElement;
					if(Search_Result==null)
						System.Windows.MessageBox.Show(this.Search_Name.Text.Trim()+ "未找到");
					else
					{
						Searched_Detail Search_Info = new Searched_Detail(Search_Result,this.Doc);
						Doc.Save(Xmlpath);
						this.Contact_Book_View.ItemsSource = Doc.SelectSingleNode(NodeTree + "/" + Sign_in.Get_Confidential_User_ID().Trim()).ChildNodes;
						Contact_Book_View.UpdateLayout();

						Search_Name.Text="输入姓名";
					}
				}
			}
			else
				System.Windows.MessageBox.Show("No Contact Book Loaded!");
		}

		#endregion

		private void Click_Log_Out(object sender,RoutedEventArgs e)
		{
			Sign_in Temp = new Sign_in();
			InitialXmlDocument();
			InitializeComponent();
		}

        private void DataGrid_Selected_Cells_Changed(object sender, SystemControls.SelectedCellsChangedEventArgs e)
        {
            //System.Windows.MessageBox.Show(this.Contact_Book_View.SelectedCells.)
            System.Windows.MessageBox.Show(this.Contact_Book_View.SelectedValue.ToString()+"2\n"+ this.Contact_Book_View.SelectedItem.ToString());
        }
    }
}
