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
            this.Hide();
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width, workingArea.Bottom - Size.Height);
            Displaynotify();
        }

        protected void Displaynotify()
        {
            try
            {
                notifyIcon1.Text = "Authenticating";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "Question";
                notifyIcon1.BalloonTipText = "Do you want to clock in?  \nClick for more details";
                notifyIcon1.ShowBalloonTip(1000000);
            }
            catch (Exception)
            {
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            using (new Dialog_class(this))
            {
            
                DialogResult result1 = MessageBox.Show("Do you want to clock in now?", "Question ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes)
                {
                    Form1 keylog = new Form1();
                    keylog.Show();
                 
                  
                    timer1.Stop();
                }
                else
                {
                    timer1.Start();
                }

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Displaynotify();
        }

    }
}
