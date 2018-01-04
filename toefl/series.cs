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
    public partial class series : Form
    {
        private float X;
        private float Y;
        private int tpoNo;
        private int model;
        public series(int tpoNo,int model)
        {
            InitializeComponent();
            this.tpoNo = tpoNo;
            this.model = model;
            X = this.Width;
            Y = this.Height;
            this.Resize += new System.EventHandler(this.Form1_Resize);
           // this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
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

        private void reading_Click(object sender, EventArgs e)
        {
            int artnumber=Convert.ToInt32((sender as Button).Name.Replace("button", ""));
    
            string name = "reading" + artnumber.ToString();
            string sql = "select "+name+" from [dbo].[TestSet] where id=" + this.tpoNo.ToString();

            SqlDataReader reader = DatabaseHelp.getReader(sql);
            reader.Read();
            int x = DatabaseHelp.convert(1, reader[name]);
            reader.Close();

            reading read_form = new reading(1,x);
            read_form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            write write_form = new write(tpoNo);
            write_form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            write write_form = new write(tpoNo, 2);
            write_form.ShowDialog();
        }
    }
}
