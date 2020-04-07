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
using Microsoft.Win32;
using System.Net;
using System.Json;
using System.Text.Json;
namespace advanced_ckey
{
    public partial class Formlog : Form
    {
        public Formlog()
        {
            // don't show if already logged in
            if( Properties.Settings.Default.auth_token != String.Empty )
            {

            //    this.initTimer();
            //    this.startLogging();
            //    return;
            }
            //  What do you think you are doing?
            InitializeComponent();

        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

      

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtweb.Text == "workplace.comeriver.com")
            { 
            //    MessageBox.Show("Address Missing");
            //    txtweb.Focus();
            //    return;
            }
                if (txtname.Text == "e.g example@gmail.com")
                {
                    MessageBox.Show("Email Missing");
                    txtname.Focus();
                    return;
                }
                if (txtpass.Text == "******")
                {
                    MessageBox.Show("Password Missing");
                    txtpass.Focus();
                    return;
                }

            this.Cursor = Cursors.WaitCursor;
            button1.Enabled = false;
            try
            {
                string URI = "https://" + txtweb.Text + "/widgets/Workplace_Authenticate?pc_widget_output_method=JSON";
                string myParameters = "email=" + txtname.Text + "&password=" + txtpass.Text;

                using (WebClient wc = new WebClient())
                {
                    Properties.Settings.Default.username = txtname.Text;
                    Properties.Settings.Default.password = txtpass.Text;
                    Properties.Settings.Default.weburl = txtweb.Text;
                    Properties.Settings.Default.Save();
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string jsonResponse = "{}";
                    string message = "Logging in into Workplace";
                    Program.GetSignInForm().displayNotification(message);

                    try
                    {
                        jsonResponse = wc.UploadString(URI, myParameters);
                    }
                    catch( Exception )
                    {
                        message = "There seem to be a connection error. Ensure you have a working internet connection and try again";
                        Program.GetSignInForm().displayNotification(message);
                        Cursor = Cursors.Default;
                        button1.Enabled = true;
                        MessageBox.Show( message );
                        return;
                    }

                    dynamic result = JsonValue.Parse( "{}" );
                    try
                    {
                        result = JsonValue.Parse(jsonResponse);

                     //  MessageBox.Show("This is showing the result information" + result);
                    }
                    catch (Exception)
                    {
                        message = "There seem to be a server error. Please try again later or contact support.";
                        Program.GetSignInForm().displayNotification(message);
                        Cursor = Cursors.Default;
                        button1.Enabled = true;
                        MessageBox.Show( message );
                        return;
                    }

                    try
                    {
                        helper.versioning(result);

                        if( result.ContainsKey( "badnews" ) )
                        {
                            //  wrong username or password
                            MessageBox.Show( result["badnews"] );
                            Program.GetSignInForm().displayNotification( result["badnews"] );
                            Cursor = Cursors.Default;
                            button1.Enabled = true;
                            return;
                        }

                        if ( result.ContainsKey( "auth_token" ) )
                        {
                            Properties.Settings.Default.auth_token = result["auth_token"];
                            Properties.Settings.Default.user_id = result["user_id"];
                            Properties.Settings.Default.Save();
                            helper.iswaiting = false;

                           

                            //  set workspaces
                            if( result.ContainsKey( "workspaces" ) )
                            {
                                foreach( var x in result["workspaces"] )
                                {
                                    foreach ( var workspace in x )
                                    {
                                        var a = string.Join("", workspace.Value) + " (Team ID " + workspace.Key + ")";
                                        myWorkspaces.Items.Add( a );
                                        workspaces[a] = workspace.Key;
                                    }
                                }
                                Properties.Settings.Default.workspaces = JsonSerializer.Serialize( workspaces );
                                Properties.Settings.Default.Save();

                            }

                            //marks this place
                            try
                            {
                                if (workspaces.Count == 0)
                                {
                                    message = "Error! You don't have any confirmed workspace invitations on your account. Let your team leader create a workspace on " + Properties.Settings.Default.weburl + " and send you an invitation to " + Properties.Settings.Default.username;
                                    Program.GetSignInForm().displayNotification(result["badnews"]);
                                    Cursor = Cursors.Default;
                                    button1.Enabled = true;
                                    MessageBox.Show(message);
                                    return;

                                }
                            }catch(Exception)
                            {
                              //  MessageBox.Show(ex.Message);
                            }
                          
                            if (result.ContainsKey("interval"))
                            {
                                Program.GetSignInForm().Sceenshot_timer.Interval = ( result["interval"] ? result["interval"] : 60 ) * 1000;
                            }
                           

                            Cursor = Cursors.Default;
                            button1.Enabled = true;
                            panel7.Visible = true;
                            message = "You have successfully logged into Workplace";
                            Program.GetSignInForm().displayNotification(message);

                        }
                        else
                        {
                            message = "We couldn't authenticate with the information you provided. Please contact support.";
                            Program.GetSignInForm().displayNotification(message);
                            Cursor = Cursors.Default;
                            button1.Enabled = true;
                            MessageBox.Show( message );
                            return;
                        }
                    }                     
                    catch (Exception r )
                    {
                        Cursor = Cursors.Default;
                        button1.Enabled = true;
                        Console.Write(r.Message);
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                button1.Enabled = true;
                Console.Write( ex.Message );
                return;
            }


        //    this.Hide();
        //    sign_in log = new sign_in();
        //    log.ShowDialog();
        //    this.Close(); 

        }

        public Dictionary<string, string> workspaces = new Dictionary<string, string>();
        public List<string> workspacesToGo = new List<string>();

        private void startLogging()
        {
            Form1 keylog = new Form1();
            keylog.Show();
            keylog.Hide();
            timer1.Stop();
        }

        private void Formlog_Load(object sender, EventArgs e)
        {
            helper.iswaiting = true;
            startup();

           // Properties.Settings.Default.Reset();
            if (string.IsNullOrEmpty(Properties.Settings.Default.auth_token))
            {
                panel7.Visible = false;
                string message = "Login into your workspace with your Workplace email and password";
                Program.GetSignInForm().displayNotification(message);
            }
            else
            {
                panel7.Visible = true;

                string message = "You are now logged into Workplace. You may now start a work session.";
                Program.GetSignInForm().displayNotification(message);

                this.setClockButtons();

                //  the code for loading the workplaces can be shared here......
                workspaces = JsonSerializer.Deserialize<Dictionary<String, String>>( Properties.Settings.Default.workspaces );
                foreach( var w in workspaces )
                {
                    myWorkspaces.Items.Add( w.Key );
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Are you sure?", "Exit Comeriver Workplace", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                string message = "Workplace is now closing.";
                Program.GetSignInForm().displayNotification(message);
                // this.Close();
                Application.Exit();
            }
            
        }

        private void Panel2_Click(object sender, EventArgs e)
        {
            txturl.Focus();
        }

        private void Panel3_Click(object sender, EventArgs e)
        {
            txtuname.Focus();
        }

        private void Panel4_Click(object sender, EventArgs e)
        {
            txtpass.Focus();
        }     

        private void Txtpass_Enter_1(object sender, EventArgs e)
        {
            if(txtpass.Text == "******")
            {
                txtpass.Clear();
                txtpass.ForeColor = Color.Black;
            }
            if (txtpass.Text != "******")
            {
                txtpass.ForeColor = Color.Black;
            }
            else { txtpass.ForeColor = Color.Silver; }
          
        }

        private void Txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "******";
            }
            if (txtpass.Text != "******") { txtpass.ForeColor = Color.Black; } else { txtpass.ForeColor = Color.Silver; }
            
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true && txtpass.UseSystemPasswordChar == true)
            {
                txtpass.UseSystemPasswordChar = false;
            }
            else
            {
                txtpass.UseSystemPasswordChar = true;
            }
         
        }

