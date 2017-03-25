using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemControls = System.Windows.Controls;
using System.Resources;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Configuration;
namespace Contact_Book
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>

	public partial class MainWindow: Window
	{
		private int Insert_Verifycode = 0;
		private string XmlPath = System.Environment.CurrentDirectory+@"\Resources\MeiDiBianPinKongTiao.xml";
		private XmlDocument Doc;
		System.Random VerifyCode_Generator = new System.Random(new System.DateTime().Millisecond%1000);
		private static int Running_Confidential_Value;
		public MainWindow()
		{
			try
			{
				Sign_in Logging = new Sign_in();
				Logging.Close();
				Running_Confidential_Value=Sign_in.Get_Confidential_Level();
				Refresh_DataGrid();
			}
			catch(Exception e)
			{
				System.Windows.MessageBox.Show(e.Message+"10549");
			}

			InitializeComponent();
		}

		#region DataGrid数据绑定

		private void Refresh_DataGrid()
		{
			using(SqlConnection Link = new SqlConnection(Properties.Settings.Default.SqlPath))
			{
				try
				{
					Link.Open();
				}
				catch(Exception e)
				{
					System.Windows.MessageBox.Show(e.Message+"\n##Link Error##");
				}

				//SqlCommand Operation = Link.CreateCommand();

				string CommandText = "select * from Contactors where Belonging = '"+Sign_in.Get_Confidential_User_ID().Trim()+"';";
				SqlDataAdapter DataAdapter = new SqlDataAdapter(CommandText,Link);
				DataTable Dt = new DataTable();
				try
				{
					DataAdapter.Fill(Dt);
					Contact_Book_View.ItemsSource=Dt.DefaultView;
				}
				catch(Exception e)
				{
					System.Windows.MessageBox.Show(e.Message+"425735");
				}

				Link.Close();
			}
		}

		private SqlConnection Get_Connect_DataBase()
		{
			using(SqlConnection Link = new SqlConnection(Properties.Settings.Default.SqlPath))
			{
				try
				{
					Link.Open();
				}
				catch(Exception e)
				{
					System.Windows.MessageBox.Show(e.Message+"\n##Link Error##");
				}
				Link.Close();
				return Link;
			}
		}

		private void KeyDown_Reload(object sender,System.Windows.Input.KeyEventArgs e)
		{
			this.Refresh_DataGrid();
		}

		private void MouseDown_Reload(object sender,System.Windows.Input.MouseButtonEventArgs e)
		{
			this.Refresh_DataGrid();
		}
		#endregion

		#region 菜单Open动作
		private string NodeTree = "Config";//Config/?
		private void Click_Open(object sender,RoutedEventArgs e)
		{
			OpenFileDialog fbd = new OpenFileDialog();
			fbd.Filter="数据表|*.xml";
			fbd.InitialDirectory=System.Environment.CurrentDirectory;
			if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				this.XmlPath=fbd.FileName;
				if(this.XmlPath!=null)
				{
					Doc=new XmlDocument();
					Doc.Load(XmlPath);
					this.Contact_Book_View.ItemsSource=Doc.SelectSingleNode(NodeTree).ChildNodes;
				}
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

		private void Click_New_Save(object sender,RoutedEventArgs e)
		{
			if(Doc!=null)
			{
				SaveFileDialog fbd = new SaveFileDialog();
				fbd.Filter="数据表|*.xml";
				fbd.InitialDirectory=System.Environment.CurrentDirectory;
				if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
				{
					this.XmlPath=fbd.FileName;;
					while(File.Exists(XmlPath))
					{
						System.Windows.Forms.MessageBox.Show("A file of same name exisited!");
						if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
							XmlPath=fbd.FileName;
					}
					Doc.Save(fbd.FileName);
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Not yet load a Contact Book");
			}
			return;
		}

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

		private void Click_New(object sender,RoutedEventArgs e)
		{
			SaveFileDialog fbd = new SaveFileDialog();
			fbd.InitialDirectory=System.Environment.CurrentDirectory;
			fbd.Filter="数据表|*.xml";
			if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				this.XmlPath=fbd.FileName;
				while(File.Exists(XmlPath))
				{
					System.Windows.Forms.MessageBox.Show("A file of same name exisited!");
					if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
						XmlPath=fbd.FileName;
				}

				if(this.XmlPath!=null)
				{
					Doc=new XmlDocument();
					XmlNode Type_Node = Doc.CreateXmlDeclaration("1.0","utf-8",null);
					Doc.AppendChild(Type_Node);

					XmlNode Config = Doc.CreateNode(XmlNodeType.Element,"Config",string.Empty);
					Doc.AppendChild(Config);
					Doc.Save(XmlPath);
					Contact_Book_View.ItemsSource=Doc.GetElementsByTagName("Contactor");
				}
			}
		}

		#endregion

		#region 插入数据

		private void Click_Insert(object sender,RoutedEventArgs e)
		{
			if(this.Insert_Name.Text.Equals(string.Empty))
				System.Windows.MessageBox.Show("Empty Name Inserted!");
			else
			{
				DataTable Dt = new DataTable();
				SqlDataAdapter Temp_DataAdapter = new SqlDataAdapter();
				SqlConnection Temp_Link=Get_Connect_DataBase();
				Temp_DataAdapter.SelectCommand=new SqlCommand("select * from Contactors where Belonging='"+Sign_in.Get_Confidential_User_ID()+"';",Temp_Link);
				SqlCommandBuilder SCB = new SqlCommandBuilder(Temp_DataAdapter);
			

				try
				{
					Temp_DataAdapter.Fill(Dt);
				}
				catch(Exception ex)
				{
					System.Windows.MessageBox.Show(ex.Message+"\t4wefsdf8647");
				}
				try
				{
					DataRow Insert_Row = Dt.NewRow();
					Insert_Row["Name"]=ContactData.Get_Name();
					Insert_Row["City"]=ContactData.Get_City();
					Insert_Row["Tel"]=ContactData.Get_Tel();
					Insert_Row["QQ"]=ContactData.Get_QQ();
					Insert_Row["Belonging"]=Sign_in.Get_Confidential_User_ID();
					Dt.ImportRow(Insert_Row);
					Temp_DataAdapter.Update(Dt);

					Dt.AcceptChanges();
				}
				catch(Exception ex)
				{
					System.Windows.MessageBox.Show(ex.Message+"\t489797");
				}
				finally
				{
					Temp_Link.Close();
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

		//public void Xml_Carrier(string Name,string City,string Tel,string QQ)
		//{
		//	XmlElement Root = null, temp = null;
		//	Root=Doc.CreateElement("Name");
		//	Root.InnerText=Name;
		//	temp=Doc.CreateElement("City");
		//	temp.InnerText=City;
		//	Root.AppendChild(temp);

		//	temp=Doc.CreateElement("Tel");
		//	temp.InnerText=Tel;
		//	Root.AppendChild(temp);


		//	temp=Doc.CreateElement("QQ");
		//	temp.InnerText=QQ;
		//	Root.AppendChild(temp);

		//	Doc.AppendChild(Root);
		//}
		#region 搜索数据
		private void Click_Search(object sender,RoutedEventArgs e)
		{
			string temp = this.Search_Name.Text;
			if(Doc!=null)
			{
				if(this.Insert_Name.Text.Equals(string.Empty))
					System.Windows.MessageBox.Show("Empty Name Inserted!");
				else
				{
					XmlElement Search_Result = Doc.SelectSingleNode(NodeTree+"/"+temp) as XmlElement;
					if(Search_Result==null)
						System.Windows.MessageBox.Show(temp+"未找到");
					else
					{
						Searched_Detail Search_Info = new Searched_Detail(Search_Result,this.Doc);
						Doc.Save(XmlPath);
						this.Contact_Book_View.ItemsSource=Doc.SelectSingleNode(NodeTree).ChildNodes;
						Contact_Book_View.UpdateLayout();

						Search_Name.Text="输入姓名";
					}
				}
			}
			else
				System.Windows.MessageBox.Show("No Contact Book Loaded!");
		}

		#endregion

		//#region 自动生成数据列
		//private void DataGrid_AutoGeneratingColumn(object sender,SystemControls.DataGridAutoGeneratingColumnEventArgs e)
		//{
		//	SystemControls.DataGridTemplateColumn Name = new SystemControls.DataGridTemplateColumn();
		//	Name.Header="Name";
		//	Name.CellTemplate=(DataTemplate)Resources["NameCellTemplate"];
		//	Name.CellEditingTemplate=(DataTemplate)Resources["NameCellEditingTemplate"];
		//	Name.SortMemberPath="Name";

		//	SystemControls.DataGridTemplateColumn City = new SystemControls.DataGridTemplateColumn();
		//	City.Header="City";
		//	City.CellTemplate=(DataTemplate)Resources["CityCellTemplate"];
		//	City.CellEditingTemplate=(DataTemplate)Resources["CityCellEditingTemplate"];
		//	City.SortMemberPath="City";

		//	SystemControls.DataGridTemplateColumn Tel = new SystemControls.DataGridTemplateColumn();
		//	Tel.Header="Tel";
		//	Tel.CellTemplate=(DataTemplate)Resources["TelCellTemplate"];
		//	Tel.CellEditingTemplate=(DataTemplate)Resources["TelCellEditingTemplate"];
		//	Tel.SortMemberPath="Tel";

		//	SystemControls.DataGridTemplateColumn QQ = new SystemControls.DataGridTemplateColumn();
		//	QQ.Header="QQ";
		//	QQ.CellTemplate=(DataTemplate)Resources["QQCellTemplate"];
		//	QQ.CellEditingTemplate=(DataTemplate)Resources["QQCellEditingTemplate"];
		//	QQ.SortMemberPath="QQ";

		//	string temp = e.Column.ToString();
		//	if(temp!="Name"||temp!="City"||temp!="Tel"||temp!="QQ")
		//		e.Cancel=true;
		//}

		//#endregion
	}
}
