using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Data;
using System.IO;


namespace Hello
{
	public class BasicForm:Form
	{
		public MenuStrip mainmenu;
		public ToolStripMenuItem menu_file;  //文件
		public ToolStripMenuItem menu_file_drop1;
		public ToolStripMenuItem menu_file_drop2;
		public ToolStripMenuItem menu_file_drop3;
		public ToolStripMenuItem menu_file_drop4;
		public ToolStripMenuItem menu_edit_;  //编辑
		public ToolStripMenuItem menu_edit_drop1;
		public ToolStripMenuItem menu_edit_drop2;
		public ToolStripMenuItem menu_edit_drop3;
		public ToolStripMenuItem menu_func_;  //功能
		public ToolStripMenuItem menu_func_drop1;
		public ToolStripMenuItem menu_func_drop2;
		public ToolStripMenuItem menu_func_drop3;
		public ToolStripMenuItem menu_func_drop31;
		public ToolStripMenuItem menu_func_drop32;
		public ToolStripMenuItem menu_help_;  //帮助
		public ToolStripMenuItem menu_help_drop1;
		public ToolStripMenuItem menu_help_drop2;

		public DataGrid Data;
		public BasicForm()
		{
			this.Text = "通讯录";
			this.WindowState = FormWindowState.Maximized;

			mainmenu = new MenuStrip();
			menu_file = new ToolStripMenuItem();
			menu_file_drop1 = new ToolStripMenuItem();
			menu_file_drop1.Text = "新建";
			menu_file_drop1.Font = new Font("微软雅黑",12,FontStyle.Bold);
			menu_file.DropDownItems.Add(menu_file_drop1);
			menu_file_drop2 = new ToolStripMenuItem();
			menu_file_drop2.Text = "打开";
			menu_file_drop2.Font = new Font("微软雅黑",12,FontStyle.Bold);
			menu_file.DropDownItems.Add(menu_file_drop2);
			menu_file_drop3 = new ToolStripMenuItem();
			menu_file_drop3.Text = "保存";
			menu_file_drop3.Font = new Font("微软雅黑",12,FontStyle.Bold);
			menu_file.DropDownItems.Add(menu_file_drop2);
			menu_file_drop4 = new ToolStripMenuItem();
			menu_file_drop4.Text = "关闭";
			menu_file_drop4.Font = new Font("微软雅黑",12,FontStyle.Bold);
			menu_file.DropDownItems.Add(menu_file_drop2);

			Data = new DataGrid();
			Data.AllowSorting = true;//允许通过点击列标签进行排序
			Data.BorderStyle = BorderStyle.FixedSingle;//边框的风格,枚举类型，0/1/2
			Data.CaptionFont = new Font("微软雅黑",12,FontStyle.Bold);
			Data.DataSource = new ListView();
		}
		public string Xmlpath;
		private void File_drop1(object sender,EventArgs e)
		{
			XmlDocument doc = new XmlDocument();
			string XmlPath = System.Environment.CurrentDirectory + @"\Resource\DataBace.Xml";
			if(!File.Exists(XmlPath))
				doc.Load(XmlPath);
			doc.Load(XmlPath)

		}
	}
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new BasicForm());
		}
	}
}
