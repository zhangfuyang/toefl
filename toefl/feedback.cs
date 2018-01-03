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
    public partial class feedback : Form
    {
        public feedback()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sc1 = textBox1.Text;
            string sc="";
            string[] ssts = sc1.Split('\'');
            for (int i = 0; i < ssts.Count(); i++)
            {
                if (i != 0)
                {
                    sc += "''";
                }
                sc += ssts[i];
            }

            int result;
            string cm1 = richTextBox1.Text;
            string cm = "";
            string[] sts = cm1.Split('\'');
            for (int i = 0; i < sts.Count(); i++)
            {
                if (i != 0)
                {
                    cm += "''";
                }
                cm += sts[i];
            }
            string sql = "INSERT INTO [dbo].[UserSuggestion]([name],[suggest],[date],[title])  ";
            sql += "VALUES('"+SystemConfig.name+"','"+cm +"','"+DateTime.Now +"','"+sc+"')";
            try
            {
                result = DatabaseHelp.executeCommand(sql);
            }
            catch (Exception ex)
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
}
