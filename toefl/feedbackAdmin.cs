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
    public partial class feedbackAdmin : Form
    {
        private bool x=false;
        public feedbackAdmin(string title, string name, string suggest, string date)
        {
            InitializeComponent();

            textBox1.Text = title;
            textBox2.Text = name;
            textBox3.Text = date;
            richTextBox1.Text = suggest;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            x = true;
            this.Close();
        }

        private void feedbackAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (x)
                DialogResult = DialogResult.Yes;
            else
                DialogResult = DialogResult.No;
        }
    }
}
