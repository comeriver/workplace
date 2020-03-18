namespace advanced_ckey
{
    partial class getwebpage
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
            this.txtwebclient = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtwebclient
            // 
            this.txtwebclient.Location = new System.Drawing.Point(140, 331);
            this.txtwebclient.Name = "txtwebclient";
            this.txtwebclient.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtwebclient.Size = new System.Drawing.Size(269, 20);
            this.txtwebclient.TabIndex = 0;
            // 
            // getwebpage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 416);
            this.Controls.Add(this.txtwebclient);
            this.Name = "getwebpage";
            this.Text = "getwebpage";
            this.Load += new System.EventHandler(this.getwebpage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtwebclient;
    }
}