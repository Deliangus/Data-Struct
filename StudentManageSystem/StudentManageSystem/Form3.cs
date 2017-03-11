using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Threading;
using System.IO;
using System.Xml;

namespace StudentManageSystem
{
    /// <summary>
    /// 学生界面
    /// </summary>
    public partial class Form3 : Form1
    {
        private ToolStripMenuItem query;
        public Form3()
        {
            InitializeComponent();
            this.Text = "学生成绩管理系统--学生";
            base.menu1drop1.Visible = false;
            base.menu2.Visible = false;
            base.menu3.Visible = false;
            base.menu4drop1.Visible = false;
            base.menu4drop2.Visible = false;
            base.menu4drop3.Visible = false;

            query = new ToolStripMenuItem();
            query.Text = "查询成绩";
            query.Click += new EventHandler(GetInfo);
            base.menu4.DropDownItems.Add(query);
        }
        private void GetInfo(object sender, EventArgs e)
        {
            Form fsearch = new Form();                          //此处直接Form1的代码
            fsearch.FormBorderStyle = FormBorderStyle.None;
            fsearch.BackColor = Color.White;
            fsearch.Width = 300;
            fsearch.Height = 300;
            fsearch.StartPosition = FormStartPosition.CenterScreen;
            fsearch.Paint += new PaintEventHandler(AboutForm);
            fsearch.ShowInTaskbar = false;
            fsearch.TopMost = true;
            fsearch.Show();

            Button m4d1button1 = new Button();            //返回按钮
            m4d1button1.Text = " 返回 ";
            m4d1button1.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m4d1button1.BackColor = Color.DeepSkyBlue;
            m4d1button1.AutoSize = true;
            fsearch.Controls.Add(m4d1button1);
            m4d1button1.Top = 260;
            m4d1button1.Left = 290 - m4d1button1.Width;
            m4d1button1.Click += delegate(Object o, EventArgs v) { M4D1ButtonClick1(m4d1button1, fsearch); };

            DataGridView dgv = new DataGridView();
            dgv.BackgroundColor = Color.LightSkyBlue;
            dgv.BorderStyle = BorderStyle.Fixed3D;
            dgv.Width = 280;
            dgv.Height = 240;
            dgv.Top = 10;
            dgv.Left = 10;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            fsearch.Controls.Add(dgv);

            string temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;
            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select * From Student Where 学号 = '" + Vari.CurrentID + "';");
            dgv.DataSource = dt;
            DB.dbpath = temp;
        }
    }
}
