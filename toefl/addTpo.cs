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
    public partial class addTpo : Form
    {
        private int model = 1;
        private int model2;
        private int index = 0;
        private string[,,] Readtext = new string[3,20,12]; 
        //[i,0,0]是文章，[i,1~19,*]是题目, [i,1~19,0]是题目类型, 1是段落号, 2是题干, 3--9是选项, 10是ans, 11是解析
        private string[] comWritetext = new string[4];
        //0是题目, 1是类型, 2是范文, 3是阅读材料
        private string[] indWritetext = new string[3];
        //[0]是题目, 1是类型, 2是范文
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
            index = 0;
            if (!(sender as RadioButton).Checked)
                return;
            switch((sender as RadioButton).Name)
            {
                case "radioButton1":
                    model = 1;
                    break;
                case "radioButton2":
                    model = 2;
                    break;
                case "radioButton3":
                    model = 3;
                    break;
                case "radioButton4":
                    model = 4;
                    break;
                case "radioButton5":
                    model = 5;
                    break;
            }
            if(model == 5)
            {
                radioButton7.Enabled = false;
                radioButton6.Checked = true;
                radioButton8.Enabled = true;
            }
            else if(model == 4)
            {
                radioButton6.Enabled = true;
                radioButton7.Enabled = true;
                radioButton8.Enabled = true;
                radioButton6.Checked = true;
            }
            else
            {
                radioButton7.Enabled = true;
                radioButton6.Checked = true;
                radioButton8.Enabled = false;
            }
            update();
        }

        private void intostring()//写入字符串
        {
            if(model2 == 1) //输入题干
            {
                if (model <= 3) //阅读的
                {
                    Readtext[model - 1, index + 1, 2] = richTextBox2.Text;
                    Readtext[model - 1, index + 1, 3] = richTextBox3.Text;
                    Readtext[model - 1, index + 1, 4] = richTextBox4.Text;
                    Readtext[model - 1, index + 1, 5] = richTextBox5.Text;
                    Readtext[model - 1, index + 1, 6] = richTextBox6.Text;
                    Readtext[model - 1, index + 1, 7] = richTextBox7.Text;
                    Readtext[model - 1, index + 1, 8] = richTextBox8.Text;
                    Readtext[model - 1, index + 1, 9] = richTextBox9.Text;
                    Readtext[model - 1, index + 1, 0] = richTextBox11.Text;
                    Readtext[model - 1, index + 1, 1] = richTextBox12.Text;
                    Readtext[model - 1, index + 1, 11] = richTextBox10.Text;
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
            if(model2 == 1) //输入题干
            {
                #region
                richTextBox1.Visible = false;
                richTextBox1.Enabled = false;
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
                #endregion 
                if (model <= 3) //阅读的
                {
                }
                else //综合独立写作的
                {
                    #region
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
                    label10.Visible = false;
                    label10.Enabled = false;
                    richTextBox10.Visible = false;
                    richTextBox10.Enabled = false;
                    label12.Visible = false;
                    label12.Enabled = false;
                    richTextBox12.Visible = false;
                    richTextBox12.Enabled = false;
                    #endregion
                }
            }
            else if(model2 == 2)//输入文章
            {
                #region
                richTextBox1.Visible = true;
                richTextBox1.Enabled = true;
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
                #endregion
            }
            else //输入范文
            {
                #region
                richTextBox1.Visible = true;
                richTextBox1.Enabled = true;
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
                #endregion
            }
            update();
        }
        private void update()
        {
            if (model <= 3) //阅读
            {
                richTextBox1.Text = Readtext[model - 1, 0, 0];
                richTextBox2.Text = Readtext[model - 1, index + 1, 2];
                richTextBox3.Text = Readtext[model - 1, index + 1, 3];
                richTextBox4.Text = Readtext[model - 1, index + 1, 4];
                richTextBox5.Text = Readtext[model - 1, index + 1, 5];
                richTextBox6.Text = Readtext[model - 1, index + 1, 6];
                richTextBox7.Text = Readtext[model - 1, index + 1, 7];
                richTextBox8.Text = Readtext[model - 1, index + 1, 8];
                richTextBox9.Text = Readtext[model - 1, index + 1, 9];
                richTextBox10.Text = Readtext[model - 1, index + 1, 11];
                richTextBox11.Text = Readtext[model - 1, index + 1, 0];
                richTextBox12.Text = Readtext[model - 1, index + 1, 1];
                for (int i = 0; i < Readtext[model - 1, index + 1, 10].Length; i++)
                {
                    if (Readtext[model - 1, index + 1, 10][i] - 'A' == 0)
                        checkBox1.Checked = true;
                    if (Readtext[model - 1, index + 1, 10][i] - 'B' == 0)
                        checkBox2.Checked = true;
                    if (Readtext[model - 1, index + 1, 10][i] - 'C' == 0)
                        checkBox3.Checked = true;
                    if (Readtext[model - 1, index + 1, 10][i] - 'D' == 0)
                        checkBox4.Checked = true;
                    if (Readtext[model - 1, index + 1, 10][i] - 'E' == 0)
                        checkBox5.Checked = true;
                    if (Readtext[model - 1, index + 1, 10][i] - 'F' == 0)
                        checkBox6.Checked = true;
                    if (Readtext[model - 1, index + 1, 10][i] - 'G' == 0)
                        checkBox7.Checked = true;
                }
            }
            else if (model == 4) //综合写作
            {
                richTextBox2.Text = comWritetext[0];
                richTextBox11.Text = comWritetext[1];
                if (model2 == 2)
                    richTextBox1.Text = comWritetext[2];
                if (model2 == 3)
                    richTextBox1.Text = comWritetext[3];
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
            if (index == 20)
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
            if (index == 0)
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
            for(int i = 0; i < 3; i ++)
            {
                xmlelem2 = xmldoc.CreateElement("Reading" + i);
                for (int j = 0; j <= 19; j++)
                {
                    if (Readtext[i, j, 0] == "")
                        continue;
                    XmlElement xmlQuestion;
                    xmlQuestion = xmldoc.CreateElement("question");
                    xmlQuestion.SetAttribute("articleid", textBox1.Text);
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
                        if(Readtext[i,j,k] == "")
                        {
                            break;
                        }
                    }
                    xmlQuestion.SetAttribute("opnum", k.ToString());
                    for(int l = 0; l < 7; l++)
                    {
                        xmlQuestion.SetAttribute("option" + (l + 1), Readtext[i, j, 3 + j]);
                    }
                    xmlQuestion.SetAttribute("ans", Readtext[i, j, 10]);
                    xmlQuestion.SetAttribute("analysis", Readtext[i,j,11]);

                    xmlelem2.AppendChild(xmlQuestion);
                }
                xmlelem.AppendChild(xmlelem2);
                
            }
            root.AppendChild(xmlelem);


            DatabaseHelp.ExecuteProc(xmldoc.InnerXml, "insert_new_tpo");
        }
    }
}