        private void Txtweb_Enter(object sender, EventArgs e)
        {

            if (txtweb.Text == "workplace.comeriver.com")
            {
                txtweb.Clear();
            }
            if (txtweb.Text != "workplace.comeriver.com")
            {
                txtweb.ForeColor = Color.Black;
            }
            else { txtweb.ForeColor = Color.Silver; }

        }

        private void Txtweb_Leave(object sender, EventArgs e)
        {

            if (txtweb.Text == "")
            {
                txtweb.Text = "workplace.comeriver.com";
            }
            if (txtweb.Text != "workplace.comeriver.com" ){ txtweb.ForeColor = Color.Black; } else { txtweb.ForeColor = Color.Silver; }
        }

        private void Txtname_Enter(object sender, EventArgs e)
        {
            if (txtname.Text == "e.g example@gmail.com")
            {
                txtname.Clear();
            }
            if (txtname.Text != "e.g example@gmail.com")
            {
                txtname.ForeColor = Color.Black;
            }
            else { txtname.ForeColor = Color.Silver; }

        }

        private void Txtname_Leave(object sender, EventArgs e)
        {
            if (txtname.Text == "")
            {
                txtname.Text = "e.g example@gmail.com";
            }
            if (txtname.Text != "e.g example@gmail.com" ){ txtname.ForeColor = Color.Black; } else { txtname.ForeColor = Color.Silver; }
        }

