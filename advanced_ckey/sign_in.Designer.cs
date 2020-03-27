namespace advanced_ckey
{
    partial class sign_in
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
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sign_in));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnyes = new System.Windows.Forms.Button();
            this.btnno = new System.Windows.Forms.Button();
            this.btnclockout = new System.Windows.Forms.Button();
            this.lblhour = new System.Windows.Forms.Label();
            this.lblminute = new System.Windows.Forms.Label();
            this.lblseconds = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblcurrent = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.mynotifyicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 9000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Do you want to clock into ComeRiver workplace now?";
            // 
            // btnyes
            // 
            this.btnyes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnyes.Location = new System.Drawing.Point(99, 107);
            this.btnyes.Name = "btnyes";
            this.btnyes.Size = new System.Drawing.Size(128, 34);
            this.btnyes.TabIndex = 1;
            this.btnyes.Text = "Yes, clock in";
            this.btnyes.UseVisualStyleBackColor = true;
            this.btnyes.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnno
            // 
            this.btnno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnno.Location = new System.Drawing.Point(233, 107);
            this.btnno.Name = "btnno";
            this.btnno.Size = new System.Drawing.Size(131, 34);
            this.btnno.TabIndex = 2;
            this.btnno.Text = "No, not yet";
            this.btnno.UseVisualStyleBackColor = true;
            this.btnno.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnclockout
            // 
            this.btnclockout.Enabled = false;
            this.btnclockout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclockout.Location = new System.Drawing.Point(400, 188);
            this.btnclockout.Name = "btnclockout";
            this.btnclockout.Size = new System.Drawing.Size(76, 34);
            this.btnclockout.TabIndex = 3;
            this.btnclockout.Text = "Clock out";
            this.btnclockout.UseVisualStyleBackColor = true;
            this.btnclockout.Click += new System.EventHandler(this.btnclockout_Click);
            // 
            // lblhour
            // 
            this.lblhour.AutoSize = true;
            this.lblhour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhour.Location = new System.Drawing.Point(13, 197);
            this.lblhour.Name = "lblhour";
            this.lblhour.Size = new System.Drawing.Size(22, 16);
            this.lblhour.TabIndex = 4;
            this.lblhour.Text = "00";
            // 
            // lblminute
            // 
            this.lblminute.AutoSize = true;
            this.lblminute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblminute.Location = new System.Drawing.Point(51, 197);
            this.lblminute.Name = "lblminute";
            this.lblminute.Size = new System.Drawing.Size(22, 16);
            this.lblminute.TabIndex = 5;
            this.lblminute.Text = "00";
            // 
            // lblseconds
            // 
            this.lblseconds.AutoSize = true;
            this.lblseconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblseconds.Location = new System.Drawing.Point(96, 197);
            this.lblseconds.Name = "lblseconds";
            this.lblseconds.Size = new System.Drawing.Size(22, 16);
            this.lblseconds.TabIndex = 6;
            this.lblseconds.Text = "00";
            this.lblseconds.TextChanged += new System.EventHandler(this.lblseconds_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = ";";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(79, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = ";";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hr";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(51, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "MIn";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(92, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Sec";
            // 
            // lblcurrent
            // 
            this.lblcurrent.AutoSize = true;
            this.lblcurrent.Location = new System.Drawing.Point(216, 199);
            this.lblcurrent.Name = "lblcurrent";
            this.lblcurrent.Size = new System.Drawing.Size(62, 13);
            this.lblcurrent.TabIndex = 12;
            this.lblcurrent.Text = "current time";
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // mynotifyicon
            // 
            this.mynotifyicon.BalloonTipText = "Form minimized";
            this.mynotifyicon.Icon = ((System.Drawing.Icon)(resources.GetObject("mynotifyicon.Icon")));
            this.mynotifyicon.Text = "show sign_in module";
            this.mynotifyicon.Visible = true;
            this.mynotifyicon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mynotifyicon_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(468, -4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 31);
            this.label3.TabIndex = 13;
            this.label3.Text = "-";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            this.label3.MouseLeave += new System.EventHandler(this.label3_MouseLeave);
            this.label3.MouseHover += new System.EventHandler(this.label3_MouseHover);
            // 
            // sign_in
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 231);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblcurrent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblseconds);
            this.Controls.Add(this.lblminute);
            this.Controls.Add(this.lblhour);
            this.Controls.Add(this.btnclockout);
            this.Controls.Add(this.btnno);
            this.Controls.Add(this.btnyes);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "sign_in";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "sign_in";
            this.Load += new System.EventHandler(this.sign_in_Load);
            this.Resize += new System.EventHandler(this.sign_in_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnyes;
        private System.Windows.Forms.Button btnno;
        private System.Windows.Forms.Button btnclockout;
        private System.Windows.Forms.Label lblhour;
        private System.Windows.Forms.Label lblminute;
        private System.Windows.Forms.Label lblseconds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblcurrent;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.NotifyIcon mynotifyicon;
        private System.Windows.Forms.Label label3;
    }
}