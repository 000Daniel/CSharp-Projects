using System;
using System.Drawing;
using System.Management;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SpecDisplay
{
    public partial class Form1 : Form
    {
        static float RAM_total = 0;
        static String hardWareInfo = " ";


        public const int WM_NCLBUTTONDOWN = 0xA1;                   //those lines are for the window dragging function.
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)             //This fetches and changes all label texts with component's name.
        {
            GetComponent("Win32_OperatingSystem", "Name");              //This gets the OS name.
            label2.Text = hardWareInfo;
            label2.ForeColor = Color.White;
            label2.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            
            GetComponent("Win32_OperatingSystem", "Version");           //OS Version.
            label15.Text = hardWareInfo;
            label15.ForeColor = Color.White;
            label15.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            GetComponent("Win32_ComputerSystem", "Name");               //Device name.
            label13.Text = hardWareInfo;
            label13.ForeColor = Color.White;
            label13.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);


            GetComponent("Win32_Processor", "Name");                    //CPU name.
            label3.Text = hardWareInfo;
            label3.ForeColor = Color.WhiteSmoke;
            label3.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            GetComponent("Win32_VideoController", "Name");              //GPU Name.
            label5.Text = hardWareInfo;
            label5.ForeColor = Color.White;
            label5.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            GetComponent("Win32_PhysicalMemory", "Capacity");           //Total RAM.
            label7.Text = RAM_total + "GB @ " + hardWareInfo;           //and first RAM stick's Speed.
            label7.ForeColor = Color.WhiteSmoke;
            label7.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            GetComponent("Win32_BaseBoard", "Product");                 //Mobo's name/model.
            label11.Text = hardWareInfo;
            label11.ForeColor = Color.WhiteSmoke;
            label11.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            GetComponent("Win32_BaseBoard", "Manufacturer");            //Mobo's manufacturer.
            label9.Text = hardWareInfo;
            label9.ForeColor = Color.WhiteSmoke;
            label9.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }

        protected override void WndProc(ref Message m)      //this function allows you to mouse drag the main window.
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)          //This closes the software.
        {
            Environment.Exit(0);
        }

        private void button2_Click(object sender, EventArgs e)          //This minimizes the software.
        {
            foreach (Form form in Application.OpenForms)
            {
                form.WindowState = FormWindowState.Minimized;
            }
        }

        private void From_MouseMove(object sender, MouseEventArgs e)    //Allows us to move the window with panels, pictures and more.
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)     //Allows us to click the link.
        {
            var ps = new ProcessStartInfo("https://github.com/000Daniel/")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }


        public void GetComponent(string hwclass, string syntax)             //This function stores hardware info in 'hardWareInfo'.
        {                                                                   //'hwclass' is what hardware, 'syntax' is what do we want to know about the hardware.
            RAM_total = 0;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);
            
            foreach (ManagementObject mj in mos.Get())
            {
                if (Convert.ToString(mj[syntax]) != "")
                {

                    if (hwclass == "Win32_PhysicalMemory")              //If we refer to the RAM.
                    {
                        syntax = "Capacity";                            //Coverts all memory from all sticks from bytes to GB.
                        string temp_string = Convert.ToString(mj[syntax]);
                        float temp_int = float.Parse(temp_string, System.Globalization.CultureInfo.InvariantCulture);
                        RAM_total += temp_int / 1073741824;             //Note: 1 Gigabyte = 1073741824 Bytes
                        
                        syntax = "Speed";                               //Reads the first RAM stick's speed.
                        hardWareInfo = Convert.ToString(mj[syntax]) + "MHz";
                    }

                    if (hwclass == "Win32_OperatingSystem" && syntax == "Name")     //If we refer to the OS and its name.
                    {
                        string temp_string = Convert.ToString(mj[syntax]);
                        int temp_index = temp_string.IndexOf("|");                  //This prints only the name of the OS and ignores other info.
                        hardWareInfo = temp_string.Substring(0, temp_index);
                    }

                    else if (hwclass != "Win32_PhysicalMemory")                     //If we DON'T refer to RAM or the OS, store info into 'hardWareInfo' as normal.
                    {
                        hardWareInfo = Convert.ToString(mj[syntax]);                //Note: hardWareInfo is info that's used to in labels to display to the user.
                    }
                }
            }
        }
    }
}
