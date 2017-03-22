using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Book
{
	class ContactData
	{
		private static string Name;
		private static string City;
		private static string Tel;
		private static string QQ;
		private static int VerifyCode;

		private ContactData()
		{
		}

		public static void Write_Name(string temp)
		{
			Name=temp;
		}

        public static void Write_City(string temp)
		{
			City=temp;
		}

        public static void Write_Tel(string temp)
		{
			Tel=temp;
		}

        public static void Write_QQ(string temp)
		{
			QQ=temp;
		}

        public static void Write_VerifyCode(int temp)
		{
			VerifyCode=temp;
		}

        public static string Get_Name()
		{
			return Name;
		}

        public static string Get_City()
		{
			return City;
		}

        public static string Get_Tel()
		{
			return Tel;
		}

        public static string Get_QQ()
		{
			return QQ;
		}

        public static int Get_VerifyCode()
		{
			return VerifyCode;
		}
	}
}