        private void Panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Panel6_Click(object sender, EventArgs e)
        {
            txtpass.Focus();
        }

        private void Panel5_Click(object sender, EventArgs e)
        {
            txtname.Focus();
        }

        private void Panel4_Click_1(object sender, EventArgs e)
        {
            txtweb.Focus();
        }

      public  void startup() 
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey StartupPath;
            StartupPath = rk.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (StartupPath.GetValue("Workplace") == null)
            {
                StartupPath.SetValue("Workplace", Application.ExecutablePath.ToString(), RegistryValueKind.ExpandString);
            }

            txtpass.Text = Properties.Settings.Default.password != string.Empty ? Properties.Settings.Default.password : "******";


            txtname.Text = Properties.Settings.Default.username != string.Empty ? Properties.Settings.Default.username : "e.g example@gmail.com";

            //RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //reg.SetValue("Workplace", Application.ExecutablePath.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // startup();
        }

        public void setClockButtons()
        {
            if (helper.islogged == false)
            {
                label2.Text = "Start your work session";
                btnstartsession.Text = "Start Session";
            }
            else
            {
                label2.Text = "Your work session is on";
                btnstartsession.Text = "Clock Out";
            }
        }

        public void toggleClockButtons()
        {
            if (helper.islogged == true)
            {
                string message = "Your work session has now ended";
                Program.GetSignInForm().displayNotification(message);
                helper.islogged = false;
            }
            else
            {
                string message = "You have just started a work session in the selected workspaces.";
                if ( !workspacesToGo.Any() )
                {
                    message = "Please select a workplace to join for team work";
                    Program.GetSignInForm().displayNotification(message);
                    MessageBox.Show( message );
                    return;
                }

                Program.GetSignInForm().displayNotification(message);
                helper.islogged = true;
            }
            setClockButtons();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            toggleClockButtons();
        //    this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string message = "Workplace is still running. Click the icon here to bring back the screen.";
            Program.GetSignInForm().displayNotification(message);
            this.Hide();
        }

        private void MyWorkspaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.workspacesToGo.Clear();
            for (int i = 0; i < myWorkspaces.Items.Count; i++)
            {
                if (myWorkspaces.GetItemChecked(i))
                {
                    string str = (string)myWorkspaces.Items[i];
                //    MessageBox.Show(str);
                    if( workspaces.ContainsKey( str ) )
                    {
                        string dic = this.workspaces[str];
                        workspacesToGo.Add( dic );
                    }
                }
            }
            Properties.Settings.Default.workspaces_to_go = JsonSerializer.Serialize( this.workspacesToGo );
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        public void LogOutNow()
        {
            Properties.Settings.Default.auth_token = String.Empty;
            Properties.Settings.Default.user_id = String.Empty;
            Properties.Settings.Default.Save();

            helper.islogged = false;
            string message = "You have successfully logged out of Workplace";
            Program.GetSignInForm().displayNotification(message);

        }

        public void LogOut(object sender, EventArgs e)
        {
            LogOutNow();
            panel7.Visible = false;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor =  Color.White;
            button1.ForeColor = Color.Goldenrod;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.Goldenrod;
            button1.ForeColor = Color.White;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.Goldenrod;
            button3.ForeColor = Color.White;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Goldenrod;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            btnstartsession.BackColor = SystemColors.Control;
            btnstartsession.ForeColor = Color.Goldenrod;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            btnstartsession.BackColor = Color.Goldenrod;
            btnstartsession.ForeColor = Color.White;
        }

        private void btnhide_MouseHover(object sender, EventArgs e)
        {
            btnhide.ForeColor = Color.White;
        }

        private void btnhide_MouseLeave(object sender, EventArgs e)
        {
            btnhide.BackColor = SystemColors.Control;
            btnhide.ForeColor = Color.Goldenrod;
        }

        private void btnlogout_MouseHover(object sender, EventArgs e)
        {
            btnlogout.ForeColor = Color.Goldenrod;
        }

        private void btnlogout_MouseLeave(object sender, EventArgs e)
        {
            btnlogout.ForeColor = Color.White;
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                visitLink();
            }
            catch(Exception)
            {
                MessageBox.Show("Unable to open link that was clicked.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void visitLink()
        {
            lblsupport.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.workplace.comeriver.com");
        }
    }
}

