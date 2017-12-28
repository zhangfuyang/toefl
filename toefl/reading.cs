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
        private article arti;
        private int nownum;
        private readingQuestion[] rq;
        private int model;
        private int artnumber;
        public reading(int model,int parm)
        {
            InitializeComponent();
            if (model == 1)//点tpo，点分项联系，点了一篇阅读，阅读的id是parm
            {
                this.model = model;
                this.artnumber = parm;
                this.arti = new article();
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
                this.nownum += 1;
                button3.Enabled = true;
                if (this.nownum == this.arti.questionnum-1)
                {
                    button4.Enabled = false;
                }
                load_left_ins();
                return;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.model == 1)
            {
                this.nownum -= 1;
                button4.Enabled = true;
                if (this.nownum == 1)
                {
                    button3.Enabled = false;
                }
                load_left_ins();
                return;
            }
        }

        private string ProString(string x)
        {
            int i;
            int length = x.Length;
            for (i = 0; 15 * i < length; i++)
            {
                x=x.Insert(i + 15 * (i + 1), "\n");
            }
            return x;
        }

        private void load_left_ins()
        {
            question_label.Text = this.rq[this.nownum].stem;
            if (this.rq[this.nownum].opnum == 4)
            {
                checkBox1.Text = "A:  "+this.rq[this.nownum].optionx[0];
                checkBox2.Text = "B:  " + this.rq[this.nownum].optionx[1];
                checkBox3.Text = "C:  " + this.rq[this.nownum].optionx[2];
                checkBox4.Text = "D:  " + this.rq[this.nownum].optionx[3];
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = false;
                checkBox6.Visible = false;

            }else if (this.rq[this.nownum].opnum == 6)
            {
                checkBox1.Text = "A:  " + this.rq[this.nownum].optionx[0];
                checkBox2.Text = "B:  " + this.rq[this.nownum].optionx[1];
                checkBox3.Text = "C:  " + this.rq[this.nownum].optionx[2];
                checkBox4.Text = "D:  " + this.rq[this.nownum].optionx[3];
                checkBox3.Text = "E:  " + this.rq[this.nownum].optionx[4];
                checkBox4.Text = "F:  " + this.rq[this.nownum].optionx[5];
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;
                checkBox6.Visible = true;
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
            for (int i= 0;i< this.arti.questionnum; i++)
            {
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

            this.webBrowser1.DocumentText = " <p>&nbsp;<span style='color: rgb(128, 128, 128); text-transform: none; text-indent: 0px; letter-spacing: normal; font-family: \"Helvetica Neue\", Helvetica, \"Hiragino Sans GB\", \"Microsoft YaHei\", Arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 400; word-spacing: 0px; float: none; display: inline !important; white-space: normal; orphans: 2; widows: 2; background-color: rgb(255, 255, 255); font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial;'>"
                 + "this is a line\nthis is another line"
                + " </ span ></ p >" + "<p>ananother line</ p>";
            button3.Enabled = false;
            this.nownum = 0;
            load_left_ins();
            this.Show();
        }


        private void reading_Load(object sender, EventArgs e)//对不同的模式，有不同的load函数
        {
            if (this.model == 1)
            {
                load4model1();
            }
        }
    }
}
