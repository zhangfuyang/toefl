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
        public ansList()
        {
            InitializeComponent();
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
    }
}
