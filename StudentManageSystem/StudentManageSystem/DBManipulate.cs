using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using ADOX;

namespace StudentManageSystem
{
    /// <summary>
    /// 数据库具体操作的集合
    /// </summary>
    class DBManipulate
    {
        public DBManipulate()
        {
            //实例化
        }
        /// <summary>
        /// 更新Student表
        /// </summary>
        /// <param name="dgv"></param>
        public bool Update1(DataGridView dgv)
        {
            //初始化
            OleDbDataAdapter ada = new OleDbDataAdapter();
            DataTable table = (DataTable)dgv.DataSource;  //获取表
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + DB.dbpath + ";";
            ada.SelectCommand = new OleDbCommand("Select * From Student;", conn);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(ada);
            ada.UpdateCommand = builder.GetUpdateCommand();

            try
            {
                ada.Update(table);
                table.AcceptChanges();
                dgv.DataSource = table;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                ada.Dispose();
            }
            return true;
        }
        /// <summary>
        /// 更新SC表以及Teacher表,传入1为SC表,其他为Teacher
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="num"></param>
        public bool Update2(DataGridView dgv, int num)
        {
            //初始化
            OleDbDataAdapter ada = new OleDbDataAdapter();
            DataTable table = (DataTable)dgv.DataSource;  //获取表
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + DB.dbpath + ";";
            if(num == 1)
            {
                ada.SelectCommand = new OleDbCommand("Select * From SC;", conn);
                for(int i = 0; i < table.Rows.Count; i++)
                    if(String.IsNullOrEmpty(table.Rows[i][1].ToString()))
                        return false;
            }
            else
            {
                ada.SelectCommand = new OleDbCommand("Select * From Teacher;", conn);
                for(int i = 0; i < table.Rows.Count; i++)
                    if(String.IsNullOrEmpty(table.Rows[i][0].ToString()))
                        return false;
            }
            OleDbCommandBuilder builder = new OleDbCommandBuilder(ada);
            ada.UpdateCommand = builder.GetUpdateCommand();

            try
            {
                ada.Update(table);
                table.AcceptChanges();
                dgv.DataSource = table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                ada.Dispose();
            }
            return true;
        }
        /// <summary>
        /// 算加权及添加加权排名
        /// </summary>
        /// <param name="insertstr"></param>
        /// <returns></returns>
        public bool GetAvgAndRank()
        {
            double sum1 = 0.0;            //计算学分总和
            double sum2 = 0.0;            //计算加权的分子
            int count = 0, count2 = 0;
            OleDbDataReader test = MySQL.dataReader("Select count(*) From SC;");
            test.Read();
            count = test.GetInt32(0);
            test.Close();
            test.Dispose();

            string[] cname = new string[count];
            double[] cpoint = new double[count];
            int i = 0;
            OleDbDataReader reader = MySQL.dataReader("Select * From SC;");
            while (reader.Read())               //获取科目及学分信息
            {
                cname[i] = reader.GetString(0);
                if(reader[1] == DBNull.Value)
                {
                    reader.Close();
                    reader.Dispose();
                    return false;
                }
                cpoint[i] = reader.GetDouble(1);
                sum1 += cpoint[i];
                i++;
            }
            reader.Close();
            reader.Dispose();

            OleDbDataReader ccount = MySQL.dataReader("Select count(*) From Student;");      //获取所有学号集合
            ccount.Read();
            count2 = ccount.GetInt32(0);
            ccount.Close();
            ccount.Dispose();
            string[] xuehao = new string[count2];
            OleDbDataReader cnum = MySQL.dataReader("Select 学号 From Student;");
            i = 0;
            while (cnum.Read())
            {
                xuehao[i++] = cnum.GetString(0);
            }
            cnum.Close();
            cnum.Dispose();

            for (int j = 0; j < count2; j++)          //计算加权
            {
                sum2 = 0;
                for (i = 0; i < count; i++)
                {
                    OleDbDataReader tempreader = MySQL.dataReader("Select " + cname[i] + " From Student Where 学号 = '" + xuehao[j] + "';");
                    tempreader.Read();
                    if (DBNull.Value.Equals(tempreader[0]))
                    {
                        tempreader.Close();
                        tempreader.Dispose();
                        return false;
                    }
                    int score = tempreader.GetInt32(0);
                    sum2 += cpoint[i] * score;
                    tempreader.Close();
                    tempreader.Dispose();
                }
                double jiaquan = sum2 / sum1;
                MySQL.excuteSQL("Update Student Set 加权 = " + jiaquan.ToString() + " Where 学号 = '" + xuehao[j] + "';"); //将加权加入表中
            }
            GetRank();
            return true;
        }
        /// <summary>
        /// 算加权排名
        /// </summary>
        private void GetRank()
        {
            int count, i;
            OleDbDataReader ccount = MySQL.dataReader("Select count(*) From Student;");      //算Student表中行数目
            ccount.Read();
            count = ccount.GetInt32(0);
            ccount.Close();
            ccount.Dispose();
            string[] all = new string[count];
            //算排名
            OleDbDataReader rank = MySQL.dataReader("Select 学号 From Student Order By 加权 DESC;");
            i = 0;
            while (rank.Read())
            {
                all[i++] = rank.GetString(0);
            }
            rank.Close();
            rank.Dispose();
            for (i = 0; i < count; i++)
            {
                MySQL.excuteSQL("Update Student Set 加权排名 = " + (i + 1).ToString() + " Where 学号 = '" + all[i] + "';");
            }
        }
        /// <summary>
        /// 保存数据库默认路径文件的操作
        /// </summary>
        /// <returns></returns>
        public static string DafaultDB()
        {
            string temp = System.Environment.CurrentDirectory + @"\Data\DafaultDB.zzz";
            if (File.Exists(temp) == false)
            {
                FileStream fs = new FileStream(temp, FileMode.Create);       //与写入文件关联
                StreamWriter sw = new StreamWriter(fs);      //与fs关联
                sw.WriteLine(System.Environment.CurrentDirectory + @"\Data\Student.accdb");
                sw.Flush();
                sw.Close();
                fs.Close();
                return System.Environment.CurrentDirectory + @"\Data\Student.accdb";
            }
            else
            {
                string getstr = File.ReadAllText(temp);
                return getstr;
            }
        }
        /// <summary>
        /// 获取各科平均分
        /// </summary>
        /// <param name="dgv"></param>
        public void getavg(DataGridView dgv)
        {
            OleDbDataReader reader1 = MySQL.dataReader("Select count(*) From SC;");     //算科目数目
            reader1.Read();
            int count = reader1.GetInt32(0);
            string[] ans = new string[count];
            reader1.Close();
            reader1.Dispose();

            OleDbDataReader reader2 = MySQL.dataReader("Select Course From SC;");
            int i = 0;
            while (reader2.Read())
            {
                ans[i] = "AVG(" + reader2.GetString(0) + ") As " + reader2.GetString(0) + "平均分";
                i++;
            }
            reader2.Close();
            reader2.Dispose();
            StringBuilder sb = new StringBuilder();
            for (i = 0; i < count; i++)
            {
                sb.Append(ans[i]);
                if (i != count - 1)
                {
                    sb.Append(",");
                }
            }
            string all = sb.ToString();

            DataTable dt = new DataTable();
            dt = MySQL.dataTable("Select " + all + " From Student;");
            dgv.DataSource = dt;
        }
        /// <summary>
        /// 获得学生的学号并导出到txt
        /// </summary>
        public void GetXuehao()
        {
            OleDbDataReader reader = MySQL.dataReader("Select Count(*) From Student;");
            reader.Read();
            int count = reader.GetInt32(0);
            string[] xuehao = new string[count];
            reader.Close();
            reader.Dispose();

            OleDbDataReader reader2 = MySQL.dataReader("Select 学号 From Student;");
            int i = 0;
            while (reader2.Read())
            {
                xuehao[i++] = reader2.GetString(0);
            }
            reader2.Close();
            reader2.Dispose();

            if (File.Exists(System.Environment.CurrentDirectory + @"\Data\导出学号.txt"))
            {
                File.Delete(System.Environment.CurrentDirectory + @"\Data\导出学号.txt");
            }
            FileStream fs = new FileStream(System.Environment.CurrentDirectory + @"\Data\导出学号.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (i = 0; i < count; i++)
            {
                sw.WriteLine(xuehao[i]);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        public string GetCourse(string str)
        {
            OleDbDataReader reader = MySQL.dataReader("Select Course From Teacher Where Teacher = '" + str + "';");
            reader.Read();
            string gett = reader.GetString(0);
            reader.Close();
            reader.Dispose();
            return gett;
        }
        /// <summary>
        /// 检查科目是否存在
        /// </summary>
        /// <param name="str"></param>
        public bool CheckCourseExist(string str)
        {
            OleDbDataReader reader = MySQL.dataReader("Select Count(*) From SC Where Course = '" + str + "';");
            reader.Read();
            if (reader.GetInt32(0) == 0)
            {
                reader.Close();
                reader.Dispose();
                return false;
            }
            reader.Close();
            reader.Dispose();
            return true;
        }
        /// <summary>
        /// 检查学号是否存在
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckXuehaoExist(string str)
        {
            OleDbDataReader reader = MySQL.dataReader("Select Count(*) From Student Where 学号 = '" + str + "';");
            reader.Read();
            if (reader.GetInt32(0) == 0)
            {
                reader.Close();
                reader.Dispose();
                return false;
            }
            reader.Close();
            reader.Dispose();
            return true;
        }
        /// <summary>
        /// 检查姓名是否存在
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckNameExist(string str)
        {
            OleDbDataReader reader = MySQL.dataReader("Select Count(*) From Student Where 姓名 = '" + str + "';");
            reader.Read();
            if (reader.GetInt32(0) == 0)
            {
                reader.Close();
                reader.Dispose();
                return false;
            }
            reader.Close();
            reader.Dispose();
            return true;
        }
        /// <summary>
        /// 检查加权排名是否存在
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool CheckRankExist(string str)
        {
            OleDbDataReader reader = MySQL.dataReader("Select Count(*) From Student Where 加权排名 = " + str + ";");
            reader.Read();
            if (reader.GetInt32(0) == 0)
            {
                reader.Close();
                reader.Dispose();
                return false;
            }
            reader.Close();
            reader.Dispose();
            return true;
        }
    }
}
