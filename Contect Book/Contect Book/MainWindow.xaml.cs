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
		public string Xmlpath;
		XmlDocument doc = new XmlDocument();
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
				this.doc.LoadXml(Xmlpath);
			}
		}

		private void MenuItem_Click(object sender,RoutedEventArgs e)
		{

		}
	}
}
