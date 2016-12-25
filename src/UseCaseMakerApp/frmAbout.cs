using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per frmAbout.
	/// </summary>
	public class frmAbout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel pnlSep1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkLblLicenses;
		private System.Windows.Forms.Label lblUCMWebSiteTitle;
		private System.Windows.Forms.LinkLabel lnkLblUMCCredit1Mail;
		private System.Windows.Forms.Label lblUCMCredit1Name;
		private System.Windows.Forms.Label lblUCMCreditsTitle;
		private System.Windows.Forms.LinkLabel lnkLblUCMWebSite;
		private System.Windows.Forms.LinkLabel lnkLblUCMAuthorMail;
		private System.Windows.Forms.Label lblUCMAuthorName;
        private System.Windows.Forms.Label lblUCMAuthorTitle;
		private System.Windows.Forms.Button btnOK;
        private LinkLabel lnkLblITextSharp;
        private Label lblITextSharp;
        private Label lblThanks1;
        private LinkLabel lnkLblDockPanelSuite;
        private Label lblDockPanelSuite;
        private Label label3;
        private Label lblUCMVersion;
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAbout(bool isSplash)
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			InitializeComponent();

			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			lblUCMVersion.Text = Application.ProductVersion;
			if(isSplash)
			{
				btnOK.Visible = false;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.pnlSep1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.lnkLblDockPanelSuite = new System.Windows.Forms.LinkLabel();
            this.lblDockPanelSuite = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lnkLblITextSharp = new System.Windows.Forms.LinkLabel();
            this.lblITextSharp = new System.Windows.Forms.Label();
            this.lblThanks1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkLblLicenses = new System.Windows.Forms.LinkLabel();
            this.lblUCMWebSiteTitle = new System.Windows.Forms.Label();
            this.lnkLblUMCCredit1Mail = new System.Windows.Forms.LinkLabel();
            this.lblUCMCredit1Name = new System.Windows.Forms.Label();
            this.lblUCMCreditsTitle = new System.Windows.Forms.Label();
            this.lnkLblUCMWebSite = new System.Windows.Forms.LinkLabel();
            this.lnkLblUCMAuthorMail = new System.Windows.Forms.LinkLabel();
            this.lblUCMAuthorName = new System.Windows.Forms.Label();
            this.lblUCMAuthorTitle = new System.Windows.Forms.Label();
            this.lblUCMVersion = new System.Windows.Forms.Label();
            this.pnlSep1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSep1
            // 
            this.pnlSep1.BackColor = System.Drawing.Color.DarkOrange;
            this.pnlSep1.Controls.Add(this.btnOK);
            this.pnlSep1.Controls.Add(this.lnkLblDockPanelSuite);
            this.pnlSep1.Controls.Add(this.lblDockPanelSuite);
            this.pnlSep1.Controls.Add(this.label3);
            this.pnlSep1.Controls.Add(this.lnkLblITextSharp);
            this.pnlSep1.Controls.Add(this.lblITextSharp);
            this.pnlSep1.Controls.Add(this.lblThanks1);
            this.pnlSep1.Controls.Add(this.label1);
            this.pnlSep1.Controls.Add(this.lnkLblLicenses);
            this.pnlSep1.Controls.Add(this.lblUCMWebSiteTitle);
            this.pnlSep1.Controls.Add(this.lnkLblUMCCredit1Mail);
            this.pnlSep1.Controls.Add(this.lblUCMCredit1Name);
            this.pnlSep1.Controls.Add(this.lblUCMCreditsTitle);
            this.pnlSep1.Controls.Add(this.lnkLblUCMWebSite);
            this.pnlSep1.Controls.Add(this.lnkLblUCMAuthorMail);
            this.pnlSep1.Controls.Add(this.lblUCMAuthorName);
            this.pnlSep1.Controls.Add(this.lblUCMAuthorTitle);
            this.pnlSep1.Location = new System.Drawing.Point(19, 164);
            this.pnlSep1.Name = "pnlSep1";
            this.pnlSep1.Size = new System.Drawing.Size(510, 165);
            this.pnlSep1.TabIndex = 14;
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(416, 124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lnkLblDockPanelSuite
            // 
            this.lnkLblDockPanelSuite.ActiveLinkColor = System.Drawing.Color.Black;
            this.lnkLblDockPanelSuite.BackColor = System.Drawing.Color.Transparent;
            this.lnkLblDockPanelSuite.DisabledLinkColor = System.Drawing.Color.White;
            this.lnkLblDockPanelSuite.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lnkLblDockPanelSuite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblDockPanelSuite.ForeColor = System.Drawing.Color.Black;
            this.lnkLblDockPanelSuite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lnkLblDockPanelSuite.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.lnkLblDockPanelSuite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLblDockPanelSuite.LinkColor = System.Drawing.Color.Black;
            this.lnkLblDockPanelSuite.Location = new System.Drawing.Point(24, 140);
            this.lnkLblDockPanelSuite.Name = "lnkLblDockPanelSuite";
            this.lnkLblDockPanelSuite.Size = new System.Drawing.Size(234, 16);
            this.lnkLblDockPanelSuite.TabIndex = 29;
            this.lnkLblDockPanelSuite.TabStop = true;
            this.lnkLblDockPanelSuite.Text = "http://sourceforge.net/projects/dockpanelsuite";
            this.lnkLblDockPanelSuite.UseCompatibleTextRendering = true;
            this.lnkLblDockPanelSuite.VisitedLinkColor = System.Drawing.Color.Black;
            this.lnkLblDockPanelSuite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblDockPanelSuite_LinkClicked);
            // 
            // lblDockPanelSuite
            // 
            this.lblDockPanelSuite.BackColor = System.Drawing.Color.Transparent;
            this.lblDockPanelSuite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDockPanelSuite.ForeColor = System.Drawing.Color.White;
            this.lblDockPanelSuite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDockPanelSuite.Location = new System.Drawing.Point(70, 124);
            this.lblDockPanelSuite.Name = "lblDockPanelSuite";
            this.lblDockPanelSuite.Size = new System.Drawing.Size(160, 16);
            this.lblDockPanelSuite.TabIndex = 28;
            this.lblDockPanelSuite.Text = "DockPanel Suite";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Thanks:";
            // 
            // lnkLblITextSharp
            // 
            this.lnkLblITextSharp.ActiveLinkColor = System.Drawing.Color.Black;
            this.lnkLblITextSharp.BackColor = System.Drawing.Color.Transparent;
            this.lnkLblITextSharp.DisabledLinkColor = System.Drawing.Color.White;
            this.lnkLblITextSharp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lnkLblITextSharp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblITextSharp.ForeColor = System.Drawing.Color.Black;
            this.lnkLblITextSharp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lnkLblITextSharp.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.lnkLblITextSharp.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLblITextSharp.LinkColor = System.Drawing.Color.Black;
            this.lnkLblITextSharp.Location = new System.Drawing.Point(26, 105);
            this.lnkLblITextSharp.Name = "lnkLblITextSharp";
            this.lnkLblITextSharp.Size = new System.Drawing.Size(234, 16);
            this.lnkLblITextSharp.TabIndex = 26;
            this.lnkLblITextSharp.TabStop = true;
            this.lnkLblITextSharp.Text = "http://itextsharp.sourceforge.net";
            this.lnkLblITextSharp.UseCompatibleTextRendering = true;
            this.lnkLblITextSharp.VisitedLinkColor = System.Drawing.Color.Black;
            this.lnkLblITextSharp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblITextSharp_LinkClicked);
            // 
            // lblITextSharp
            // 
            this.lblITextSharp.BackColor = System.Drawing.Color.Transparent;
            this.lblITextSharp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblITextSharp.ForeColor = System.Drawing.Color.White;
            this.lblITextSharp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblITextSharp.Location = new System.Drawing.Point(70, 89);
            this.lblITextSharp.Name = "lblITextSharp";
            this.lblITextSharp.Size = new System.Drawing.Size(160, 16);
            this.lblITextSharp.TabIndex = 25;
            this.lblITextSharp.Text = "iTextSharp";
            // 
            // lblThanks1
            // 
            this.lblThanks1.BackColor = System.Drawing.Color.Transparent;
            this.lblThanks1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThanks1.ForeColor = System.Drawing.Color.Black;
            this.lblThanks1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblThanks1.Location = new System.Drawing.Point(16, 89);
            this.lblThanks1.Name = "lblThanks1";
            this.lblThanks1.Size = new System.Drawing.Size(48, 16);
            this.lblThanks1.TabIndex = 24;
            this.lblThanks1.Text = "Thanks:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(264, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "GNU GPL & LGPL licenses:";
            this.label1.UseMnemonic = false;
            // 
            // lnkLblLicenses
            // 
            this.lnkLblLicenses.ActiveLinkColor = System.Drawing.Color.White;
            this.lnkLblLicenses.BackColor = System.Drawing.Color.Transparent;
            this.lnkLblLicenses.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lnkLblLicenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblLicenses.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lnkLblLicenses.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.lnkLblLicenses.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLblLicenses.LinkColor = System.Drawing.Color.White;
            this.lnkLblLicenses.Location = new System.Drawing.Point(272, 60);
            this.lnkLblLicenses.Name = "lnkLblLicenses";
            this.lnkLblLicenses.Size = new System.Drawing.Size(234, 16);
            this.lnkLblLicenses.TabIndex = 22;
            this.lnkLblLicenses.TabStop = true;
            this.lnkLblLicenses.Text = "http://www.fsf.org/licenses";
            this.lnkLblLicenses.UseCompatibleTextRendering = true;
            this.lnkLblLicenses.VisitedLinkColor = System.Drawing.Color.White;
            this.lnkLblLicenses.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblUCMWebSiteTitle
            // 
            this.lblUCMWebSiteTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblUCMWebSiteTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUCMWebSiteTitle.ForeColor = System.Drawing.Color.Black;
            this.lblUCMWebSiteTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUCMWebSiteTitle.Location = new System.Drawing.Point(264, 8);
            this.lblUCMWebSiteTitle.Name = "lblUCMWebSiteTitle";
            this.lblUCMWebSiteTitle.Size = new System.Drawing.Size(216, 16);
            this.lblUCMWebSiteTitle.TabIndex = 21;
            this.lblUCMWebSiteTitle.Text = "Web Site:";
            // 
            // lnkLblUMCCredit1Mail
            // 
            this.lnkLblUMCCredit1Mail.ActiveLinkColor = System.Drawing.Color.Black;
            this.lnkLblUMCCredit1Mail.BackColor = System.Drawing.Color.Transparent;
            this.lnkLblUMCCredit1Mail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lnkLblUMCCredit1Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblUMCCredit1Mail.ForeColor = System.Drawing.Color.Black;
            this.lnkLblUMCCredit1Mail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lnkLblUMCCredit1Mail.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.lnkLblUMCCredit1Mail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLblUMCCredit1Mail.LinkColor = System.Drawing.Color.Black;
            this.lnkLblUMCCredit1Mail.Location = new System.Drawing.Point(24, 60);
            this.lnkLblUMCCredit1Mail.Name = "lnkLblUMCCredit1Mail";
            this.lnkLblUMCCredit1Mail.Size = new System.Drawing.Size(234, 16);
            this.lnkLblUMCCredit1Mail.TabIndex = 20;
            this.lnkLblUMCCredit1Mail.TabStop = true;
            this.lnkLblUMCCredit1Mail.Text = "rufinelli@users.sourceforge.net";
            this.lnkLblUMCCredit1Mail.UseCompatibleTextRendering = true;
            this.lnkLblUMCCredit1Mail.VisitedLinkColor = System.Drawing.Color.Black;
            this.lnkLblUMCCredit1Mail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblUMCCredit1Mail_LinkClicked);
            // 
            // lblUCMCredit1Name
            // 
            this.lblUCMCredit1Name.BackColor = System.Drawing.Color.Transparent;
            this.lblUCMCredit1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUCMCredit1Name.ForeColor = System.Drawing.Color.White;
            this.lblUCMCredit1Name.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUCMCredit1Name.Location = new System.Drawing.Point(70, 44);
            this.lblUCMCredit1Name.Name = "lblUCMCredit1Name";
            this.lblUCMCredit1Name.Size = new System.Drawing.Size(160, 16);
            this.lblUCMCredit1Name.TabIndex = 19;
            this.lblUCMCredit1Name.Text = "Marco Rufinelli";
            // 
            // lblUCMCreditsTitle
            // 
            this.lblUCMCreditsTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblUCMCreditsTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUCMCreditsTitle.ForeColor = System.Drawing.Color.Black;
            this.lblUCMCreditsTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUCMCreditsTitle.Location = new System.Drawing.Point(16, 44);
            this.lblUCMCreditsTitle.Name = "lblUCMCreditsTitle";
            this.lblUCMCreditsTitle.Size = new System.Drawing.Size(48, 16);
            this.lblUCMCreditsTitle.TabIndex = 18;
            this.lblUCMCreditsTitle.Text = "Credits:";
            // 
            // lnkLblUCMWebSite
            // 
            this.lnkLblUCMWebSite.ActiveLinkColor = System.Drawing.Color.White;
            this.lnkLblUCMWebSite.BackColor = System.Drawing.Color.Transparent;
            this.lnkLblUCMWebSite.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lnkLblUCMWebSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblUCMWebSite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lnkLblUCMWebSite.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.lnkLblUCMWebSite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLblUCMWebSite.LinkColor = System.Drawing.Color.White;
            this.lnkLblUCMWebSite.Location = new System.Drawing.Point(272, 24);
            this.lnkLblUCMWebSite.Name = "lnkLblUCMWebSite";
            this.lnkLblUCMWebSite.Size = new System.Drawing.Size(234, 16);
            this.lnkLblUCMWebSite.TabIndex = 17;
            this.lnkLblUCMWebSite.TabStop = true;
            this.lnkLblUCMWebSite.Text = "http://use-case-maker.sourceforge.net";
            this.lnkLblUCMWebSite.UseCompatibleTextRendering = true;
            this.lnkLblUCMWebSite.VisitedLinkColor = System.Drawing.Color.White;
            this.lnkLblUCMWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblUCMWebSite_LinkClicked);
            // 
            // lnkLblUCMAuthorMail
            // 
            this.lnkLblUCMAuthorMail.ActiveLinkColor = System.Drawing.Color.Black;
            this.lnkLblUCMAuthorMail.BackColor = System.Drawing.Color.Transparent;
            this.lnkLblUCMAuthorMail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lnkLblUCMAuthorMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLblUCMAuthorMail.ForeColor = System.Drawing.Color.Black;
            this.lnkLblUCMAuthorMail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lnkLblUCMAuthorMail.LinkArea = new System.Windows.Forms.LinkArea(0, 100);
            this.lnkLblUCMAuthorMail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkLblUCMAuthorMail.LinkColor = System.Drawing.Color.Black;
            this.lnkLblUCMAuthorMail.Location = new System.Drawing.Point(24, 24);
            this.lnkLblUCMAuthorMail.Name = "lnkLblUCMAuthorMail";
            this.lnkLblUCMAuthorMail.Size = new System.Drawing.Size(234, 16);
            this.lnkLblUCMAuthorMail.TabIndex = 16;
            this.lnkLblUCMAuthorMail.TabStop = true;
            this.lnkLblUCMAuthorMail.Text = "gaspardis@users.sourceforge.net";
            this.lnkLblUCMAuthorMail.UseCompatibleTextRendering = true;
            this.lnkLblUCMAuthorMail.VisitedLinkColor = System.Drawing.Color.Black;
            this.lnkLblUCMAuthorMail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblUCMAuthorMail_LinkClicked);
            // 
            // lblUCMAuthorName
            // 
            this.lblUCMAuthorName.BackColor = System.Drawing.Color.Transparent;
            this.lblUCMAuthorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUCMAuthorName.ForeColor = System.Drawing.Color.White;
            this.lblUCMAuthorName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUCMAuthorName.Location = new System.Drawing.Point(70, 8);
            this.lblUCMAuthorName.Name = "lblUCMAuthorName";
            this.lblUCMAuthorName.Size = new System.Drawing.Size(160, 16);
            this.lblUCMAuthorName.TabIndex = 15;
            this.lblUCMAuthorName.Text = "Gabriele Gaspardis";
            // 
            // lblUCMAuthorTitle
            // 
            this.lblUCMAuthorTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblUCMAuthorTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUCMAuthorTitle.ForeColor = System.Drawing.Color.Black;
            this.lblUCMAuthorTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUCMAuthorTitle.Location = new System.Drawing.Point(16, 8);
            this.lblUCMAuthorTitle.Name = "lblUCMAuthorTitle";
            this.lblUCMAuthorTitle.Size = new System.Drawing.Size(48, 16);
            this.lblUCMAuthorTitle.TabIndex = 14;
            this.lblUCMAuthorTitle.Text = "Author:";
            // 
            // lblUCMVersion
            // 
            this.lblUCMVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblUCMVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUCMVersion.ForeColor = System.Drawing.Color.White;
            this.lblUCMVersion.Location = new System.Drawing.Point(191, 73);
            this.lblUCMVersion.Name = "lblUCMVersion";
            this.lblUCMVersion.Size = new System.Drawing.Size(116, 18);
            this.lblUCMVersion.TabIndex = 15;
            this.lblUCMVersion.Text = "[0.0.0.0]";
            this.lblUCMVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(550, 350);
            this.ControlBox = false;
            this.Controls.Add(this.lblUCMVersion);
            this.Controls.Add(this.pnlSep1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Use Case Maker";
            this.pnlSep1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void lnkLblUCMAuthorMail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:gaspardis@users.sourceforge.net");
		}

		private void lnkLblUMCCredit1Mail_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("mailto:rufinelli@users.sourceforge.net");
		}

		private void lnkLblUCMWebSite_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://use-case-maker.sourceforge.net");
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.fsf.org/licenses");
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void lnkLblITextSharp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://itextsharp.sourceforge.net");
        }

        private void lnkLblDockPanelSuite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://sourceforge.net/projects/dockpanelsuite");
        }
	}
}
