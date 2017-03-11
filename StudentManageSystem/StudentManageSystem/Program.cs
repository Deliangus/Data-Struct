using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.IO;

namespace StudentManageSystem
{
    /// <summary>
    /// 全局变量集合
    /// </summary>
    public class Vari     //通用变量
    {
        public static int Level;
        public static string DefaultDB = DBManipulate.DafaultDB();
        public static string CurrentID;
    }
    public class DB       //数据库用的变量
    {
        public static string dbpath;
        public static string command;
    }

    #region 登录窗体
    public class MyForm : Form
    {
        /// <summary>
        /// 控件管理区域
        /// </summary>
        private TextBox textbox1;
        private TextBox textbox2;
        private Button button;
        private Label label1;
        private Label label2;
        public Form1 form1;
        public Form2 form2;
        public Form3 form3;

        /// <summary>
        /// 初始化区域
        /// </summary>
        public MyForm()
        {
            //图标设置
            Bitmap b = new Bitmap(System.Environment.CurrentDirectory + @"\Resources\1.ico");
            IntPtr hicon = b.GetHicon();
            Icon icon = Icon.FromHandle(hicon);
            //配置Form
            this.Text = "学生成绩管理系统";
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = Image.FromFile(System.Environment.CurrentDirectory + @"\Resources\28.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = icon;

            this.textbox1 = new TextBox();
            this.label1 = new Label();
            this.label1.Text = "账号: ";
            this.label1.Font = new Font("宋体", 14);
            this.label1.ForeColor = Color.AliceBlue;
            this.label1.BackColor = Color.Transparent;
            this.label1.AutoSize = true;
            //两个textbox
            this.textbox2 = new TextBox();
            this.textbox2.PasswordChar = '*';
            this.label2 = new Label();
            this.label2.Text = "密码: ";
            this.label2.Font = new Font("宋体", 14);
            this.label2.ForeColor = Color.AliceBlue;
            this.label2.BackColor = Color.Transparent;
            this.label2.AutoSize = true;

            this.button = new Button();
            this.button.Text = "确定";
            this.button.Click += new EventHandler(ButtonClick);

            this.Controls.Add(this.textbox1);
            this.Controls.Add(this.textbox2);
            this.Controls.Add(this.button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);

            Vari.DefaultDB = DBManipulate.DafaultDB();
        }

        /// <summary>
        /// 加载区域
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.textbox1.Top = (this.Height - this.textbox1.Height) / 3;
            this.textbox1.Left = this.Width / 2;
            this.label1.Top = this.textbox1.Top;
            this.label1.Left = this.textbox1.Left - this.label1.Width;

            this.textbox2.Top = this.textbox1.Top + this.textbox1.Height + 50;
            this.textbox2.Left = this.textbox1.Left;
            this.label2.Top = this.textbox2.Top;
            this.label2.Left = this.textbox2.Left - this.label2.Width;

            this.button.Top = this.textbox2.Top + this.textbox2.Height + 50;
            this.button.Left = this.textbox1.Left;

            ConfigForXml data = new ConfigForXml();     //账号密码创建
            data.CreateXml();
        }

        /// <summary>
        /// Click事件区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            string s1 = this.textbox1.Text.ToString();
            string s2 = this.textbox2.Text.ToString();

            //输入的友好界面
            if(s1.Trim().Equals(string.Empty))
            {
                MessageBox.Show("账号不能为空白!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textbox1.Focus();
                return;
            }
            if(s2.Trim().Equals(string.Empty))
            {
                MessageBox.Show("密码不能为空白!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textbox2.Focus();
                return;
            }
            
            //读取Xml中的账号和密码，查看是否匹配
            ConfigForXml data = new ConfigForXml();
            string password = data.ReadXmlData(s1);
            if(password.Equals(string.Empty))
            {
                MessageBox.Show("账号不存在!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.textbox1.Focus();
                return;
            }
            if(string.Compare(s2,password) == 0)
            {
                Vari.Level = JudgeLevel(s1);
                Vari.CurrentID = s1;
                MessageBox.Show("登陆成功!");
                if(Vari.Level == 1)
                {
                    this.textbox1.Clear();
                    this.textbox2.Clear();
                    form1 = new Form1();
                    form1.Tag = this;
                    form1.Show();
                    this.Hide();
                    //new Thread(() => Application.Run(new Form1())).Start();
                }
                else if (Vari.Level == 2)
                {
                    this.textbox1.Clear();
                    this.textbox2.Clear();
                    form2 = new Form2();
                    form2.Tag = this;
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    this.textbox1.Clear();
                    this.textbox2.Clear();
                    form3 = new Form3();
                    form3.Tag = this;
                    form3.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("密码错误!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //友好界面构筑完成
        }

        /// <summary>
        /// 判断登录用户的级别
        /// </summary>
        private int JudgeLevel(string id)
        {
            if(string.Compare(id, "Administrator") == 0)
                return 1;
            if(id[0] == 'T')
                return 2;
            if(id[0] == 'U')
                return 3;
            return 0;
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
    }
    #endregion
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
            Application.Run(new MyForm());
        }
    }
}