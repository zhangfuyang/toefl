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
        public write(int tpo)
        {
            this.tpo = tpo;
            InitializeComponent();
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

        private void write_Load(object sender, EventArgs e)
        {
            string sql = "select * from IndWritingProblem where setid = " + tpo.ToString();
            SqlDataReader reader = DatabaseHelp.getReader(sql);
            reader.Read();
            label1.Text = "题目:\n" + reader["stem"];
            reader.Close();

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
    }
}
