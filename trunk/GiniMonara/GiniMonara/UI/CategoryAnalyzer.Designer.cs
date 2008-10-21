namespace GiniMonara.UI
{
    partial class CategoryAnalyzer
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
            this.ribbonButtonOk = new mRibbon.RibbonButton();
            this.groupBoxCategory = new System.Windows.Forms.GroupBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.dataGridViewTags = new System.Windows.Forms.DataGridView();
            this.groupBoxCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTags)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonButtonOk
            // 
            this.ribbonButtonOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonOk.Image = global::GiniMonara.Properties.Resources.ok;
            this.ribbonButtonOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButtonOk.IsFlat = true;
            this.ribbonButtonOk.IsPressed = false;
            this.ribbonButtonOk.Location = new System.Drawing.Point(356, 349);
            this.ribbonButtonOk.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonButtonOk.Name = "ribbonButtonOk";
            this.ribbonButtonOk.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonOk.Size = new System.Drawing.Size(60, 60);
            this.ribbonButtonOk.TabIndex = 5;
            this.ribbonButtonOk.Text = "&Ok";
            this.ribbonButtonOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButtonOk.Click += new System.EventHandler(this.ribbonButtonOk_Click);
            // 
            // groupBoxCategory
            // 
            this.groupBoxCategory.Controls.Add(this.comboBoxCategory);
            this.groupBoxCategory.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCategory.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCategory.Name = "groupBoxCategory";
            this.groupBoxCategory.Size = new System.Drawing.Size(425, 46);
            this.groupBoxCategory.TabIndex = 6;
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
            this.comboBoxCategory.Size = new System.Drawing.Size(419, 21);
            this.comboBoxCategory.TabIndex = 0;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // dataGridViewTags
            // 
            this.dataGridViewTags.AllowUserToAddRows = false;
            this.dataGridViewTags.AllowUserToDeleteRows = false;
            this.dataGridViewTags.AllowUserToOrderColumns = true;
            this.dataGridViewTags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTags.Location = new System.Drawing.Point(12, 52);
            this.dataGridViewTags.Name = "dataGridViewTags";
            this.dataGridViewTags.ReadOnly = true;
            this.dataGridViewTags.Size = new System.Drawing.Size(401, 294);
            this.dataGridViewTags.TabIndex = 7;
            // 
            // CategoryAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 418);
            this.Controls.Add(this.dataGridViewTags);
            this.Controls.Add(this.groupBoxCategory);
            this.Controls.Add(this.ribbonButtonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CategoryAnalyzer";
            this.Text = "Category Analyzer";
            this.Load += new System.EventHandler(this.CategoryAnalyzer_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CategoryAnalyzer_FormClosed);
            this.groupBoxCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private mRibbon.RibbonButton ribbonButtonOk;
        private System.Windows.Forms.GroupBox groupBoxCategory;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.DataGridView dataGridViewTags;
    }
}