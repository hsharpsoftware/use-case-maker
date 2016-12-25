   using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using UseCaseMakerLibrary;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per frmRefSelector.
	/// </summary>
	public class frmRefSelector : System.Windows.Forms.Form
	{
		private Model model;
		private UseCase caller;
		private DependencyItem.ReferenceType callerRefType;
		private UseCase selected;
		private Localizer localizer;

		private System.Windows.Forms.Label lblStereotypeTitle;
		private System.Windows.Forms.TextBox tbStereotype;
		private System.Windows.Forms.Label lblUpperUseCase;
		private System.Windows.Forms.Label lblLowerUseCase;
		private System.Windows.Forms.Label lblDepFromTitle;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ImageList imgListModelBrowser;
        private System.Windows.Forms.GroupBox gbRelationship;
		private System.Windows.Forms.Button btnSwap;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblStereotype;
        private Button btnSelectUseCase;
		private System.ComponentModel.IContainer components;

		public frmRefSelector(UseCase caller, Model model, Localizer localizer)
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			InitializeComponent();

			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			this.caller = caller;
			this.model = model;
			this.localizer = localizer;
			this.localizer.LocalizeControls(this);
			this.lblUpperUseCase.Text = caller.Name;
			this.lblLowerUseCase.Text = "";
			this.selected = null;
			this.btnOK.Enabled = false;
			this.btnSwap.Enabled = false;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRefSelector));
            this.lblStereotypeTitle = new System.Windows.Forms.Label();
            this.tbStereotype = new System.Windows.Forms.TextBox();
            this.imgListModelBrowser = new System.Windows.Forms.ImageList(this.components);
            this.gbRelationship = new System.Windows.Forms.GroupBox();
            this.lblStereotype = new System.Windows.Forms.Label();
            this.btnSwap = new System.Windows.Forms.Button();
            this.lblDepFromTitle = new System.Windows.Forms.Label();
            this.lblLowerUseCase = new System.Windows.Forms.Label();
            this.lblUpperUseCase = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnSelectUseCase = new System.Windows.Forms.Button();
            this.gbRelationship.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStereotypeTitle
            // 
            this.lblStereotypeTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblStereotypeTitle.Location = new System.Drawing.Point(11, 8);
            this.lblStereotypeTitle.Name = "lblStereotypeTitle";
            this.lblStereotypeTitle.Size = new System.Drawing.Size(117, 16);
            this.lblStereotypeTitle.TabIndex = 0;
            this.lblStereotypeTitle.Text = "[Stereotype]";
            // 
            // tbStereotype
            // 
            this.tbStereotype.Location = new System.Drawing.Point(136, 8);
            this.tbStereotype.Name = "tbStereotype";
            this.tbStereotype.Size = new System.Drawing.Size(378, 20);
            this.tbStereotype.TabIndex = 1;
            this.tbStereotype.TextChanged += new System.EventHandler(this.tbStereotype_TextChanged);
            // 
            // imgListModelBrowser
            // 
            this.imgListModelBrowser.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListModelBrowser.ImageStream")));
            this.imgListModelBrowser.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListModelBrowser.Images.SetKeyName(0, "");
            this.imgListModelBrowser.Images.SetKeyName(1, "");
            this.imgListModelBrowser.Images.SetKeyName(2, "");
            // 
            // gbRelationship
            // 
            this.gbRelationship.Controls.Add(this.btnSelectUseCase);
            this.gbRelationship.Controls.Add(this.lblStereotype);
            this.gbRelationship.Controls.Add(this.btnSwap);
            this.gbRelationship.Controls.Add(this.lblDepFromTitle);
            this.gbRelationship.Controls.Add(this.lblLowerUseCase);
            this.gbRelationship.Controls.Add(this.lblUpperUseCase);
            this.gbRelationship.Controls.Add(this.pictureBox1);
            this.gbRelationship.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gbRelationship.Location = new System.Drawing.Point(11, 34);
            this.gbRelationship.Name = "gbRelationship";
            this.gbRelationship.Size = new System.Drawing.Size(503, 184);
            this.gbRelationship.TabIndex = 4;
            this.gbRelationship.TabStop = false;
            this.gbRelationship.Text = "[Relationship]";
            // 
            // lblStereotype
            // 
            this.lblStereotype.BackColor = System.Drawing.Color.White;
            this.lblStereotype.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblStereotype.Location = new System.Drawing.Point(16, 96);
            this.lblStereotype.Name = "lblStereotype";
            this.lblStereotype.Size = new System.Drawing.Size(160, 23);
            this.lblStereotype.TabIndex = 9;
            this.lblStereotype.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSwap
            // 
            this.btnSwap.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSwap.Location = new System.Drawing.Point(374, 54);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(123, 24);
            this.btnSwap.TabIndex = 4;
            this.btnSwap.Text = "[Swap]";
            this.btnSwap.Click += new System.EventHandler(this.btnSwapUseCases_Click);
            // 
            // lblDepFromTitle
            // 
            this.lblDepFromTitle.BackColor = System.Drawing.Color.White;
            this.lblDepFromTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDepFromTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepFromTitle.Location = new System.Drawing.Point(232, 48);
            this.lblDepFromTitle.Name = "lblDepFromTitle";
            this.lblDepFromTitle.Size = new System.Drawing.Size(128, 24);
            this.lblDepFromTitle.TabIndex = 6;
            this.lblDepFromTitle.Text = "[depends from]";
            this.lblDepFromTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLowerUseCase
            // 
            this.lblLowerUseCase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblLowerUseCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblLowerUseCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowerUseCase.Location = new System.Drawing.Point(160, 128);
            this.lblLowerUseCase.Name = "lblLowerUseCase";
            this.lblLowerUseCase.Size = new System.Drawing.Size(160, 24);
            this.lblLowerUseCase.TabIndex = 3;
            this.lblLowerUseCase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpperUseCase
            // 
            this.lblUpperUseCase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblUpperUseCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUpperUseCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpperUseCase.Location = new System.Drawing.Point(56, 48);
            this.lblUpperUseCase.Name = "lblUpperUseCase";
            this.lblUpperUseCase.Size = new System.Drawing.Size(152, 24);
            this.lblUpperUseCase.TabIndex = 2;
            this.lblUpperUseCase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 152);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Location = new System.Drawing.Point(267, 224);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "[Cancel]";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(139, 224);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 24);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "[OK]";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSelectUseCase
            // 
            this.btnSelectUseCase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelectUseCase.Location = new System.Drawing.Point(374, 24);
            this.btnSelectUseCase.Name = "btnSelectUseCase";
            this.btnSelectUseCase.Size = new System.Drawing.Size(123, 24);
            this.btnSelectUseCase.TabIndex = 10;
            this.btnSelectUseCase.Text = "[Select a use case]";
            this.btnSelectUseCase.Click += new System.EventHandler(this.btnSelectUseCase_Click);
            // 
            // frmRefSelector
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(526, 255);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbRelationship);
            this.Controls.Add(this.tbStereotype);
            this.Controls.Add(this.lblStereotypeTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRefSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[Select use case dependency]";
            this.gbRelationship.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	
		public String Stereotype
		{
			get
			{
				return this.tbStereotype.Text;
			}
		}

		public UseCase SelectedUseCase
		{
			get
			{
				return this.selected;
			}
		}

		public DependencyItem.ReferenceType ReferenceType
		{
			get
			{
				return this.callerRefType;
			}
		}

		private void btnSwapUseCases_Click(object sender, System.EventArgs e)
		{
			string tmp = this.lblUpperUseCase.Text;
			this.lblUpperUseCase.Text = this.lblLowerUseCase.Text;
			this.lblLowerUseCase.Text = tmp;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(this.lblUpperUseCase.Text == this.caller.Name)
			{
				this.callerRefType = DependencyItem.ReferenceType.Client;
			}
			else
			{
				this.callerRefType = DependencyItem.ReferenceType.Supplier;
			}
		}

		private void tbStereotype_TextChanged(object sender, System.EventArgs e)
		{
			if(tbStereotype.Text != "")
			{
				lblStereotype.Text = "<<" + tbStereotype.Text + ">>";
			}
			else
			{
				lblStereotype.Text = "";
			}
		}

        private void btnSelectUseCase_Click(object sender, EventArgs e)
        {
            frmChooser frm = new frmChooser(
                this.model,
                this.localizer,
                this.localizer.GetValue("Globals", "UseCase"));
            frm.ShowActors = false;
            frm.ShowUseCases = true;
            frm.PackageSelectionIsValid = false;
            frm.UseCaseSelectionIsValid = true;
            frm.ActorSelectionIsValid = false;
            frm.Initialize();

            if(frm.ShowDialog(this) == DialogResult.OK)
            {
                this.selected = (UseCase)frm.SelectedElement;
                this.lblUpperUseCase.Text = caller.Name;
                this.lblLowerUseCase.Text = selected.Name;
                this.btnOK.Enabled = true;
                this.btnSwap.Enabled = true;
            }

            frm.Dispose();
        }
	}
}
