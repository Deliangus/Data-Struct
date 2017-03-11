using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contect_Book
{
	class ContectData
	{
		private static string Name;
		private static string City;
		private static string Tel;
		private static string QQ;
		private static int VerifyCode;

		public ContectData()
		{
		}

		public void Write_Name(string temp)
		{
			Name=temp;
		}

		public void Write_City(string temp)
		{
			City=temp;
		}

		public void Write_Tel(string temp)
		{
			Tel=temp;
		}

		public void Write_QQ(string temp)
		{
			QQ=temp;
		}

		public void Write_VerifyCode(int temp)
		{
			VerifyCode=temp;
		}

		public string Get_Name()
		{
			return Name;
		}

		public string Get_City()
		{
			return City;
		}

		public string Get_Tel()
		{
			return Tel;
		}

		public string Get_QQ()
		{
			return QQ;
		}

		public int Get_VerifyCode()
		{
			return VerifyCode;
		}
	}
}
