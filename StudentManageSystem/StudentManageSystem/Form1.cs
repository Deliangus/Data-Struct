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
using System.Runtime.InteropServices;
using System.Diagnostics;
using ADOX;

namespace StudentManageSystem
{
    /// <summary>
    /// 管理员登录界面
    /// </summary>
    public partial class Form1 : Form
    {
        #region 初始化区域
        /// <summary>
        /// 控件管理区域
        /// </summary>
        public MyMenuStrip mainmenu;
        public ToolStripMenuItem menu1;  //管理
        public ToolStripMenuItem menu1drop1;
        public ToolStripMenuItem menu1drop2;
        public ToolStripMenuItem menu1drop4;
        public ToolStripMenuItem menu1drop5;
        public ToolStripMenuItem menu2;  //文件
        public ToolStripMenuItem menu2drop1;
        public ToolStripMenuItem menu2drop2;
        public ToolStripMenuItem menu2drop4;
        public ToolStripMenuItem menu3;  //编辑
        public ToolStripMenuItem menu3drop1;
        public ToolStripMenuItem menu3drop2;
        public ToolStripMenuItem menu3drop3;
        public ToolStripMenuItem menu4;  //功能
        public ToolStripMenuItem menu4drop1;
        public ToolStripMenuItem menu4drop2;
        public ToolStripMenuItem menu4drop3;
        public ToolStripMenuItem menu4drop31;
        public ToolStripMenuItem menu4drop32;
        public ToolStripMenuItem menu5;  //帮助
        public ToolStripMenuItem menu5drop1;
        public ToolStripMenuItem menu5drop2;
        public Form1()
        {
            InitializeComponent();
            Bitmap b = new Bitmap(System.Environment.CurrentDirectory + @"\Resources\1.ico");
            IntPtr hicon = b.GetHicon();
            Icon icon = Icon.FromHandle(hicon);
            //初始化此窗体
            this.Text = "学生成绩管理系统--管理员";
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = Image.FromFile(System.Environment.CurrentDirectory + @"\Resources\28.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = icon;
            this.ControlBox = false;

            //菜单
            this.mainmenu = new MyMenuStrip();

            this.menu1 = new ToolStripMenuItem();
            this.menu1.Text = " 管理 ";
            this.menu1.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            this.menu1.ForeColor = Color.LightBlue;
            this.menu1drop1 = new ToolStripMenuItem();
            this.menu1drop1.Text = "账号密码管理";
            this.menu1.DropDownItems.Add(menu1drop1);
            this.menu1drop1.Click += new EventHandler(M1D1Click);
            this.menu1drop2 = new ToolStripMenuItem();
            this.menu1drop2.Text = "修改密码";
            this.menu1.DropDownItems.Add(menu1drop2);
            this.menu1drop2.Click += new EventHandler(M1D2Click);
            this.menu1drop4 = new ToolStripMenuItem();
            this.menu1drop4.Text = "切换账号";
            this.menu1drop4.Click += new EventHandler(M1D4Click);
            this.menu1.DropDownItems.Add(menu1drop4);
            this.menu1drop5 = new ToolStripMenuItem();
            this.menu1drop5.Text = "退出";
            this.menu1.DropDownItems.Add(menu1drop5);
            this.menu1drop5.Click += new EventHandler(M1D5Click);

            this.menu2 = new ToolStripMenuItem();
            this.menu2.Text = " 文件 ";
            this.menu2.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            this.menu2.ForeColor = Color.LightBlue;
            this.menu2drop1 = new ToolStripMenuItem();
            this.menu2drop1.Text = "新建";
            this.menu2.DropDownItems.Add(menu2drop1);
            this.menu2drop1.Click += delegate(Object o, EventArgs v) { M2D1Click(menu2drop1, menu2drop4); };
            this.menu2drop2 = new ToolStripMenuItem();
            this.menu2drop2.Text = "读取";
            this.menu2drop2.Click += delegate(Object o, EventArgs v) { M2D2Click(menu2drop2, menu2drop4); };
            this.menu2.DropDownItems.Add(menu2drop2);
            this.menu2drop4 = new ToolStripMenuItem();
            this.menu2drop4.Text = "另存为";
            this.menu2.DropDownItems.Add(menu2drop4);

            this.menu3 = new ToolStripMenuItem();
            this.menu3.Text = " 编辑 ";
            this.menu3.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            this.menu3.ForeColor = Color.LightBlue;
            this.menu3drop1 = new ToolStripMenuItem();
            this.menu3drop1.Text = "查看默认数据库";
            this.menu3drop1.Click += new EventHandler(M3D1Click);
            this.menu3.DropDownItems.Add(menu3drop1);
            this.menu3drop2 = new ToolStripMenuItem();
            this.menu3drop2.Text = "设置默认数据库";
            this.menu3drop2.Click += new EventHandler(M3D2Click);
            this.menu3.DropDownItems.Add(menu3drop2);
            this.menu3drop3 = new ToolStripMenuItem();
            this.menu3drop3.Text = "导出学号txt文件";
            this.menu3drop3.Click += new EventHandler(M3D3Click);
            this.menu3.DropDownItems.Add(menu3drop3);
            

            this.menu4 = new ToolStripMenuItem();
            this.menu4.Text = " 功能 ";
            this.menu4.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            this.menu4.ForeColor = Color.LightBlue;
            this.menu4drop1 = new ToolStripMenuItem();
            this.menu4drop1.Text = "查询成绩";
            this.menu4drop1.Click += new EventHandler(M4D1Click);
            this.menu4.DropDownItems.Add(menu4drop1);
            this.menu4drop2 = new ToolStripMenuItem();
            this.menu4drop2.Text = "分析成绩";
            this.menu4.DropDownItems.Add(menu4drop2);
            ToolStripMenuItem menu4drop21 = new ToolStripMenuItem();
            menu4drop21.Text = "各科平均分";
            menu4drop21.Click += new EventHandler(M4D2Click1);
            this.menu4drop2.DropDownItems.Add(menu4drop21);
            ToolStripMenuItem menu4drop22 = new ToolStripMenuItem();
            menu4drop22.Text = "各科不及格学生";
            menu4drop22.Click += new EventHandler(M4D2Click2);
            this.menu4drop2.DropDownItems.Add(menu4drop22);
            ToolStripMenuItem menu4drop23 = new ToolStripMenuItem();
            menu4drop23.Text = "各科成绩前10";
            menu4drop23.Click += new EventHandler(M4D2Click3);
            this.menu4drop2.DropDownItems.Add(menu4drop23);
            ToolStripMenuItem menu4drop24 = new ToolStripMenuItem();
            menu4drop24.Text = "加权排名前15";
            menu4drop24.Click += new EventHandler(M4D2Click4);
            this.menu4drop2.DropDownItems.Add(menu4drop24);
            this.menu4drop3 = new ToolStripMenuItem();
            this.menu4drop3.Text = "excel操作";
            this.menu4.DropDownItems.Add(menu4drop3);
            this.menu4drop31 = new ToolStripMenuItem();
            this.menu4drop31.Text = "默认数据库导出excel";
            this.menu4drop31.Click += new EventHandler(M4D3Click1);
            this.menu4drop3.DropDownItems.Add(menu4drop31);
            this.menu4drop32 = new ToolStripMenuItem();
            this.menu4drop32.Text = "导入excel";
            this.menu4drop32.Click += new EventHandler(M4D3Click2);
            this.menu4drop3.DropDownItems.Add(menu4drop32);

            this.menu5 = new ToolStripMenuItem();
            this.menu5.Text = " 帮助 ";
            this.menu5.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            this.menu5.ForeColor = Color.LightBlue;
            this.menu5drop1 = new ToolStripMenuItem();
            this.menu5drop1.Text = "查看帮助";
            this.menu5drop1.Click += new EventHandler(M5D1Click);
            this.menu5.DropDownItems.Add(menu5drop1);
            this.menu5drop2 = new ToolStripMenuItem();
            this.menu5drop2.Text = "关于此废品";
            this.menu5drop2.Click += new EventHandler(M5D2Click);
            this.menu5.DropDownItems.Add(menu5drop2);

            this.mainmenu.BackColor = Color.Transparent;
            this.mainmenu.Items.Add(menu1);
            this.mainmenu.Items.Add(menu2);
            this.mainmenu.Items.Add(menu3);
            this.mainmenu.Items.Add(menu4);
            this.mainmenu.Items.Add(menu5);


            //控件添加
            this.Controls.Add(this.mainmenu);
        }

        /// <summary>
        /// 加载区域
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #endregion

        #region menu1事件
        /// <summary>
        /// 账号密码管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M1D1Click(object sender, EventArgs e)
        {
            Panel m1d1panel = new Panel();
            this.Controls.Add(m1d1panel);
            m1d1panel.Width = 1000;
            m1d1panel.Height = 700;
            m1d1panel.Left = 200;
            m1d1panel.BorderStyle = BorderStyle.FixedSingle;
            m1d1panel.BackColor = Color.Transparent;

            Button m1d1button1 = new Button();            //返回按钮
            m1d1button1.Text = " 返回 ";
            m1d1button1.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m1d1button1.BackColor = Color.DeepSkyBlue;
            m1d1button1.AutoSize = true;
            m1d1panel.Controls.Add(m1d1button1);
            m1d1button1.Top = 630 + m1d1button1.Height;
            m1d1button1.Left = 970 - m1d1button1.Width;
            m1d1button1.Click += delegate(Object o, EventArgs v) { M1D1ButtonClick1(m1d1button1, m1d1panel); };

            //建立ListView
            ListView lv = new ListView();
            lv.Sorting = SortOrder.Ascending; //升序排列
            lv.HideSelection = false;
            m1d1panel.Controls.Add(lv);
            lv.BackColor = Color.LightSkyBlue;
            lv.FullRowSelect = true;
            lv.View = System.Windows.Forms.View.Details;
            lv.Left = 50;
            lv.Top = 50;
            lv.Width = 400;
            lv.Height = 600;

            //导入账号密码数据
            M1D1ListInfo(lv);

            //写入，修改，删除数据
            Label m1d1label1 = new Label();
            Label m1d1label2 = new Label();
            m1d1label1.Text = "账号 : ";
            m1d1label2.Text = "密码 : ";
            m1d1panel.Controls.Add(m1d1label1);
            m1d1panel.Controls.Add(m1d1label2);
            m1d1label1.AutoSize = true;
            m1d1label2.AutoSize = true;
            m1d1label1.Font = new Font("微软雅黑", 10);
            m1d1label2.Font = new Font("微软雅黑", 10);
            m1d1label1.ForeColor = Color.LightSkyBlue;
            m1d1label2.ForeColor = Color.LightSkyBlue;
            m1d1label1.Top = 300;
            m1d1label1.Left = 500;
            m1d1label2.Top = 350;
            m1d1label2.Left = 500;
            Label m1d1label3 = new Label();
            m1d1label3.Text = "注意事项 :\n添加账号需填入账号密码\n修改账号需先选中条目.\n删除账号只需选中条目即可.\n批量导入需先从学生成绩内\n导出学号的txt\n学生账号需和学号匹配\n(不然无法查询成绩)";
            m1d1label3.AutoSize = true;
            m1d1label3.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            m1d1label3.ForeColor = Color.DeepSkyBlue;
            m1d1panel.Controls.Add(m1d1label3);
            m1d1label3.Top = 50;
            m1d1label3.Left = 500;

            TextBox m1d1textbox1 = new TextBox();
            TextBox m1d1textbox2 = new TextBox();
            m1d1panel.Controls.Add(m1d1textbox1);
            m1d1panel.Controls.Add(m1d1textbox2);
            m1d1textbox1.Width = 150;
            m1d1textbox1.Height = m1d1label1.Height;
            m1d1textbox2.Width = 150;
            m1d1textbox2.Height = m1d1label2.Height;
            m1d1textbox1.Top = 300;
            m1d1textbox1.Left = 500 + m1d1label1.Width;
            m1d1textbox2.Top = 350;
            m1d1textbox2.Left = 500 + m1d1label2.Width;

            Button m1d1button2 = new Button();
            Button m1d1button3 = new Button();
            Button m1d1button4 = new Button();
            Button m1d1button5 = new Button();
            m1d1button2.Text = " 添加账号 ";
            m1d1button3.Text = " 修改账号 ";
            m1d1button4.Text = " 删除账号 ";
            m1d1button5.Text = " 批量导入 ";
            m1d1button2.Font = new Font("微软雅黑", 10);
            m1d1button3.Font = new Font("微软雅黑", 10);
            m1d1button4.Font = new Font("微软雅黑", 10);
            m1d1button5.Font = new Font("微软雅黑", 10);
            m1d1button2.Width = 150;
            m1d1button3.Width = 150;
            m1d1button4.Width = 150;
            m1d1button5.Width = 150;
            m1d1panel.Controls.Add(m1d1button2);
            m1d1panel.Controls.Add(m1d1button3);
            m1d1panel.Controls.Add(m1d1button4);
            m1d1panel.Controls.Add(m1d1button5);
            m1d1button2.AutoSize = true;
            m1d1button3.AutoSize = true;
            m1d1button4.AutoSize = true;
            m1d1button5.AutoSize = true;
            m1d1button2.Top = 400;
            m1d1button3.Top = 450;
            m1d1button4.Top = 500;
            m1d1button5.Top = 550;
            m1d1button2.Left = m1d1textbox1.Left;
            m1d1button3.Left = m1d1button2.Left;
            m1d1button4.Left = m1d1button2.Left;
            m1d1button5.Left = m1d1button2.Left;
            m1d1button2.BackColor = Color.Pink;
            m1d1button3.BackColor = Color.DeepSkyBlue;
            m1d1button4.BackColor = Color.DarkSlateGray;
            m1d1button5.BackColor = Color.SeaGreen;

            m1d1button2.Click += delegate(Object o, EventArgs v) { M1D1ButtonClick2(m1d1button2, m1d1textbox1, m1d1textbox2, lv); };
            m1d1button3.Click += delegate(Object o, EventArgs v) { M1D1ButtonClick3(m1d1button3, m1d1textbox1, m1d1textbox2, lv); };
            m1d1button4.Click += delegate(Object o, EventArgs v) { M1D1ButtonClick4(m1d1button4, lv); };
            m1d1button5.Click += delegate(Object o, EventArgs v) { M1D1ButtonClick5(m1d1button5, lv); };
        }

        /// <summary>
        /// Xml数据导入listview内
        /// </summary>
        /// <param name="lv"></param>
        private void M1D1ListInfo(ListView lv)
        {
            //读取Xml文件
            ConfigForXml cfx = new ConfigForXml();
            XmlDocument clsxmldoc = new XmlDocument();
            clsxmldoc.Load(cfx.XmlPath);
            XmlNode clsxmlnode = clsxmldoc.SelectSingleNode(cfx.NodeTree);
            XmlNodeList clsxmlnl = clsxmlnode.ChildNodes;

            //表头与分组
            lv.Columns.Add("ID", 200, HorizontalAlignment.Left);
            lv.Columns.Add("Password", 200, HorizontalAlignment.Left);
            ListViewGroup grp = new ListViewGroup();
            grp.Header = "教师";
            lv.Groups.Add(grp);
            grp = new ListViewGroup();
            grp.Header = "学生";
            lv.Groups.Add(grp);

            //开始录入信息
            ListViewItem lvi;
            foreach (XmlElement xn in clsxmlnl)
            {
                if (xn.Name != "Administrator")
                {
                    lvi = new ListViewItem();
                    lvi.Text = xn.Name;
                    lvi.SubItems.Add(xn.InnerText);
                    lvi.Font = new Font("微软雅黑", 10);
                    if (lvi.Text[0] == 'T')
                        lvi.Group = lv.Groups[0];
                    else
                        lvi.Group = lv.Groups[1];
                    lv.Items.Add(lvi);
                }
            }
        }
        /// <summary>
        /// Menu1Drop1按钮事件集合,1为返回,2为添加,3为修改,4为删除,5为批量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pan"></param>
        private void M1D1ButtonClick1(object sender, Panel pan)
        {
            this.Controls.Remove(pan);
            pan.Dispose();
        }
        private void M1D1ButtonClick2(object sender, TextBox tb1, TextBox tb2, ListView lv)
        {
            string s1 = tb1.Text;
            string s2 = tb2.Text;
            if(s1.Trim().Equals(string.Empty))
            {
                MessageBox.Show("账号不能为空!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tb1.Focus();
                return;
            }
            else if(s2.Trim().Equals(string.Empty))
            {
                MessageBox.Show("密码不能为空!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tb2.Focus();
                return;
            }
            else
            {
                if(namefalse(s1))
                {
                    MessageBox.Show("账号只能为数字和字母!\n且第一位必须为字母!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tb1.Focus();
                    return;
                }
                foreach (ListViewItem lvi in lv.Items)
                {
                    if(lvi.Text == s1)
                    {
                        MessageBox.Show("账号重复!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        tb1.Focus();
                        return;
                    }
                }
                ConfigForXml cfx = new ConfigForXml();
                cfx.SetXmlData(s1, s2);
                lv.Clear();
                M1D1ListInfo(lv);
                tb1.Clear();
                tb2.Clear();
                return;
            }
        }

        private void M1D1ButtonClick3(object sender, TextBox tb1, TextBox tb2, ListView lv)
        {
            string s1 = tb1.Text;
            string s2 = tb2.Text;
            if (lv.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选定条目!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ListViewItem lvi = lv.SelectedItems[0];  //只读取第一项
            string id = lvi.Text;
            string pw = lvi.SubItems[1].Text;
            ConfigForXml cfx = new ConfigForXml();

            if (s1.Trim().Equals(string.Empty))
            {
                if(s2.Trim().Equals(string.Empty))
                {
                    MessageBox.Show("账号密码不能同时为空!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tb1.Focus();
                    lvi.Selected = true;
                    return;
                }
                pw = s2;
                cfx.SetXmlData(id, pw);
            }
            else
            {
                if (namefalse(s1))
                {
                    MessageBox.Show("账号只能为数字和字母!\n且第一位必须为字母!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tb1.Focus();
                    return;
                }
                cfx.RemoveXmlData(id);
                id = s1;
                if(s2.Trim().Equals(string.Empty) == false)
                {
                    pw = s2;
                }
                cfx.SetXmlData(id, pw);
            }

            lv.Clear();
            M1D1ListInfo(lv);
            tb1.Clear();
            tb2.Clear();
            return;
        }

        private void M1D1ButtonClick4(object sender, ListView lv)
        {
            if (lv.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选定条目!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ConfigForXml cfx = new ConfigForXml();
            DialogResult dr = MessageBox.Show(String.Format("你确定删除这{0}项吗", lv.SelectedItems.Count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if(dr == DialogResult.OK)
            {
                    foreach (ListViewItem lvi in lv.SelectedItems)
                    {
                        cfx.RemoveXmlData(lvi.Text);
                    }
                lv.Clear();
                M1D1ListInfo(lv);
                return;
            }
            return;
        }

        private void M1D1ButtonClick5(object sender, ListView lv)
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\Data\导出学号.txt") == false)
            {
                MessageBox.Show("成绩导出的学号txt文件不存在!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string[] str = File.ReadAllLines(System.Windows.Forms.Application.StartupPath + @"\Data\导出学号.txt");
            if(str.Length == 0)
            {
                MessageBox.Show("txt文件中无学号!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ConfigForXml cfx = new ConfigForXml();
            DialogResult dr = MessageBox.Show("你确定要批量导入吗?", "Hint", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if(dr != DialogResult.OK)
            {
                return;
            }

            int listcount = lv.Items.Count;

            for(int i = 0; i < str.Length; i++)
            {
                string ps = str[i].Substring(1);
                cfx.SetXmlData(str[i], ps);
            }
            lv.Clear();
            M1D1ListInfo(lv);

            if(listcount == lv.Items.Count)
            {
                MessageBox.Show("账号数目未增加", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show(String.Format("添加了{0}个账号", lv.Items.Count - listcount), "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        public void M1D2Click(object sender, EventArgs e)
        {
            Form fsearch = new Form();             //借用了一下menu4的窗口代码
            fsearch.FormBorderStyle = FormBorderStyle.None;
            fsearch.BackColor = Color.White;
            fsearch.Width = 200;
            fsearch.Height = 200;
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
            m4d1button1.Top = 170;
            m4d1button1.Left = 190 - m4d1button1.Width;
            m4d1button1.Click += delegate(Object o, EventArgs v) { M4D1ButtonClick1(m4d1button1, fsearch); };

            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            label1.Text = "  该账号 :  " + Vari.CurrentID;
            label2.Text = "  旧密码 : ";
            label3.Text = "  新密码 : ";
            fsearch.Controls.Add(label1);
            fsearch.Controls.Add(label2);
            fsearch.Controls.Add(label3);
            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            label1.Font = new Font("微软雅黑", 10);
            label2.Font = new Font("微软雅黑", 10);
            label3.Font = new Font("微软雅黑", 10);
            label1.ForeColor = Color.DeepSkyBlue;
            label2.ForeColor = Color.DeepSkyBlue;
            label3.ForeColor = Color.DeepSkyBlue;
            label1.Top = 5;
            label2.Top = 55;
            label3.Top = 105;
            label1.Left = label2.Left = label3.Left = 5;

            TextBox tb2 = new TextBox();
            fsearch.Controls.Add(tb2);
            tb2.Width = 100;
            tb2.Height = 30;
            tb2.Left = 10 + label2.Width;
            tb2.Top = 55;
            TextBox tb3 = new TextBox();
            fsearch.Controls.Add(tb3);
            tb3.Width = 100;
            tb3.Height = 30;
            tb3.Left = 10 + label3.Width;
            tb3.Top = 105;

            Button button = new Button();
            button.Text = " 确认修改 ";
            button.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            button.BackColor = Color.LightSkyBlue;
            button.AutoSize = true;
            fsearch.Controls.Add(button);
            button.Top = 135;
            button.Left = (200 - button.Width) / 2;
            button.Click += delegate(Object o, EventArgs v) { M1D2ButtonClick(button, tb2, tb3); };
        }
        private void M1D2ButtonClick(object sender, TextBox tb1, TextBox tb2)
        {
            if (tb1.Text.Equals(string.Empty))
            {
                MessageBox.Show("旧密码不能为空!");
                tb1.Focus();
                return;
            }
            if (tb2.Text.Equals(string.Empty))
            {
                MessageBox.Show("新密码不能为空!");
                tb2.Focus();
                return;
            }
            ConfigForXml cfx = new ConfigForXml();
            string password = cfx.ReadXmlData(Vari.CurrentID);
            if (string.Compare(tb1.Text, password) == 0)
            {
                cfx.SetXmlData(Vari.CurrentID, tb2.Text);
                tb1.Clear();
                tb2.Clear();
                MessageBox.Show("修改成功!");
            }
            else
            {
                MessageBox.Show("旧密码不正确!");
                tb1.Focus();
                return;
            }
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M1D4Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            ((Form)this.Tag).Show();
            //new Thread(() => Application.Run(new MyForm())).Start();
        }

        private void M1D5Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            ((Form)this.Tag).Close();
            ((Form)this.Tag).Dispose();
        }
        #endregion

        #region menu2事件
        /// <summary>
        /// 新建数据库文件的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M2D1Click(object sender, ToolStripMenuItem tsmi)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "数据库文件|*.accdb";
            if (file.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(file.FileName) == true)
                {
                    File.Delete(file.FileName);
                }
                DB.dbpath = file.FileName;
                try
                {
                    CreateDataFile();
                    OpenUIInitialize(tsmi);
                    tsmi.Click += new EventHandler(M2D3Click);
                }
                catch (Exception z)
                {
                    throw new Exception(z.Message);
                }
            }
        }
        /// <summary>
        /// 打开文件的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M2D2Click(object sender, ToolStripMenuItem tsmi)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "数据库文件|*.accdb";
            if (file.ShowDialog() == DialogResult.OK)       //确认打开
            {
                DB.dbpath = file.FileName;
                OpenUIInitialize(tsmi);
                tsmi.Click += new EventHandler(M2D3Click);
            }
        }
        /// <summary>
        /// 创建数据库的函数
        /// </summary>
        public void CreateDataFile()
        {
            ADOX.CatalogClass cat = new ADOX.CatalogClass();
            string temp = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + DB.dbpath + ";";
            cat.Create(temp);
            cat = null;          //新建数据库文件完毕

            ADODB.Connection cn = new ADODB.Connection();     //开始新建数据库的表
            cat = new ADOX.CatalogClass();
            cn.Open(temp, null, null, -1);
            cat.ActiveConnection = cn;
            ADOX.TableClass tb = new ADOX.TableClass();
            tb.ParentCatalog = cat;
            tb.Name = "Student";              //表Student
            ADOX.ColumnClass col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "学号";
            tb.Columns.Append(col, ADOX.DataTypeEnum.adChar);
            tb.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "学号");      //设置学号主键
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "姓名";
            tb.Columns.Append(col, ADOX.DataTypeEnum.adChar);
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "班级";
            tb.Columns.Append(col, ADOX.DataTypeEnum.adChar);
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "加权";
            col.Type = DataTypeEnum.adDouble;
            tb.Columns.Append(col);
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "加权排名";
            col.Type = DataTypeEnum.adInteger;
            tb.Columns.Append(col);
            cat.Tables.Append(tb);        //将Student表加入数据库
            tb = null;
            ADOX.TableClass tb1 = new ADOX.TableClass();         //创建SC表
            tb1.ParentCatalog = cat;
            tb1.Name = "SC";
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "Course";
            tb1.Columns.Append(col, ADOX.DataTypeEnum.adChar);
            tb1.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "Course");         //设置Course主键
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "Point";
            col.Type = DataTypeEnum.adDouble;
            tb1.Columns.Append(col);
            cat.Tables.Append(tb1);          //将SC表加入数据库
            tb1 = null;
            ADOX.TableClass tb2 = new ADOX.TableClass();         //创建Teacher表
            tb2.ParentCatalog = cat;
            tb2.Name = "Teacher";
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "Teacher";
            tb2.Columns.Append(col, ADOX.DataTypeEnum.adChar);
            col = new ADOX.ColumnClass();
            col.ParentCatalog = cat;
            col.Name = "Course";
            tb2.Columns.Append(col, ADOX.DataTypeEnum.adChar);
            tb2.Keys.Append("PrimaryKey", KeyTypeEnum.adKeyPrimary, "Course");         //设置Course主键
            cat.Tables.Append(tb2);          //将Teacher表加入数据库

            col = null;
            tb2 = null;      //结束
            cat = null;
            cn.Close();
        }
        /// <summary>
        /// 打开文件后的界面初始化函数
        /// </summary>
        public void OpenUIInitialize(ToolStripMenuItem tsmi)
        {
            Panel panel = new Panel();
            DataGridView dgv = new DataGridView();         //Student表
            DataGridView dgv1 = new DataGridView();        //SC表
            DataGridView dgv2 = new DataGridView();        //Teacher表
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
            m2d2button1.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick1(m2d2button1, panel, tsmi); };

            Button m2d2button3 = new Button();
            m2d2button3.Text = " 删除记录 ";
            m2d2button3.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button3.BackColor = Color.DeepSkyBlue;
            m2d2button3.AutoSize = true;
            panel.Controls.Add(m2d2button3);
            m2d2button3.Top = 630 + m2d2button3.Height;
            m2d2button3.Left = 70;
            m2d2button3.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick3(m2d2button3, dgv); };

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

            Button m2d2button5 = new Button();       //隶属SC表
            m2d2button5.Text = " 确认修改 ";
            m2d2button5.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button5.BackColor = Color.DeepSkyBlue;
            m2d2button5.AutoSize = true;
            panel.Controls.Add(m2d2button5);
            m2d2button5.Top = 260;
            m2d2button5.Left = 560;
            m2d2button5.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick5(m2d2button5, dgv1, dgv, panel); };

            Button m2d2button6 = new Button();         //隶属Teacher表
            m2d2button6.Text = " 确认修改 ";
            m2d2button6.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button6.BackColor = Color.DeepSkyBlue;
            m2d2button6.AutoSize = true;
            panel.Controls.Add(m2d2button6);
            m2d2button6.Top = 260;
            m2d2button6.Left = 810;
            m2d2button6.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick6(m2d2button6, dgv2); };

            Button m2d2button7 = new Button();            //重置两表
            m2d2button7.Text = " 撤销修改 ";
            m2d2button7.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button7.BackColor = Color.DeepSkyBlue;
            m2d2button7.AutoSize = true;
            panel.Controls.Add(m2d2button7);
            m2d2button7.Top = 260;
            m2d2button7.Left = 685;
            m2d2button7.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick7(m2d2button7, panel, dgv1, dgv2); };

            Label m2d2label1 = new Label();
            Label m2d2label2 = new Label();
            Label m2d2label3 = new Label();
            m2d2label1.Text = " Course ";
            m2d2label2.Text = " Point ";
            m2d2label3.Text = " Teacher ";
            panel.Controls.Add(m2d2label1);
            panel.Controls.Add(m2d2label2);
            panel.Controls.Add(m2d2label3);
            m2d2label1.AutoSize = true;
            m2d2label2.AutoSize = true;
            m2d2label3.AutoSize = true;
            m2d2label1.Font = new Font("微软雅黑", 10);
            m2d2label2.Font = new Font("微软雅黑", 10);
            m2d2label3.Font = new Font("微软雅黑", 10);
            m2d2label1.ForeColor = Color.LightSkyBlue;
            m2d2label2.ForeColor = Color.LightSkyBlue;
            m2d2label3.ForeColor = Color.LightSkyBlue;
            m2d2label1.Top = 300;
            m2d2label1.Left = 550;
            m2d2label2.Top = 300;
            m2d2label2.Left = 700;
            m2d2label3.Top = 300;
            m2d2label3.Left = 850;

            TextBox m2d2textbox1 = new TextBox();
            TextBox m2d2textbox2 = new TextBox();
            TextBox m2d2textbox3 = new TextBox();
            panel.Controls.Add(m2d2textbox1);
            panel.Controls.Add(m2d2textbox2);
            panel.Controls.Add(m2d2textbox3);
            m2d2textbox1.Width = 100;
            m2d2textbox2.Width = 100;
            m2d2textbox3.Width = 100;
            m2d2textbox1.Top = 330;
            m2d2textbox1.Left = 530;
            m2d2textbox2.Top = 330;
            m2d2textbox2.Left = 680;
            m2d2textbox3.Top = 330;
            m2d2textbox3.Left = 830;

            Button m2d2button8 = new Button();
            m2d2button8.Text = " 添加科目 ";
            m2d2button8.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button8.BackColor = Color.DeepSkyBlue;
            m2d2button8.AutoSize = true;
            panel.Controls.Add(m2d2button8);
            m2d2button8.Top = 370;
            m2d2button8.Left = 610;
            m2d2button8.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick8(m2d2button8, m2d2textbox1, m2d2textbox2, m2d2textbox3, panel, dgv, dgv1, dgv2); };

            Button m2d2button9 = new Button();
            m2d2button9.Text = " 删除科目 ";
            m2d2button9.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2button9.BackColor = Color.DeepSkyBlue;
            m2d2button9.AutoSize = true;
            panel.Controls.Add(m2d2button9);
            m2d2button9.Top = 370;
            m2d2button9.Left = 770;
            m2d2button9.Click += delegate(Object o, EventArgs v) { M2D2ButtonClick9(m2d2button9, m2d2textbox1, m2d2textbox2, m2d2textbox3, panel, dgv, dgv1, dgv2); };

            Label m2d2label4 = new Label();
            m2d2label4.Text = "1.三击格子即可编辑修改。(加权和排名会自动计算)\n   不可修改项:左表的加权及加权排名、上两表的Course\n2.左表删除记录点行首的白砖(可删除多条)\n3.三表的撤销修改均可使表恢复初始状态(包括左表的删除记录)\n4.三表的确认修改均为保存所有修改,将不可撤销。\n5.若添加科目,填好课程学分和对应教师即可。\n   添加成功后需在左表填好成绩,否则无法计算加权和加权排名。\n6.若删除科目,只需填好科目名称(加权和排名会自动更新)\n7.未点「确认修改」而退出,将不会保存先前对该表的修改。\n  (确认修改后会有短暂延迟,请耐心等待)\n8.添加科目后,科目将会排在最后方便填写成绩,确认修改后\n   重新打开该文件该科目将自动排在加权前\n9.写入科目的分数应取整数";
            m2d2label4.AutoSize = true;
            m2d2label4.Font = new Font("微软雅黑", 12, FontStyle.Bold);
            m2d2label4.ForeColor = Color.DeepSkyBlue;
            panel.Controls.Add(m2d2label4);
            m2d2label4.Top = 410;
            m2d2label4.Left = 500;

            Label m2d2label5 = new Label();
            m2d2label5.Text = "点击某列即可对该列排序(升降均可)";
            m2d2label5.AutoSize = true;
            m2d2label5.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m2d2label5.ForeColor = Color.DeepSkyBlue;
            panel.Controls.Add(m2d2label5);
            m2d2label5.Top = 30;
            m2d2label5.Left = 100;

            Flash1(panel, dgv);
            Flash2(panel, dgv1, dgv2);
        }
        /// <summary>
        /// 用以刷新Student表，更新数据视图
        /// </summary>
        /// <param name="p"></param>
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
            dgv.Columns["加权"].ReadOnly = true;
            dgv.Columns["加权排名"].ReadOnly = true;
        }
        private void Flash2(Panel p, DataGridView dgv1, DataGridView dgv2)
        {
            DataTable dt1 = MySQL.dataTable("Select * From SC;");
            DataTable dt2 = MySQL.dataTable("Select * From Teacher;");
            dgv1.DataSource = dt1;
            dgv2.DataSource = dt2;
            p.Controls.Add(dgv1);
            dgv1.BackgroundColor = Color.LightSkyBlue;
            dgv1.Left = 500;
            dgv1.Top = 50;
            dgv1.Width = 200;
            dgv1.Height = 200;
            p.Controls.Add(dgv2);
            dgv2.BackgroundColor = Color.LightSkyBlue;
            dgv2.Left = 750;
            dgv2.Top = 50;
            dgv2.Width = 200;
            dgv2.Height = 200;
            dgv1.Columns[0].ReadOnly = true;
            dgv2.Columns[1].ReadOnly = true;
            dgv1.AllowUserToAddRows = false;
            dgv2.AllowUserToAddRows = false;
        }
        private void M2D2ButtonClick1(object sender, Panel p, ToolStripMenuItem tsmi)
        {
            this.Controls.Remove(p);
            p.Dispose();
            tsmi.Click -= new EventHandler(M2D3Click);
        }
        private void M2D2ButtonClick2(object sender, DataGridView dgv, Panel p)
        {
            DialogResult dr = MessageBox.Show(String.Format("确定修改?(无法撤销)", dgv.SelectedRows.Count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                DBManipulate dbm = new DBManipulate();
                if(dbm.Update1(dgv) == false)
                {
                    MessageBox.Show("信息不完整!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(dbm.GetAvgAndRank() == false)
                {
                    MessageBox.Show("科目分数不完整!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("修改成功");
                Flash1(p, dgv);
            }
        }
        private void M2D2ButtonClick3(object sender, DataGridView dgv)
        {
            if(dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选中一行!");
                return;
            }
            DialogResult dr = MessageBox.Show(String.Format("删除这{0}条记录?", dgv.SelectedRows.Count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                for (int i = dgv.SelectedRows.Count; i > 0; i--)
                {
                    dgv.Rows.RemoveAt(dgv.SelectedRows[i - 1].Index);
                }
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
        private void M2D2ButtonClick5(object sender, DataGridView dgv, DataGridView dgv0, Panel p)
        {
            DialogResult dr = MessageBox.Show(String.Format("确定修改?(无法撤销)", dgv.SelectedRows.Count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                DBManipulate dbm = new DBManipulate();
                if(dbm.Update2(dgv, 1) == false)
                {
                    MessageBox.Show("科目学分有空缺!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dbm.GetAvgAndRank();
                Flash1(p, dgv0);
                MessageBox.Show("修改成功");
            }
        }
        private void M2D2ButtonClick6(object sender, DataGridView dgv)
        {
            DialogResult dr = MessageBox.Show(String.Format("确定修改?(无法撤销)", dgv.SelectedRows.Count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                DBManipulate dbm = new DBManipulate();
                if (dbm.Update2(dgv, 2) == false)
                {
                    MessageBox.Show("教师不能为空!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("修改成功");
            }
        }
        private void M2D2ButtonClick7(object sender, Panel p, DataGridView dgv1, DataGridView dgv2)
        {
            DialogResult dr = MessageBox.Show("撤销之前所有的修改(删除)?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                Flash2(p, dgv1, dgv2);
            }
        }
        private void M2D2ButtonClick8(object sender, TextBox tb1, TextBox tb2, TextBox tb3, Panel p, DataGridView dgv0, DataGridView dgv1, DataGridView dgv2)
        {
            string course = tb1.Text;
            string point = tb2.Text;
            string t = tb3.Text;
            if (course.Equals(string.Empty) || point.Equals(string.Empty) || t.Equals(string.Empty))
            {
                MessageBox.Show("添加项均不能为空!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (isnum(point) == false)
            {
                MessageBox.Show("学分必须为数字!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dr = MessageBox.Show(String.Format("确定添加「{0}」「{1}」「{2}」?", course, point, t), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                MySQL.excuteSQL("Insert Into Teacher Values('" + t + "', '" + course + "');");
                MySQL.excuteSQL("Insert Into SC Values('" + course + "', " + point + ");");
                MySQL.excuteSQL("Alter table Student Drop column 加权, 加权排名;");
                MySQL.excuteSQL("Alter table Student Add " + course + " int;");
                MySQL.excuteSQL("Alter table Student add 加权 double, 加权排名 int;");
                Flash1(p, dgv0);
                Flash2(p, dgv1, dgv2);
                MessageBox.Show("添加成功");
                tb1.Clear();
                tb2.Clear();
                tb3.Clear();
            }
        }
        private void M2D2ButtonClick9(object sender, TextBox tb1, TextBox tb2, TextBox tb3, Panel p, DataGridView dgv0, DataGridView dgv1, DataGridView dgv2)
        {
            string course = tb1.Text;
            DataTable dt = (DataTable)dgv1.DataSource;
            int i;
            for (i = 0; i < dt.Rows.Count; i++)             //判断是否存在该科目
            {
                if(dt.Rows[i][0].ToString() == course)
                {
                    i = 0;
                    break;
                }
            }
            if(i == dt.Rows.Count)
            {
                MessageBox.Show("不存在该科目!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dr = MessageBox.Show(String.Format("你确定要删除「{0}」?", course), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if(dr == DialogResult.OK)
            {
                MySQL.excuteSQL("Delete From Teacher Where Course = '" + course + "';");
                MySQL.excuteSQL("Delete From SC Where Course = '" + course + "';");
                MySQL.excuteSQL("Alter table Student Drop column " + course + ";");
                DBManipulate dbm = new DBManipulate();
                dbm.GetAvgAndRank();
                Flash1(p, dgv0);
                Flash2(p, dgv1, dgv2);
                MessageBox.Show("删除成功");
                tb1.Clear();
                tb2.Clear();
                tb3.Clear();
            }
        }
        /// <summary>
        /// 另存为事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M2D3Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "数据库文件|*.accdb";
            if (file.ShowDialog() == DialogResult.OK)
            {
                string SavePath = file.FileName;
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";                //使用CMD命令行方法另存为
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine("copy /y " + DB.dbpath + " " + SavePath);     // /y为默认覆盖
                p.StandardInput.WriteLine("exit");
                p.StandardOutput.ReadToEnd();
                p.Close();
            }
        }
        #endregion

        #region menu3事件
        /// <summary>
        /// 查看默认数据库路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M3D1Click(object sender, EventArgs e)
        {
            MessageBox.Show("当前默认数据库路径:\n" + Vari.DefaultDB);
        }
        /// <summary>
        /// 设置默认数据库路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M3D2Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "数据库文件|*.accdb";
            if (file.ShowDialog() == DialogResult.OK)
            {
                string temp = System.Environment.CurrentDirectory + @"\Data\DafaultDB.zzz";
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";                //使用CMD命令行方法删除文件后再创建
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine("del/f/s/q " + temp);
                p.StandardInput.WriteLine("exit");
                p.StandardOutput.ReadToEnd();
                p.Close();

                FileStream fs = new FileStream(temp, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(file.FileName);
                sw.Flush();
                sw.Close();
                fs.Close();
                Vari.DefaultDB = File.ReadAllText(temp);
                MessageBox.Show("设置成功,路径:\n" + Vari.DefaultDB);
            }
        }
        private void M3D3Click(object sender, EventArgs e)
        {
            DBManipulate dbm = new DBManipulate();
            string temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;
            dbm.GetXuehao();
            DB.dbpath = temp;
            MessageBox.Show("导出成功!");
        }
        #endregion

        #region menu4事件
        private void M4D1Click(object sender, EventArgs e)
        {
            Form fsearch = new Form();
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
            dgv.Height = 150;
            dgv.Top = 10;
            dgv.Left = 10;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            fsearch.Controls.Add(dgv);
            dgvFlush(dgv);

            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            label1.Text = "       学号 : ";
            label2.Text = "       姓名 : ";
            label3.Text = "加权排名 : ";
            fsearch.Controls.Add(label1);
            fsearch.Controls.Add(label2);
            fsearch.Controls.Add(label3);
            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            label1.Font = new Font("微软雅黑", 10);
            label2.Font = new Font("微软雅黑", 10);
            label3.Font = new Font("微软雅黑", 10);
            label1.ForeColor = Color.DeepSkyBlue;
            label2.ForeColor = Color.DeepSkyBlue;
            label3.ForeColor = Color.DeepSkyBlue;
            label1.Top = 180;
            label2.Top = 230;
            label3.Top = 280;
            label1.Left = label2.Left = label3.Left = 10;

            TextBox tb1 = new TextBox();
            fsearch.Controls.Add(tb1);
            tb1.Width = 100;
            tb1.Height = 30;
            tb1.Left = 20 + label1.Width;
            tb1.Top = 180;
            TextBox tb2 = new TextBox();
            fsearch.Controls.Add(tb2);
            tb2.Width = 100;
            tb2.Height = 30;
            tb2.Left = 20 + label2.Width;
            tb2.Top = 230;
            TextBox tb3 = new TextBox();
            fsearch.Controls.Add(tb3);
            tb3.Width = 100;
            tb3.Height = 30;
            tb3.Left = 20 + label3.Width;
            tb3.Top = 280;

            Button m4d1button2 = new Button();
            m4d1button2.Text = " 按学号查 ";
            m4d1button2.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m4d1button2.BackColor = Color.LightSkyBlue;
            m4d1button2.AutoSize = true;
            fsearch.Controls.Add(m4d1button2);
            m4d1button2.Top = 175;
            m4d1button2.Left = tb1.Left + 140;
            m4d1button2.Click += delegate(Object o, EventArgs v) { M4D1ButtonClick2(m4d1button2, dgv, tb1.Text); };
            Button m4d1button3 = new Button();
            m4d1button3.Text = " 按姓名查 ";
            m4d1button3.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m4d1button3.BackColor = Color.LightSkyBlue;
            m4d1button3.AutoSize = true;
            fsearch.Controls.Add(m4d1button3);
            m4d1button3.Top = 225;
            m4d1button3.Left = tb2.Left + 140;
            m4d1button3.Click += delegate(Object o, EventArgs v) { M4D1ButtonClick3(m4d1button3, dgv, tb2.Text); };
            Button m4d1button4 = new Button();
            m4d1button4.Text = " 按排名查 ";
            m4d1button4.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            m4d1button4.BackColor = Color.LightSkyBlue;
            m4d1button4.AutoSize = true;
            fsearch.Controls.Add(m4d1button4);
            m4d1button4.Top = 275;
            m4d1button4.Left = tb3.Left + 140;
            m4d1button4.Click += delegate(Object o, EventArgs v) { M4D1ButtonClick4(m4d1button4, dgv, tb3.Text); };
        }
        /// <summary>
        /// 供dgv刷新出现列名用
        /// </summary>
        /// <param name="dgv"></param>
        private void dgvFlush(DataGridView dgv)
        {
            DB.dbpath = Vari.DefaultDB;      //令数据库为默认数据库
            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select * From Student Where 学号 = ''");
            dgv.DataSource = dt;
        }
        public void M4D1ButtonClick1(object sender, Form temp)
        {
            temp.Close();
            temp.Dispose();
        }
        private void M4D1ButtonClick2(object sender, DataGridView dgv, string str)
        {
            string temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;      //令数据库为默认数据库
            DBManipulate dbm = new DBManipulate();
            if (dbm.CheckXuehaoExist(str) == false)
            {
                DB.dbpath = temp;
                MessageBox.Show("该学号不存在!");
                return;
            }
            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select * From Student Where 学号 = '"+str+"';");
            dgv.DataSource = dt;
            DB.dbpath = temp;
        }
        private void M4D1ButtonClick3(object sender, DataGridView dgv, string str)
        {
            string temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;      //令数据库为默认数据库
            DBManipulate dbm = new DBManipulate();
            if (dbm.CheckNameExist(str) == false)
            {
                DB.dbpath = temp;
                MessageBox.Show("该姓名不存在!");
                return;
            }
            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select * From Student Where 姓名 = '" + str + "';");
            dgv.DataSource = dt;
            DB.dbpath = temp;
        }
        private void M4D1ButtonClick4(object sender, DataGridView dgv, string str)
        {
            if(isnum(str) == false)
            {
                MessageBox.Show("排名应该输入整数!");
                return;
            }
            string temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;      //令数据库为默认数据库
            DBManipulate dbm = new DBManipulate();
            if (dbm.CheckRankExist(str) == false)
            {
                DB.dbpath = temp;
                MessageBox.Show("该排名不存在!");
                return;
            }
            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select * From Student Where 加权排名 = " + str + ";");
            dgv.DataSource = dt;
            DB.dbpath = temp;
        }

        private void M4D2Click1(object sender, EventArgs e)
        {
            Form fsearch = new Form();                          //此处直接拷贝查询栏的部分代码
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

            DBManipulate dbm = new DBManipulate();
            string temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;
            dbm.getavg(dgv);
            DB.dbpath = temp;
        }
        private void M4D2Click2(object sender, EventArgs e)
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
            dgv.Height = 300;
            dgv.Top = 10;
            dgv.Left = 10;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            fsearch.Controls.Add(dgv);

            Label label = new Label();
            label.Text = "科目 : ";
            fsearch.Controls.Add(label);
            label.AutoSize = true;
            label.Font = new Font("微软雅黑", 10);
            label.ForeColor = Color.DeepSkyBlue;
            label.Top = 320;
            label.Left = 10;
            TextBox tb = new TextBox();
            fsearch.Controls.Add(tb);
            tb.Width = 100;
            tb.Height = 30;
            tb.Left = 20 + label.Width;
            tb.Top = 320;

            Button button = new Button();
            button.Text = " 确定 ";
            button.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            button.BackColor = Color.LightSkyBlue;
            button.AutoSize = true;
            fsearch.Controls.Add(button);
            button.Top = 317;
            button.Left = tb.Left + tb.Width + 10;
            button.Click += delegate(Object o, EventArgs v) { M4D2ButtonClick2(button, dgv, tb.Text); };
        }
        private void M4D2ButtonClick2(object sender, DataGridView dgv ,string str)
        {
            string temp;
            temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;
            DBManipulate dbm = new DBManipulate();
            if (dbm.CheckCourseExist(str) == false)
            {
                DB.dbpath = temp;
                MessageBox.Show("该科目不存在!");
                return;
            }
            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select 学号, 姓名, 班级, " + str + " From Student Where " + str + " < 60;");
            dgv.DataSource = dt;
            DB.dbpath = temp;
        }
        private void M4D2Click3(object sender, EventArgs e)
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
            dgv.Height = 300;
            dgv.Top = 10;
            dgv.Left = 10;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            fsearch.Controls.Add(dgv);

            Label label = new Label();
            label.Text = "科目 : ";
            fsearch.Controls.Add(label);
            label.AutoSize = true;
            label.Font = new Font("微软雅黑", 10);
            label.ForeColor = Color.DeepSkyBlue;
            label.Top = 320;
            label.Left = 10;
            TextBox tb = new TextBox();
            fsearch.Controls.Add(tb);
            tb.Width = 100;
            tb.Height = 30;
            tb.Left = 20 + label.Width;
            tb.Top = 320;

            Button button = new Button();
            button.Text = " 确定 ";
            button.Font = new Font("微软雅黑", 10, FontStyle.Bold);
            button.BackColor = Color.LightSkyBlue;
            button.AutoSize = true;
            fsearch.Controls.Add(button);
            button.Top = 317;
            button.Left = tb.Left + tb.Width + 10;
            button.Click += delegate(Object o, EventArgs v) { M4D2ButtonClick3(button, dgv, tb.Text); };
        }
        private void M4D2ButtonClick3(object sender, DataGridView dgv, string str)
        {
            string temp;
            temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;
            DBManipulate dbm = new DBManipulate();
            if (dbm.CheckCourseExist(str) == false)
            {
                DB.dbpath = temp;
                MessageBox.Show("该科目不存在!");
                return;
            }
            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select top 10 学号, 姓名, 班级, " + str + " From Student Order by " + str + " DESC;");
            dgv.DataSource = dt;
            DB.dbpath = temp;
        }
        private void M4D2Click4(object sender, EventArgs e)
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
            string temp = DB.dbpath;
            DB.dbpath = Vari.DefaultDB;
            dt = MySQL.dataTable("Select top 15 * From Student Order By 加权排名 ASC;");
            DB.dbpath = temp;
            dgv.DataSource = dt;
        }
        

        private void M4D3Click1(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Excel文件(*.xls)|*.xls";
            if (file.ShowDialog() == DialogResult.OK)
            {
                string fn = file.FileName;
                if (File.Exists(fn))
                {
                    File.Delete(fn);
                }
                string temp = DB.dbpath;
                DB.dbpath = Vari.DefaultDB;
                MySQL.excuteSQL("Select * into [Excel 8.0;database=" + fn + "].[学生成绩表] from Student;");
                MySQL.excuteSQL("Select * into [Excel 8.0;database=" + fn + "].[课程表] from SC;");
                MySQL.excuteSQL("Select * into [Excel 8.0;database=" + fn + "].[教师表] from Teacher;");
                MessageBox.Show("导出成功!");
            }
        }
        private void M4D3Click2(object sender, EventArgs e)
        {
            MessageBox.Show("该功能尚未开放");
        }
        #endregion

        #region menu5事件
        private void M5D1Click(object sender, EventArgs e)
        {
            Help.ShowHelp(new Control(), System.Environment.CurrentDirectory + @"\Resources\功能说明.chm");
        }
        private void M5D2Click(object sender, EventArgs e)
        {
            Form temp = new Form();
            Button button = new Button();
            Label label = new Label();
            Label llabel = new Label();
            Label labelin = new Label();
            Panel panel = new Panel();
            PictureBox picturebox = new PictureBox();
            Bitmap b = new Bitmap(System.Environment.CurrentDirectory + @"\Resources\1.ico");
            IntPtr hicon = b.GetHicon();
            Icon icon = Icon.FromHandle(hicon);


            temp.FormBorderStyle = FormBorderStyle.None;
            temp.BackColor = Color.White;
            temp.Icon = icon;
            temp.Width = 400;
            temp.Height = 400;
            temp.StartPosition = FormStartPosition.CenterScreen;
            temp.Paint += new PaintEventHandler(AboutForm);
            temp.ShowInTaskbar = false;
            temp.TopMost = true;
            temp.Show();

            picturebox.Image = Image.FromFile(System.Environment.CurrentDirectory + @"\Resources\1.ico");
            temp.Controls.Add(picturebox);
            picturebox.Top = 10;
            picturebox.Left = 20;
            picturebox.Height = 100;
            picturebox.Width = 100;
            picturebox.SizeMode = PictureBoxSizeMode.StretchImage;

            llabel.Text = " Student\n Manage\n System";
            llabel.Font = new Font("Time New Roman", 20, FontStyle.Bold);
            temp.Controls.Add(llabel);
            llabel.AutoSize = true;
            llabel.Top = 15;
            llabel.Left = 170;

            panel.BorderStyle = BorderStyle.Fixed3D;
            panel.BackColor = Color.Transparent;
            temp.Controls.Add(panel);
            panel.Width = 300;
            panel.Height = 200;
            panel.Top = 140;
            panel.Left = 50;

            label.Text = "Author :     刘晋通团队\nTelephone :     15927297895";
            label.Font = new Font("宋体", 15, FontStyle.Strikeout);
            label.AutoSize = true;
            panel.Controls.Add(label);
            label.Left = (300 - label.Width) / 2;
            label.Top = 20;

            labelin.Text = "StudentManageSystem\nVersion : Trial Version\n\n(。・`ω´・)";
            labelin.Font = new Font("宋体", 15, FontStyle.Bold);
            labelin.AutoSize = true;
            panel.Controls.Add(labelin);
            labelin.Left = (300 - labelin.Width) / 2;
            labelin.Top = 90;

            button.Text = " 确定 ";
            button.BackColor = Color.LightSkyBlue;
            button.AutoSize = true;
            temp.Controls.Add(button);
            button.Top = 360;
            button.Left = (400 - button.Width) / 2;
            button.Click += delegate(Object o, EventArgs v) { M5D2ButtonClick(button, temp); };   //匿名事件加载
        }

        public void AboutForm(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, 399, 399);
        }
        private void M5D2ButtonClick(object sender, Form temp)
        {
            temp.Close();
            temp.Dispose();
        }
        #endregion

        #region 一些小工具
        /// <summary>
        /// 判断ID是否正确用
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool namefalse(string s)
        {
            if (Char.IsLetter(s[0]) == false)
                return true;
            for(int i = 0; i < s.Length; i++)
            {
                if (Char.IsLetterOrDigit(s[i]) == false)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 防闪屏
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        /// <summary>
        /// 判断字符串是否为数字
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private bool isnum(string a)
        {
            try
            {
                double t = Convert.ToDouble(a);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
