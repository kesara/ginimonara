namespace GiniMonara.UI
{
    partial class CategoriesForm
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
            this.ribbonControl1 = new mRibbon.RibbonControl();
            this.tabPageHome = new System.Windows.Forms.TabPage();
            this.tabPageTags = new System.Windows.Forms.TabPage();
            this.groupBoxTag = new System.Windows.Forms.GroupBox();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.ribbonButtonTagAdd = new mRibbon.RibbonButton();
            this.groupBoxType = new System.Windows.Forms.GroupBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.groupBoxCategory = new System.Windows.Forms.GroupBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.tabPageCategory = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxCategory = new System.Windows.Forms.TextBox();
            this.ribbonButtonCategoryAdd = new mRibbon.RibbonButton();
            this.ribbonButtonReset = new mRibbon.RibbonButton();
            this.ribbonButtonOk = new mRibbon.RibbonButton();
            this.ribbonControl1.SuspendLayout();
            this.tabPageTags.SuspendLayout();
            this.groupBoxTag.SuspendLayout();
            this.groupBoxType.SuspendLayout();
            this.groupBoxCategory.SuspendLayout();
            this.tabPageCategory.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.Controls.Add(this.tabPageHome);
            this.ribbonControl1.Controls.Add(this.tabPageTags);
            this.ribbonControl1.Controls.Add(this.tabPageCategory);
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.SelectedIndex = 0;
            this.ribbonControl1.Size = new System.Drawing.Size(372, 247);
            this.ribbonControl1.TabIndex = 0;
            // 
            // tabPageHome
            // 
            this.tabPageHome.Location = new System.Drawing.Point(4, 25);
            this.tabPageHome.Name = "tabPageHome";
            this.tabPageHome.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHome.Size = new System.Drawing.Size(364, 218);
            this.tabPageHome.TabIndex = 0;
            this.tabPageHome.Text = "GiniMonara";
            this.tabPageHome.UseVisualStyleBackColor = true;
            // 
            // tabPageTags
            // 
            this.tabPageTags.Controls.Add(this.groupBoxTag);
            this.tabPageTags.Controls.Add(this.ribbonButtonTagAdd);
            this.tabPageTags.Controls.Add(this.groupBoxType);
            this.tabPageTags.Controls.Add(this.groupBoxCategory);
            this.tabPageTags.Location = new System.Drawing.Point(4, 25);
            this.tabPageTags.Name = "tabPageTags";
            this.tabPageTags.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTags.Size = new System.Drawing.Size(364, 0);
            this.tabPageTags.TabIndex = 2;
            this.tabPageTags.Text = "Tags";
            this.tabPageTags.UseVisualStyleBackColor = true;
            // 
            // groupBoxTag
            // 
            this.groupBoxTag.Controls.Add(this.textBoxTag);
            this.groupBoxTag.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTag.Location = new System.Drawing.Point(3, 95);
            this.groupBoxTag.Name = "groupBoxTag";
            this.groupBoxTag.Size = new System.Drawing.Size(358, 50);
            this.groupBoxTag.TabIndex = 2;
            this.groupBoxTag.TabStop = false;
            this.groupBoxTag.Text = "Tag";
            // 
            // textBoxTag
            // 
            this.textBoxTag.Location = new System.Drawing.Point(7, 20);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(345, 20);
            this.textBoxTag.TabIndex = 0;
            // 
            // ribbonButtonTagAdd
            // 
            this.ribbonButtonTagAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonTagAdd.Image = global::GiniMonara.Properties.Resources.add;
            this.ribbonButtonTagAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButtonTagAdd.IsFlat = true;
            this.ribbonButtonTagAdd.IsPressed = false;
            this.ribbonButtonTagAdd.Location = new System.Drawing.Point(301, 148);
            this.ribbonButtonTagAdd.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonButtonTagAdd.Name = "ribbonButtonTagAdd";
            this.ribbonButtonTagAdd.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonTagAdd.Size = new System.Drawing.Size(60, 60);
            this.ribbonButtonTagAdd.TabIndex = 3;
            this.ribbonButtonTagAdd.Text = "Add";
            this.ribbonButtonTagAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButtonTagAdd.Click += new System.EventHandler(this.ribbonButtonTagAdd_Click);
            // 
            // groupBoxType
            // 
            this.groupBoxType.Controls.Add(this.comboBoxType);
            this.groupBoxType.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxType.Location = new System.Drawing.Point(3, 49);
            this.groupBoxType.Name = "groupBoxType";
            this.groupBoxType.Size = new System.Drawing.Size(358, 46);
            this.groupBoxType.TabIndex = 1;
            this.groupBoxType.TabStop = false;
            this.groupBoxType.Text = "Type";
            // 
            // comboBoxType
            // 
            this.comboBoxType.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(3, 16);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(352, 21);
            this.comboBoxType.TabIndex = 0;
            // 
            // groupBoxCategory
            // 
            this.groupBoxCategory.Controls.Add(this.comboBoxCategory);
            this.groupBoxCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCategory.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCategory.Name = "groupBoxCategory";
            this.groupBoxCategory.Size = new System.Drawing.Size(358, 46);
            this.groupBoxCategory.TabIndex = 0;
            this.groupBoxCategory.TabStop = false;
            this.groupBoxCategory.Text = "Category";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(3, 16);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(352, 21);
            this.comboBoxCategory.TabIndex = 0;
            // 
            // tabPageCategory
            // 
            this.tabPageCategory.Controls.Add(this.groupBox1);
            this.tabPageCategory.Controls.Add(this.ribbonButtonCategoryAdd);
            this.tabPageCategory.Location = new System.Drawing.Point(4, 25);
            this.tabPageCategory.Name = "tabPageCategory";
            this.tabPageCategory.Size = new System.Drawing.Size(364, 218);
            this.tabPageCategory.TabIndex = 3;
            this.tabPageCategory.Text = "Categories";
            this.tabPageCategory.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCategory);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 50);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Category";
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.Location = new System.Drawing.Point(7, 20);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.Size = new System.Drawing.Size(345, 20);
            this.textBoxCategory.TabIndex = 0;
            // 
            // ribbonButtonCategoryAdd
            // 
            this.ribbonButtonCategoryAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonCategoryAdd.Image = global::GiniMonara.Properties.Resources.add;
            this.ribbonButtonCategoryAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButtonCategoryAdd.IsFlat = true;
            this.ribbonButtonCategoryAdd.IsPressed = false;
            this.ribbonButtonCategoryAdd.Location = new System.Drawing.Point(304, 53);
            this.ribbonButtonCategoryAdd.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonButtonCategoryAdd.Name = "ribbonButtonCategoryAdd";
            this.ribbonButtonCategoryAdd.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonCategoryAdd.Size = new System.Drawing.Size(60, 60);
            this.ribbonButtonCategoryAdd.TabIndex = 5;
            this.ribbonButtonCategoryAdd.Text = "Add";
            this.ribbonButtonCategoryAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButtonCategoryAdd.Click += new System.EventHandler(this.ribbonButtonCategoryAdd_Click);
            // 
            // ribbonButtonReset
            // 
            this.ribbonButtonReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonReset.Image = global::GiniMonara.Properties.Resources.reset;
            this.ribbonButtonReset.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButtonReset.IsFlat = true;
            this.ribbonButtonReset.IsPressed = false;
            this.ribbonButtonReset.Location = new System.Drawing.Point(248, 250);
            this.ribbonButtonReset.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonButtonReset.Name = "ribbonButtonReset";
            this.ribbonButtonReset.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonReset.Size = new System.Drawing.Size(60, 60);
            this.ribbonButtonReset.TabIndex = 6;
            this.ribbonButtonReset.Text = "Reset";
            this.ribbonButtonReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButtonReset.Click += new System.EventHandler(this.ribbonButtonReset_Click);
            // 
            // ribbonButtonOk
            // 
            this.ribbonButtonOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonOk.Image = global::GiniMonara.Properties.Resources.ok;
            this.ribbonButtonOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButtonOk.IsFlat = true;
            this.ribbonButtonOk.IsPressed = false;
            this.ribbonButtonOk.Location = new System.Drawing.Point(308, 250);
            this.ribbonButtonOk.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonButtonOk.Name = "ribbonButtonOk";
            this.ribbonButtonOk.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonOk.Size = new System.Drawing.Size(60, 60);
            this.ribbonButtonOk.TabIndex = 4;
            this.ribbonButtonOk.Text = "Ok";
            this.ribbonButtonOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButtonOk.Click += new System.EventHandler(this.ribbonButtonOk_Click);
            // 
            // CategoriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 318);
            this.Controls.Add(this.ribbonButtonReset);
            this.Controls.Add(this.ribbonButtonOk);
            this.Controls.Add(this.ribbonControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CategoriesForm";
            this.Text = "GiniMonara Categories";
            this.Load += new System.EventHandler(this.CategoriesForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CategoriesForm_FormClosed);
            this.ribbonControl1.ResumeLayout(false);
            this.tabPageTags.ResumeLayout(false);
            this.groupBoxTag.ResumeLayout(false);
            this.groupBoxTag.PerformLayout();
            this.groupBoxType.ResumeLayout(false);
            this.groupBoxCategory.ResumeLayout(false);
            this.tabPageCategory.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private mRibbon.RibbonControl ribbonControl1;
        private System.Windows.Forms.TabPage tabPageHome;
        private System.Windows.Forms.TabPage tabPageTags;
        private System.Windows.Forms.GroupBox groupBoxCategory;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.GroupBox groupBoxType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private mRibbon.RibbonButton ribbonButtonOk;
        private mRibbon.RibbonButton ribbonButtonTagAdd;
        private mRibbon.RibbonButton ribbonButtonReset;
        private System.Windows.Forms.GroupBox groupBoxTag;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.TabPage tabPageCategory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxCategory;
        private mRibbon.RibbonButton ribbonButtonCategoryAdd;

    }
}