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
    public partial class getwebpage : Form
    {
        public getwebpage()
        {
            InitializeComponent();
        }

        private void getwebpage_Load(object sender, EventArgs e)
        {


            //using (var webClient = new System.Net.WebClient())
            //{
            //    var json = webClient.DownloadString("http://localhost/widgets/Screenshot_save?pc_widget_output_method=JSON");
            //    Newtonsoft.Json.Linq.JObject o = Newtonsoft.Json.Linq.JObject.Parse(json);
            //    var value = (int)o["sucess"]["File is saved at 25"];
            //    txtwebclient.Text = value.ToString();
            //}



            //using(var webclient = new WebClient()) 
            //{
            //    string rawJSON = webclient.DownloadString("http://localhost/widgets/Screenshot_show?pc_widget_output_method=JSON");
            //    json collection = JsonConvert.DeserializeObject<json>(rawJSON);
            //}





               WebClient client = new WebClient();   //gets an object to access a website
               string download = client.DownloadString("http://localhost/widgets/Screenshot_show?pc_widget_output_method=JSON"); //download the json file into a string
               dynamic dobj = JsonConvert.DeserializeObject<dynamic>(download); //this function of newtonsoft here deserialize json data 

            string location = (string)dobj["JSON"];


               txtwebclient.Text = location;


        }
    }
}
