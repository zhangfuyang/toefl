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
    public partial class write : Form
    {
        private float X;
        private float Y;
        private int tpo;
        private int model;
        public write(int tpo)
        {
            this.tpo = tpo;
            InitializeComponent();
            X = this.Width;
            Y = this.Height;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            setTag(this);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.model = 1;//默认是独立写作
        }
        public write(int tpo,int model)//重载一个构造函数，可以选择model2综合写作
        {
            this.tpo = tpo;
            InitializeComponent();
            X = this.Width;
            Y = this.Height;
            this.Resize += new System.EventHandler(this.Form1_Resize);
            setTag(this);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.model = model;
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

        private string ProString(string x, int ra)
        {
            int i;
            int j;
            int length = x.Length;
            for (i = 0; ra * (i + 1) < length; i++)
            {
                j = i + ra * (i + 1);
                while (x[j] != ' ' && x[j] != '.' && j < length - 1)
                {
                    j++;
                }
                x = x.Insert(j, "\n");
            }
            return x;
        }


        private void write_Load(object sender, EventArgs e)
        {
            if (this.model == 1)
            {
                string sql = "select * from IndWritingProblem where setid = " + tpo.ToString();
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                reader.Read();
                label1.Text = ProString("题目:\n" + reader["stem"],25);
                reader.Close();
            }else if (this.model == 2)
            {
                string sql = "select * from ComWritingProblem where setid = " + tpo.ToString();
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                reader.Read();
                label1.Text = ProString("题目:\n" + reader["stem"],25)
                    +ProString("\n\n阅读材料:\n\n" + reader["redmaterial"] , 25)
                    +ProString("\n\n听力材料:\n\n" + reader["lismaterial"], 25);
                reader.Close();
            }

        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V))
            {
                base.OnKeyDown(e);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Equals(""))
                return;
            Clipboard.SetDataObject(richTextBox1.SelectedText, false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
                richTextBox1.Cut();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                richTextBox1.Paste();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "Word Count: " + countWord().ToString();
        }

        private int countWord()
        {
            return richTextBox1.Text.Split(new Char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.model == 1)
            {
                DialogResult submitWindowsBox = MessageBox.Show("是否确定提交", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (submitWindowsBox == DialogResult.Yes)
                {
                    //写入数据库
                    string sql = "insert into IndWritingAns (name, date, id, ans) values('" + SystemConfig.name + "', '" + DateTime.Now.ToString() + "', " + tpo.ToString() + ", '" + richTextBox1.Text.Replace("'", "''") + "')";
                    if (DatabaseHelp.executeCommand(sql) == 0)
                        MessageBox.Show("保存出现问题，请联系管理员!");
                    else
                        MessageBox.Show("保存成功!");
                    DialogResult = DialogResult.OK;
                }
            }else if (this.model == 2)
            {
                DialogResult submitWindowsBox = MessageBox.Show("是否确定提交", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (submitWindowsBox == DialogResult.Yes)
                {
                    //写入数据库
                    string sql = "insert into ComWritingAns (name, date, id, ans) values('" + SystemConfig.name + "', '" + DateTime.Now.ToString() + "', " + tpo.ToString() + ", '" + richTextBox1.Text.Replace("'", "''") + "')";
                    if (DatabaseHelp.executeCommand(sql) == 0)
                        MessageBox.Show("保存出现问题，请联系管理员!");
                    else
                        MessageBox.Show("保存成功!");
                    DialogResult = DialogResult.OK;
                }
            }
            //xxx.ShowDialog();
        }
    }
}
