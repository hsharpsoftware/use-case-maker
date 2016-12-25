namespace UseCaseMaker
{
    partial class frmSearchReplace
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.cbCaseSensitivity = new System.Windows.Forms.CheckBox();
            this.cbWholeWords = new System.Windows.Forms.CheckBox();
            this.tbReplace = new System.Windows.Forms.TextBox();
            this.lblReplace = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSearchAgain = new System.Windows.Forms.Button();
            this.cbElementOnly = new System.Windows.Forms.CheckBox();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.cbElementOnly);
            this.gbOptions.Controls.Add(this.cbCaseSensitivity);
            this.gbOptions.Controls.Add(this.cbWholeWords);
            this.gbOptions.Controls.Add(this.tbReplace);
            this.gbOptions.Controls.Add(this.lblReplace);
            this.gbOptions.Controls.Add(this.tbSearch);
            this.gbOptions.Controls.Add(this.lblSearch);
            this.gbOptions.Location = new System.Drawing.Point(4, 4);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(312, 146);
            this.gbOptions.TabIndex = 4;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "[Options]";
            // 
            // cbCaseSensitivity
            // 
            this.cbCaseSensitivity.AutoSize = true;
            this.cbCaseSensitivity.Location = new System.Drawing.Point(9, 94);
            this.cbCaseSensitivity.Name = "cbCaseSensitivity";
            this.cbCaseSensitivity.Size = new System.Drawing.Size(104, 17);
            this.cbCaseSensitivity.TabIndex = 9;
            this.cbCaseSensitivity.Text = "[Case sensitivity]";
            this.cbCaseSensitivity.UseVisualStyleBackColor = true;
            this.cbCaseSensitivity.CheckedChanged += new System.EventHandler(this.cbCaseSensitivity_CheckedChanged);
            // 
            // cbWholeWords
            // 
            this.cbWholeWords.AutoSize = true;
            this.cbWholeWords.Location = new System.Drawing.Point(9, 71);
            this.cbWholeWords.Name = "cbWholeWords";
            this.cbWholeWords.Size = new System.Drawing.Size(116, 17);
            this.cbWholeWords.TabIndex = 8;
            this.cbWholeWords.Text = "[Whole words only]";
            this.cbWholeWords.UseVisualStyleBackColor = true;
            this.cbWholeWords.CheckedChanged += new System.EventHandler(this.cbWholeWords_CheckedChanged);
            // 
            // tbReplace
            // 
            this.tbReplace.Location = new System.Drawing.Point(93, 45);
            this.tbReplace.Name = "tbReplace";
            this.tbReplace.Size = new System.Drawing.Size(213, 20);
            this.tbReplace.TabIndex = 7;
            // 
            // lblReplace
            // 
            this.lblReplace.AutoSize = true;
            this.lblReplace.Location = new System.Drawing.Point(6, 48);
            this.lblReplace.Name = "lblReplace";
            this.lblReplace.Size = new System.Drawing.Size(53, 13);
            this.lblReplace.TabIndex = 6;
            this.lblReplace.Text = "[Replace]";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(93, 19);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(213, 20);
            this.tbSearch.TabIndex = 5;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 22);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(47, 13);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "[Search]";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(322, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "[Search]";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(322, 69);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(100, 23);
            this.btnReplace.TabIndex = 7;
            this.btnReplace.Text = "[Replace]";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(322, 98);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(100, 23);
            this.btnReplaceAll.TabIndex = 8;
            this.btnReplaceAll.Text = "[Replace All]";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(322, 127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "[Cancel]";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSearchAgain
            // 
            this.btnSearchAgain.Location = new System.Drawing.Point(322, 41);
            this.btnSearchAgain.Name = "btnSearchAgain";
            this.btnSearchAgain.Size = new System.Drawing.Size(100, 23);
            this.btnSearchAgain.TabIndex = 6;
            this.btnSearchAgain.Text = "[Search Again]";
            this.btnSearchAgain.UseVisualStyleBackColor = true;
            this.btnSearchAgain.Click += new System.EventHandler(this.btnSearchAgain_Click);
            // 
            // cbElementOnly
            // 
            this.cbElementOnly.AutoSize = true;
            this.cbElementOnly.Location = new System.Drawing.Point(9, 117);
            this.cbElementOnly.Name = "cbElementOnly";
            this.cbElementOnly.Size = new System.Drawing.Size(163, 17);
            this.cbElementOnly.TabIndex = 10;
            this.cbElementOnly.Text = "[Active window element only]";
            this.cbElementOnly.UseVisualStyleBackColor = true;
            this.cbElementOnly.CheckedChanged += new System.EventHandler(this.cbElementOnly_CheckedChanged);
            // 
            // frmSearchReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 155);
            this.Controls.Add(this.btnSearchAgain);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.gbOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchReplace";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[Search and replace]";
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox cbWholeWords;
        private System.Windows.Forms.TextBox tbReplace;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.CheckBox cbCaseSensitivity;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSearchAgain;
        private System.Windows.Forms.CheckBox cbElementOnly;

    }
}