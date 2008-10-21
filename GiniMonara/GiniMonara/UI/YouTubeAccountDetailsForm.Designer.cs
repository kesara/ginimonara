namespace GiniMonara.UI
{
    partial class YouTubeAccountDetailsForm
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
            this.ribbonButtonCancel = new mRibbon.RibbonButton();
            this.ribbonButtonOk = new mRibbon.RibbonButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonButtonCancel
            // 
            this.ribbonButtonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButtonCancel.Image = global::GiniMonara.Properties.Resources.cancel;
            this.ribbonButtonCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButtonCancel.IsFlat = true;
            this.ribbonButtonCancel.IsPressed = false;
            this.ribbonButtonCancel.Location = new System.Drawing.Point(168, 102);
            this.ribbonButtonCancel.Margin = new System.Windows.Forms.Padding(1);
            this.ribbonButtonCancel.Name = "ribbonButtonCancel";
            this.ribbonButtonCancel.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ribbonButtonCancel.Size = new System.Drawing.Size(60, 60);
            this.ribbonButtonCancel.TabIndex = 14;
            this.ribbonButtonCancel.Text = "Cancel";
            this.ribbonButtonCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButtonCancel.Click += new System.EventHandler(this.ribbonButtonCancel_Click);
            // 
            // ribbonButtonOk
            // 
            this.ribbonButtonOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButtonOk.Image = global::GiniMonara.Properties.Resources.ok;
            this.ribbonButtonOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButtonOk.IsFlat = true;
            this.ribbonButtonOk.IsPressed = false;
            this.ribbonButtonOk.Location = new System.Drawing.Point(230, 102);
            this.ribbonButtonOk.Margin = new System.Windows.Forms.Padding(1);
            this.ribbonButtonOk.Name = "ribbonButtonOk";
            this.ribbonButtonOk.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonOk.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ribbonButtonOk.Size = new System.Drawing.Size(60, 60);
            this.ribbonButtonOk.TabIndex = 13;
            this.ribbonButtonOk.Text = "Ok";
            this.ribbonButtonOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButtonOk.Click += new System.EventHandler(this.ribbonButtonOk_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxPassword);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 49);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(292, 49);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPassword.Location = new System.Drawing.Point(3, 16);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(286, 20);
            this.textBoxPassword.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxUserName);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(292, 49);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "User Name";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUserName.Location = new System.Drawing.Point(3, 16);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(286, 20);
            this.textBoxUserName.TabIndex = 1;
            // 
            // YouTubeAccountDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 169);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.ribbonButtonCancel);
            this.Controls.Add(this.ribbonButtonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "YouTubeAccountDetailsForm";
            this.Text = "YouTube Account Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.YouTubeAccountDetailsForm_FormClosed);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private mRibbon.RibbonButton ribbonButtonCancel;
        private mRibbon.RibbonButton ribbonButtonOk;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxUserName;
    }
}