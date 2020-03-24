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
using Newtonsoft.Json;

namespace advanced_ckey
{
    public partial class client_information : Form
    {
        public client_information()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            string strPage = client.DownloadString("http://" + Properties.Settings.Default.username + "/tools/classplayer/get/object_name/Workplace_Authenticate?pc_widget_output_method=JSON");
            dynamic dobj = JsonConvert.DeserializeObject<dynamic>(strPage);
            string auth_token = dobj["auth_token"];
            string user_id = dobj["user_id"];
            txtauthtoken.Text = auth_token;
            txtuserid.Text = user_id;


            Properties.Settings.Default.username = textBox1.Text;
        }
    }
}
