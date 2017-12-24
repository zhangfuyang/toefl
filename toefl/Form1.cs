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
        public Form1()
        {
            InitializeComponent();
            X = this.Width;
            Y = this.Height;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            setTag(this);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            login login_Form = new login();

            DialogResult result = login_Form.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                System.Environment.Exit(0);
            }
            setup();
        }

        private void setup()
        {
            label1.Text = "你好! " + SystemConfig.name;
            study_label.Text = "    累计学习时间:" + SystemConfig.time.ToString() + "h";
            right_label.Text = "      平均正确率:" + SystemConfig.acc.ToString();
            count_label.Text = "累计练习题目数量:" + SystemConfig.question_num.ToString();

            listBox1.Items.Clear();
            string sql = "select * from ReadingQuestion inner join (select id,ReadingAns.date from ReadingAns where (name = '";
            sql+= SystemConfig.name + "'and ReadingAns.date in (select max(date) as date from ReadingAns where name = '";
            sql += SystemConfig.name;
            sql += "' and correct = 0 group by id))) as xx on ReadingQuestion.id = xx.id order by date desc";
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            for(int i = 0; i < 15; i++)
            {
                if (!reader.Read())
                    break;
                listBox1.Items.Add(reader["type"] + ":" + reader["stem"]);
            }
            reader.Close();
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
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult closeWindowsBox = MessageBox.Show("是否保存本次做题情况到数据库", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(closeWindowsBox == DialogResult.Yes)
            {
                //写入数据库

            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index == System.Windows.Forms.ListBox.NoMatches)
            {
                return;
            }
            //链接到reading界面
        }
    }
}
