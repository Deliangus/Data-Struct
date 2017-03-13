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

namespace Contect_Book
{
	/// <summary>
	/// Searched_Detail.xaml 的交互逻辑
	/// </summary>
	public partial class Searched_Detail: Window
	{
		private XmlElement Name;
		private XmlElement City;
		private XmlElement Tel;
		private XmlElement QQ;

		public Searched_Detail(XmlNode Contector)
		{
			XmlNode Carrier = Contector;
			Name = Contector.FirstChild as XmlElement;
			TextBox_Name.Text = Name.InnerText;

			City = Name.NextSibling as XmlElement;
			TextBox_City.Text = City.InnerText;

			Tel = City.NextSibling as XmlElement;
			TextBox_Tel.Text = Tel.InnerText;

			QQ = Tel.NextSibling as XmlElement;
			TextBox_QQ.Text = QQ.InnerText;
			InitializeComponent();
		}

		private void Button_Search_Detail_Save_Click(object sender,RoutedEventArgs e)
		{

		}

		private void Button_Search_Detail_Delete_Click(object sender,RoutedEventArgs e)
		{

		}
	}
}
