using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Contact_Book
{
	/// <summary>
	/// Searched_Detail.xaml 的交互逻辑
	/// </summary>
	public partial class Searched_Detail:Window
	{
		DataRow Carrier_Row;

		public Searched_Detail(DataRow Carrier)
		{
			InitializeComponent();
			Carrier_Row=Carrier;
			TextBox_Name.Text=Carrier_Row["Name"].ToString();
			TextBox_City.Text=Carrier_Row["City"].ToString();
			TextBox_Tel.Text=Carrier_Row["Tel"].ToString();
			TextBox_QQ.Text=Carrier_Row["QQ"].ToString();

			this.ShowDialog();
		}

		private void Button_Search_Detail_Save_Click(object sender,RoutedEventArgs e)
		{
			Carrier_Row["Name"]= TextBox_Name.Text;
			Carrier_Row["City"]= TextBox_City.Text;
			Carrier_Row["Tel"]= TextBox_Tel.Text;
			Carrier_Row["QQ"]= TextBox_QQ.Text;

			this.Close();
		}

		private void Button_Search_Detail_Delete_Click(object sender,RoutedEventArgs e)
		{
			Carrier_Row.Delete();
			this.Close();
		}
	}
}
