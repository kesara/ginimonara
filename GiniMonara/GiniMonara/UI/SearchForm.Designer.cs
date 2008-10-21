namespace GiniMonara.UI
{
    partial class SearchForm
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
            this.ribbonButtonSearch = new mRibbon.RibbonButton();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonButtonSearch
            // 
            this.ribbonButtonSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ribbonButtonSearch.Image = global::GiniMonara.Properties.Resources.search;
            this.ribbonButtonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ribbonButtonSearch.IsFlat = true;
            this.ribbonButtonSearch.IsPressed = false;
            this.ribbonButtonSearch.Location = new System.Drawing.Point(451, 9);
            this.ribbonButtonSearch.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonButtonSearch.Name = "ribbonButtonSearch";
            this.ribbonButtonSearch.Padding = new System.Windows.Forms.Padding(2);
            this.ribbonButtonSearch.Size = new System.Drawing.Size(81, 35);
            this.ribbonButtonSearch.TabIndex = 6;
            this.ribbonButtonSearch.Text = "Search";
            this.ribbonButtonSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ribbonButtonSearch.Click += new System.EventHandler(this.ribbonButtonSearch_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(12, 17);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(436, 20);
            this.textBoxSearch.TabIndex = 5;
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.AllowUserToOrderColumns = true;
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Location = new System.Drawing.Point(12, 47);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.Size = new System.Drawing.Size(517, 379);
            this.dataGridViewResults.TabIndex = 7;
            this.dataGridViewResults.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_CellContentClick);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 438);
            this.Controls.Add(this.dataGridViewResults);
            this.Controls.Add(this.ribbonButtonSearch);
            this.Controls.Add(this.textBoxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SearchForm";
            this.Text = "GiniMonara Search";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SearchForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private mRibbon.RibbonButton ribbonButtonSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.DataGridView dataGridViewResults;

    }
}