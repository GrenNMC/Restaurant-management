using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalTermProject
{
    public partial class AdminKeyForm : Form
    {
        public AdminKeyForm()
        {
            InitializeComponent();
        }

        private bool isTrue = false;

        public bool IsTrue { get { return isTrue; } } 

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "QLNH")
            {
                this.Close();
                isTrue = true;
            }
            else
            {
                this.Close();
                isTrue = false;
            }
        }
    }
}
