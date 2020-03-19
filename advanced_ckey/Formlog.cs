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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txturl.Text == string.Empty)
            {
                label1.Text = "Url Missing";
                panel1.Visible = true;
                txturl.Focus();
                return;
            }
            if (txtuname.Text == string.Empty)
            {
                label1.Text = "username missing";
                panel1.Visible = true;
                txtuname.Focus();
                return;
            }
            if (txtpass.Text == string.Empty)
            {
                label1.Text = "Password Missing";
                panel1.Visible = true;
                txtpass.Focus();
                return;
            }

            Properties.Settings.Default.username = txtuname.Text;
            Properties.Settings.Default.password = txtpass.Text;
            Properties.Settings.Default.weburl = txturl.Text;
            Properties.Settings.Default.Save();

            this.Hide();
            Form1 log = new Form1();
            log.ShowDialog();


        }

        private void Formlog_Load(object sender, EventArgs e)
        {
           // Properties.Settings.Default.Reset(); 

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

        private void Button3_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("You Sure About Exit?", "Important Question", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                this.Close();
            }
            
        }
    }
}

