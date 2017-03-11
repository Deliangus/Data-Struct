using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManageSystem
{
    public class Course
    {
        public static string Tcourse;
    }
    public partial class Form2 : Form1
    {
        private ToolStripMenuItem GetStudent;
        private ToolStripMenuItem GetLow60;
        private ToolStripMenuItem GetTop15;
        public Form2()
        {
            InitializeComponent();
            this.Text = "学生成绩管理系统--教师";
            base.menu1drop1.Visible = false;
            base.menu2.Visible = false;
            base.menu3.Visible = false;
            base.menu4drop1.Visible = false;
            base.menu4drop2.Visible = false;
            base.menu4drop3.Visible = false;

            DBManipulate dbm = new DBManipulate();
            DB.dbpath = Vari.DefaultDB;
            Course.Tcourse = dbm.GetCourse(Vari.CurrentID);

            GetStudent = new ToolStripMenuItem();
            GetStudent.Text = "查看学生成绩";
            GetStudent.Click += new EventHandler(GetInfo1);
            base.menu4.DropDownItems.Add(GetStudent);

            GetLow60 = new ToolStripMenuItem();
            GetLow60.Text = "查看不及格的学生";
            GetLow60.Click += new EventHandler(GetInfo2);
            base.menu4.DropDownItems.Add(GetLow60);

            GetTop15 = new ToolStripMenuItem();
            GetTop15.Text = "查看排名前15学生";
            GetTop15.Click += new EventHandler(GetInfo3);
            base.menu4.DropDownItems.Add(GetTop15);
        }
        private void GetInfo1(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            DataGridView dgv = new DataGridView();         //Student表
            this.Controls.Add(panel);
            panel.Width = 1000;
            panel.Height = 700;
            panel.Left = 200;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = Color.Transparent;

            Button m2d2button1 = new Button();            //返回按钮
            m2d2button1.Text = " 返回 ";
            m2d2button1.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button1.BackColor = Color.DeepSkyBlue;
            m2d2button1.AutoSize = true;
            panel.Controls.Add(m2d2button1);
            m2d2button1.Top = 630 + m2d2button1.Height;
            m2d2button1.Left = 970 - m2d2button1.Width;
            m2d2button1.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick1(m2d2button1, panel); };

            Button m2d2button4 = new Button();
            m2d2button4.Text = " 撤销修改 ";
            m2d2button4.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button4.BackColor = Color.DeepSkyBlue;
            m2d2button4.AutoSize = true;
            panel.Controls.Add(m2d2button4);
            m2d2button4.Top = 630 + m2d2button4.Height;
            m2d2button4.Left = 200;
            m2d2button4.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick4(m2d2button4, panel, dgv); };

            Button m2d2button2 = new Button();
            m2d2button2.Text = " 确认修改 ";
            m2d2button2.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button2.BackColor = Color.DeepSkyBlue;
            m2d2button2.AutoSize = true;
            panel.Controls.Add(m2d2button2);
            m2d2button2.Top = 630 + m2d2button2.Height;
            m2d2button2.Left = 330;
            m2d2button2.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick2(m2d2button2, dgv, panel); };

            Label m2d2label1 = new Label();
            m2d2label1.Text = "教师 \n\n您只能查看并修改\n自己所教科目的学生的成绩";
            panel.Controls.Add(m2d2label1);
            m2d2label1.AutoSize = true;
            m2d2label1.Font = new Font("微软雅黑", 15, FontStyle.Bold);
            m2d2label1.ForeColor = Color.LightSkyBlue;
            m2d2label1.Top = 100;
            m2d2label1.Left = 550;

            Flash1(panel, dgv);
        }
        private void Flash1(Panel p, DataGridView dgv)
        {
            DataTable dt = MySQL.dataTable("Select * From Student;");
            dgv.DataSource = dt;
            p.Controls.Add(dgv);
            dgv.BackgroundColor = Color.LightSkyBlue;
            dgv.Left = 50;
            dgv.Top = 50;
            dgv.Width = 400;
            dgv.Height = 600;
            dgv.AllowUserToAddRows = false;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[2].ReadOnly = true;
            int count = dgv.Columns.Count;
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                if (dgv.Columns[i].HeaderText == Course.Tcourse)
                {
                    index = i;
                    break;
                }
            }
            for (int i = 3; i < count; i++)
            {
                if (i == index) continue;
                dgv.Columns[i].Visible = false;
            }
        }
        private void M2D2ButtonClick1(object sender, Panel p)
        {
            this.Controls.Remove(p);
            p.Dispose();
        }
        private void M2D2ButtonClick2(object sender, DataGridView dgv, Panel p)
        {
            DialogResult dr = MessageBox.Show(String.Format("确定修改?(无法撤销)", dgv.SelectedRows.Count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                DBManipulate dbm = new DBManipulate();
                if (dbm.Update1(dgv) == false)
                {
                    MessageBox.Show("信息不完整!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dbm.GetAvgAndRank() == false)
                {
                    MessageBox.Show("科目分数不完整!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("修改成功");
                Flash1(p, dgv);
            }
        }
        private void M2D2ButtonClick4(object sender, Panel p, DataGridView dgv)
        {
            DialogResult dr = MessageBox.Show("撤销之前所有的修改(删除)?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                Flash1(p, dgv);
            }
        }
        private void GetInfo2(object sender, EventArgs e)
        {
            Form fsearch = new Form();         //同理复制一下前面的代码
            fsearch.FormBorderStyle = FormBorderStyle.None;
            fsearch.BackColor = Color.White;
            fsearch.Width = 400;
            fsearch.Height = 400;
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
            m4d1button1.Top = 360;
            m4d1button1.Left = 390 - m4d1button1.Width;
            m4d1button1.Click += delegate(Object o, EventArgs v) { M4D1ButtonClick1(m4d1button1, fsearch); };

            DataGridView dgv = new DataGridView();
            dgv.BackgroundColor = Color.LightSkyBlue;
            dgv.BorderStyle = BorderStyle.Fixed3D;
            dgv.Width = 380;
            dgv.Height = 340;
            dgv.Top = 10;
            dgv.Left = 10;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            fsearch.Controls.Add(dgv);

            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select 学号, 姓名, 班级, " + Course.Tcourse + " From Student Where " + Course.Tcourse + " < 60;");
            dgv.DataSource = dt;
        }
        private void GetInfo3(object sender, EventArgs e)
        {
            Form fsearch = new Form();         //同理复制一下前面的代码
            fsearch.FormBorderStyle = FormBorderStyle.None;
            fsearch.BackColor = Color.White;
            fsearch.Width = 400;
            fsearch.Height = 400;
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
            m4d1button1.Top = 360;
            m4d1button1.Left = 390 - m4d1button1.Width;
            m4d1button1.Click += delegate(Object o, EventArgs v) { M4D1ButtonClick1(m4d1button1, fsearch); };

            DataGridView dgv = new DataGridView();
            dgv.BackgroundColor = Color.LightSkyBlue;
            dgv.BorderStyle = BorderStyle.Fixed3D;
            dgv.Width = 380;
            dgv.Height = 340;
            dgv.Top = 10;
            dgv.Left = 10;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            fsearch.Controls.Add(dgv);

            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select top 15 学号, 姓名, 班级, " + Course.Tcourse + " From Student Order By " + Course.Tcourse + " DESC;");
            dgv.DataSource = dt;
        }
    }
}
