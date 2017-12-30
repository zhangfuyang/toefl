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
    public partial class ansList : Form
    {
        private reading rrd;
        public ansList(reading rd)
        {
            InitializeComponent();
            this.rrd = rd;
            double totalColumnWidth = 8.0;  //1.0+2.0+1.0 = 4.0

            //设置第一列所占百分比
            double colPercentage0 = 1 / totalColumnWidth;
            listView1.Columns[0].Width = (int)(colPercentage0 * listView1.ClientRectangle.Width);

            //设置第二列所占百分比
            double colPercentage1 = 5 / totalColumnWidth;
            listView1.Columns[1].Width = (int)(colPercentage1 * listView1.ClientRectangle.Width);

            //设置第三列所占百分比
            double colPercentage2 = 1 / totalColumnWidth;
            listView1.Columns[2].Width = (int)(colPercentage2 * listView1.ClientRectangle.Width);
        }

        private void ansList_Load(object sender, EventArgs e)
        {
            int i;
            if (this.rrd.model == 1)
            {
                for(i = 0; i < this.rrd.arti.questionnum; i++)
                {
                    listView1.Items.Add(new ListViewItem(new string[] {(i+1).ToString(),this.rrd.rq[i].stem,this.rrd.studentAnswers[i]==""?"-----": this.rrd.studentAnswers[i] }));
                }
            }
            else  if (this.rrd.model == 2)
            {
                int end=this.rrd.artis[0].questionnum;
                if (this.rrd.nowarti >= 1)
                {
                    end += this.rrd.artis[1].questionnum;
                }
                if (this.rrd.nowarti >= 2)
                {
                    end += this.rrd.artis[2].questionnum;
                }


                for ( i= 0; i < end; i++)
                {
                    listView1.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), this.rrd.rq[i].stem, this.rrd.studentAnswers[i] == "" ? "-----" : this.rrd.studentAnswers[i] }));
                }
                for (; i < this.rrd.allrqnum; i++)
                {
                    listView1.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), "-----", this.rrd.studentAnswers[i] == "" ? "-----" : this.rrd.studentAnswers[i] }));
                }

            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
