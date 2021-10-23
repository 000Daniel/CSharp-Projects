using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Dotnet_simple_Compiler
{
    public partial class Form1 : Form
    {
        public static Form1 instance;

        public bool dark_mode = true;
        public bool resizing_now = false;
        public bool edited_text = true;
        private bool holdingMouse = false;
        public string old_code = "";
        static List<string> rtextBoxLines = new List<string>();

        private Color dm_default = Color.FromArgb(255, 255, 255);
        private Color dm_library = Color.FromArgb(37,125,188);
        private Color dm_string = Color.FromArgb(255,163,30);
        private Color dm_ifelse = Color.FromArgb(242, 53, 87);
        private Color dm_static = Color.FromArgb(140, 186, 219);
        private Color dm_void = Color.FromArgb(112, 97, 175);
        private Color dm_parentheses = Color.FromArgb(111, 198, 160);

        private Color lm_default = Color.FromArgb(35, 35, 35);
        private Color lm_library = Color.FromArgb(27, 115, 178);
        private Color lm_string = Color.FromArgb(201, 89, 0);
        private Color lm_ifelse = Color.FromArgb(192, 3, 37);
        private Color lm_static = Color.FromArgb(70, 116, 149);
        private Color lm_void = Color.FromArgb(82, 67, 145);
        private Color lm_parentheses = Color.FromArgb(57, 96, 69);

        private int colorUpdate_timer = 0;
        private bool colors_upToDate = false;
        private int spaces_in_current_line = 0;

        string savefile_name = "";
        string full_string = "";
        string name_only = "";
        public string path_only = "";

        public string compile_to_path = "Compiled";
        public int compile_framwork = 0;
        public int compile_runtime = 0;
        public bool compile_independent = true;

        public Form1()
        {
            InitializeComponent();
            instance = this;
        }

        public void ResizeElements(object sender, EventArgs e)
        {
            resizing_now = true;

            if (this.Width > 30)
            {
                richTextBox1.Width = this.Width - 40;
                panel1.Width = this.Width;
            }
            if (this.Width > 656) //544
            { 
                pictureBox1.Location = new Point(this.Width - 79, 18);
            }

            if (this.Height > 30)
                richTextBox1.Height = this.Height - 128;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeElements(sender, e);
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            resizing_now = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DarkMode(sender, e);
        }

        public void DarkMode(object sender, EventArgs e)
        {
            if (dark_mode)
            {
                pictureBox1.Image = Properties.Resources.Slider_LightMode;
                dark_mode = !dark_mode;

                this.BackColor = Color.FromArgb(240, 240, 240);
                panel1.BackColor = Color.FromArgb(202, 202, 202);
                richTextBox1.BackColor = Color.FromArgb(222, 222, 222);
                richTextBox1.ForeColor = lm_default;

                button1.FlatAppearance.BorderColor = Color.Black;
                button2.FlatAppearance.BorderColor = Color.Black;
                button3.FlatAppearance.BorderColor = Color.Black;
                button4.FlatAppearance.BorderColor = Color.Black;
                button5.FlatAppearance.BorderColor = Color.Black;
                button1.ForeColor = Color.White;
                button2.ForeColor = Color.White;
                button1.BackColor = Color.FromArgb(60, 60, 60);
                button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(40,40,40);
                button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(50,50,50);
                button2.BackColor = Color.FromArgb(60, 60, 60);
                button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(40, 40, 40);
                button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            } else
            {
                pictureBox1.Image = Properties.Resources.Slider_DarkMode;
                dark_mode = !dark_mode;

                this.BackColor = Color.FromArgb(35, 35, 35);
                panel1.BackColor = Color.FromArgb(55, 55, 55);
                richTextBox1.BackColor = Color.FromArgb(30, 30, 30);
                richTextBox1.ForeColor = dm_default;

                button1.FlatAppearance.BorderColor = Color.White;
                button2.FlatAppearance.BorderColor = Color.White;
                button3.FlatAppearance.BorderColor = Color.White;
                button4.FlatAppearance.BorderColor = Color.White;
                button5.FlatAppearance.BorderColor = Color.White;
                button1.ForeColor = Color.Black;
                button2.ForeColor = Color.Black;
                button1.BackColor = Color.FromArgb(100, 100, 100);
                button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
                button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 90, 90);
                button2.BackColor = Color.FromArgb(100, 100, 100);
                button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(80, 80, 80);
                button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 90, 90);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.AutoWordSelection = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "New Project - C Sharp|*.cs";
            saveFileDialog1.Title = "Create new C# project here";
            saveFileDialog1.ShowDialog();

            savefile_name = saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf("\\") + 1);

            if (savefile_name != "Program" && savefile_name != "Program.cs")
            {
                if (saveFileDialog1.FileName != "" && savefile_name != "")
                {
                    FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                    switch (saveFileDialog1.FilterIndex)
                    {
                        case 1:
                            full_string = Path.GetFullPath(saveFileDialog1.FileName);

                            name_only = full_string.Substring(full_string.LastIndexOf("\\") + 1); // full_string.IndexOf(".cs")
                            path_only = full_string.Substring(0, full_string.LastIndexOf("\\"));

                            string tempname_only = name_only.Substring(0, name_only.IndexOf(".cs"));
                            string cmd_create_command = "dotnet new console --output \"" + path_only + "\"  --name \"" + tempname_only + "\" --force";

                            Process process = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo();

                            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            startInfo.FileName = "cmd.exe";
                            startInfo.Arguments = "/c " + cmd_create_command;

                            process.StartInfo = startInfo;
                            process.Start();

                            fs.Close();
                            int counter = 0;
                            while (!File.Exists(path_only + "\\Program.cs") && counter < 4)
                            {
                                Thread.Sleep(1000);
                                counter++;
                            }
                            if (File.Exists(path_only + "\\Program.cs"))
                            {
                                richTextBox1.Text = File.ReadAllText(path_only + "\\Program.cs");
                                old_code = richTextBox1.Text;
                                name_only = "Program.cs";
                                this.Text = "Simple C# IDE (Program.cs)";
                            }

                            if (File.Exists(full_string) && full_string.Substring(full_string.LastIndexOf("\\") + 1) != "Program.cs")
                            {
                                File.Delete(full_string);
                            }
                            break;
                    }
                    fs.Close();
                }
            } else
            {
                MessageBox.Show("Error! please enter a valid name.", "Project creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = ".Net - C Sharp|*.cs";
            OpenFileDialog1.Title = "Open C# file here";
            OpenFileDialog1.ShowDialog();

            if (OpenFileDialog1.FileName != "")
            {
                FileStream fs = (FileStream)OpenFileDialog1.OpenFile();
                switch (OpenFileDialog1.FilterIndex)
                {
                    case 1:
                        full_string = Path.GetFullPath(OpenFileDialog1.FileName);
                        
                        name_only = full_string.Substring(full_string.LastIndexOf("\\") + 1);
                        path_only = full_string.Substring(0, full_string.LastIndexOf("\\"));

                        fs.Close();
                        int counter = 0;
                        while (!File.Exists(path_only + "\\" + name_only) && counter < 4)
                        {
                            Thread.Sleep(1000);
                            counter++;
                        }
                        if (File.Exists(path_only + "\\" + name_only))
                        {
                            richTextBox1.Text = File.ReadAllText(path_only + "\\" + name_only);
                            old_code = richTextBox1.Text;
                            this.Text = "Simple C# IDE (" + name_only + ")";
                        }

                        break;
                }
                edited_text = false;
                fs.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFile(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (full_string == "" || name_only == "" || path_only == "" || path_only == null)
            {
                MessageBox.Show("Error! please create or open a project to compile.", "Run Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (edited_text)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to continue without saving?", "Run Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            string cmd_create_command = "dotnet run --project \"" + path_only + "\"  --name \"" + name_only + "\"";
            Process.Start("cmd", "/k " + cmd_create_command + " && echo ------------------------------------------------------------------------ && echo Debug session for '" + name_only + "' was launched and or started successfully! && echo With the 'Dotnet Simple IDE' developed by 000Daniel (GitHub). && pause && exit");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (path_only == "" || path_only == null)
            {
                MessageBox.Show("Error! please create or open a project to compile.", "Compiler Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (edited_text)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to continue without saving?", "Compiler Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (Application.OpenForms.OfType<Form2>().Any())
            {
                Application.OpenForms.OfType<Form2>().First().BringToFront();
            }
            else
            {
                Form2 frm2 = new Form2();
                frm2.Show();
            }
        }

        public void saveFile(object sender, EventArgs e)
        {
            if (full_string == "" || name_only == "" || path_only == "")
            {
                MessageBox.Show("Error! please create or open a project to compile.", "Saving Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1_Click(sender, e);
                return;
            }

            if (name_only.LastIndexOf(".cs") <= 0)
            {
                name_only = name_only + ".cs";
            }

            FileStream fParameter = new FileStream(path_only + "\\" + name_only, FileMode.Create, FileAccess.Write);
            StreamWriter m_WriterParameter = new StreamWriter(fParameter);
            m_WriterParameter.BaseStream.Seek(0, SeekOrigin.End);
            m_WriterParameter.Write(richTextBox1.Text);
            m_WriterParameter.Flush();
            m_WriterParameter.Close();

            edited_text = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (old_code != richTextBox1.Text)
            {
                edited_text = true;
            }
            old_code = richTextBox1.Text;

            colors_upToDate = false;
            colorUpdate_timer = 0;
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            holdingMouse = true;
            colorUpdate_timer = -1;
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            holdingMouse = false;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveFile(sender, e);
            }
        }
        
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                e.Handled = true;
                richTextBox1.SelectedText = new string(' ', 4);
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                richTextBox1.SelectedText = new string(' ', spaces_in_current_line);
            }
        }

        void colorCodeUpdate(object sender, EventArgs e,string start_word,string last_word,Color clr,int state)
        {
            int index_start = 0;
            int index_end = 0;
            int char_passed = 0;

            for (int i = 0; i < richTextBox1.Lines.Count(); i++)
            {
                string temp_lineText = richTextBox1.Lines[i].ToString();
                int temp_charpass = 0;

                while (temp_lineText.Length > 2)
                {
                    if (temp_lineText.ToLower().Contains(start_word) && temp_lineText.ToLower().Contains(last_word))
                    {
                        if ((temp_lineText.ToLower().IndexOf(start_word) < temp_lineText.ToLower().IndexOf(last_word)) || state == 2)
                        {
                            string temp_lineTextShort = temp_lineText.Substring(temp_lineText.ToLower().IndexOf(start_word));
                            index_end = temp_lineTextShort.ToLower().IndexOf(last_word) + 1;
                            if (char_passed <= 0 && temp_charpass <= 0)
                            {
                                index_start = temp_lineText.ToLower().IndexOf(start_word);

                                if (state == 0)
                                {
                                    richTextBox1.Select(index_start, index_end);
                                }
                                if (state == 1)
                                {
                                    richTextBox1.Select(index_start, start_word.Length);
                                }
                                if (state == 2)
                                {
                                    temp_lineText = temp_lineText.Substring(temp_lineText.ToLower().IndexOf(start_word) + 1);
                                    temp_charpass = richTextBox1.Lines[i].Length - temp_lineText.Length;
                                    index_end = temp_lineTextShort.ToLower().IndexOf(last_word) + 1;

                                    richTextBox1.Select(index_start, index_end);
                                }

                            }
                            else
                            {
                                index_start = temp_lineText.ToLower().IndexOf(start_word) + char_passed + temp_charpass - 1;

                                if (state == 0)
                                {
                                    richTextBox1.Select(index_start + 1, index_end);
                                }
                                if (state == 1)
                                {
                                    richTextBox1.Select(index_start + 1, start_word.Length);
                                }
                                if (state == 2)
                                {
                                    temp_lineText = temp_lineText.Substring(temp_lineText.ToLower().IndexOf(start_word) + 1);
                                    temp_charpass = richTextBox1.Lines[i].Length - temp_lineText.Length;
                                    index_end = temp_lineText.ToLower().IndexOf(last_word) + 2;

                                    richTextBox1.Select(index_start + 1, index_end);
                                }
                            }

                            richTextBox1.SelectionColor = clr;
                            richTextBox1.Select(index_start + index_end + char_passed + temp_charpass + 1, 1);

                            if (state == 4)
                            {
                                if (dark_mode)
                                {
                                    richTextBox1.SelectionColor = dm_default;
                                }
                                else
                                {
                                    richTextBox1.SelectionColor = lm_default;
                                }
                            }

                            temp_lineText = temp_lineText.Substring(temp_lineText.ToLower().IndexOf(last_word) + 1);
                            temp_charpass = richTextBox1.Lines[i].Length - temp_lineText.Length;
                        }
                        else
                        {
                            temp_lineText = "";
                        }
                    }
                    else
                    {
                        temp_lineText = "";
                    }
                }
                char_passed += richTextBox1.Lines[i].Length + 1;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (edited_text && name_only != "")
            {
                DialogResult dialog = MessageBox.Show("Are you sure you want to exit without saving?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form2>().Any())
            {
                if (Form.ActiveForm == this)
                {
                    System.Media.SystemSounds.Hand.Play();
                    Application.OpenForms.OfType<Form2>().First().BringToFront();
                    Application.OpenForms.OfType<Form2>().First().TopMost = true;
                }

                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                pictureBox1.Enabled = false;
            } else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                pictureBox1.Enabled = true;
            }
        }

        private void timer_color_update_Tick(object sender, EventArgs e)
        {
            if (colorUpdate_timer < 2)
            {
                colorUpdate_timer++;
            } else if (!holdingMouse && !colors_upToDate)
            {
                colorUpdate_timer = -1;

                int corrent_selection = richTextBox1.SelectionStart;
                int corrent_selection_length = richTextBox1.SelectionLength;
                richTextBox1.SelectAll();
                

                if (dark_mode)
                {
                    richTextBox1.SelectionColor = dm_default;
                    colorCodeUpdate(sender, e, "using", ";", dm_library, 0);
                    colorCodeUpdate(sender, e, "static", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "int", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "string", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "bool", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "long", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "float", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "char", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "double", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "decimal", ";", dm_static, 0);
                    colorCodeUpdate(sender, e, "if", "(", dm_ifelse, 1);
                    colorCodeUpdate(sender, e, "else", "{", dm_ifelse, 1);
                    colorCodeUpdate(sender, e, "else", "(", dm_ifelse, 1);
                    colorCodeUpdate(sender, e, "void", "(", dm_void, 1);
                    colorCodeUpdate(sender, e, "(", ")", dm_parentheses, 2);
                    colorCodeUpdate(sender, e, "\"", "\"", dm_string, 2);
                } else
                {
                    richTextBox1.SelectionColor = lm_default;
                    colorCodeUpdate(sender, e, "using", ";", lm_library, 0);
                    colorCodeUpdate(sender, e, "static", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "int", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "string", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "bool", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "long", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "float", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "char", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "double", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "decimal", ";", lm_static, 0);
                    colorCodeUpdate(sender, e, "if", "(", lm_ifelse, 1);
                    colorCodeUpdate(sender, e, "else", "{", lm_ifelse, 1);
                    colorCodeUpdate(sender, e, "else", "(", lm_ifelse, 1);
                    colorCodeUpdate(sender, e, "void", "(", lm_void, 1);
                    colorCodeUpdate(sender, e, "(", ")", lm_parentheses, 2);
                    colorCodeUpdate(sender, e, "\"", "\"", lm_string, 2);
                }

                colors_upToDate = true;
                richTextBox1.Select(corrent_selection, corrent_selection_length);
            }
        }

        private void timer_spaceCounter_Tick(object sender, EventArgs e)
        {
            spaces_in_current_line = 0;

            if (richTextBox1.Lines.Count() > 0)
            {
                int CurrentLineIndex = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
                string CurrentLine = richTextBox1.Lines[CurrentLineIndex];
                int space_index = -1;

                if (CurrentLine.Contains(" "))
                {
                    space_index = CurrentLine.IndexOf(" ");
                }
                while (space_index == 0 && CurrentLine.Contains(" "))
                {
                    CurrentLine = CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1);
                    spaces_in_current_line++;
                    space_index = CurrentLine.IndexOf(" ");
                }
            }
        }
    }
}
