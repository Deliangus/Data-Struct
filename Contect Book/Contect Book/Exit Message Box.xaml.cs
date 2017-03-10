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

namespace Contect_Book
{
	/// <summary>
	/// Window1.xaml 的交互逻辑
	/// </summary>
	public partial class Exit_Message_Box: Window
	{
		int State = 0;

		public Exit_Message_Box()
		{
			InitializeComponent();
		}

		public int Get_State()
		{
			return State;
		}
		
		private void Click_Exit_Yes(object sender,RoutedEventArgs e)
		{
			State = 1;
			this.Hide();
		}

		private void Click_Exit_No(object sender,RoutedEventArgs e)
		{
			State = 2;
			this.Hide();
		}

		private void Click_Exit_Cancel(object sender,RoutedEventArgs e)
		{
			State = 3;
			this.Hide();
		}
		

	}
}
