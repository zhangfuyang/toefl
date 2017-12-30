using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace toefl
{
    public class readingQuestion
    {
        public int id;
        public int articleid;
        public int num;//题号
        public string type;//题型
        public string stem;//题干
        public int opnum;//选项个数
        public int paragraph;//对应原文起始段
        public int paragraph2;//对应原文结束段
        public string[] optionx;
        public string ans;
        public double acc;//国服平均水平
        public string analysis;//
        //private 
    }
}
