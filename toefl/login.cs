using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace toefl
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        //注册事件
        private void button3_Click(object sender, EventArgs e)
        {
            register register_form = new register();
            register_form.ShowDialog();
        }

        //退出事件
        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
        }

        //确定事件
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM [dbo].[Users] WHERE name='" + textBox1.Text + "' AND password='" + textBox2.Text + "'";
            int result = DatabaseHelp.SelectNum(sql);
            if (result != 0)
            {
                SystemConfig.name = textBox1.Text;
                SqlDataReader reader = DatabaseHelp.getReader(sql);
                reader.Read();
                SystemConfig.name = DatabaseHelp.convert(SystemConfig.name, reader["name"]);
                SystemConfig.password = DatabaseHelp.convert(SystemConfig.password, reader["password"]);
                SystemConfig.email = DatabaseHelp.convert(SystemConfig.email, reader["email"]);
                SystemConfig.acc = DatabaseHelp.convert(SystemConfig.acc, reader["acc"]);
                SystemConfig.time = DatabaseHelp.convert(SystemConfig.time, reader["time"]);
                SystemConfig.question_num = DatabaseHelp.convert(SystemConfig.question_num, reader["question_num"]);
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("用户名或密码不对");
        }
    }
}
