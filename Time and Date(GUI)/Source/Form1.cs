using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Date_GUI
{
    public partial class Form1 : Form
    {
        static bool topMost_mode = true;

        public Form1()
        {

            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string s_time = dt.ToString("HH:mm") + "." + dt.ToString("ss");
            string s_date = dt.ToString("dd/MM/yyyy");
            label1.Text = String.Format("Time: {0} \nDate: {1}", s_time, s_date);

            if (topMost_mode)
            {
                this.TopMost = true;
            } else
            {
                this.TopMost = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            topMost_mode = !topMost_mode;
            if (topMost_mode)
                pictureBox1.Image = Properties.Resources.Slider_TopMostMode_on;

            if (!topMost_mode)
                pictureBox1.Image = Properties.Resources.Slider_TopMostMode_off;
        }
    }
}
