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

namespace advanced_ckey
{
    public partial class Formlog : Form
    {
        public Formlog()
        {
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
            if (txtweb.Text == "e.g workplace.comeriver.com")
            {
                MessageBox.Show("Address Missing");
                txtweb.Focus();
                return;
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

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(txtweb.Text);

                request.Method = "POST"; //set method property

                string requestParams = "username: " + txtuname.Text + "password: " + txtpass.Text;

                byte[] byteArray = Encoding.UTF8.GetBytes(requestParams);  //converts it to byte array

                request.ContentType = "application/x-www-form-urlencoded";  //content type property

                request.ContentLength = byteArray.Length;  //sets content lenght of the request

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(byteArray, 0, byteArray.Length);
                    requestStream.Close();
                    //requestStream.Write(byteArray, 0, byteArray.Length);
                }

                // Get the response.
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader rdr = new StreamReader(responseStream, Encoding.UTF8);
                        string Json = rdr.ReadToEnd(); // response from server
                        MessageBox.Show(Json);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            Properties.Settings.Default.username = txtname.Text;
            Properties.Settings.Default.password = txtpass.Text;
            Properties.Settings.Default.weburl = txtweb.Text;
            Properties.Settings.Default.Save();


            this.Hide();
            sign_in log = new sign_in();
            log.ShowDialog();
            this.Close();

        }

        private void Formlog_Load(object sender, EventArgs e)
        {
            startup();
           Properties.Settings.Default.Reset(); 

            if (Properties.Settings.Default.username != string.Empty && Properties.Settings.Default.password != string.Empty)
            {
                this.Hide();
                sign_in log = new sign_in();
                log.ShowDialog();
                this.Close();
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("You Sure About Exit?", "Important Question", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                this.Close();
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

            if (txtweb.Text == "e.g workplace.comeriver.com")
            {
                txtweb.Clear();
            }
            if (txtweb.Text != "e.g workplace.comeriver.com")
            {
                txtweb.ForeColor = Color.Black;
            }
            else { txtweb.ForeColor = Color.Silver; }

        }

        private void Txtweb_Leave(object sender, EventArgs e)
        {

            if (txtweb.Text == "")
            {
                txtweb.Text = "e.g workplace.comeriver.com";
            }
            if (txtweb.Text != "e.g workplace.comeriver.com" ){ txtweb.ForeColor = Color.Black; } else { txtweb.ForeColor = Color.Silver; }
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


            //RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //reg.SetValue("Workplace", Application.ExecutablePath.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startup();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://workplace.comeriver.com/");
        }
    }
}

