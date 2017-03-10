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
		}
		private void Click_Open(object sender,RoutedEventArgs e)
		{
			OpenFileDialog fbd = new OpenFileDialog();
			fbd.InitialDirectory=System.Environment.CurrentDirectory;
			if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				this.Xmlpath=fbd.FileName;
				this.doc=new XmlDocument();
				doc.LoadXml(Xmlpath);
			}
		}

		private void Click_Save(object sender,RoutedEventArgs e)
		{
			if(doc != null)
			{
				doc.Save(Xmlpath);
				Xmlpath = null;
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Not yet load a Contect Book");
			}
		}

		private void Click_New_Save(object sender,RoutedEventArgs e)
		{
			if(doc!=null)
			{
				OpenFileDialog fbd = new OpenFileDialog();
				fbd.InitialDirectory=System.Environment.CurrentDirectory;
				if(fbd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
				{
					this.Xmlpath=fbd.FileName;
					doc.Save(fbd.FileName);
					Xmlpath=null;
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Not yet load a Contect Book");
			}
		}

		private void Click_Exit(object sender,RoutedEventArgs e)
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
	}
}
