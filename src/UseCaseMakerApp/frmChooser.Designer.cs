namespace UseCaseMaker
{
    partial class frmChooser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChooser));
            this.tvModelBrowser = new System.Windows.Forms.TreeView();
            this.lblElementSelectedTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.imgListModelBrowser = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tvModelBrowser
            // 
            this.tvModelBrowser.HideSelection = false;
            this.tvModelBrowser.ImageIndex = 0;
            this.tvModelBrowser.ImageList = this.imgListModelBrowser;
            this.tvModelBrowser.Location = new System.Drawing.Point(12, 31);
            this.tvModelBrowser.Name = "tvModelBrowser";
            this.tvModelBrowser.SelectedImageIndex = 0;
            this.tvModelBrowser.ShowNodeToolTips = true;
            this.tvModelBrowser.Size = new System.Drawing.Size(364, 128);
            this.tvModelBrowser.TabIndex = 5;
            this.tvModelBrowser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvModelBrowser_AfterSelect);
            // 
            // lblElementSelectedTitle
            // 
            this.lblElementSelectedTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblElementSelectedTitle.Location = new System.Drawing.Point(12, 9);
            this.lblElementSelectedTitle.Name = "lblElementSelectedTitle";
            this.lblElementSelectedTitle.Size = new System.Drawing.Size(351, 19);
            this.lblElementSelectedTitle.TabIndex = 4;
            this.lblElementSelectedTitle.Text = "[Element selected]";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(197, 165);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "[Cancel]";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(69, 165);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 24);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "[OK]";
            // 
            // imgListModelBrowser
            // 
            this.imgListModelBrowser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListModelBrowser.ImageStream")));
            this.imgListModelBrowser.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListModelBrowser.Images.SetKeyName(0, "");
            this.imgListModelBrowser.Images.SetKeyName(1, "");
            this.imgListModelBrowser.Images.SetKeyName(2, "");
            this.imgListModelBrowser.Images.SetKeyName(3, "Actors.ico");
            this.imgListModelBrowser.Images.SetKeyName(4, "Actor.ico");
            // 
            // frmChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 196);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tvModelBrowser);
            this.Controls.Add(this.lblElementSelectedTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChooser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[Element selection]";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvModelBrowser;
        private System.Windows.Forms.Label lblElementSelectedTitle;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ImageList imgListModelBrowser;
    }
}