using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace advanced_ckey
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            helper.islogged = false;
            var k = Program.GetSignInForm();
        //    k.Show();
        //    Program.signInForm.Hide();
            Application.Run( k );
        }

        public static Form1 GetSignInForm()
        {
            if( Program.signInForm == null || Program.signInForm.IsDisposed )
            {
                Program.signInForm = new Form1();
            }
            return Program.signInForm;
        }

        public static Formlog GetClockInForm()
        {
            if( Program.clockInForm == null || Program.clockInForm.IsDisposed )
            {
                Program.clockInForm = new Formlog();
            }
            return Program.clockInForm;
        }
        public static Form1 signInForm;
        public static Formlog clockInForm;

    }
}
