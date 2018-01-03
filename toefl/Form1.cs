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
                    newButton.Location = new Point(41, 9 + 110 * row);
                }
                else if ((int)reader["id"] % 3 == 2) //265
                {
                    newButton.Location = new Point(265, 9 + 110 * row);
                }
                else //503
                {
                    newButton.Location = new Point(503, 9 + 110 * row);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            if (SystemConfig.name == "admin")
            {
                SystemConfig.authority = true;
                this.panel1.ContextMenuStrip = this.contextMenuStrip1;
            }
            else
                SystemConfig.authority = false;

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
                newButton.Location = new Point(41, 9 + 110 * row);
            }
            else if(tpoid % 3 == 2) //265
            {
                newButton.Location = new Point(265, 9 + 110 * row);
            }
            else //503
            {
                newButton.Location = new Point(503, 9 + 110 * row);
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
    }
}
