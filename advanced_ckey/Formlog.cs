using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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




            Properties.Settings.Default.username = txtname.Text;
            Properties.Settings.Default.password = txtpass.Text;
            Properties.Settings.Default.weburl = txtweb.Text;
            Properties.Settings.Default.Save();

            this.Hide();
            Form1 log = new Form1();
            log.ShowDialog();
            this.Close();



        }

        private void Formlog_Load(object sender, EventArgs e)
        {
           //Properties.Settings.Default.Reset(); 

            if (Properties.Settings.Default.username != string.Empty && Properties.Settings.Default.password != string.Empty)
            {

                this.Hide();
                Form1 log = new Form1();
                log.ShowDialog();
                this.Close();

            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void Txturl_TextChanged(object sender, EventArgs e)
        {

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

        private void Txtpass_Leave_1(object sender, EventArgs e)
        {

        }

        private void Txtpass_Enter(object sender, EventArgs e)
        {

        }

        private void Txtpass_Enter_1(object sender, EventArgs e)
        {
            if(txtpass.Text == "******")
            {

                txtpass.Clear();
            }
            

        }

        private void Txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "******";
            }
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

        }

        private void Txtweb_Leave(object sender, EventArgs e)
        {

            if (txtweb.Text == "")
            {
                txtweb.Text = "e.g workplace.comeriver.com";
            }
        }

        private void Txtname_Enter(object sender, EventArgs e)
        {
            if (txtname.Text == "e.g example@gmail.com")
            {
                txtname.Clear();
            }

        }

        private void Txtname_Leave(object sender, EventArgs e)
        {
            if (txtname.Text == "")
            {
                txtname.Text = "e.g example@gmail.com";
            }


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
    }
}

