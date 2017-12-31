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
    public partial class score : Form
    {
        private reading rd;
        public score(reading rrd)
        {
            InitializeComponent();
            this.rd = rrd;
        }

        private void score_Load(object sender, EventArgs e)
        {
            int i;
            if (this.rd.model == 1)
            {
                for (i = 0; i < this.rd.arti.questionnum; i++)
                {
                    listView1.Items.Add(new ListViewItem(new string[] 
                    { (i + 1).ToString(),
                        this.rd.studentAnswers[i]==""?"-----":this.rd.studentAnswers[i],
                        this.rd.rq[i].ans }));
                }
                richTextBox1.Text = this.rd.rq[0].analysis;
                //load文章
            }else if (this.rd.model == 2)
            {
                for (i = 0; i < this.rd.allrqnum; i++)
                {
                    listView1.Items.Add(new ListViewItem(new string[]
                    { (i + 1).ToString(),
                        this.rd.studentAnswers[i]==""?"-----":this.rd.studentAnswers[i],
                        this.rd.rq[i].ans }));
                }
                richTextBox1.Text = this.rd.rq[0].analysis;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rd.model == 1)
            {
                if (listView1.SelectedItems.Count != 1)
                    return;
                int num = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                richTextBox1.Text = this.rd.rq[num - 1].analysis;
            }else if (this.rd.model == 2)
            {
                if (listView1.SelectedItems.Count != 1)
                    return;
                int num = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                richTextBox1.Text = this.rd.rq[num - 1].analysis;
                
                //文章号：this.rd.rq[num - 1].articleid;
            }
        }
    }

}
