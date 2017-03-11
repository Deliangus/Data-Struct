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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Contect_Book;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.ComponentModel;
using System.Data.OleDb;

namespace Contect_Book
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow: Window
	{
		public string Xmlpath = null;
		XmlDocument doc = null;

		public MainWindow()
		{
			InitializeComponent();
			Contect_Book_View.AutoGenerateColumns = true;
		}
		private void Click_Open(object sender,RoutedEventArgs e)
		{
			OpenFileDialog fbd = new OpenFileDialog();
			fbd.Filter="*.xml";
			fbd.InitialDirectory=System.Environment.CurrentDirectory;
			if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				this.Xmlpath=fbd.FileName;
				if(this.Xmlpath != null)
				{
					string TableCon = "provider=microsoft.jet.oledb.4.0;data source="+Xmlpath+";extended properties=excel 8.0";
					this.doc = new XmlDocument();
					doc.LoadXml(Xmlpath);
					Contect_Book_View.ItemsSource = doc;
				}
			}
		}

		private void Click_Save(object sender,RoutedEventArgs e)
		{
			if(doc != null)
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
				OpenFileDialog fbd = new OpenFileDialog();
				fbd.Filter=".xml";
				fbd.InitialDirectory=System.Environment.CurrentDirectory;
				if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
				{
					this.Xmlpath=fbd.FileName;
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
			if(doc == null)
				this.Close();
			else
			{
				Exit_Message_Box temp = new Exit_Message_Box();
				temp.Activate();
				temp.Topmost = true;
				temp.ShowDialog();
				int temp_State = temp.Get_State();
				if(temp_State == 1)
				{
					this.Click_Save(temp,new RoutedEventArgs());
					temp.Close();
					this.Close();
				}
				else if(temp_State == 2)
				{
					temp.Close();
					this.Close();
				}
				else
					temp.Close();
			}
			return;
		}

		private void Click_Insert(object sender,RoutedEventArgs e)
		{
			string temp = this.Insert_Name.Text;
			
		}

		private void Click_Search(object sender,RoutedEventArgs e)
		{
			string temp = this.Search_Name.Text;

		}

		private void Click_New(object sender,RoutedEventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.SelectedPath = System.Environment.CurrentDirectory;
			if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.Xmlpath = fbd.SelectedPath;
				if(this.Xmlpath != null)
				{
					this.doc = new XmlDocument();
					XmlNode Type_Node = doc.CreateXmlDeclaration("1.0","uft-8","");
					doc.AppendChild(Type_Node);
					//XmlNode Root_Node = doc.CreateElement("Name");
					//doc.AppendChild(Root_Node);
					doc.Save(Xmlpath+)
					Contect_Book_View.ItemsSource = doc;
				}
			}
		}
	}

	//class InsertTestGain: INotifyPropertyChanged
	//{
	//	public event PropertyChangedEventHandler PropertyChanged;
	//	public void NotifyPropertyChanged(string ProName)
	//	{
	//		PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(ProName));
	//	}
	//}
}
