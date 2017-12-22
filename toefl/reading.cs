using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public reading()
        {
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
    }
}
