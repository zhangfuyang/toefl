using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace toefl
{
    public partial class peer_check : Form
    {

        public writing []wrt1;
        public writing []wrt2;
        public int allcom, allind;
        public int nowno;
        public int nowtype;
        public peer_check()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (1==1)
            {
                if (listView1.SelectedItems.Count != 1)
                    return;
                int num = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
                if (listView1.SelectedItems[0].SubItems[3].Text == "独立写作")
                {
                    richTextBox2.Text = wrt2[num - allcom - 1].stem;
                    richTextBox3.Text = wrt2[num - allcom - 1].ans;
                    nowtype = 2;
                }else if(listView1.SelectedItems[0].SubItems[3].Text == "综合写作")
                {
                    richTextBox2.Text = wrt1[num  - 1].stem+"\n\n阅读材料\n"
                        +wrt1[num-1].redmaterial+"\n\n听力材料\n"
                        +wrt1[num-1].lismaterial;
                    richTextBox3.Text = wrt1[num  - 1].ans;
                    nowtype = 1;
                }
                nowno = num;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sc= textBox1.Text;
            string cm1= richTextBox1.Text;
            string cm="";
            string []sts=cm1.Split('\'');
            for(int i = 0; i < sts.Count(); i++)
            {
                if (i != 0)
                {
                    cm += "''";
                }
                cm += sts[i];
            }

            int scnum;
            try
            {
                scnum = int.Parse(sc);
            }
            catch (Exception)
            {
                MessageBox.Show("请在分数栏输入正确的数字");
                return;
            }
            if (scnum < 0 || scnum > 30)
            {
                MessageBox.Show("分数必须介于0和30之间");
                return;
            }
            if (nowtype == 1)
            {
                int result;
                string sql = "UPDATE [dbo].[ComWritingAns]";
                sql += " SET [score] = " + scnum;
                sql += " ,[comment] = \'" + cm + "\'";
                sql += " ,[comname] =\'" + SystemConfig.name + "'";
                sql += " WHERE [name] = \'" + wrt1[nowno  - 1].name + "\'";
                sql += " and [date] = \'" + wrt1[nowno - 1].date + "'";
                sql += " and [id] = " + wrt1[nowno  - 1].id;
                try
                {
                    result = DatabaseHelp.executeCommand(sql);
                }
                catch (Exception)
                {
                    MessageBox.Show("评分失败，请联系管理员");
                    return;
                }
                if (result == 0)
                {
                    MessageBox.Show("评分失败，请联系管理员");
                    return;
                }
                MessageBox.Show("评分成功");
                DialogResult = DialogResult.OK;
            }else if (nowtype == 2)
            {
                int result;
                string sql = "UPDATE [dbo].[IndWritingAns]";
                sql += " SET [score] = " + scnum;
                sql += " ,[comment] = \'" + cm+"\'";
                sql += " ,[comname] =\'" + SystemConfig.name+"'";
                sql += " WHERE [name] = \'" + wrt2[nowno-allcom - 1].name+"\'";
                sql += " and [date] = \'" + wrt2[nowno - allcom - 1].date+"'";
                sql += " and [id] = " + wrt2[nowno - allcom - 1].id;
                try
                {
                    result = DatabaseHelp.executeCommand(sql);
                }
                catch (Exception)
                {
                    MessageBox.Show("评分失败，请联系管理员");
                    return;
                }
                if (result == 0)
                {
                    MessageBox.Show("评分失败，请联系管理员");
                    return;
                }
                MessageBox.Show("评分成功");
                DialogResult = DialogResult.OK;
            }
        }

        private void peer_check_Load(object sender, EventArgs e)
        {
            
            int i;
            string sql = "SELECT[dbo].[ComWritingAns].id as id,";
            sql += "[dbo].[ComWritingAns].date as date,";
            sql += "[dbo].[ComWritingAns].name as name,";
            sql += "[dbo].[ComWritingAns].ans as ans,";
            sql += "[dbo].[ComWritingAns].score as score,";
            sql += "[dbo].[ComWritingAns].comment as comment,";
            sql += "[dbo].[ComWritingProblem].stem,";
            sql += "[dbo].[ComWritingProblem].setid as tpoid,";
            sql += "[dbo].[ComWritingProblem].lismaterial as lismaterial,";
            sql += "[dbo].[ComWritingProblem].redmaterial as redmaterial ";
            sql += "FROM[dbo].[ComWritingAns],[dbo].[ComWritingProblem] ";
            sql += "where[dbo].[ComWritingAns].id=[dbo].[ComWritingProblem].id ";
            sql+= "and ISNULL([dbo].[ComWritingAns].score,-1)= -1 ";
            sql += "and [dbo].[ComWritingAns].name!=\'"+SystemConfig.name+"\'" ;
            allcom = DatabaseHelp.SelectNum(sql);
            if (allcom > 0)
            {
                wrt1 = new writing[allcom];
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                for (i = 0; i < allcom; i++)
                {
                    reader.Read();
                    wrt1[i] =new writing();
                    wrt1[i].type = 1;
                    wrt1[i].tpoid = DatabaseHelp.convert(wrt1[i].tpoid, reader["tpoid"]);
                    wrt1[i].id = DatabaseHelp.convert(wrt1[i].id, reader["id"]);
                    wrt1[i].date= DatabaseHelp.convert(wrt1[i].date, reader["date"]);
                    wrt1[i].name= DatabaseHelp.convert(wrt1[i].name, reader["name"]);
                    wrt1[i].ans = DatabaseHelp.convert(wrt1[i].ans, reader["ans"]);
                    wrt1[i].stem= DatabaseHelp.convert(wrt1[i].stem, reader["stem"]);
                    wrt1[i].lismaterial = DatabaseHelp.convert(wrt1[i].lismaterial, reader["lismaterial"]);
                    wrt1[i].redmaterial= DatabaseHelp.convert(wrt1[i].redmaterial, reader["redmaterial"]);
                }
                reader.Close();
            }
            sql = "SELECT[dbo].[IndWritingAns].id as id,";
            sql += "[dbo].[IndWritingAns].date as date,";
            sql += "[dbo].[IndWritingAns].name as name,";
            sql += "[dbo].[IndWritingAns].ans as ans,";
            sql += "[dbo].[IndWritingAns].score as score,";
            sql += "[dbo].[IndWritingAns].comment as comment,";
            sql += "[dbo].[IndWritingProblem].stem,";
            sql += "[dbo].[IndWritingProblem].setid as tpoid ";
            sql += "FROM[dbo].[IndWritingAns],[dbo].[IndWritingProblem] ";
            sql += "where[dbo].[IndWritingAns].id=[dbo].[IndWritingProblem].id ";
            sql += "and ISNULL([dbo].[IndWritingAns].score,-1)= -1 ";
            sql += "and [dbo].[IndWritingAns].name!=\'" + SystemConfig.name+"\'";
            allind = DatabaseHelp.SelectNum(sql);
            if (allind > 0)
            {
                wrt2 = new writing[allind];
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                for (i = 0; i < allind; i++)
                {
                    reader.Read();
                    wrt2[i] = new writing();
                    wrt2[i].type = 1;
                    wrt2[i].tpoid = DatabaseHelp.convert(wrt2[i].tpoid, reader["tpoid"]);
                    wrt2[i].id = DatabaseHelp.convert(wrt2[i].id, reader["id"]);
                    wrt2[i].date = DatabaseHelp.convert(wrt2[i].date, reader["date"]);
                    wrt2[i].name = DatabaseHelp.convert(wrt2[i].name, reader["name"]);
                    wrt2[i].ans = DatabaseHelp.convert(wrt2[i].ans, reader["ans"]);
                    wrt2[i].stem = DatabaseHelp.convert(wrt2[i].stem, reader["stem"]);
                }
                reader.Close();
            }
            for (i = 0; i < allcom; i++)
            {
                listView1.Items.Add(new ListViewItem(new string[]
                {   (i+1).ToString(),
                    wrt1[i].tpoid.ToString(),
                    this.wrt1[i].name,
                    "综合写作",
                    this.wrt1[i].date.ToString() }));
            }
            for (i = 0; i < allind; i++)
            {
                listView1.Items.Add(new ListViewItem(new string[]
                {   (i+allcom+1).ToString(),
                    wrt2[i].tpoid.ToString(),
                    this.wrt2[i].name,
                    "独立写作",
                    this.wrt2[i].date.ToString() }));
            }

        }
    }
}
