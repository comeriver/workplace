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
        //    MessageBox.Show( Properties.Settings.Default.auth_token );
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
                    try
                    {
                        jsonResponse = wc.UploadString(URI, myParameters);
                    }
                    catch( Exception )
                    {
                        MessageBox.Show( "There seem to be a connection error. Ensure you have a working internet connection and try again" );
                        return;
                    }
                    //    MessageBox.Show( jsonResponse ); 
                    dynamic result = JsonValue.Parse( "{}" );
                    try
                    {
                        result = JsonValue.Parse(jsonResponse);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show( "There seem to be a server error. Please try again later or contact support." );
                        return;
                    }

                    try
                    {
                        helper.versioning(result);

                        if( result.ContainsKey( "badnews" ) )
                        {
                            //  wrong username or password
                            MessageBox.Show( result["badnews"] );
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
                                        this.myWorkspaces.Items.Add( a );
                                        this.workspaces[a] = workspace.Key;
                                    }
                                }
                                Properties.Settings.Default.workspaces = JsonSerializer.Serialize( this.workspaces );
                                Properties.Settings.Default.Save();

                            }
                            if( this.workspaces.Count() == 0 )
                            {
                                MessageBox.Show( "Error! You don't have any confirmed workspace invitations on your account. Let your team leader create a workspace on " + Properties.Settings.Default.weburl + " and send you an invitation to " + Properties.Settings.Default.username );
                                return;
                            }
                            if (result.ContainsKey("interval"))
                            {
                                Program.GetSignInForm().Sceenshot_timer.Interval = ( result["interval"] ? result["interval"] : 60 ) * 1000;
                                Console.Write(Program.GetSignInForm().Sceenshot_timer.Interval);
                            }

                            panel7.Show();

                        }
                    }                     
                    catch (Exception r )
                    {
                        Console.Write(r.Message);
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
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

                panel7.Hide();
            }
            else
            {
                panel7.Show();

                this.setClockButtons();

                //  the code for loading the workplaces can be shared here......
                this.workspaces = JsonSerializer.Deserialize<Dictionary<String, String>>( Properties.Settings.Default.workspaces );
                foreach( var w in this.workspaces )
                {
                    this.myWorkspaces.Items.Add( w.Key );
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

            this.txtpass.Text = Properties.Settings.Default.password != string.Empty ? Properties.Settings.Default.password : "******";


            this.txtname.Text = Properties.Settings.Default.username != string.Empty ? Properties.Settings.Default.username : "e.g example@gmail.com";

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
                this.label2.Text = "Start your work session";
                button2.Text = "Start Session";
            }
            else
            {
                this.label2.Text = "Your work session is on";
                button2.Text = "Clock Out";
            }
        }

        public void toggleClockButtons()
        {
            if (helper.islogged == true)
            {
                helper.islogged = false;
            }
            else
            {
                if( ! this.workspacesToGo.Any() )
                {
                    MessageBox.Show( "Please select a workplace to join" );
                    return;
                }

                helper.islogged = true;
            }
            this.setClockButtons();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.toggleClockButtons();
        //    this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
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
                    if( this.workspaces.ContainsKey( str ) )
                    {
                        string dic = this.workspaces[str];
                        this.workspacesToGo.Add( dic );
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
            this.Close();

        }

        public void LogOut(object sender, EventArgs e)
        {
            this.LogOutNow();
        }
    }
}

