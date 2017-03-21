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

namespace Contact_Book
{
	/// <summary>
	/// Searched_Detail.xaml 的交互逻辑
	/// </summary>
	public partial class Searched_Detail: Window
	{
		private XmlElement _Name;
		private XmlElement City;
		private XmlElement Tel;
		private XmlElement QQ;
		XmlNode Carrier_Node;
		XmlDocument Carrier_Doc;

		public Searched_Detail(XmlNode Contactor,XmlDocument Doc)
		{
			InitializeComponent();

			Carrier_Node = Contactor;
			Carrier_Doc = Doc;

			_Name = Contactor.FirstChild as XmlElement;
			if(_Name==null)
				System.Windows.MessageBox.Show("Name = Contactor.FirstChild as XmlElement;Empty"+"\n"+Carrier_Node.Name);
			else
			{

				TextBox_Name.Text=_Name.InnerText;

				City=_Name.NextSibling as XmlElement;
				TextBox_City.Text=City.InnerText;

				Tel=City.NextSibling as XmlElement;
				TextBox_Tel.Text=Tel.InnerText;

				QQ=Tel.NextSibling as XmlElement;
				TextBox_QQ.Text=QQ.InnerText;
			}

			this.ShowDialog();
		}

		private void Button_Search_Detail_Save_Click(object sender,RoutedEventArgs e)
		{
			_Name.InnerText = TextBox_Name.Text;
			City.InnerText = TextBox_City.Text;
			Tel.InnerText = TextBox_Tel.Text;
			QQ.InnerText = TextBox_QQ.Text;
			this.Close();
		}

		private void Button_Search_Detail_Delete_Click(object sender,RoutedEventArgs e)
		{
			Carrier_Node.ParentNode.RemoveChild(Carrier_Node);
			this.Close();
		}
	}
}
