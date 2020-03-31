using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace advanced_ckey
{
  public   class helper
    {
        public static  bool islogged { get; set; }
        public static  bool updateChecked { get; set; }
        public static bool iswaiting { get; set; }

        public static void openHomePage()
        {
            System.Diagnostics.Process.Start( "http://" + Properties.Settings.Default.weburl + "/" );
        }

        public static void versioning(dynamic serverResponse)
        {
            if ( helper.updateChecked == true )
            {
                return;
            }

            if ( serverResponse.ContainsKey( "supported_versions" ) )
            {
                bool supported = false;
                foreach (var version in serverResponse["supported_versions"] )
                {
                    if( version == Program.VERSION )
                    {
                        supported = true;
                        break;
                    }
                }
                if( supported == false )
                {
                    MessageBox.Show( "Your version of Comeriver Workplace is no longer supported. You must update immediately to continue to use it." );
                    helper.openHomePage();
                    Application.Exit();
                }
            }
            if( serverResponse.ContainsKey("current_stable_version") )
            {
                if( serverResponse["current_stable_version"] != Program.VERSION )
                {
                    MessageBox.Show("A new version of Comeriver Workplace is available. Please update as soon as possible.");
                    helper.openHomePage();
                }
            }
            helper.updateChecked = true;
        }


    }
}
