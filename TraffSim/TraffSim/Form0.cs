using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TraffSim
{
    public partial class Form0 : Form
    {
        // mean=3 cars/minute, simulate over 10 minutes
        int mean;
        int min;

        Form1 f1;
        Form2 f2;
        Form3 f3;

        public Form0()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            mean = int.Parse(textBox2.Text);
            min = int.Parse(textBox1.Text);

            if (radioButton1.Checked == true)
            {
                f1 = new Form1(mean, min);
                f1.Show();
                //this.Hide();
                return;
            }
            else if (radioButton2.Checked == true)
            {
                f2 = new Form2(mean, min);
                f2.Show();
                //this.Hide();
                return;
            }
            else if (radioButton3.Checked == true)
            {
                f3 = new Form3(mean, min);
                f3.Show();
                //this.Hide();
                return;
            }
            else
            {
                MessageBox.Show("Please select one of the roads above");
            }
        }

    }
}
