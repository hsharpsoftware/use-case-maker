namespace UseCaseMaker
{
    partial class frmModelBrowser
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModelBrowser));
            this.tvModelBrowser = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvModelBrowser
            // 
            this.tvModelBrowser.AllowDrop = true;
            this.tvModelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvModelBrowser.HideSelection = false;
            this.tvModelBrowser.Location = new System.Drawing.Point(2, 2);
            this.tvModelBrowser.Name = "tvModelBrowser";
            this.tvModelBrowser.ShowNodeToolTips = true;
            this.tvModelBrowser.Size = new System.Drawing.Size(146, 224);
            this.tvModelBrowser.TabIndex = 1;
            this.tvModelBrowser.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvModelBrowser_DragDrop);
            this.tvModelBrowser.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvModelBrowser_BeforeExpand);
            this.tvModelBrowser.DragOver += new System.Windows.Forms.DragEventHandler(this.tvModelBrowser_DragOver);
            this.tvModelBrowser.Enter += new System.EventHandler(this.tvModelBrowser_Enter);
            this.tvModelBrowser.DoubleClick += new System.EventHandler(this.tvModelBrowser_DoubleClick);
            this.tvModelBrowser.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvModelBrowser_BeforeCollapse);
            this.tvModelBrowser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvModelBrowser_AfterSelect);
            this.tvModelBrowser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tvModelBrowser_KeyDown);
            this.tvModelBrowser.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvModelBrowser_ItemDrag);
            this.tvModelBrowser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvModelBrowser_MouseDown);
            // 
            // frmModelBrowser
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(148, 226);
            this.CloseButton = false;
            this.ControlBox = false;
            this.Controls.Add(this.tvModelBrowser);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft;
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModelBrowser";
            this.Padding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TabText = "[Model Browser]";
            this.Text = "[Model Browser]";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView tvModelBrowser;
    }
}