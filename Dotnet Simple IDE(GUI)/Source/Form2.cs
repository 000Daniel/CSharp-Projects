using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Dotnet_simple_Compiler
{
    public partial class Form2 : Form
    {
        public static Form2 instance;

        public Form2()
        {
            InitializeComponent();
            instance = this;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Form1.instance.compile_to_path;             //this loads the user's compile settings or the default settings.

            comboBox1.SelectedItem = comboBox1.Items[Form1.instance.compile_framwork];
            comboBox2.SelectedItem = comboBox2.Items[Form1.instance.compile_runtime];
            checkBox1.Checked = Form1.instance.compile_independent;

            if (Form1.instance.dark_mode)
            {
                this.BackColor = Color.FromArgb(35, 35, 35);
                richTextBox1.BackColor = Color.FromArgb(30, 30, 30);
                richTextBox1.ForeColor = Color.FromArgb(255, 255, 255);
                comboBox1.BackColor = Color.FromArgb(30, 30, 30);
                comboBox1.ForeColor = Color.FromArgb(255, 255, 255);
                comboBox2.BackColor = Color.FromArgb(30, 30, 30);
                comboBox2.ForeColor = Color.FromArgb(255, 255, 255);
                button5.FlatAppearance.BorderColor = Color.White;

                checkBox1.ForeColor = Color.FromArgb(255, 255, 255);
                label1.ForeColor = Color.FromArgb(255, 255, 255);
                label2.ForeColor = Color.FromArgb(255, 255, 255);
                label3.ForeColor = Color.FromArgb(255, 255, 255);
                label4.ForeColor = Color.FromArgb(255, 255, 255);
            } else
            {
                this.BackColor = Color.FromArgb(240, 240, 240);
                richTextBox1.BackColor = Color.FromArgb(222, 222, 222);
                richTextBox1.ForeColor = Color.FromArgb(35, 35, 35);
                comboBox1.BackColor = Color.FromArgb(222, 222, 222);
                comboBox1.ForeColor = Color.FromArgb(35, 35, 35);
                comboBox2.BackColor = Color.FromArgb(222, 222, 222);
                comboBox2.ForeColor = Color.FromArgb(35, 35, 35);
                button5.FlatAppearance.BorderColor = Color.Black;

                checkBox1.ForeColor = Color.FromArgb(35, 35, 35);
                label1.ForeColor = Color.FromArgb(35, 35, 35);
                label2.ForeColor = Color.FromArgb(35, 35, 35);
                label3.ForeColor = Color.FromArgb(35, 35, 35);
                label4.ForeColor = Color.FromArgb(35, 35, 35);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string user_path = richTextBox1.Text;
            string user_framework = comboBox1.Text;
            string user_runtime = comboBox2.Text;
            bool user_selfcontained = checkBox1.Checked;

            Form1.instance.compile_to_path = richTextBox1.Text;             //this saves the user's compile settings.
            Form1.instance.compile_framwork = comboBox1.SelectedIndex;
            Form1.instance.compile_runtime = comboBox2.SelectedIndex;
            Form1.instance.compile_independent = checkBox1.Checked;


            string comp_path = "";
            string def_path = Form1.instance.path_only;

            char[] illigalChar = { '/', '>', '<', '>', '*', '|', '\"' };
            for (int i = 0; i < illigalChar.Length; i++)                    //checks if a file contains any of those illigal characters.
            {
                if (user_path.Contains(illigalChar[i]))
                {
                    MessageBox.Show("Error! the desired Directory contains invalid characters.", "Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (Directory.Exists(user_path) || user_path.Contains(":\\"))
            {
                comp_path = user_path;
            }
            else
            {

                comp_path = def_path + "\\" + user_path;
            }

            if (user_framework != ".Net 5.0")
            {
                MessageBox.Show("Error! the desired Framework isn't supported.", "Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else
            {
                if (user_framework == ".Net 5.0")
                {
                    user_framework = "net5.0";
                }
            }

            if (user_runtime != "win-x64" && user_runtime != "win-x86" && user_runtime != "win-arm" && user_runtime != "win-arm64" && user_runtime != "linux-x64" && user_runtime != "linux-musl-x64" && user_runtime != "linux-arm" && user_runtime != "linux-arm64")
            {
                MessageBox.Show("Error! the desired Runtime setting isn't supported.", "Setting Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string comp_trim = "";
            if (user_selfcontained)         //if the program is dependent dotnet cannot do a 'file timm', so this function enables trimming only in independent mode.
            {
                comp_trim = "-p:PublishTrimmed=True -p:TrimMode=Link ";
            }
                    //the cmd first goes to the required directory and then switches to its disk volume, after that it runs 'dotnet publish...'.
            string cmd_path_command = "cd \"" + def_path + "\"";
            string cmd_diskvolume_command = def_path.Substring(0, def_path.IndexOf(":\\") + 1);
            string cmd_compile_command = String.Format("dotnet publish --output \"{0}\" --self-contained {1} -c Release -r {2} -f {3} {4}-p:PublishSingleFile=true", comp_path, user_selfcontained, user_runtime, user_framework, comp_trim);

            Process process = new Process();

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/k " + cmd_path_command + " && " + cmd_diskvolume_command + " && " + cmd_compile_command + " && start explorer.exe \"" + comp_path + "\" && exit";
            
            process.Start();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var ps = new ProcessStartInfo("https://github.com/000Daniel/")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }
    }
}
