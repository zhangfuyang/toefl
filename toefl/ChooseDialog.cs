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
    public partial class ChooseDialog : Form
    {
        public ChooseDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //选择练习
        private void button2_Click(object sender, EventArgs e)
        {
            series Series_Form = new series();
            this.Hide();
            Series_Form.WindowState = FormWindowState.Maximized;
            Series_Form.ShowDialog();
            this.Close();
        }
    }
}
