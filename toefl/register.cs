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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        //确定事件
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT name FROM [dbo].[Users] WHERE name='" + name_textbox.Text + "'";
            int result = DatabaseHelp.SelectNum(sql);
            if(passwd_textbox.Text != passwd2_textbox.Text)
            {
                MessageBox.Show("请输入相同的密码");
                return;
            }
            if (result != 0)
            {
                MessageBox.Show("该用户名已被注册！");
            }
            else
            {
                sql = "INSERT INTO [dbo].[Users] (name, password, email, time, acc, question_num) VALUES";
                sql += "('" + name_textbox.Text + "','" + passwd_textbox.Text + "','" + mail_textbox.Text + "',";
                sql += "0.0, 0.0, 0)";
                result = DatabaseHelp.executeCommand(sql);
                if (result == 0)
                {
                    MessageBox.Show("用户创建失败，请联系管理员");
                    return;
                }
                MessageBox.Show("用户创建成功");
                DialogResult = DialogResult.OK;
            }
        }

        //取消事件
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
