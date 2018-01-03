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
                this.webBrowser1.DocumentText = "<span style='color: rgb(128, 128, 128); text-transform: none; text-indent: 0px; letter-spacing: normal; font-family: \"Helvetica Neue\", Helvetica, \"Hiragino Sans GB\", \"Microsoft YaHei\", Arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 400; word-spacing: 0px; float: none; display: inline !important; white-space: normal; orphans: 2; widows: 2; background-color: rgb(255, 255, 255); font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial;'>"
                 + "<p align=\"center\">" + rd.arti.title + "</p>" + "<p>" + rd.arti.content.Replace("\r\n", "</p><p>").Replace("\n", "</p><p>") + "</p></span>";
            }
            else if (this.rd.model == 2)
            {
                for (i = 0; i < this.rd.allrqnum; i++)
                {
                    listView1.Items.Add(new ListViewItem(new string[]
                    { (i + 1).ToString(),
                        this.rd.studentAnswers[i]==""?"-----":this.rd.studentAnswers[i],
                        this.rd.rq[i].ans }));
                }
                richTextBox1.Text = this.rd.rq[0].analysis;
                this.webBrowser1.DocumentText = "<span style='color: rgb(128, 128, 128); text-transform: none; text-indent: 0px; letter-spacing: normal; font-family: \"Helvetica Neue\", Helvetica, \"Hiragino Sans GB\", \"Microsoft YaHei\", Arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 400; word-spacing: 0px; float: none; display: inline !important; white-space: normal; orphans: 2; widows: 2; background-color: rgb(255, 255, 255); font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial;'>"
                 + "<p align=\"center\">" + rd.artis[0].title + "</p>" + "<p>" + rd.artis[0].content.Replace("\r\n", "</p><p>").Replace("\n", "</p><p>") + "</p></span>";
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
                this.webBrowser1.DocumentText = "<span style='color: rgb(128, 128, 128); text-transform: none; text-indent: 0px; letter-spacing: normal; font-family: \"Helvetica Neue\", Helvetica, \"Hiragino Sans GB\", \"Microsoft YaHei\", Arial, sans-serif; font-size: 16px; font-style: normal; font-weight: 400; word-spacing: 0px; float: none; display: inline !important; white-space: normal; orphans: 2; widows: 2; background-color: rgb(255, 255, 255); font-variant-ligatures: normal; font-variant-caps: normal; -webkit-text-stroke-width: 0px; text-decoration-style: initial; text-decoration-color: initial;'>"
                 + "<p align=\"center\">" + rd.artis[rd.rq[num - 1].articleid-1].title + "</p>" + "<p>" + rd.artis[rd.rq[num - 1].articleid-1].content.Replace("\r\n", "</p><p>").Replace("\n", "</p><p>") + "</p></span>";
                //this.rd.rq[num - 1].articleid-1;
            }
        }
    }

}
