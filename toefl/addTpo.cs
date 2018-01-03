using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace toefl
{
    public delegate void TransfDelegate(String value);
    public partial class addTpo : Form
    {
        private int model = 1;
        private int model2;
        private int index = 0;
        private string[,,] Readtext = new string[3,20,12]; 
        //[i,0,0]是文章, [i,0,1]是文章标题，[i,0,2]是文章的主题, [i,1~19,*]是题目, [i,1~19,0]是题目类型, 1是段落号, 2是题干, 3--9是选项, 10是ans, 11是解析
        private string[] comWritetext = new string[4];
        //0是题目, 1是类型, 2是范文, 3是阅读材料
        private string[] indWritetext = new string[3];
        //[0]是题目, 1是类型, 2是范文
        public event TransfDelegate TransfEvent;
        public addTpo()
        {
            InitializeComponent();
            init();
        }
        void init()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 20; j++)
                    for (int k = 0; k < 12; k++)
                    {
                        Readtext[i, j, k] = "";
                    }
            for(int i = 0; i < 4; i ++)
            {
                comWritetext[i] = "";
            }
            for(int i = 0; i < 3; i ++)
            {
                indWritetext[i] = "";
            }
        }
        //选择题目reading writing
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            index = 1;
            if (!(sender as RadioButton).Checked)
                return;
            switch((sender as RadioButton).Name)
            {
                case "radioButton1":
                    model = 1;
                    radioButton7.Enabled = true;
                    radioButton6.Checked = true;
                    radioButton8.Enabled = false;
                    break;
                case "radioButton2":
                    model = 2;
                    radioButton7.Enabled = true;
                    radioButton6.Checked = true;
                    radioButton8.Enabled = false;
                    break;
                case "radioButton3":
                    model = 3;
                    radioButton7.Enabled = true;
                    radioButton6.Checked = true;
                    radioButton8.Enabled = false;
                    break;
                case "radioButton4":
                    model = 4;
                    radioButton6.Enabled = true;
                    radioButton7.Enabled = true;
                    radioButton8.Enabled = true;
                    radioButton6.Checked = true;
                    break;
                case "radioButton5":
                    model = 5;
                    radioButton7.Enabled = false;
                    radioButton6.Checked = true;
                    radioButton8.Enabled = true;
                    break;
            }
            radioButton6.Checked = true;
            getbutton();
            update();
        }
        private void getbutton()
        {
            if (model == 5)
            {
                #region model2 = 1
                if (model2 == 1)
                {
                    checkBox1.Visible = false;
                    checkBox1.Enabled = false;
                    checkBox2.Visible = false;
                    checkBox2.Enabled = false;
                    checkBox3.Visible = false;
                    checkBox3.Enabled = false;
                    checkBox4.Visible = false;
                    checkBox4.Enabled = false;
                    checkBox5.Visible = false;
                    checkBox5.Enabled = false;
                    checkBox6.Visible = false;
                    checkBox6.Enabled = false;
                    checkBox7.Visible = false;
                    checkBox7.Enabled = false;

                    richTextBox1.Visible = false;
                    richTextBox1.Enabled = false;
                    label2.Visible = true;
                    label2.Enabled = true;
                    richTextBox2.Visible = true;
                    richTextBox2.Enabled = true;
                    label3.Visible = false;
                    label3.Enabled = false;
                    richTextBox3.Visible = false;
                    richTextBox3.Enabled = false;
                    label4.Visible = false;
                    label4.Enabled = false;
                    richTextBox4.Visible = false;
                    richTextBox4.Enabled = false;
                    label5.Visible = false;
                    label5.Enabled = false;
                    richTextBox5.Visible = false;
                    richTextBox5.Enabled = false;
                    label6.Visible = false;
                    label6.Enabled = false;
                    richTextBox6.Visible = false;
                    richTextBox6.Enabled = false;
                    label7.Visible = false;
                    label7.Enabled = false;
                    richTextBox7.Visible = false;
                    richTextBox7.Enabled = false;
                    label8.Visible = false;
                    label8.Enabled = false;
                    richTextBox8.Visible = false;
                    richTextBox8.Enabled = false;
                    label9.Visible = false;
                    label9.Enabled = false;
                    richTextBox9.Visible = false;
                    richTextBox9.Enabled = false;
                    label10.Visible = false;
                    label10.Enabled = false;
                    richTextBox10.Visible = false;
                    richTextBox10.Enabled = false;
                    label11.Visible = true;
                    label11.Enabled = true;
                    richTextBox11.Visible = true;
                    richTextBox11.Enabled = true;
                    label12.Visible = false;
                    label12.Enabled = false;
                    richTextBox12.Visible = false;
                    richTextBox12.Enabled = false;
                }
                #endregion
                #region model2 = 3
                if (model2 == 3)
                {
                    checkBox1.Visible = false;
                    checkBox1.Enabled = false;
                    checkBox2.Visible = false;
                    checkBox2.Enabled = false;
                    checkBox3.Visible = false;
                    checkBox3.Enabled = false;
                    checkBox4.Visible = false;
                    checkBox4.Enabled = false;
                    checkBox5.Visible = false;
                    checkBox5.Enabled = false;
                    checkBox6.Visible = false;
                    checkBox6.Enabled = false;
                    checkBox7.Visible = false;
                    checkBox7.Enabled = false;

                    richTextBox1.Visible = true;
                    richTextBox1.Enabled = true;
                    label2.Visible = false;
                    label2.Enabled = false;
                    richTextBox2.Visible = false;
                    richTextBox2.Enabled = false;
                    label3.Visible = false;
                    label3.Enabled = false;
                    richTextBox3.Visible = false;
                    richTextBox3.Enabled = false;
                    label4.Visible = false;
                    label4.Enabled = false;
                    richTextBox4.Visible = false;
                    richTextBox4.Enabled = false;
                    label5.Visible = false;
                    label5.Enabled = false;
                    richTextBox5.Visible = false;
                    richTextBox5.Enabled = false;
                    label6.Visible = false;
                    label6.Enabled = false;
                    richTextBox6.Visible = false;
                    richTextBox6.Enabled = false;
                    label7.Visible = false;
                    label7.Enabled = false;
                    richTextBox7.Visible = false;
                    richTextBox7.Enabled = false;
                    label8.Visible = false;
                    label8.Enabled = false;
                    richTextBox8.Visible = false;
                    richTextBox8.Enabled = false;
                    label9.Visible = false;
                    label9.Enabled = false;
                    richTextBox9.Visible = false;
                    richTextBox9.Enabled = false;
                    label10.Visible = false;
                    label10.Enabled = false;
                    richTextBox10.Visible = false;
                    richTextBox10.Enabled = false;
                    label11.Visible = false;
                    label11.Enabled = false;
                    richTextBox11.Visible = false;
                    richTextBox11.Enabled = false;
                    label12.Visible = false;
                    label12.Enabled = false;
                    richTextBox12.Visible = false;
                    richTextBox12.Enabled = false;
                }
                #endregion

            }
            else if (model == 4)
            {
                #region model2 = 1
                if (model2 == 1)
                {
                    checkBox1.Visible = false;
                    checkBox1.Enabled = false;
                    checkBox2.Visible = false;
                    checkBox2.Enabled = false;
                    checkBox3.Visible = false;
                    checkBox3.Enabled = false;
                    checkBox4.Visible = false;
                    checkBox4.Enabled = false;
                    checkBox5.Visible = false;
                    checkBox5.Enabled = false;
                    checkBox6.Visible = false;
                    checkBox6.Enabled = false;
                    checkBox7.Visible = false;
                    checkBox7.Enabled = false;

                    richTextBox1.Visible = false;
                    richTextBox1.Enabled = false;
                    label2.Visible = true;
                    label2.Enabled = true;
                    richTextBox2.Visible = true;
                    richTextBox2.Enabled = true;
                    label3.Visible = false;
                    label3.Enabled = false;
                    richTextBox3.Visible = false;
                    richTextBox3.Enabled = false;
                    label4.Visible = false;
                    label4.Enabled = false;
                    richTextBox4.Visible = false;
                    richTextBox4.Enabled = false;
                    label5.Visible = false;
                    label5.Enabled = false;
                    richTextBox5.Visible = false;
                    richTextBox5.Enabled = false;
                    label6.Visible = false;
                    label6.Enabled = false;
                    richTextBox6.Visible = false;
                    richTextBox6.Enabled = false;
                    label7.Visible = false;
                    label7.Enabled = false;
                    richTextBox7.Visible = false;
                    richTextBox7.Enabled = false;
                    label8.Visible = false;
                    label8.Enabled = false;
                    richTextBox8.Visible = false;
                    richTextBox8.Enabled = false;
                    label9.Visible = false;
                    label9.Enabled = false;
                    richTextBox9.Visible = false;
                    richTextBox9.Enabled = false;
                    label10.Visible = false;
                    label10.Enabled = false;
                    richTextBox10.Visible = false;
                    richTextBox10.Enabled = false;
                    label11.Visible = true;
                    label11.Enabled = true;
                    richTextBox11.Visible = true;
                    richTextBox11.Enabled = true;
                    label12.Visible = false;
                    label12.Enabled = false;
                    richTextBox12.Visible = false;
                    richTextBox12.Enabled = false;
                }
                #endregion
                #region model2 = 2 or 3
                if (model2 == 2 || model2 == 3)
                {
                    checkBox1.Visible = false;
                    checkBox1.Enabled = false;
                    checkBox2.Visible = false;
                    checkBox2.Enabled = false;
                    checkBox3.Visible = false;
                    checkBox3.Enabled = false;
                    checkBox4.Visible = false;
                    checkBox4.Enabled = false;
                    checkBox5.Visible = false;
                    checkBox5.Enabled = false;
                    checkBox6.Visible = false;
                    checkBox6.Enabled = false;
                    checkBox7.Visible = false;
                    checkBox7.Enabled = false;

                    richTextBox1.Visible = true;
                    richTextBox1.Enabled = true;
                    label2.Visible = false;
                    label2.Enabled = false;
                    richTextBox2.Visible = false;
                    richTextBox2.Enabled = false;
                    label3.Visible = false;
                    label3.Enabled = false;
                    richTextBox3.Visible = false;
                    richTextBox3.Enabled = false;
                    label4.Visible = false;
                    label4.Enabled = false;
                    richTextBox4.Visible = false;
                    richTextBox4.Enabled = false;
                    label5.Visible = false;
                    label5.Enabled = false;
                    richTextBox5.Visible = false;
                    richTextBox5.Enabled = false;
                    label6.Visible = false;
                    label6.Enabled = false;
                    richTextBox6.Visible = false;
                    richTextBox6.Enabled = false;
                    label7.Visible = false;
                    label7.Enabled = false;
                    richTextBox7.Visible = false;
                    richTextBox7.Enabled = false;
                    label8.Visible = false;
                    label8.Enabled = false;
                    richTextBox8.Visible = false;
                    richTextBox8.Enabled = false;
                    label9.Visible = false;
                    label9.Enabled = false;
                    richTextBox9.Visible = false;
                    richTextBox9.Enabled = false;
                    label10.Visible = false;
                    label10.Enabled = false;
                    richTextBox10.Visible = false;
                    richTextBox10.Enabled = false;
                    label11.Visible = false;
                    label11.Enabled = false;
                    richTextBox11.Visible = false;
                    richTextBox11.Enabled = false;
                    label12.Visible = false;
                    label12.Enabled = false;
                    richTextBox12.Visible = false;
                    richTextBox12.Enabled = false;
                }
                #endregion
            }
            else
            {

                #region model2 = 1
                if (model2 == 1)
                {
                    checkBox1.Visible = true;
                    checkBox1.Enabled = true;
                    checkBox2.Visible = true;
                    checkBox2.Enabled = true;
                    checkBox3.Visible = true;
                    checkBox3.Enabled = true;
                    checkBox4.Visible = true;
                    checkBox4.Enabled = true;
                    checkBox5.Visible = true;
                    checkBox5.Enabled = true;
                    checkBox6.Visible = true;
                    checkBox6.Enabled = true;
                    checkBox7.Visible = true;
                    checkBox7.Enabled = true;

                    richTextBox1.Visible = false;
                    richTextBox1.Enabled = false;
                    label2.Visible = true;
                    label2.Enabled = true;
                    richTextBox2.Visible = true;
                    richTextBox2.Enabled = true;
                    label3.Visible = true;
                    label3.Enabled = true;
                    richTextBox3.Visible = true;
                    richTextBox3.Enabled = true;
                    label4.Visible = true;
                    label4.Enabled = true;
                    richTextBox4.Visible = true;
                    richTextBox4.Enabled = true;
                    label5.Visible = true;
                    label5.Enabled = true;
                    richTextBox5.Visible = true;
                    richTextBox5.Enabled = true;
                    label6.Visible = true;
                    label6.Enabled = true;
                    richTextBox6.Visible = true;
                    richTextBox6.Enabled = true;
                    label7.Visible = true;
                    label7.Enabled = true;
                    richTextBox7.Visible = true;
                    richTextBox7.Enabled = true;
                    label8.Visible = true;
                    label8.Enabled = true;
                    richTextBox8.Visible = true;
                    richTextBox8.Enabled = true;
                    label9.Visible = true;
                    label9.Enabled = true;
                    richTextBox9.Visible = true;
                    richTextBox9.Enabled = true;
                    label10.Visible = true;
                    label10.Enabled = true;
                    richTextBox10.Visible = true;
                    richTextBox10.Enabled = true;
                    label11.Visible = true;
                    label11.Enabled = true;
                    richTextBox11.Visible = true;
                    richTextBox11.Enabled = true;
                    label12.Visible = true;
                    label12.Enabled = true;
                    richTextBox12.Visible = true;
                    richTextBox12.Enabled = true;
                }
                #endregion

                #region model2 = 2
                if(model2 == 2)
                {
                    checkBox1.Visible = false;
                    checkBox1.Enabled = false;
                    checkBox2.Visible = false;
                    checkBox2.Enabled = false;
                    checkBox3.Visible = false;
                    checkBox3.Enabled = false;
                    checkBox4.Visible = false;
                    checkBox4.Enabled = false;
                    checkBox5.Visible = false;
                    checkBox5.Enabled = false;
                    checkBox6.Visible = false;
                    checkBox6.Enabled = false;
                    checkBox7.Visible = false;
                    checkBox7.Enabled = false;

                    richTextBox1.Visible = true;
                    richTextBox1.Enabled = true;
                    label2.Visible = false;
                    label2.Enabled = false;
                    richTextBox2.Visible = false;
                    richTextBox2.Enabled = false;
                    label3.Visible = false;
                    label3.Enabled = false;
                    richTextBox3.Visible = false;
                    richTextBox3.Enabled = false;
                    label4.Visible = false;
                    label4.Enabled = false;
                    richTextBox4.Visible = false;
                    richTextBox4.Enabled = false;
                    label5.Visible = false;
                    label5.Enabled = false;
                    richTextBox5.Visible = false;
                    richTextBox5.Enabled = false;
                    label6.Visible = false;
                    label6.Enabled = false;
                    richTextBox6.Visible = false;
                    richTextBox6.Enabled = false;
                    label7.Visible = false;
                    label7.Enabled = false;
                    richTextBox7.Visible = false;
                    richTextBox7.Enabled = false;
                    label8.Visible = false;
                    label8.Enabled = false;
                    richTextBox8.Visible = false;
                    richTextBox8.Enabled = false;
                    label9.Visible = false;
                    label9.Enabled = false;
                    richTextBox9.Visible = false;
                    richTextBox9.Enabled = false;
                    label10.Visible = false;
                    label10.Enabled = false;
                    richTextBox10.Visible = false;
                    richTextBox10.Enabled = false;
                    label11.Visible = false;
                    label11.Enabled = false;
                    richTextBox11.Visible = false;
                    richTextBox11.Enabled = false;
                    label12.Visible = false;
                    label12.Enabled = false;
                    richTextBox12.Visible = false;
                    richTextBox12.Enabled = false;
                }
                #endregion
            }

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender as RadioButton).Checked)
                return;
            switch ((sender as RadioButton).Name)
            {
                case "radioButton6":
                    model2 = 1;
                    break;
                case "radioButton7":
                    model2 = 2;
                    break;
                case "radioButton8":
                    model2 = 3;
                    break;
            }
            getbutton();
            update();
        }
        private void intostring()//写入字符串
        {
            if(model2 == 1) //输入题干
            {
                if (model <= 3) //阅读的
                {
                    Readtext[model - 1, index, 2] = richTextBox2.Text;
                    Readtext[model - 1, index, 3] = richTextBox3.Text;
                    Readtext[model - 1, index, 4] = richTextBox4.Text;
                    Readtext[model - 1, index, 5] = richTextBox5.Text;
                    Readtext[model - 1, index, 6] = richTextBox6.Text;
                    Readtext[model - 1, index, 7] = richTextBox7.Text;
                    Readtext[model - 1, index, 8] = richTextBox8.Text;
                    Readtext[model - 1, index, 9] = richTextBox9.Text;
                    Readtext[model - 1, index, 0] = richTextBox11.Text;
                    Readtext[model - 1, index, 1] = richTextBox12.Text;
                    Readtext[model - 1, index, 11] = richTextBox10.Text;

                    Readtext[model - 1, index, 10] = "";
                    if (checkBox1.Checked)
                        Readtext[model - 1, index, 10] += "A";
                    if (checkBox2.Checked)
                        Readtext[model - 1, index, 10] += "B";
                    if (checkBox3.Checked)
                        Readtext[model - 1, index, 10] += "C";
                    if (checkBox4.Checked)
                        Readtext[model - 1, index, 10] += "D";
                    if (checkBox5.Checked)
                        Readtext[model - 1, index, 10] += "E";
                    if (checkBox6.Checked)
                        Readtext[model - 1, index, 10] += "F";
                    if (checkBox7.Checked)
                        Readtext[model - 1, index, 10] += "G";
                }
                else if (model == 4)//综合写作的
                {
                    comWritetext[0] = richTextBox2.Text;
                    comWritetext[1] = richTextBox11.Text;
                }
                else if(model == 5) //独立写作
                {
                    indWritetext[0] = richTextBox2.Text;
                    indWritetext[1] = richTextBox11.Text;
                }
            }
            else if(model2 == 2) //文章
            {
                if (model <= 3)
                {
                    Readtext[model - 1, 0, 0] = richTextBox1.Text;
                    Readtext[model - 1, 0, 1] = textBox2.Text;
                    Readtext[model - 1, 0, 2] = textBox3.Text;
                }
                else if (model == 4)
                    comWritetext[3] = richTextBox1.Text;
            }
            else //范文
            {
                if (model == 4)
                    comWritetext[2] = richTextBox1.Text;
                else if (model == 5)
                    indWritetext[2] = richTextBox1.Text;
            }


        }

       
        private void update()
        {
            if (model <= 3) //阅读
            {
                label13.Text = "题号" + (index);
                richTextBox1.Text = Readtext[model - 1, 0, 0];
                richTextBox2.Text = Readtext[model - 1, index, 2];
                richTextBox3.Text = Readtext[model - 1, index, 3];
                richTextBox4.Text = Readtext[model - 1, index, 4];
                richTextBox5.Text = Readtext[model - 1, index, 5];
                richTextBox6.Text = Readtext[model - 1, index, 6];
                richTextBox7.Text = Readtext[model - 1, index, 7];
                richTextBox8.Text = Readtext[model - 1, index, 8];
                richTextBox9.Text = Readtext[model - 1, index, 9];
                richTextBox10.Text = Readtext[model - 1, index, 11];
                richTextBox11.Text = Readtext[model - 1, index, 0];
                richTextBox12.Text = Readtext[model - 1, index, 1];
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                for (int i = 0; i < Readtext[model - 1, index, 10].Length; i++)
                {
                    if (Readtext[model - 1, index, 10][i] - 'A' == 0)
                        checkBox1.Checked = true;
                    if (Readtext[model - 1, index, 10][i] - 'B' == 0)
                        checkBox2.Checked = true;
                    if (Readtext[model - 1, index, 10][i] - 'C' == 0)
                        checkBox3.Checked = true;
                    if (Readtext[model - 1, index, 10][i] - 'D' == 0)
                        checkBox4.Checked = true;
                    if (Readtext[model - 1, index, 10][i] - 'E' == 0)
                        checkBox5.Checked = true;
                    if (Readtext[model - 1, index, 10][i] - 'F' == 0)
                        checkBox6.Checked = true;
                    if (Readtext[model - 1, index, 10][i] - 'G' == 0)
                        checkBox7.Checked = true;
                }
            }
            else if (model == 4) //综合写作
            {
                richTextBox2.Text = comWritetext[0];
                richTextBox11.Text = comWritetext[1];
                if (model2 == 2)
                    richTextBox1.Text = comWritetext[3];
                if (model2 == 3)
                    richTextBox1.Text = comWritetext[2];
            }
            else //独立写作
            {
                richTextBox2.Text = indWritetext[0];
                richTextBox11.Text = indWritetext[1];
                richTextBox1.Text = indWritetext[2];
            }
        }

        //下一道
        private void button1_Click(object sender, EventArgs e)
        {
            if (index == 19)
            {
                MessageBox.Show("已经是最后一道了");
                return;
            }
            index++;
            update();
        }

        //上一道
        private void button2_Click(object sender, EventArgs e)
        {
            if (index == 1)
            {
                MessageBox.Show("已经是第一道了");
                return;
            }
            index--;
            update();
        }

        //完成
        private void button3_Click(object sender, EventArgs e)
        {
            intostring();
            MessageBox.Show("完成");
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //提交
        {
            XmlDocument xmldoc;
            XmlElement xmlelem;
            xmldoc = new XmlDocument();
            XmlDeclaration xmldecl;
            xmldecl = xmldoc.CreateXmlDeclaration("1.0", "gb2312", null);
            xmldoc.AppendChild(xmldecl);

            xmlelem = xmldoc.CreateElement("", "NEWTPO", "");
            xmldoc.AppendChild(xmlelem);

            XmlNode root = xmldoc.SelectSingleNode("NEWTPO");
            xmlelem = xmldoc.CreateElement("IndWriting");
            xmlelem.SetAttribute("setid", textBox1.Text);
            xmlelem.SetAttribute("subject", indWritetext[1]);
            xmlelem.SetAttribute("stem", indWritetext[0]);
            xmlelem.SetAttribute("model", indWritetext[2]);
            root.AppendChild(xmlelem);

            xmlelem = xmldoc.CreateElement("ComWriting");
            xmlelem.SetAttribute("setid", textBox1.Text);
            xmlelem.SetAttribute("subject", comWritetext[1]);
            xmlelem.SetAttribute("stem", comWritetext[0]);
            xmlelem.SetAttribute("model", comWritetext[2]);
            xmlelem.SetAttribute("redmaterial", comWritetext[3]);
            root.AppendChild(xmlelem);

            xmlelem = xmldoc.CreateElement("Reading");
            XmlElement xmlelem2;
            //[i,0,0]是文章，[i,1~19,*]是题目, [i,1~19,0]是题目类型, 1是段落号, 2是题干, 3--9是选项, 10是ans, 11是解析
            for (int i = 0; i < 3; i ++)
            {
                xmlelem2 = xmldoc.CreateElement("Reading" + i);
                XmlElement xmlarticle;
                xmlarticle = xmldoc.CreateElement("Article");
                xmlarticle.SetAttribute("article", Readtext[i, 0, 0]);
                xmlarticle.SetAttribute("title", Readtext[i, 0, 1]);
                xmlarticle.SetAttribute("setid", textBox1.Text);
                xmlarticle.SetAttribute("subject", Readtext[i, 0, 2]);
                int no = 0;
                for(no = 1; no <= 19; no++)
                {
                    if (Readtext[i, no, 2] == "")
                        break;
                }
                xmlarticle.SetAttribute("questionno", (no-1).ToString());
                xmlelem2.AppendChild(xmlarticle);
                for (int j = 1; j <= 19; j++)
                {
                    if (Readtext[i, j, 2] == "")
                        continue;
                    XmlElement xmlQuestion;
                    xmlQuestion = xmldoc.CreateElement("question");
                    xmlQuestion.SetAttribute("id", j.ToString());
                    xmlQuestion.SetAttribute("articleid", (i+1).ToString());
                    xmlQuestion.SetAttribute("num", j.ToString());
                    xmlQuestion.SetAttribute("type", Readtext[i, j, 0]);
                    string[] temp_para = Readtext[i, j, 1].Split(new char[2] { ',', ' ' });
                    if(temp_para.Length == 1)
                    {
                        xmlQuestion.SetAttribute("paragraph", temp_para[0]);
                        xmlQuestion.SetAttribute("paragraph2", temp_para[0]);
                    }
                    else
                    {
                        xmlQuestion.SetAttribute("paragraph", temp_para[0]);
                        xmlQuestion.SetAttribute("paragraph2", temp_para[1]);
                    }
                    xmlQuestion.SetAttribute("stem", Readtext[i, j, 2]);
                    int k;
                    for ( k = 0; k < 7; k++)
                    {
                        if(Readtext[i,j,k+3] == "")
                        {
                            break;
                        }
                    }
                    xmlQuestion.SetAttribute("opnum", k.ToString());
                    for(int l = 0; l < 7; l++)
                    {
                        xmlQuestion.SetAttribute("option" + (l + 1), Readtext[i, j, 3 + l]);
                    }
                    xmlQuestion.SetAttribute("ans", Readtext[i, j, 10]);
                    xmlQuestion.SetAttribute("analysis", Readtext[i,j,11]);

                    xmlelem2.AppendChild(xmlQuestion);
                }
                xmlelem.AppendChild(xmlelem2);
                
            }
            root.AppendChild(xmlelem);

            //string test;
            //test = "<?xml version=\"1.0\" encoding=\"gb2312\"?><NEWTPO><IndWriting setid=\"101\" subject=\"随便答题\" stem=\"为什么张夫洋最帅\" model=\"就是帅\" /><ComWriting setid=\"101\" subject=\"简单题\" stem=\"谁最帅\" model=\"张夫洋\" redmaterial=\"到底谁最帅\" /><Reading><Reading0><Article article=\"民工漫\" title=\"漫画\" setid=\"101\" subject=\"中文话题\" questionno=\"2\" /><question id=\"1\" articleid=\"1\" num=\"1\" type=\"错别字\" paragraph=\"1\" paragraph2=\"1\" stem=\"海贼王\" opnum=\"4\" option1=\"路费\" option2=\"娜美\" option3=\"香吉士\" option4=\"索隆\" option5=\"\" option6=\"\" option7=\"\" ans=\"A\" analysis=\"\" /><question id=\"2\" articleid=\"1\" num=\"2\" type=\"女性\" paragraph=\"2\" paragraph2=\"2\" stem=\"火影\" opnum=\"4\" option1=\"名人\" option2=\"宇智波鼬\" option3=\"佐助\" option4=\"小樱\" option5=\"\" option6=\"\" option7=\"\" ans=\"D\" analysis=\"\" /></Reading0><Reading1><Article article=\"NBA的题\" title=\"NBA\" setid=\"101\" subject=\"NBA话题\" questionno=\"1\" /><question id=\"1\" articleid=\"2\" num=\"1\" type=\"最nb\" paragraph=\"0\" paragraph2=\"1\" stem=\"NBA\" opnum=\"4\" option1=\"休斯顿火箭\" option2=\"圣安东尼奥马刺\" option3=\"奥克拉荷马雷霆\" option4=\"金州勇士\" option5=\"\" option6=\"\" option7=\"\" ans=\"D\" analysis=\"\" /></Reading1><Reading2><Article article=\"计算机知识\" title=\"计算机\" setid=\"101\" subject=\"计算机话题\" questionno=\"2\" /><question id=\"1\" articleid=\"3\" num=\"1\" type=\"随便选\" paragraph=\"20\" paragraph2=\"20\" stem=\"CPU\" opnum=\"5\" option1=\"主频\" option2=\"寄存器\" option3=\"时钟\" option4=\"cache\" option5=\"总线\" option6=\"\" option7=\"\" ans=\"D\" analysis=\"\" /><question id=\"2\" articleid=\"3\" num=\"2\" type=\"重要\" paragraph=\"5\" paragraph2=\"5\" stem=\"硬件\" opnum=\"5\" option1=\"cpu\" option2=\"内存\" option3=\"外存\" option4=\"显卡\" option5=\"显示器\" option6=\"\" option7=\"\" ans=\"A\" analysis=\"\" /></Reading2></Reading></NEWTPO>";
           // DatabaseHelp.ExecuteProc(test, "insert_new_tpo");

            DatabaseHelp.ExecuteProc(xmldoc.InnerXml, "insert_new_tpo");
            TransfEvent(textBox1.Text);
            this.Close();
            return;
        }
    }
}
