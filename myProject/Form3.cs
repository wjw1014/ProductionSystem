using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myProject
{
    public partial class Form3 : Form
    {

        public string str = "";

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string str)
        {
            InitializeComponent();
            this.str = str;
            this.Text = "设置 " + this.str + " 谓词   ©wjw";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this.str);
            f2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
