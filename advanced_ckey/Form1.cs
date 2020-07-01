﻿using System;
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
        public bool processingSavedRequests = false;


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
                this.loggedText.Clear();
                try
                {
                    //string URI = "http://" + txtweb.Text + "/widgets/Workplace_Authenticate?pc_widget_output_method=JSON";
                    string URI = "http://" + Properties.Settings.Default.weburl + "/widgets/Workplace_Log?pc_widget_output_method=JSON";

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
                            Program.online = true;
                            jsonResponse = Encoding.UTF8.GetString(responseBytes);
                          //  MessageBox.Show( jsonResponse );

                        }
                        catch ( Exception g )
                        {
                            if( Program.online )
                            {
                                displayNotification( "Seems like you are not online. " +
                                    "Your work will continue to be saved and processed when you get back online",
                                   "Now Working Offline" );
                                Program.online = false;
                            }
                            this.savedRequests.Add( paramsX );
                            return;
                        }
                        dynamic result = JsonValue.Parse(jsonResponse);

                        helper.versioning(result);

                        try
                        {
                            if( result.ContainsKey( "authenticated" ) && result["authenticated"] == false )
                            {
                                string message = "Authentication failed. Please login again";
                                displayNotification( message );
                                MessageBox.Show( message );
                                Program.GetClockInForm().LogOutNow();
                                Program.GetSignInForm().Show();
                                return;
                            }
                        }
                        catch (Exception ex )
                        {
                            Console.WriteLine(ex.Message);
                            if (  result.ContainsKey( "authenticated" ) && result["authenticated"] == false )
                            {
                                string message = "Authentication failed. Please login again";
                                displayNotification( message );
                                MessageBox.Show( message );
                                Program.GetClockInForm().LogOutNow();
                                Program.GetSignInForm().Show();
                            }
                            return;
                        }
                        if( this.savedRequests.Any() && processingSavedRequests == false )
                        {
                            processingSavedRequests = true;
                            displayNotification("Seems like you lost connection for a while. " +
                                "Now that you are back online, we will process your " +
                                "" + this.savedRequests.Count() + " saved work requests.",
                               "Welcome Back Online");
                            int ci = 0;
                            foreach (NameValueCollection xParam in this.savedRequests.ToArray() )
                            {
                                try
                                {

                                    using (WebClient wcX = new WebClient())
                                    {
                                        wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                        Byte[] savedResponse = wcX.UploadValues(URI, "POST", xParam);
                                    }
                                    Console.WriteLine("Processed saved request " + ci);
                                }
                                catch (Exception dd)
                                {
                                    Console.WriteLine( "We were unable to process this saved request " + ci );
                                    Console.WriteLine(dd.Message);
                                    break;
                                }
                                var t = this.savedRequests.IndexOf(xParam);
                                try
                                {
                                    this.savedRequests.RemoveAt(t);
                                }
                                catch ( Exception )
                                {
                                    Console.WriteLine( "Could not remove " + t + " or " + ci );
                                }
                                ci++;
                            }
                            displayNotification( "All saved offline work requests have now been processed" );
                            Console.WriteLine("Completed saved requests. Now on " + this.savedRequests.Count() );
                            processingSavedRequests = false;
                        }

                    }

                }
                catch (Exception r )
                {
                    Console.Write(r.Message);
                }
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

        }

         
        public void displayNotification( string text, string title = "Workplace Notification", int time = 1000000 )
        {
            try
            {
                notifyIcon1.Text = "Workplace Session";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = title;
                notifyIcon1.BalloonTipText = text;
                notifyIcon1.ShowBalloonTip( time );
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

        private void timer4_Tick(object sender, EventArgs e)
        {

        }
    }
}

  

