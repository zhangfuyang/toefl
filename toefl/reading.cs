using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace toefl
{
    public partial class reading : Form
    {
        private float X;
        private float Y;
        //for model1
        public article arti;
        public int nownum;
        public readingQuestion[] rq;
        public string[] studentAnswers;
        public int model;
        public int artnumber;
        public TimeSpan leftTime;
        //for moedel2
        public int tponum;
        public article[] artis;
        public int []artinum=new int[3];
        public int allrqnum=0;
        public int nowarti;
        public int nowartinum = 0;
        public reading(int model,int parm)
        {
            InitializeComponent();
            if (model == 1)//点tpo，点分项联系，点了一篇阅读，阅读的id是parm
            {
                this.model = model;
                this.artnumber = parm;
                this.arti = new article();
            }
            else if (model == 2)//点tpo，点综合练习
            {
                this.model = model;
                this.tponum = parm;
                this.artis = new article[3];
            }
            
            X = this.Width;
            Y = this.Height;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            setTag(this);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

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

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.model == 1) {
                /*
                if (nownum == this.arti.questionnum - 1)
                {
                    //提交答案
                    return;
                }*/
                string ans="";
                ans += checkBox1.Checked ? "A" : "";
                ans += checkBox2.Checked ? "B" : "";
                ans += checkBox3.Checked ? "C" : "";
                ans += checkBox4.Checked ? "D" : "";
                ans += checkBox5.Checked ? "E" : "";
                ans += checkBox6.Checked ? "F" : "";
                this.studentAnswers[nownum] = ans;
                this.nownum += 1;
                button3.Enabled = true;
                if (this.nownum == this.arti.questionnum-1)
                {
                    button4.Enabled = false;
                }
                load_left_ins();
                return;
            }else if (this.model == 2)
            {
                string ans = "";
                ans += checkBox1.Checked ? "A" : "";
                ans += checkBox2.Checked ? "B" : "";
                ans += checkBox3.Checked ? "C" : "";
                ans += checkBox4.Checked ? "D" : "";
                ans += checkBox5.Checked ? "E" : "";
                ans += checkBox6.Checked ? "F" : "";
                this.studentAnswers[nownum] = ans;
                this.nownum += 1;
                this.nowartinum += 1;
                button3.Enabled = true;
                if (this.nowartinum == this.artis[nowarti].questionnum - 1)
                {
                    button4.Enabled = false;
                    
                }
                load_left_ins();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.model == 1)
            {
                string ans = "";
                ans += checkBox1.Checked ? "A" : "";
                ans += checkBox2.Checked ? "B" : "";
                ans += checkBox3.Checked ? "C" : "";
                ans += checkBox4.Checked ? "D" : "";
                ans += checkBox5.Checked ? "E" : "";
                ans += checkBox6.Checked ? "F" : "";
                this.studentAnswers[nownum] = ans;
                this.nownum -= 1;
                button4.Enabled=true;
                if (this.nownum == 0)
                {
                    button3.Enabled = false;
                }
                load_left_ins();
                return;
            }else if (this.model == 2)
            {
                string ans = "";
                ans += checkBox1.Checked ? "A" : "";
                ans += checkBox2.Checked ? "B" : "";
                ans += checkBox3.Checked ? "C" : "";
                ans += checkBox4.Checked ? "D" : "";
                ans += checkBox5.Checked ? "E" : "";
                ans += checkBox6.Checked ? "F" : "";
                this.studentAnswers[nownum] = ans;
                nowartinum -= 1;
                nownum -= 1;
                button4.Enabled = true;

                if (this.nowartinum == 0)//不能后退了
                {
                    button3.Enabled = false;
                }
                load_left_ins();
                return;

            }
        }

        private string ProString(string x,int ra,ref int line)
        {
            int i;
            int j;
            int length = x.Length;
            for (i = 0; ra * (i+1) < length; i++)
            {
                j = i + ra * (i + 1);
                while(x[j]!=' ' && x[j] != '.' && j<length-1)
                {
                    j++;
                }
                x =x.Insert(j, "\n");
            }
            line = i+1;
            return x;
        }

      

        private void load_left_ins4model1()
        {
            int line = 0;
            int loc = 12;
            question_label.Text =  ProString((nownum+1).ToString()+":  "+ this.rq[this.nownum].stem,35,ref line);
            question_label.Location =new Point(40,loc);
            loc += line * 30+10;
            if (this.rq[this.nownum].opnum == 4)
            {
                checkBox1.Text = "A:  " + ProString( this.rq[this.nownum].optionx[0],40,ref line);
                checkBox1.Location = new Point(40, loc);
                loc += line * 20+10;
                checkBox2.Text = "B:  " + ProString(this.rq[this.nownum].optionx[1],40,ref line);
                checkBox2.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox3.Text = "C:  " + ProString(this.rq[this.nownum].optionx[2],40,ref line);
                checkBox3.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox4.Text = "D:  " + ProString(this.rq[this.nownum].optionx[3],40,ref line);
                checkBox4.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = false;
                checkBox6.Visible = false;
                checkBox1.Checked = studentAnswers[this.nownum].Contains('A');
                checkBox2.Checked = studentAnswers[this.nownum].Contains('B');
                checkBox3.Checked = studentAnswers[this.nownum].Contains('C');
                checkBox4.Checked = studentAnswers[this.nownum].Contains('D');
                checkBox5.Checked = studentAnswers[this.nownum].Contains('E');
                checkBox6.Checked = studentAnswers[this.nownum].Contains('F');

            }
            else if (this.rq[this.nownum].opnum == 6)
            {
                checkBox1.Text = "A:  " + ProString(this.rq[this.nownum].optionx[0],40, ref line);
                checkBox1.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox2.Text = "B:  " + ProString(this.rq[this.nownum].optionx[1],40, ref line);
                checkBox2.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox3.Text = "C:  " + ProString(this.rq[this.nownum].optionx[2],40, ref line);
                checkBox3.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox4.Text = "D:  " + ProString(this.rq[this.nownum].optionx[3],40, ref line);
                checkBox4.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox5.Text = "E:  " + ProString(this.rq[this.nownum].optionx[4],40, ref line);
                checkBox5.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox6.Text = "F:  " + ProString(this.rq[this.nownum].optionx[5],40, ref line);
                checkBox6.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;
                checkBox6.Visible = true;
                checkBox1.Checked = studentAnswers[this.nownum].Contains('A');
                checkBox2.Checked = studentAnswers[this.nownum].Contains('B');
                checkBox3.Checked = studentAnswers[this.nownum].Contains('C');
                checkBox4.Checked = studentAnswers[this.nownum].Contains('D');
                checkBox5.Checked = studentAnswers[this.nownum].Contains('E');
                checkBox6.Checked = studentAnswers[this.nownum].Contains('F');
            }
        }

        private void load_left_ins4model2()
        {
            int line = 0;
            int loc = 12;
            question_label.Text = ProString((nownum + 1).ToString() + ":  " + this.rq[this.nownum].stem, 35, ref line);
            question_label.Location = new Point(40, loc);
            loc += line * 30 + 10;
            if (this.rq[this.nownum].opnum == 4)
            {
                checkBox1.Text = "A:  " + ProString(this.rq[this.nownum].optionx[0], 40, ref line);
                checkBox1.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox2.Text = "B:  " + ProString(this.rq[this.nownum].optionx[1], 40, ref line);
                checkBox2.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox3.Text = "C:  " + ProString(this.rq[this.nownum].optionx[2], 40, ref line);
                checkBox3.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox4.Text = "D:  " + ProString(this.rq[this.nownum].optionx[3], 40, ref line);
                checkBox4.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = false;
                checkBox6.Visible = false;
                checkBox1.Checked = studentAnswers[this.nownum].Contains('A');
                checkBox2.Checked = studentAnswers[this.nownum].Contains('B');
                checkBox3.Checked = studentAnswers[this.nownum].Contains('C');
                checkBox4.Checked = studentAnswers[this.nownum].Contains('D');
                checkBox5.Checked = studentAnswers[this.nownum].Contains('E');
                checkBox6.Checked = studentAnswers[this.nownum].Contains('F');

            }
            else if (this.rq[this.nownum].opnum == 6)
            {
                checkBox1.Text = "A:  " + ProString(this.rq[this.nownum].optionx[0], 40, ref line);
                checkBox1.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox2.Text = "B:  " + ProString(this.rq[this.nownum].optionx[1], 40, ref line);
                checkBox2.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox3.Text = "C:  " + ProString(this.rq[this.nownum].optionx[2], 40, ref line);
                checkBox3.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox4.Text = "D:  " + ProString(this.rq[this.nownum].optionx[3], 40, ref line);
                checkBox4.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox5.Text = "E:  " + ProString(this.rq[this.nownum].optionx[4], 40, ref line);
                checkBox5.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox6.Text = "F:  " + ProString(this.rq[this.nownum].optionx[5], 40, ref line);
                checkBox6.Location = new Point(40, loc);
                loc += line * 20 + 10;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;
                checkBox6.Visible = true;
                checkBox1.Checked = studentAnswers[this.nownum].Contains('A');
                checkBox2.Checked = studentAnswers[this.nownum].Contains('B');
                checkBox3.Checked = studentAnswers[this.nownum].Contains('C');
                checkBox4.Checked = studentAnswers[this.nownum].Contains('D');
                checkBox5.Checked = studentAnswers[this.nownum].Contains('E');
                checkBox6.Checked = studentAnswers[this.nownum].Contains('F');
            }
        }

        private void load_left_ins()
        {
            if (this.model == 1)
            {
                load_left_ins4model1();
            }
            else if (this.model == 2)
            {
                load_left_ins4model2();
            }
        }

        private void load4model1()
        {
            this.Hide();//先藏起来，悄咪咪的load数据
            string sql = "SELECT * FROM [dbo].[ReadingArticle] WHERE id = " + this.artnumber.ToString();

            int result = DatabaseHelp.SelectNum(sql);
            if (result == 0)
            {
                MessageBox.Show("本篇阅读尚未导入，敬请期待！");
                DialogResult = DialogResult.Cancel;
                this.Show();
                return;
            }
            if (result != 0)//把数据存起来
            {
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                reader.Read();
                this.arti.title = DatabaseHelp.convert(this.arti.title, reader["title"]);
                this.arti.content = DatabaseHelp.convert(this.arti.content, reader["article"]);
                this.arti.questionnum = DatabaseHelp.convert(this.arti.questionnum, reader["questionno"]);
                this.arti.questionIds = new int[this.arti.questionnum];
                for (int i = 0; i < this.arti.questionnum; i++)
                {
                    this.arti.questionIds[i] = DatabaseHelp.convert(this.arti.questionIds[i], reader["questionid" + (i + 1).ToString()]);
                }
                this.arti.average = DatabaseHelp.convert(this.arti.average, reader["average"]);
                reader.Close();
            }
            this.rq = new readingQuestion[this.arti.questionnum];
            this.studentAnswers = new string[this.arti.questionnum];
            for (int i= 0;i< this.arti.questionnum; i++)
            {
                studentAnswers[i] = "";
                sql = "SELECT * FROM [dbo].[ReadingQuestion] WHERE id="+this.arti.questionIds[i];
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                reader.Read();
                this.rq[i] = new readingQuestion();
                this.rq[i].id = this.arti.questionIds[i];
                this.rq[i].articleid = DatabaseHelp.convert(this.rq[i].articleid,reader["articleid"]);

                this.rq[i].num = DatabaseHelp.convert(this.rq[i].num, reader["num"]);
                this.rq[i].type = DatabaseHelp.convert(this.rq[i].type, reader["type"]);
                this.rq[i].stem = DatabaseHelp.convert(this.rq[i].stem, reader["stem"]);
                this.rq[i].opnum= DatabaseHelp.convert(this.rq[i].opnum, reader["opnum"]);
                this.rq[i].paragraph = DatabaseHelp.convert(this.rq[i].paragraph, reader["paragraph"]);
                this.rq[i].paragraph2 = DatabaseHelp.convert(this.rq[i].paragraph2, reader["paragraph2"]);
                this.rq[i].optionx = new string[this.rq[i].opnum];
                for(int j=0;j< this.rq[i].opnum; j++)
                {
                    this.rq[i].optionx[j]= DatabaseHelp.convert(this.rq[i].optionx[j], reader["option"+(j+1).ToString()]);
                }
                this.rq[i].ans= DatabaseHelp.convert(this.rq[i].ans, reader["ans"]);
                this.rq[i].acc = DatabaseHelp.convert(this.rq[i].acc, reader["acc"]);
                this.rq[i].analysis = DatabaseHelp.convert(this.rq[i].analysis, reader["analysis"]);
                reader.Close();
            }
            
            this.webBrowser1.DocumentText = "<span style='color: rgb(128, 128, 128); text-transform: none; text-indent: 0px; letter-spacing: normal; font-family: \"Helvetica Neue\", Helvetica, \"Hiragino Sans GB\", \"Microsoft YaHei\", Arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 400; word-spacing: 0px; float: none; display: inline !important; white-space: normal; orphans: 2; widows: 2; background-color: rgb(255, 255, 255); font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial;'>"
                 + "<p align=\"center\">" + arti.title + "</p>" + "<p>" + arti.content.Replace("\r\n", "</p><p>").Replace("\n", "</p><p>") + "</p></span>";
            button3.Enabled = false;
            this.nownum = 0;
            load_left_ins();
            this.timer1.Enabled = true;
            this.leftTime=TimeSpan.Parse("0:20:0");
            this.label1.Text = this.leftTime.ToString();
            this.Show();
        }

        private void load4model2()
        {
            int i, j, k;
            this.Hide();//先藏起来，悄咪咪的load数据

            string sql = "SELECT [id],[reading1],[reading2],[reading3] FROM[dbo].[TestSet] WHERE id=" + this.tponum.ToString();
            int result = DatabaseHelp.SelectNum(sql);
            if (result == 0)
            {
                MessageBox.Show("本套tpo尚未导入，敬请期待");
                DialogResult = DialogResult.Cancel;
                this.Show();
                return;
            }
            //找到了这套题的信息，录入阅读文章号。

            SqlDataReader reader = DatabaseHelp.getReader(sql);
            reader.Read();
            this.artinum[0] = DatabaseHelp.convert(this.artinum[0], reader["reading1"]);
            this.artinum[1] = DatabaseHelp.convert(this.artinum[1], reader["reading2"]);
            this.artinum[2] = DatabaseHelp.convert(this.artinum[2], reader["reading3"]);
            reader.Close();

            for(i = 0; i < 3; i++)//分别录入每篇文章的信息到artis
            {
                sql = "SELECT * FROM [dbo].[ReadingArticle] WHERE id = " + this.artinum[i].ToString();
                reader = DatabaseHelp.getReader(sql);
                reader.Read();
                this.artis[i] = new article();
                this.artis[i].title = DatabaseHelp.convert(this.artis[i].title, reader["title"]);
                this.artis[i].content = DatabaseHelp.convert(this.artis[i].content, reader["article"]);
                this.artis[i].questionnum = DatabaseHelp.convert(this.artis[i].questionnum, reader["questionno"]);
                this.artis[i].questionIds = new int[this.artis[i].questionnum];
                for (j = 0; j < this.artis[i].questionnum; j++)
                {
                    this.artis[i].questionIds[j] = DatabaseHelp.convert(this.artis[i].questionIds[j], reader["questionid" + (j+ 1).ToString()]);
                }
                this.artis[i].average = DatabaseHelp.convert(this.artis[i].average, reader["average"]);
                reader.Close();
            }
            this.allrqnum = this.artis[0].questionnum + this.artis[1].questionnum + this.artis[2].questionnum;
            this.rq = new readingQuestion[this.allrqnum];
            this.studentAnswers = new string[this.allrqnum];
            i = 0;
            for(k = 0; k < 3; k++)
            {
                for(j = 0; j < this.artis[k].questionnum; j++)
                {
                    studentAnswers[i] = "";
                    sql = "SELECT * FROM [dbo].[ReadingQuestion] WHERE id="+this.artis[k].questionIds[j];
                    reader = DatabaseHelp.getReader(sql);
                    reader.Read();
                    this.rq[i] = new readingQuestion();
                    this.rq[i].id = this.artis[k].questionIds[j];
                    this.rq[i].articleid = DatabaseHelp.convert(this.rq[i].articleid, reader["articleid"]);

                    this.rq[i].num = DatabaseHelp.convert(this.rq[i].num, reader["num"]);
                    this.rq[i].type = DatabaseHelp.convert(this.rq[i].type, reader["type"]);
                    this.rq[i].stem = DatabaseHelp.convert(this.rq[i].stem, reader["stem"]);
                    this.rq[i].opnum = DatabaseHelp.convert(this.rq[i].opnum, reader["opnum"]);
                    this.rq[i].paragraph = DatabaseHelp.convert(this.rq[i].paragraph, reader["paragraph"]);
                    this.rq[i].paragraph2 = DatabaseHelp.convert(this.rq[i].paragraph2, reader["paragraph2"]);
                    this.rq[i].optionx = new string[this.rq[i].opnum];
                    for (int j1 = 0; j1 < this.rq[i].opnum; j1++)
                    {
                        this.rq[i].optionx[j1] = DatabaseHelp.convert(this.rq[i].optionx[j1], reader["option" + (j1 + 1).ToString()]);
                    }
                    this.rq[i].ans = DatabaseHelp.convert(this.rq[i].ans, reader["ans"]);
                    this.rq[i].acc = DatabaseHelp.convert(this.rq[i].acc, reader["acc"]);
                    this.rq[i].analysis = DatabaseHelp.convert(this.rq[i].analysis, reader["analysis"]);
                    reader.Close();
                    i++;
                }
            }

            //load第一篇文章。
            
            button3.Enabled = false;
            this.nownum = 0;
            this.nowarti = 0;
            this.nowartinum = 0;
            this.webBrowser1.DocumentText = "<span style='color: rgb(128, 128, 128); text-transform: none; text-indent: 0px; letter-spacing: normal; font-family: \"Helvetica Neue\", Helvetica, \"Hiragino Sans GB\", \"Microsoft YaHei\", Arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 400; word-spacing: 0px; float: none; display: inline !important; white-space: normal; orphans: 2; widows: 2; background-color: rgb(255, 255, 255); font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial;'>"
                 + "<p align=\"center\">" + artis[nowarti].title + "</p>" + "<p>" + artis[nowarti].content.Replace("\r\n", "</p><p>").Replace("\n", "</p><p>") + "</p></span>";
            load_left_ins();
            this.timer1.Enabled = true;
            this.leftTime = TimeSpan.Parse("1:0:0");
            this.label1.Text = this.leftTime.ToString();
            this.Show();
        }

        private void reading_Load(object sender, EventArgs e)//对不同的模式，有不同的load函数
        {
            if (this.model == 1)
            {
                load4model1();
            }else if (this.model == 2)
            {
                load4model2();
            }
            
        }
        private void submit()
        {
            if(model == 1) //单项
            {
                string sql;
                int result;
                DateTime now = DateTime.Now;
                for (int i = 0; i < arti.questionnum; i++)
                {
                    sql = "insert into ReadingAns values('" + SystemConfig.name + "', '" + now.ToString() + "', " + rq[i].id + ", '" + studentAnswers[i] + "')";
                    try {
                        result = DatabaseHelp.executeCommand(sql);
                    }
                    catch
                    {
                        MessageBox.Show("保存失败!");
                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan t = TimeSpan.Parse("0:0:1");
            if (this.leftTime == TimeSpan.Parse("0:0:0"))
            {
                //强制提交
                if (this.model == 1)
                {
                    this.Hide();
                    this.timer1.Enabled = false;
                    score sc = new score(this);
                    this.Hide();
                    sc.WindowState = FormWindowState.Maximized;
                    sc.ShowDialog();
                    DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }else if (this.model == 2)
                {
                    //提交并关闭


                    //转综合写作
                    write wrt1 = new write(tponum, 2);
                    write wrt2 = new write(tponum, 1);
                    wrt1.ShowDialog();
                    wrt2.ShowDialog();
                    //给结果
                    score sc = new score(this);
                    this.Hide();
                    sc.WindowState = FormWindowState.Maximized;
                    sc.ShowDialog();
                    this.Close();
                    DialogResult = DialogResult.OK;
                    return;
                }
            }
            if (this.button2.Text != "暂停")
            {
                return;
            }
            this.leftTime = this.leftTime.Subtract(t);
            this.label1.Text = this.leftTime.ToString();
            if (this.leftTime <= TimeSpan.Parse("0:0:30"))
            {
                this.label1.ForeColor = Color.Red;
                if (this.leftTime == TimeSpan.Parse("0:0:30"))
                {
                    this.label1.Visible = true;
                    this.button1.Text = "隐藏时间";
                }
            }
            else
            {
                this.label1.ForeColor = Color.Black;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "暂停")
            {
                button2.Text = "继续";
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                button2.Text = "暂停";
                if (this.model == 1)
                {
                    button3.Enabled = this.nownum == 0 ? false : true;
                    button4.Enabled = this.nownum == this.arti.questionnum - 1 ? false : true;
                }else if (this.model == 2)
                {
                    button3.Enabled = this.nowartinum == 0 ? false : true;
                    button4.Enabled = this.nowartinum == this.artis[this.nowarti].questionnum - 1 ? false : true;
                }
                button5.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "隐藏时间")
            {
                button1.Text = "显示时间";
                label1.Visible = false;
            }
            else
            {
                button1.Text = "隐藏时间";
                label1.Visible = true;
            }
                
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //查看目前答题情况。
            if (this.model == 1)
            {
                string ans = "";
                ans += checkBox1.Checked ? "A" : "";
                ans += checkBox2.Checked ? "B" : "";
                ans += checkBox3.Checked ? "C" : "";
                ans += checkBox4.Checked ? "D" : "";
                ans += checkBox5.Checked ? "E" : "";
                ans += checkBox6.Checked ? "F" : "";
                this.studentAnswers[nownum] = ans;
                ansList al = new ansList(this);
                al.ShowDialog();
            }
            else if (this.model == 2)
            {
                string ans = "";
                ans += checkBox1.Checked ? "A" : "";
                ans += checkBox2.Checked ? "B" : "";
                ans += checkBox3.Checked ? "C" : "";
                ans += checkBox4.Checked ? "D" : "";
                ans += checkBox5.Checked ? "E" : "";
                ans += checkBox6.Checked ? "F" : "";
                this.studentAnswers[nownum] = ans;
                ansList al = new ansList(this);
                al.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(this.model == 1){

                //提交函数
                //保存答题结果！！！！
                submit();
                score sc = new score(this);
                this.Hide();
                sc.WindowState = FormWindowState.Maximized;
                sc.ShowDialog();
                

                DialogResult = DialogResult.OK;
            }
            else if (this.model==2)
            {
                if (this.nowarti <= 1)
                {
                    this.nowarti += 1;
                    this.webBrowser1.DocumentText = "<span style='color: rgb(128, 128, 128); text-transform: none; text-indent: 0px; letter-spacing: normal; font-family: \"Helvetica Neue\", Helvetica, \"Hiragino Sans GB\", \"Microsoft YaHei\", Arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 400; word-spacing: 0px; float: none; display: inline !important; white-space: normal; orphans: 2; widows: 2; background-color: rgb(255, 255, 255); font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial;'>"
                        + "<p align=\"center\">" + artis[nowarti].title + "</p>" + "<p>" + artis[nowarti].content.Replace("\r\n", "</p><p>").Replace("\n", "</p><p>") + "</p></span>";
                    this.nowartinum = 0;
                    if (this.nowarti == 1)
                    {
                        this.nownum = this.artis[0].questionnum;
                        button3.Enabled = false;
                        button4.Enabled = true;
                    }else if (this.nowarti == 2)
                    {
                        this.nownum = this.artis[0].questionnum + this.artis[1].questionnum;
                        button3.Enabled = false;
                        button4.Enabled = true;
                    }
                    load_left_ins();
                }
                else
                {
                    //提交并关闭


                    //转综合写作
                    write wrt1 = new write(tponum,2);
                    write wrt2 = new write(tponum,1);
                    wrt1.ShowDialog();
                    wrt2.ShowDialog();
                    //给结果
                    score sc = new score(this);
                    this.Hide();
                    sc.WindowState = FormWindowState.Maximized;
                    sc.ShowDialog();
                    DialogResult = DialogResult.OK;
                }
            }

        }
    }
}
