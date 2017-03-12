﻿using System.Windows;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemControls = System.Windows.Controls;
using System.Resources;

namespace Contect_Book
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>

	public partial class MainWindow: Window
	{
		public string Xmlpath = null;
		private XmlDocument doc = null;
		private int Insert_Veryfycode = 0;
		private XmlNode Xml_List = null;

		public MainWindow()
		{
			InitializeComponent();
		}

		#region DataGrid数据绑定

		private void KeyDown_Reload(object sender,System.Windows.Input.KeyEventArgs e)
		{
			if(doc!=null)
				this.Contect_Book_View.ItemsSource=doc.SelectSingleNode(NodeTree).ChildNodes;
		}

		private void MouseDown_Reload(object sender,System.Windows.Input.MouseButtonEventArgs e)
		{
			if(doc!=null)
				this.Contect_Book_View.ItemsSource=doc.SelectSingleNode(NodeTree).ChildNodes;
		}
		#endregion

		#region 菜单Open动作
		private string NodeTree = "Config";
		private void Click_Open(object sender,RoutedEventArgs e)
		{
			OpenFileDialog fbd = new OpenFileDialog();
			fbd.Filter="数据表|*.xml";
			fbd.InitialDirectory=System.Environment.CurrentDirectory;
			if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				this.Xmlpath=fbd.FileName;
				if(this.Xmlpath!=null)
				{
					doc=new XmlDocument();
					
					doc.Load(Xmlpath);
					this.Contect_Book_View.ItemsSource=doc.SelectSingleNode(NodeTree).ChildNodes;
				}
			}
		}

		private void Click_Save(object sender,RoutedEventArgs e)
		{
			if(doc!=null)
			{
				doc.Save(Xmlpath);
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Not yet load a Contect Book");
			}
			return;
		}

		private void Click_New_Save(object sender,RoutedEventArgs e)
		{
			if(doc!=null)
			{
				SaveFileDialog fbd = new SaveFileDialog();
				fbd.Filter="数据表|*.xml";
				fbd.InitialDirectory=System.Environment.CurrentDirectory;
				if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
				{
					this.Xmlpath=fbd.FileName;;
					while(File.Exists(Xmlpath))
					{
						System.Windows.Forms.MessageBox.Show("A file of same name exisited!");
						if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
							Xmlpath=fbd.FileName;
					}
					doc.Save(fbd.FileName);
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Not yet load a Contect Book");
			}
			return;
		}

		private void Click_Exit(object sender,RoutedEventArgs e)
		{
			if(doc==null)
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
				this.Xmlpath=fbd.FileName;
				while(File.Exists(Xmlpath))
				{
					System.Windows.Forms.MessageBox.Show("A file of same name exisited!");
					if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
						Xmlpath=fbd.FileName;
				}

				if(this.Xmlpath!=null)
				{
					doc=new XmlDocument();
					XmlNode Type_Node = doc.CreateXmlDeclaration("1.0","utf-8",null);
					doc.AppendChild(Type_Node);

					XmlNode Config = doc.CreateNode(XmlNodeType.Element,"Config",string.Empty);
					doc.AppendChild(Config);
					doc.Save(Xmlpath);
					Contect_Book_View.ItemsSource=doc.GetElementsByTagName("Contector");
				}
			}
		}

		#endregion

		#region 插入数据
		private ContectData Insert_Read = new ContectData();

		private void Click_Insert(object sender,RoutedEventArgs e)
		{
			string Name = this.Insert_Name.Text;
			Insert_Detail Item = new Insert_Detail(Name,Insert_Veryfycode);
			if(Insert_Read.Get_VerifyCode()==Insert_Veryfycode)
			{
				XmlElement Contect_Data = doc.SelectSingleNode(NodeTree+"/"+Name) as XmlElement;
				if(Contect_Data==null)
				{
					XmlNode Contector = doc.SelectSingleNode(NodeTree);
					Contect_Data=doc.CreateElement(Insert_Read.Get_Name());
					//Contect_Data.SetAttribute("QQ",Insert_Read.Get_QQ());
					//Contect_Data.SetAttribute("Tel",Insert_Read.Get_Tel());
					//Contect_Data.SetAttribute("City",Insert_Read.Get_City());
					//Contect_Data.SetAttribute("Name",Insert_Read.Get_Name());		

					XmlElement Temp;

					Temp=doc.CreateElement("Name");
					Temp.InnerText=Insert_Read.Get_Name();
					Contect_Data.AppendChild(Temp);

					Temp=doc.CreateElement("City");
					Temp.InnerText=Insert_Read.Get_City();
					Contect_Data.AppendChild(Temp);

					Temp=doc.CreateElement("Tel");
					Temp.InnerText=Insert_Read.Get_Tel();
					Contect_Data.AppendChild(Temp);

					Temp=doc.CreateElement("QQ");
					Temp.InnerText=Insert_Read.Get_QQ();
					Contect_Data.AppendChild(Temp);

					Contector.AppendChild(Contect_Data);
					doc.Save(Xmlpath);

					this.Contect_Book_View.ItemsSource=doc.SelectSingleNode(NodeTree).ChildNodes;
					Contect_Book_View.UpdateLayout();
					this.Insert_Name.Text = "输入姓名";
				}
			}
		}


		//private void Insert_Name_GotFocus(object sender,RoutedEventArgs e)
		//{
		//	Insert_Name.Text = "";
		//}

		//private void Insert_Name_LostFocus(object sender,RoutedEventArgs e)
		//{
		//	Insert_Name.Text = "输入名字";
		//}

		//private void Search_Name_GotFocus(object sender,RoutedEventArgs e)
		//{
		//	Search_Name.Text = "";
		//}

		//private void Search_Name_LostFocus(object sender,RoutedEventArgs e)
		//{
		//	Search_Name.Text = "输入名字";
		//}

		#endregion

		//public void Xml_Carrier(string Name,string City,string Tel,string QQ)
		//{
		//	XmlElement Root = null, temp = null;
		//	Root=doc.CreateElement("Name");
		//	Root.InnerText=Name;
		//	temp=doc.CreateElement("City");
		//	temp.InnerText=City;
		//	Root.AppendChild(temp);

		//	temp=doc.CreateElement("Tel");
		//	temp.InnerText=Tel;
		//	Root.AppendChild(temp);

		//	temp=doc.CreateElement("QQ");
		//	temp.InnerText=QQ;
		//	Root.AppendChild(temp);

		//	doc.AppendChild(Root);
		//}
		#region 搜索数据
		private void Click_Search(object sender,RoutedEventArgs e)
		{
			string temp = this.Search_Name.Text;
			if(doc!=null)
			{
				XmlElement Search_Result = doc.SelectSingleNode(NodeTree+"/"+temp) as XmlElement;
				if(Search_Result==null)
					System.Windows.MessageBox.Show(temp+"未找到");
				else
				{
					//Contect_Book_View.select
				}
			}
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
