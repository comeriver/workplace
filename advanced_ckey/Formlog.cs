using System;
using System.Collections.Generic;
using System.Collections;
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
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace advanced_ckey
{
    public partial class Formlog : Form
    {
        public Formlog()
        {
            // don't show if already logged in
            if (Properties.Settings.Default.auth_token != String.Empty)
            {

                  // this.initTimer();
                   this.startLogging();
                  // return;
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

        public string namee = "";
        private void MyWorkspaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.workspacesToGo.Clear();

            for (int i = 0; i < myWorkspaces.Items.Count; i++)
            {
                if (myWorkspaces.GetItemChecked(i))
                {
                    string str = (string)myWorkspaces.Items[i];
                     // MessageBox.Show(str);
                    if (this.workspaces.ContainsKey(str))
                    {

                        string dic = this.workspaces[str];
                      //  label5.Text = dic;
                        namee = this.workspaces[str]; 

                        //  label6.Text = "";
                        this.workspacesToGo.Add(dic);
                        //label6.Text = this.results.Value;




                        try
                        {
                            string URI = "http://" + txtweb.Text + "/widgets/Workplace_Authenticate?pc_widget_output_method=JSON";
                            string myParameters = "email=" + txtname.Text + "&password=" + txtpass.Text;

                            using (WebClient wc = new WebClient())
                            {
                                Properties.Settings.Default.username = txtname.Text;
                                Properties.Settings.Default.password = txtpass.Text;
                                Properties.Settings.Default.weburl = txtweb.Text;
                                Properties.Settings.Default.Save();
                                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                                string jsonResponse = "{}";
                                string message = "";
                                //Program.GetSignInForm().displayNotification(message);
                                try
                                {
                                      jsonResponse = wc.UploadString(URI, myParameters);
                                    Program.online = true;
                                }
                                catch (Exception)
                                {
                                    Program.online = false;
                                    message = "We got an invalid response from the server. Please contact Workplace Support.";
                                    Program.GetSignInForm().displayNotification(message);
                                    MessageBox.Show(message);
                                    return;
                                }
                                 JObject  result = JObject.Parse("{}");
                                try
                                {
                                   
                                  result = JObject.Parse(jsonResponse);
                                    string itemTitle = (string)result["workspaces"][1]["msg"];
                                
                                    var postTitles =from p in result["workspaces"]
                                                    where (string)p["name"]== namee
                                                    select (string)p["msg"];

                                    foreach (var item in postTitles)
                                    {
                                       
                                        
                                        label5.Text = namee;
                                       
                                        textBox1.Text = item;
                                    }

                                }
                                catch (Exception)
                                {
                                    message = "There seem to be a server error. Please try again later or contact support.";
                                    Program.GetSignInForm().displayNotification(message);
                                    MessageBox.Show(message);
                                    return;
                                }

                           
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }

                    }
                   
                }
            }
            Properties.Settings.Default.workspaces_to_go = JsonSerializer.Serialize(this.workspacesToGo);
         
        }
        private void myWorkspaces_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtweb.Text == "localhost/pageCarton")
            {
               
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
                string URI = "http://" + txtweb.Text + "/widgets/Workplace_Authenticate?pc_widget_output_method=JSON";
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
                        
                        Program.online = true;
                    }
                    catch (Exception)
                    {
                        Program.online = false;
                        message = "We got an invalid response from the server. Please contact Workplace Support.";
                        Program.GetSignInForm().displayNotification(message);
                        MessageBox.Show(message);
                        return;
                    }

               
                    dynamic  result  = JsonValue.Parse("{}");
                    try
                    {
                      
                         result = JsonValue.Parse(jsonResponse);
                    }
                    catch (Exception)
                    {
                        message = "There seem to be a server error. Please try again later or contact support.";
                        Program.GetSignInForm().displayNotification(message);
                        MessageBox.Show(message);
                        return;
                    }

                    try
                    {
                        helper.versioning(result);

                        if (result.ContainsKey("badnews"))
                        {
                            //  wrong username or password
                            MessageBox.Show(result["badnews"]);
                            Program.GetSignInForm().displayNotification(result["badnews"]);
                            return;
                        }

                        if (result.ContainsKey("auth_token"))
                        {
                            Properties.Settings.Default.auth_token = result["auth_token"];
                            Properties.Settings.Default.user_id = result["user_id"];
                            Properties.Settings.Default.Save();
                            helper.iswaiting = false;

                            //  set workspaces
                            //  this.workspaces.Clear();
                            var newWorkspaces = false;
                            if (result.ContainsKey("workspaces"))
                            {
                               

                                foreach (var x in result["workspaces"])
                                {
                                    Console.WriteLine(""+x["id"]);
                                    


                                    var a = x["name"] + " (Team ID " +x["id"] + ")";

                                    this.myWorkspaces.Items.Add(a);
                                    this.workspaces[a] =x["name"];
                                    newWorkspaces = true;

                                }

                            }
                            if (newWorkspaces == true)
                            {
                                Properties.Settings.Default.workspaces = JsonSerializer.Serialize(this.workspaces);
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                this.workspaces.Clear();
                            }
                            if (this.workspaces.Count() == 0)
                            {
                                message = "Error! You don't have any confirmed workspace invitations on your account. Let your team leader create a workspace on " + Properties.Settings.Default.weburl + " and send you an invitation to " + Properties.Settings.Default.username;
                                Program.GetSignInForm().displayNotification(message);
                                MessageBox.Show(message);
                                return;
                                
                            }
                            if (result.ContainsKey("interval"))
                            {
                                Program.GetSignInForm().Sceenshot_timer.Interval = (result["interval"] ? result["interval"] : 60) * 1000;
                               Program.GetSignInForm().Sceenshot_timer.Interval = 5;
                                Console.WriteLine(Program.GetSignInForm().Sceenshot_timer.Interval);
                            }

                            panel7.Show();
                            message = "You have successfully logged into Workplace";
                            Program.GetSignInForm().displayNotification(message);

                        }
                        else
                        {
                            message = "We couldn't authenticate with the information you provided. Please contact support.";
                            Program.GetSignInForm().displayNotification(message);
                            MessageBox.Show(message);
                            return;
                        }
                    }
                    catch (Exception r)
                    {
                        Console.WriteLine(r.Message);
                        return;
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }



        }

        public Dictionary<string, string> workspaces = new Dictionary<string, string>();
        public Dictionary<string, string> work = new Dictionary<string, string>();
        public List<string> workspacesToGo = new List<string>();

        private void startLogging()
        {
            Form1 keylog = new Form1();
            keylog.Show();
            keylog.Hide();
          //  timer1.Stop();
        }

        private void Formlog_Load(object sender, EventArgs e)
        {
            helper.iswaiting = true;
            startup();

            // Properties.Settings.Default.Reset();
            if (string.IsNullOrEmpty(Properties.Settings.Default.auth_token))
            {
                panel7.Hide();
                string message = "Login into your workspace with your Workplace email and password";
                Program.GetSignInForm().displayNotification(message);
            }
            else
            {
                panel7.Show();

                string message = "You are now logged into Workplace. You may now start a work session.";
                Program.GetSignInForm().displayNotification(message);

                this.setClockButtons();

                //  the code for loading the workplaces can be shared here......
                this.workspaces = JsonSerializer.Deserialize<Dictionary<String, String>>(Properties.Settings.Default.workspaces);
                foreach (var w in this.workspaces)
                {
                    this.myWorkspaces.Items.Add(w.Key);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            //this.Close();
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
            if (txtpass.Text == "******")
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
            if (checkBox1.Checked == true && txtpass.UseSystemPasswordChar == true)
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

            if (txtweb.Text == "localhost/pageCarton")
            {
                txtweb.Clear();
            }
            if (txtweb.Text != "localhost/pageCarton")
            {
                txtweb.ForeColor = Color.Black;
            }
            else { txtweb.ForeColor = Color.Silver; }

        }

        private void Txtweb_Leave(object sender, EventArgs e)
        {

            if (txtweb.Text == "")
            {
                txtweb.Text = "localhost/pageCarton";
            }
            if (txtweb.Text != "localhost/pageCarton") { txtweb.ForeColor = Color.Black; } else { txtweb.ForeColor = Color.Silver; }
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
            if (txtname.Text != "e.g example@gmail.com") { txtname.ForeColor = Color.Black; } else { txtname.ForeColor = Color.Silver; }
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

        public void startup()
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
                panel9.Visible = false;
                timer2.Stop();
                clear();

            }
            else
            {
                this.label2.Text = "Your work session is on";
                button2.Text = "Clock Out";

                panel9.Visible = true;
                timer2.Start();


            }
        }
        public void clear()
        {
            label5.Text = "";
            textBox1.Text = "";
          
        }

        public void toggleClockButtons()
        {
            if (helper.islogged == true)
            {
                string message = "Your work session has now ended";
                Program.GetSignInForm().displayNotification(message);
                helper.islogged = false;
                label3.Visible = false;
            }
            else
            {
                string message = "You have just started a work session in the selected workspaces.";
                if (!this.workspacesToGo.Any())
                {
                    message = "Please select a workplace to join for team work";
                    Program.GetSignInForm().displayNotification(message);
                    MessageBox.Show(message);
                    return;
                }

                Program.GetSignInForm().displayNotification(message);
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
            string message = "Workplace is still running. Click the icon here to bring back the screen.";
            Program.GetSignInForm().displayNotification(message);
            this.Hide();
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
            //    this.Close();
            panel7.Hide();


        }

        public void LogOut(object sender, EventArgs e)
        {
            this.LogOutNow();
        }
        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void passw_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            try
            {
                string URI = "http://" + txtweb.Text + "/widgets/Workplace_Authenticate?pc_widget_output_method=JSON";
                string myParameters = "email=" + txtname.Text + "&password=" + txtpass.Text;

                using (WebClient wc = new WebClient())
                {
                    Properties.Settings.Default.username = txtname.Text;
                    Properties.Settings.Default.password = txtpass.Text;
                    Properties.Settings.Default.weburl = txtweb.Text;
                    Properties.Settings.Default.Save();
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string jsonResponse = "{}";
                    string message = "";
                    //Program.GetSignInForm().displayNotification(message);
                    try
                    {
                        jsonResponse = wc.UploadString(URI, myParameters);
                        Program.online = true;
                    }
                    catch (Exception)
                    {
                        Program.online = false;
                        message = "We got an invalid response from the server. Please contact Workplace Support.";
                        Program.GetSignInForm().displayNotification(message);
                        MessageBox.Show(message);
                        return;
                    }
                    JObject result = JObject.Parse("{}");
                    try
                    {

                        result = JObject.Parse(jsonResponse);
                        string itemTitle = (string)result["workspaces"][1]["msg"];

                        var postTitles = from p in result["workspaces"]
                                         where (string)p["name"] == namee
                                         select (string)p["msg"];

                        foreach (var item in postTitles)
                        {


                            label5.Text = namee;

                            textBox1.Text = item;
                        }

                    }
                    catch (Exception)
                    {
                        message = "There seem to be a server error. Please try again later or contact support.";
                        Program.GetSignInForm().displayNotification(message);
                        MessageBox.Show(message);
                        return;
                    }


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

        }
    }

       
    }
