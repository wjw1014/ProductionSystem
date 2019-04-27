using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace myProject
{
    public partial class Form2 : Form
    {

        public string str = "";

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(string str)
        {
            InitializeComponent();
            this.str = str;
            this.Text = this.str + "  ©wjw";
        }

        // 用户选择数组
        ArrayList userChoose = new ArrayList();

        /**
         * 初始化规则库
         * 每条规则为一个数组
         * 数组最后一个元素为结果，其余元素为条件
         **/
        ArrayList mRule1 = new ArrayList() { "西部第八", "火箭队" };
        ArrayList mRule2 = new ArrayList() { "西部第三", "雷霆队" };
        ArrayList mRule3 = new ArrayList() { "东部第一", "骑士队" };
        ArrayList mRule4 = new ArrayList() { "东部第四", "热火队" };
        ArrayList mRule5 = new ArrayList() { "西部第一", "勇士队" };
        ArrayList mRule6 = new ArrayList() { "雷霆队", "35号", "前锋", "杜兰特" };
        ArrayList mRule7 = new ArrayList() { "火箭队", "13号", "后卫", "哈登" };
        ArrayList mRule8 = new ArrayList() { "火箭队", "12号", "中锋", "霍华德" };
        ArrayList mRule9 = new ArrayList() { "勇士队", "30号", "后卫", "库里" };
        ArrayList mRule10 = new ArrayList() { "热火队", "3号", "后卫", "韦德" };
        ArrayList mRule11 = new ArrayList() { "骑士队", "23号", "前锋", "詹姆斯" };
        

        /**
         * 正向推导
         * **/
        public void FDeduction()
        {
            // 存储规则
            string ruleStrs = "";
            // 存储判断规则
            string ruleStr = "";
            // 创建中间值数组，用于存放比较中间值
            ArrayList zhong = new ArrayList();
            ArrayList mRuleAll = new ArrayList() { mRule1, mRule2, mRule3, mRule4, mRule5, mRule5, mRule6, mRule7, mRule8, mRule9, mRule10, mRule11 };

            for (int k = 0; k < mRuleAll.Count; k++)
            {
                ArrayList mR = (ArrayList)mRuleAll[k];
                for (int i = 0; i <= mR.Count - 2; i++)
                {
                    bool t = ((IList)userChoose).Contains(mR[i]);
                    if (t)
                    {
                        zhong.Add(mR[i]);
                        ruleStr = ruleStr + "evidence('" + mR[i].ToString() + "')";
                    }
                    if (t && i == mR.Count - 2)
                    {
                        for (int j = 0; j < zhong.Count; j++)
                        {
                           userChoose.Remove(zhong[j]);
                        }
                        userChoose.Add(mR[mR.Count - 1]);
                        if (mR[mR.Count - 1].ToString() == "火箭队" || mR[mR.Count - 1].ToString() == "雷霆队" || mR[mR.Count - 1].ToString() == "骑士队" || mR[mR.Count - 1].ToString() == "热火队" || mR[mR.Count - 1].ToString() == "勇士队")
                        {
                            ruleStr = "itIs('" + mR[mR.Count - 1].ToString() + "'):-" + ruleStr;
                        }
                        else
                        {
                            ruleStr = "starIs(" + mR[mR.Count - 1].ToString() + "):-" + ruleStr;
                        }
                        zhong.Clear();
                        ruleStrs = ruleStrs +"\n"+ ruleStr ;
                    }
                    if (!t)
                    {
                        zhong.Clear();
                        ruleStr = "";
                        break;
                    }
                }
            }


            if (userChoose.Count > 0 && userChoose[0].ToString() != mRule1[mRule1.Count - 1].ToString() && userChoose[0].ToString() != mRule2[mRule2.Count - 1].ToString() && userChoose[0].ToString() != mRule3[mRule3.Count - 1].ToString() && userChoose[0].ToString() != mRule4[mRule4.Count - 1].ToString() && userChoose[0].ToString() != mRule5[mRule5.Count - 1].ToString() && userChoose[0].ToString() != mRule6[mRule6.Count - 1].ToString() && userChoose[0].ToString() != mRule7[mRule7.Count - 1].ToString() && userChoose[0].ToString() != mRule8[mRule8.Count - 1].ToString() && userChoose[0].ToString() != mRule9[mRule9.Count - 1].ToString() && userChoose[0].ToString() != mRule10[mRule10.Count - 1].ToString() && userChoose[0].ToString() != mRule11[mRule11.Count - 1].ToString())
            {
                textBox1.Text = "条件不足，识别球星无法确定！";
                richTextBox2.Text = ruleStrs +"\n 属性错误！识别失败！";
            }

            else if (userChoose.Count <= 0)
            {
                textBox1.Text = "条件不足，识别球星无法确定！";
                richTextBox2.Text = ruleStrs + "\n 属性错误！识别失败！";
            }

            else
            {
                textBox1.Text = userChoose[userChoose.Count - 1].ToString();
                richTextBox2.Text = ruleStrs;
            }

        }

        

        /**
         * 反向推导
         * **/
        public void BDerivation()
        {
            string startName = textBox1.Text;
            ArrayList attributes = new ArrayList();
            ArrayList mRuleAll = new ArrayList() { mRule1, mRule2, mRule3, mRule4, mRule5, mRule5, mRule6, mRule7, mRule8, mRule9, mRule10, mRule11 };
            for (int k = mRuleAll.Count-1; k >=0; k--)
            {
                ArrayList mR = (ArrayList)mRuleAll[k];
                if (mR[mR.Count - 1].ToString() == startName)
                {
                    for (int i = mR.Count - 2; i >=0; i--)
                    {
                        attributes.Add(mR[i]);
                    }
                    startName = mR[0].ToString();
                }
            }
            listBox1.Items.Clear();
            if (attributes.Count <= 0)
            {

            }
            for (int j = 0; j < attributes.Count; j++)
            {
                listBox1.Items.Add(attributes[j]);
            }
            attributes.Clear();
        }

        /**
         * 正向推理 按钮
         * **/
        private void button1_Click(object sender, EventArgs e)
        {
            // 循环 listbox1 中 item
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                // 添加 item 进入用户选择数组
                userChoose.Add(listBox1.Items[i].ToString());
            }
            // 调用推断算法
            FDeduction();
            // 清空用户选择数组
            userChoose.Clear();
        }


        /**
         * 反向推导 按钮
         * **/
        private void button2_Click(object sender, EventArgs e)
        {
            BDerivation();
        }

        /**
         * 刷新
         * **/
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            listBox1.Items.Clear();
            richTextBox2.Text = "";
            foreach (Control c in groupBox1.Controls)//遍历groupBox1内的所有控件
            {
                if (c is CheckBox)//只遍历CheckBox控件
                {
                    ((CheckBox)c).Checked = false;
                }
            }
        }


        /**
        * 用户选择触发事件
        * 当用户选择 checkbox 时触发
        * **/
        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            // 判断 checkbox 的选中状态 
            if (((CheckBox)sender).CheckState.ToString() == "Checked")
            {
                // 如果选中，将选择的文本添加到listbox列表中
                listBox1.Items.Add(((CheckBox)sender).Text);
            }
            else
            {
                // 如果没选中，移除
                listBox1.Items.Remove(((CheckBox)sender).Text);
            }
        }


        /**
         * 窗体关闭命令
         * **/
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /**
         * 窗体加载事件
         * 给所有的checkbox添加事件
         * **/
        private void Form2_Load(object sender, EventArgs e)
        {
            foreach (Control c in groupBox1.Controls)//遍历groupBox1内的所有控件
            {
                if (c is CheckBox)//只遍历CheckBox控件
                {
                    ((CheckBox)c).CheckStateChanged += new EventHandler(chk_CheckedChanged);
                }
            }

        }



    }

}
