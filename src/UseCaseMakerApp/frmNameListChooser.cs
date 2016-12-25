using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per frmNameListChooser.
	/// </summary>
	public class frmNameListChooser : Form
	{
		private Button btnOK;
		private Button btnCancel;
        private Label lblChooseNameTitle;
        private ListBox lbNames;

        private String selectedItem = string.Empty;

		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private Container components = null;

        public frmNameListChooser(String[] names, Localizer localizer)
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			InitializeComponent();

			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			localizer.LocalizeControls(this);
			lbNames.Items.Clear();
			foreach(String name in names)
			{
				lbNames.Items.Add(name);
			}

			if(lbNames.Items.Count == 0)
			{
				btnOK.Enabled = false;
			}
			else
			{
				btnOK.Enabled = true;
				lbNames.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Codice generato da Progettazione Windows Form
		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblChooseNameTitle = new System.Windows.Forms.Label();
            this.lbNames = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(48, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "[OK]";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(176, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "[Cancel]";
            // 
            // lblChooseNameTitle
            // 
            this.lblChooseNameTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblChooseNameTitle.Location = new System.Drawing.Point(8, 8);
            this.lblChooseNameTitle.Name = "lblChooseNameTitle";
            this.lblChooseNameTitle.Size = new System.Drawing.Size(328, 16);
            this.lblChooseNameTitle.TabIndex = 0;
            this.lblChooseNameTitle.Text = "[Select a name]";
            // 
            // lbNames
            // 
            this.lbNames.Location = new System.Drawing.Point(8, 32);
            this.lbNames.Name = "lbNames";
            this.lbNames.Size = new System.Drawing.Size(328, 82);
            this.lbNames.TabIndex = 0;
            // 
            // frmNameListChooser
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(346, 152);
            this.Controls.Add(this.lbNames);
            this.Controls.Add(this.lblChooseNameTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNameListChooser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[List of available names]";
            this.ResumeLayout(false);

		}
		#endregion

        public String SelectedItem
        {
            get
            {
                return (String)lbNames.SelectedItem;
            }
            set
            {
                lbNames.SelectedItem = value;
            }
        }
	}
}
