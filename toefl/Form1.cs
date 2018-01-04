using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace toefl
{
    public partial class Form1 : Form
    {
        private float X;
        private float Y;
        private int []errid;
        public Form1()
        {
            InitializeComponent();
            X = this.Width;
            Y = this.Height;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            setTag(this);
            setupTPO();
            
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            login login_Form = new login();

            DialogResult result = login_Form.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                System.Environment.Exit(0);
            }
            setup();
        }

        private void setupTPO()
        {
            string sql;
            sql = "select id from [dbo].Testset";
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            
            Button newButton;
            while(reader.Read())
            {
                newButton = new Button();
                panel1.Controls.Add(newButton);
                newButton.Name = "tpo_button" + reader["id"];
                int row = ((int)reader["id"] - 1) / 3;
                if ((int)reader["id"] % 3 == 1) //第一列 41
                {
                    newButton.Location = new Point(30, 9 + 110 * row);
                }
                else if ((int)reader["id"] % 3 == 2) //265
                {
                    newButton.Location = new Point(220, 9 + 110 * row);
                }
                else //503
                {
                    newButton.Location = new Point(410, 9 + 110 * row);
                }
                newButton.Margin = new Padding(2, 2, 2, 2);
                newButton.Size = new Size(146, 61);
                newButton.Text = "tpo" + reader["id"];
                newButton.UseVisualStyleBackColor = true;
                newButton.Click += new System.EventHandler(this.tpo_Click);

                newButton.Tag = newButton.Width + ":" + newButton.Height + ":" + newButton.Left + ":" + newButton.Top + ":" + newButton.Font.Size;
            }
            reader.Close();
        }
        private int Timer()
        {
            DateTime t;
            t = DateTime.Now;
            return t.Hour * 60 + t.Minute;
        }
        private void setup()
        {
            double time = SystemConfig.time;
            label1.Text = "你好! " + SystemConfig.name;
            study_label.Text = "    累计学习时间:" + ((int)time).ToString() + "h";
            if((int)((time - (int)time) * 60) != 0)
               study_label.Text += ((int)((time - (int)time) * 60)).ToString() + "m";
            right_label.Text = "      平均正确率:" + SystemConfig.acc.ToString("f2");
            count_label.Text = "累计练习题目数量:" + SystemConfig.question_num.ToString();
            SystemConfig.start_time = Timer();
            if (SystemConfig.name == "admin")
            {
                label5.Text = "意见反馈列表";
                listBox1.Items.Clear();
                string sql = "select * from (select *,ROW_NUMBER() over(order by date) as num from UserSuggestion) as x where num < 15";
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                while(reader.Read())
                {
                    listBox1.Items.Add(reader["name"].ToString().Trim() + ":" + reader["title"]);
                }
                reader.Close();
            }
            else
            {
                listBox1.Items.Clear();
                string sql = "select * from ReadingQuestion inner join (select id,ReadingAns.date from ReadingAns where (name = '";
                sql += SystemConfig.name + "'and ReadingAns.date in (select max(date) as date from ReadingAns where name = '";
                sql += SystemConfig.name;
                sql += "' and correct = 0 group by id))) as xx on ReadingQuestion.id = xx.id order by date desc";
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                errid = new int[15];
                for (int i = 0; i < 15; i++)
                {
                    if (!reader.Read())
                        break;
                    listBox1.Items.Add(reader["type"] + ":" + reader["stem"]);
                    errid[i] = Convert.ToInt32(reader["id"]);
                }
                reader.Close();
            }
        }

        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }

        void Form1_Resize(object sender, EventArgs e)
        {
            // throw new Exception("The method or operation is not implemented.");  
            float newx = (this.Width) / X;
            //  float newy = (this.Height - this.statusStrip1.Height) / (Y - y);  
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            //this.Text = this.Width.ToString() +" "+ this.Height.ToString();  

        }

        private void tpo_Click(object sender, EventArgs e)
        {
            int tpoNo = Convert.ToInt32((sender as Button).Name.Replace("tpo_button",""));
            ChooseDialog Dialog_form = new ChooseDialog(tpoNo);
           // this.Hide();
            Dialog_form.ShowDialog();
            //this.Show();
            updateAccNum();
        }

        private void updateAccNum()
        {
            string sql;
            sql = "select * from Users where name = '" + SystemConfig.name + "'";
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            reader.Read();
            SystemConfig.acc = DatabaseHelp.convert(SystemConfig.acc, reader["acc"]);
            SystemConfig.question_num = DatabaseHelp.convert(SystemConfig.question_num, reader["question_num"]);
            reader.Close();
            right_label.Text = "      平均正确率:" + SystemConfig.acc.ToString("f2");
            count_label.Text = "累计练习题目数量:" + SystemConfig.question_num.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult closeWindowsBox = MessageBox.Show("你舍得离我而去吗???", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(closeWindowsBox == DialogResult.Yes)
            {
                //写入数据库
                SystemConfig.end_time = Timer();
                double updateTime = SystemConfig.end_time - SystemConfig.start_time;
                updateTime = SystemConfig.time + updateTime / 60;
                string sql = "update Users set time = " + updateTime + "where name = '" + SystemConfig.name + "'";
                DatabaseHelp.executeCommand(sql);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index == System.Windows.Forms.ListBox.NoMatches)
            {
                return;
            }
            if (SystemConfig.name == "admin")
            {
                string[] item;
                item = listBox1.Items[index].ToString().Split(':');
                string name = item[0];
                string title="";
                for(int i = 1; i < item.Length; i++)
                {
                    title += item[i];
                }
                string sql = "select * from UserSuggestion where name = '" + name + "' and title = '" + title + "'";
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                while (reader.Read())
                {
                    feedbackAdmin fd = new feedbackAdmin(reader["title"].ToString(), reader["name"].ToString(), reader["suggest"].ToString(), reader["date"].ToString());
                    DialogResult result = fd.ShowDialog();
                    if (result == DialogResult.No)
                    {
                        reader.Close();
                        return;
                    }
                }
                reader.Close();
                sql = "delete from UserSuggestion where name = '" + name + "' and title = '" + title + "'";
                DatabaseHelp.executeCommand(sql);
                setup();
            }
            else
            {
                //链接到reading界面
                reading rd = new reading(3, errid[index]);
                rd.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (SystemConfig.name == "admin")
            {
                SystemConfig.authority = true;
                this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            }
            else
            {
                SystemConfig.authority = false;
            }

            string sql = "SELECT [subject] FROM[dbo].[ReadingArticleSubject]";
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["subject"]);
            }
            reader.Close();

            sql = "SELECT [subject]  FROM[dbo].[ComWritingSubject]";
            reader = DatabaseHelp.getReader(sql);
            while (reader.Read())
            {
                comboBox3.Items.Add(reader["subject"]);
            }
            reader.Close();

            sql = "SELECT [subject]  FROM[dbo].[IndWritingSubject]";
            reader = DatabaseHelp.getReader(sql);
            while (reader.Read())
            {
                comboBox4.Items.Add(reader["subject"]);
            }
            reader.Close();
        }

        private void addtpoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addTpo addTpo_form = new addTpo();
            this.Hide();
            addTpo_form.TransfEvent += frm_TransEvent;
            addTpo_form.ShowDialog();
            this.Show();
        }

        private void frm_TransEvent(string value)
        {
            int tpoid = Convert.ToInt32(value);
            Button newButton;
            newButton = new Button();
            this.panel1.Controls.Add(newButton);
            newButton.Name = "tpo_button" + value;
            newButton.Text = "tpo" + value;
            newButton.Margin = new Padding(2, 2, 2, 2);
            newButton.Size = new Size(146, 61);

            int row = (tpoid - 1) / 3;
            if(tpoid % 3 == 1) //第一列 41
            {
                newButton.Location = new Point(30, 9 + 110 * row);
            }
            else if(tpoid % 3 == 2) //230
            {
                newButton.Location = new Point(220, 9 + 110 * row);
            }
            else //503
            {
                newButton.Location = new Point(410, 9 + 110 * row);
            }
            newButton.UseVisualStyleBackColor = true;
            newButton.Click += new System.EventHandler(this.tpo_Click);
            newButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            newButton.Tag = newButton.Width + ":" + newButton.Height + ":" + newButton.Left + ":" + newButton.Top + ":" + newButton.Font.Size;
            
            Form1_Resize(this, null);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            peer_check pc = new peer_check();
            pc.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            peer_check pc = new peer_check(2);
            pc.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            feedback bk = new feedback();
            bk.ShowDialog();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)//综合写作
        {
            
            string sql = "SELECT * FROM[dbo].[ComWritingProblem] WHERE subject='"+comboBox3.SelectedItem+"'";
            listView3.Items.Clear();
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            int i = 0;
            while (reader.Read())
            {
                
                listView3.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), DatabaseHelp.convert("", reader["setid"].ToString()) }));
                i++;
            }
            reader.Close();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM[dbo].[IndWritingProblem] WHERE subject='" + comboBox4.SelectedItem + "'";
            listView4.Items.Clear();
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            int i = 0;
            while (reader.Read())
            {

                listView4.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), DatabaseHelp.convert("", reader["setid"].ToString()) }));
                i++;
            }
            reader.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM[dbo].[ReadingArticle] WHERE subject='" + comboBox1.SelectedItem + "'";
            listView1.Items.Clear();
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            int i = 0;
            while (reader.Read())
            {

                listView1.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), DatabaseHelp.convert("", reader["setid"].ToString()), DatabaseHelp.convert("", reader["title"])}));
                i++;
            }
            reader.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM[dbo].[ReadingQuestion] WHERE type='" + comboBox2.SelectedItem + "'";
            listView2.Items.Clear();
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            int i = 0;
            while (reader.Read())
            {

                listView2.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), DatabaseHelp.convert("", reader["num"].ToString()), DatabaseHelp.convert("", reader["stem"]) ,reader["id"].ToString()}));
                i++;
            }
            reader.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
                return;
            string title=listView1.SelectedItems[0].SubItems[2].Text;
            string sql = "select [id] from [dbo].[ReadingArticle] where title='"+title+"'";
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            reader.Read();
            int x = DatabaseHelp.convert(1,reader["id"]);
            reader.Close();
            reading rd = new reading(1,x);
            rd.ShowDialog();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 1)
                return;
            int num = Convert.ToInt32( listView2.SelectedItems[0].SubItems[3].Text);
            reading rd = new reading(3, num);
            rd.ShowDialog();
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView4.SelectedItems.Count != 1)
                return;
            write wrt = new write(2,Convert.ToInt32(listView4.SelectedItems[0].SubItems[1].Text));
            wrt.ShowDialog();
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count != 1)
                return;
            write wrt = new write(1, Convert.ToInt32(listView3.SelectedItems[0].SubItems[1].Text));
            wrt.ShowDialog();
        }
    }
}
