using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace advanced_ckey
{
    public partial class sign_in : Form
    {
        public sign_in()
        {
            InitializeComponent();
        }

        private void sign_in_Load(object sender, EventArgs e)
        {
           
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           clock_in();
            timer2.Enabled = true;
            timer1.Stop();
            btnclockout.Enabled = true;

            WindowState = FormWindowState.Minimized;
            
            Form1 key = new Form1();
            key.Show();
        }

        public void clock_in()
        {
            try
            {
                string URI = "https://" + Properties.Settings.Default.weburl + "/widgets/Workplace_Clock_In?pc_widget_output_method=JSON&";
                string myParameters = "&user_id=" + Properties.Settings.Default.user_id + "&in=" + lblcurrent.Text + "&auth_token=" + Properties.Settings.Default.auth_token;
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string jsonresponse = wc.UploadString(URI, myParameters);
                    MessageBox.Show(jsonresponse);
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void clock_out()
        {
            try
            {
                string URI = "https://" + Properties.Settings.Default.weburl + "/widgets/Workplace_Clock_Out?pc_widget_output_method=JSON&";
                string myParameters = "&user_id=" + Properties.Settings.Default.user_id + "&out=" + lblcurrent.Text + "&auth_token=" + Properties.Settings.Default.auth_token;

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string jsonresponse = wc.UploadString(URI, myParameters);
                    MessageBox.Show(jsonresponse);
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblseconds.Text = DateTime.Now.Second.ToString();
        }

        private void lblseconds_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lblseconds.Text) == 0) 
            {
                int a = Convert.ToInt32(lblminute.Text) + 1;
                lblminute.Text = a.ToString();
            }else if (Convert.ToInt32(lblminute.Text) == 60) 
            {
                int b = Convert.ToInt32(lblhour.Text) + 1;
                lblhour.Text = b.ToString();
                lblminute.Text = "00";
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            lblcurrent.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void btnclockout_Click(object sender, EventArgs e)
        {
            clock_out();
        }

        private void sign_in_Resize(object sender, EventArgs e)
        {
            try
            {
                if (FormWindowState.Minimized == this.WindowState)
                {
                    mynotifyicon.Visible = true;
                   
                    mynotifyicon.ShowBalloonTip(1000);
                   
                }
            }
            catch (Exception) { }
          
        }


        private void mynotifyicon_MouseClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;

            mynotifyicon.Visible = false;
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.BackColor = SystemColors.HotTrack;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.BackColor = SystemColors.Control;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
