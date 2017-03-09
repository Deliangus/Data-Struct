//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace Contect_Book
//{
//	public static class ContectCommands
//	{
//		private static RoutedUICommand open;

//		public static ICommand Open
//		{
//			get
//			{
//				return open??(open=new RoutedUICommand("Open","Open",typeof(ContectCommands)));
//			}
//		}

//		private static RoutedUICommand open_key;

//		public static ICommand Open_key
//		{
//			get
//			{
//				if(open_key==null)
//				{
//					open_key=(open=new RoutedUICommand("Open_kty","Open_key",typeof(ContectCommands)));
//					open.InputGestures.Add(new KeyGesture(Key.B,ModifierKeys.Alt));
//				}
//				return open_key;
//			}
//		}

//	}
//}
