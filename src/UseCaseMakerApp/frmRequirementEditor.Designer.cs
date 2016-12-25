namespace UseCaseMaker
{
    partial class frmRequirementEditor
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
            this.tabRequirement = new System.Windows.Forms.TabControl();
            this.pgDetails = new System.Windows.Forms.TabPage();
            this.btnAcceptToHistory = new System.Windows.Forms.Button();
            this.btnStatusToHistory = new System.Windows.Forms.Button();
            this.btnRemoveMappedUC = new System.Windows.Forms.Button();
            this.btnAddMappedUC = new System.Windows.Forms.Button();
            this.lvMappedOntoUCs = new System.Windows.Forms.ListView();
            this.chMappedUCs = new System.Windows.Forms.ColumnHeader();
            this.lblMappedOntoUCsTitle = new System.Windows.Forms.Label();
            this.cmbAcceptanceStatus = new System.Windows.Forms.ComboBox();
            this.lblAcceptanceStatusTitle = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.cmbImportance = new System.Windows.Forms.ComboBox();
            this.lblImportanceTitle = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.btnRemoveBTStakeholder = new System.Windows.Forms.Button();
            this.btnAddBTStakeholder = new System.Windows.Forms.Button();
            this.lvBTStakeholders = new System.Windows.Forms.ListView();
            this.chBenefiters = new System.Windows.Forms.ColumnHeader();
            this.lblBenefitToTitle = new System.Windows.Forms.Label();
            this.btnRemovePBStakeholder = new System.Windows.Forms.Button();
            this.btnAddPBStakeholder = new System.Windows.Forms.Button();
            this.lvPBStakeholders = new System.Windows.Forms.ListView();
            this.chProposers = new System.Windows.Forms.ColumnHeader();
            this.lblProposedByTitle = new System.Windows.Forms.Label();
            this.pgHistory = new System.Windows.Forms.TabPage();
            this.btnRemoveHistoryItem = new System.Windows.Forms.Button();
            this.lvHistory = new System.Windows.Forms.ListView();
            this.chDate = new System.Windows.Forms.ColumnHeader();
            this.chType = new System.Windows.Forms.ColumnHeader();
            this.chAction = new System.Windows.Forms.ColumnHeader();
            this.chNotes = new System.Windows.Forms.ColumnHeader();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnGoToDefinition = new System.Windows.Forms.Button();
            this.tabRequirement.SuspendLayout();
            this.pgDetails.SuspendLayout();
            this.pgHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabRequirement
            // 
            this.tabRequirement.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabRequirement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabRequirement.Controls.Add(this.pgDetails);
            this.tabRequirement.Controls.Add(this.pgHistory);
            this.tabRequirement.Location = new System.Drawing.Point(0, 0);
            this.tabRequirement.Name = "tabRequirement";
            this.tabRequirement.SelectedIndex = 0;
            this.tabRequirement.Size = new System.Drawing.Size(587, 392);
            this.tabRequirement.TabIndex = 0;
            // 
            // pgDetails
            // 
            this.pgDetails.Controls.Add(this.btnGoToDefinition);
            this.pgDetails.Controls.Add(this.btnAcceptToHistory);
            this.pgDetails.Controls.Add(this.btnStatusToHistory);
            this.pgDetails.Controls.Add(this.btnRemoveMappedUC);
            this.pgDetails.Controls.Add(this.btnAddMappedUC);
            this.pgDetails.Controls.Add(this.lvMappedOntoUCs);
            this.pgDetails.Controls.Add(this.lblMappedOntoUCsTitle);
            this.pgDetails.Controls.Add(this.cmbAcceptanceStatus);
            this.pgDetails.Controls.Add(this.lblAcceptanceStatusTitle);
            this.pgDetails.Controls.Add(this.cmbStatus);
            this.pgDetails.Controls.Add(this.lblStatusTitle);
            this.pgDetails.Controls.Add(this.cmbImportance);
            this.pgDetails.Controls.Add(this.lblImportanceTitle);
            this.pgDetails.Controls.Add(this.cmbCategory);
            this.pgDetails.Controls.Add(this.lblCategoryTitle);
            this.pgDetails.Controls.Add(this.btnRemoveBTStakeholder);
            this.pgDetails.Controls.Add(this.btnAddBTStakeholder);
            this.pgDetails.Controls.Add(this.lvBTStakeholders);
            this.pgDetails.Controls.Add(this.lblBenefitToTitle);
            this.pgDetails.Controls.Add(this.btnRemovePBStakeholder);
            this.pgDetails.Controls.Add(this.btnAddPBStakeholder);
            this.pgDetails.Controls.Add(this.lvPBStakeholders);
            this.pgDetails.Controls.Add(this.lblProposedByTitle);
            this.pgDetails.Location = new System.Drawing.Point(4, 4);
            this.pgDetails.Name = "pgDetails";
            this.pgDetails.Padding = new System.Windows.Forms.Padding(3);
            this.pgDetails.Size = new System.Drawing.Size(579, 367);
            this.pgDetails.TabIndex = 0;
            this.pgDetails.Text = "[Details]";
            this.pgDetails.UseVisualStyleBackColor = true;
            // 
            // btnAcceptToHistory
            // 
            this.btnAcceptToHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAcceptToHistory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAcceptToHistory.Location = new System.Drawing.Point(453, 218);
            this.btnAcceptToHistory.Name = "btnAcceptToHistory";
            this.btnAcceptToHistory.Size = new System.Drawing.Size(120, 23);
            this.btnAcceptToHistory.TabIndex = 99;
            this.btnAcceptToHistory.Text = "[Add to history]";
            this.btnAcceptToHistory.Click += new System.EventHandler(this.btnAcceptToHistory_Click);
            // 
            // btnStatusToHistory
            // 
            this.btnStatusToHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatusToHistory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStatusToHistory.Location = new System.Drawing.Point(453, 191);
            this.btnStatusToHistory.Name = "btnStatusToHistory";
            this.btnStatusToHistory.Size = new System.Drawing.Size(120, 23);
            this.btnStatusToHistory.TabIndex = 98;
            this.btnStatusToHistory.Text = "[Add to history]";
            this.btnStatusToHistory.Click += new System.EventHandler(this.btnStatusToHistory_Click);
            // 
            // btnRemoveMappedUC
            // 
            this.btnRemoveMappedUC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveMappedUC.Enabled = false;
            this.btnRemoveMappedUC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemoveMappedUC.Location = new System.Drawing.Point(453, 284);
            this.btnRemoveMappedUC.Name = "btnRemoveMappedUC";
            this.btnRemoveMappedUC.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveMappedUC.TabIndex = 96;
            this.btnRemoveMappedUC.Text = "[Remove]";
            this.btnRemoveMappedUC.Click += new System.EventHandler(this.btnRemoveMappedUC_Click);
            // 
            // btnAddMappedUC
            // 
            this.btnAddMappedUC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMappedUC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddMappedUC.Location = new System.Drawing.Point(453, 255);
            this.btnAddMappedUC.Name = "btnAddMappedUC";
            this.btnAddMappedUC.Size = new System.Drawing.Size(120, 23);
            this.btnAddMappedUC.TabIndex = 95;
            this.btnAddMappedUC.Text = "[Add]";
            this.btnAddMappedUC.Click += new System.EventHandler(this.btnAddMappedUC_Click);
            // 
            // lvMappedOntoUCs
            // 
            this.lvMappedOntoUCs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMappedOntoUCs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMappedUCs});
            this.lvMappedOntoUCs.FullRowSelect = true;
            this.lvMappedOntoUCs.GridLines = true;
            this.lvMappedOntoUCs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMappedOntoUCs.HideSelection = false;
            this.lvMappedOntoUCs.Location = new System.Drawing.Point(117, 255);
            this.lvMappedOntoUCs.Name = "lvMappedOntoUCs";
            this.lvMappedOntoUCs.Size = new System.Drawing.Size(330, 106);
            this.lvMappedOntoUCs.TabIndex = 94;
            this.lvMappedOntoUCs.UseCompatibleStateImageBehavior = false;
            this.lvMappedOntoUCs.View = System.Windows.Forms.View.Details;
            this.lvMappedOntoUCs.SelectedIndexChanged += new System.EventHandler(this.lvMappedOntoUCs_SelectedIndexChanged);
            // 
            // chMappedUCs
            // 
            this.chMappedUCs.Width = 300;
            // 
            // lblMappedOntoUCsTitle
            // 
            this.lblMappedOntoUCsTitle.Location = new System.Drawing.Point(8, 255);
            this.lblMappedOntoUCsTitle.Name = "lblMappedOntoUCsTitle";
            this.lblMappedOntoUCsTitle.Size = new System.Drawing.Size(103, 40);
            this.lblMappedOntoUCsTitle.TabIndex = 97;
            this.lblMappedOntoUCsTitle.Text = "[Mapped onto use cases]";
            // 
            // cmbAcceptanceStatus
            // 
            this.cmbAcceptanceStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAcceptanceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcceptanceStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbAcceptanceStatus.Items.AddRange(new object[] {
            "[Proposed]",
            "[Evaluanting]",
            "[Accepted]",
            "[Refused]",
            "[Cancelled]",
            "[Conflictual]"});
            this.cmbAcceptanceStatus.Location = new System.Drawing.Point(117, 218);
            this.cmbAcceptanceStatus.Name = "cmbAcceptanceStatus";
            this.cmbAcceptanceStatus.Size = new System.Drawing.Size(330, 21);
            this.cmbAcceptanceStatus.TabIndex = 92;
            this.cmbAcceptanceStatus.SelectedIndexChanged += new System.EventHandler(this.cmbAcceptanceStatus_SelectedIndexChanged);
            // 
            // lblAcceptanceStatusTitle
            // 
            this.lblAcceptanceStatusTitle.Location = new System.Drawing.Point(8, 218);
            this.lblAcceptanceStatusTitle.Name = "lblAcceptanceStatusTitle";
            this.lblAcceptanceStatusTitle.Size = new System.Drawing.Size(103, 19);
            this.lblAcceptanceStatusTitle.TabIndex = 93;
            this.lblAcceptanceStatusTitle.Text = "[Acceptance status]";
            // 
            // cmbStatus
            // 
            this.cmbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbStatus.Items.AddRange(new object[] {
            "[Named]",
            "[Initial]",
            "[Base]",
            "[Complete]",
            "[Deferred]",
            "[Tested]",
            "[Approved]"});
            this.cmbStatus.Location = new System.Drawing.Point(117, 191);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(330, 21);
            this.cmbStatus.TabIndex = 90;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.Location = new System.Drawing.Point(8, 194);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(103, 18);
            this.lblStatusTitle.TabIndex = 91;
            this.lblStatusTitle.Text = "[Status]";
            // 
            // cmbImportance
            // 
            this.cmbImportance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbImportance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImportance.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbImportance.Items.AddRange(new object[] {
            "[Must have]",
            "[Should have]",
            "[Nice to have]"});
            this.cmbImportance.Location = new System.Drawing.Point(117, 164);
            this.cmbImportance.Name = "cmbImportance";
            this.cmbImportance.Size = new System.Drawing.Size(330, 21);
            this.cmbImportance.TabIndex = 88;
            this.cmbImportance.SelectedIndexChanged += new System.EventHandler(this.cmbImportance_SelectedIndexChanged);
            // 
            // lblImportanceTitle
            // 
            this.lblImportanceTitle.Location = new System.Drawing.Point(8, 167);
            this.lblImportanceTitle.Name = "lblImportanceTitle";
            this.lblImportanceTitle.Size = new System.Drawing.Size(103, 18);
            this.lblImportanceTitle.TabIndex = 89;
            this.lblImportanceTitle.Text = "[Importance]";
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbCategory.Items.AddRange(new object[] {
            "[Functional]",
            "[Non-functional]"});
            this.cmbCategory.Location = new System.Drawing.Point(117, 137);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(330, 21);
            this.cmbCategory.TabIndex = 86;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // lblCategoryTitle
            // 
            this.lblCategoryTitle.Location = new System.Drawing.Point(8, 140);
            this.lblCategoryTitle.Name = "lblCategoryTitle";
            this.lblCategoryTitle.Size = new System.Drawing.Size(103, 18);
            this.lblCategoryTitle.TabIndex = 87;
            this.lblCategoryTitle.Text = "[Category]";
            // 
            // btnRemoveBTStakeholder
            // 
            this.btnRemoveBTStakeholder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveBTStakeholder.Enabled = false;
            this.btnRemoveBTStakeholder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemoveBTStakeholder.Location = new System.Drawing.Point(453, 96);
            this.btnRemoveBTStakeholder.Name = "btnRemoveBTStakeholder";
            this.btnRemoveBTStakeholder.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveBTStakeholder.TabIndex = 84;
            this.btnRemoveBTStakeholder.Text = "[Remove]";
            this.btnRemoveBTStakeholder.Click += new System.EventHandler(this.btnRemoveBTStakeholder_Click);
            // 
            // btnAddBTStakeholder
            // 
            this.btnAddBTStakeholder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBTStakeholder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddBTStakeholder.Location = new System.Drawing.Point(453, 67);
            this.btnAddBTStakeholder.Name = "btnAddBTStakeholder";
            this.btnAddBTStakeholder.Size = new System.Drawing.Size(120, 23);
            this.btnAddBTStakeholder.TabIndex = 83;
            this.btnAddBTStakeholder.Text = "[Add]";
            this.btnAddBTStakeholder.Click += new System.EventHandler(this.btnAddBTStakeholder_Click);
            // 
            // lvBTStakeholders
            // 
            this.lvBTStakeholders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvBTStakeholders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBenefiters});
            this.lvBTStakeholders.FullRowSelect = true;
            this.lvBTStakeholders.GridLines = true;
            this.lvBTStakeholders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvBTStakeholders.HideSelection = false;
            this.lvBTStakeholders.Location = new System.Drawing.Point(117, 67);
            this.lvBTStakeholders.Name = "lvBTStakeholders";
            this.lvBTStakeholders.Size = new System.Drawing.Size(330, 55);
            this.lvBTStakeholders.TabIndex = 82;
            this.lvBTStakeholders.UseCompatibleStateImageBehavior = false;
            this.lvBTStakeholders.View = System.Windows.Forms.View.Details;
            this.lvBTStakeholders.SelectedIndexChanged += new System.EventHandler(this.lvBTStakeholders_SelectedIndexChanged);
            // 
            // chBenefiters
            // 
            this.chBenefiters.Width = 300;
            // 
            // lblBenefitToTitle
            // 
            this.lblBenefitToTitle.Location = new System.Drawing.Point(6, 67);
            this.lblBenefitToTitle.Name = "lblBenefitToTitle";
            this.lblBenefitToTitle.Size = new System.Drawing.Size(105, 40);
            this.lblBenefitToTitle.TabIndex = 85;
            this.lblBenefitToTitle.Text = "[Benefit to]";
            // 
            // btnRemovePBStakeholder
            // 
            this.btnRemovePBStakeholder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemovePBStakeholder.Enabled = false;
            this.btnRemovePBStakeholder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemovePBStakeholder.Location = new System.Drawing.Point(453, 35);
            this.btnRemovePBStakeholder.Name = "btnRemovePBStakeholder";
            this.btnRemovePBStakeholder.Size = new System.Drawing.Size(120, 23);
            this.btnRemovePBStakeholder.TabIndex = 80;
            this.btnRemovePBStakeholder.Text = "[Remove]";
            this.btnRemovePBStakeholder.Click += new System.EventHandler(this.btnRemovePBStakeholder_Click);
            // 
            // btnAddPBStakeholder
            // 
            this.btnAddPBStakeholder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPBStakeholder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddPBStakeholder.Location = new System.Drawing.Point(453, 6);
            this.btnAddPBStakeholder.Name = "btnAddPBStakeholder";
            this.btnAddPBStakeholder.Size = new System.Drawing.Size(120, 23);
            this.btnAddPBStakeholder.TabIndex = 79;
            this.btnAddPBStakeholder.Text = "[Add]";
            this.btnAddPBStakeholder.Click += new System.EventHandler(this.btnAddPBStakeholder_Click);
            // 
            // lvPBStakeholders
            // 
            this.lvPBStakeholders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPBStakeholders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chProposers});
            this.lvPBStakeholders.FullRowSelect = true;
            this.lvPBStakeholders.GridLines = true;
            this.lvPBStakeholders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvPBStakeholders.HideSelection = false;
            this.lvPBStakeholders.Location = new System.Drawing.Point(117, 6);
            this.lvPBStakeholders.Name = "lvPBStakeholders";
            this.lvPBStakeholders.Size = new System.Drawing.Size(330, 55);
            this.lvPBStakeholders.TabIndex = 78;
            this.lvPBStakeholders.UseCompatibleStateImageBehavior = false;
            this.lvPBStakeholders.View = System.Windows.Forms.View.Details;
            this.lvPBStakeholders.SelectedIndexChanged += new System.EventHandler(this.lvPBStakeholders_SelectedIndexChanged);
            // 
            // chProposers
            // 
            this.chProposers.Width = 300;
            // 
            // lblProposedByTitle
            // 
            this.lblProposedByTitle.Location = new System.Drawing.Point(6, 6);
            this.lblProposedByTitle.Name = "lblProposedByTitle";
            this.lblProposedByTitle.Size = new System.Drawing.Size(105, 43);
            this.lblProposedByTitle.TabIndex = 81;
            this.lblProposedByTitle.Text = "[Proposed by]";
            // 
            // pgHistory
            // 
            this.pgHistory.Controls.Add(this.btnRemoveHistoryItem);
            this.pgHistory.Controls.Add(this.lvHistory);
            this.pgHistory.Location = new System.Drawing.Point(4, 4);
            this.pgHistory.Name = "pgHistory";
            this.pgHistory.Padding = new System.Windows.Forms.Padding(3);
            this.pgHistory.Size = new System.Drawing.Size(579, 367);
            this.pgHistory.TabIndex = 1;
            this.pgHistory.Text = "[History]";
            this.pgHistory.UseVisualStyleBackColor = true;
            // 
            // btnRemoveHistoryItem
            // 
            this.btnRemoveHistoryItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveHistoryItem.Enabled = false;
            this.btnRemoveHistoryItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRemoveHistoryItem.Location = new System.Drawing.Point(453, 6);
            this.btnRemoveHistoryItem.Name = "btnRemoveHistoryItem";
            this.btnRemoveHistoryItem.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveHistoryItem.TabIndex = 3;
            this.btnRemoveHistoryItem.Text = "[Remove]";
            this.btnRemoveHistoryItem.Click += new System.EventHandler(this.btnRemoveHistoryItem_Click);
            // 
            // lvHistory
            // 
            this.lvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDate,
            this.chType,
            this.chAction,
            this.chNotes});
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.GridLines = true;
            this.lvHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvHistory.HideSelection = false;
            this.lvHistory.LabelWrap = false;
            this.lvHistory.Location = new System.Drawing.Point(6, 6);
            this.lvHistory.MultiSelect = false;
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(439, 353);
            this.lvHistory.TabIndex = 2;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            this.lvHistory.SelectedIndexChanged += new System.EventHandler(this.lvHistory_SelectedIndexChanged);
            // 
            // chDate
            // 
            this.chDate.Text = "[Date]";
            this.chDate.Width = 100;
            // 
            // chType
            // 
            this.chType.Text = "[type]";
            this.chType.Width = 100;
            // 
            // chAction
            // 
            this.chAction.Text = "[Action]";
            this.chAction.Width = 86;
            // 
            // chNotes
            // 
            this.chNotes.Text = "[Notes]";
            this.chNotes.Width = 150;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(457, 398);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(120, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "[OK]";
            // 
            // btnGoToDefinition
            // 
            this.btnGoToDefinition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToDefinition.Enabled = false;
            this.btnGoToDefinition.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGoToDefinition.Location = new System.Drawing.Point(453, 313);
            this.btnGoToDefinition.Name = "btnGoToDefinition";
            this.btnGoToDefinition.Size = new System.Drawing.Size(120, 23);
            this.btnGoToDefinition.TabIndex = 100;
            this.btnGoToDefinition.Text = "[Go to definition]";
            this.btnGoToDefinition.Click += new System.EventHandler(this.btnGoToDefinition_Click);
            // 
            // frmRequirementEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 435);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabRequirement);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(595, 460);
            this.Name = "frmRequirementEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "[Edit requirement]";
            this.tabRequirement.ResumeLayout(false);
            this.pgDetails.ResumeLayout(false);
            this.pgHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabRequirement;
        private System.Windows.Forms.TabPage pgDetails;
        private System.Windows.Forms.TabPage pgHistory;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnRemoveMappedUC;
        private System.Windows.Forms.Button btnAddMappedUC;
        private System.Windows.Forms.ListView lvMappedOntoUCs;
        private System.Windows.Forms.ColumnHeader chMappedUCs;
        private System.Windows.Forms.Label lblMappedOntoUCsTitle;
        private System.Windows.Forms.ComboBox cmbAcceptanceStatus;
        private System.Windows.Forms.Label lblAcceptanceStatusTitle;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.ComboBox cmbImportance;
        private System.Windows.Forms.Label lblImportanceTitle;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategoryTitle;
        private System.Windows.Forms.Button btnRemoveBTStakeholder;
        private System.Windows.Forms.Button btnAddBTStakeholder;
        private System.Windows.Forms.ListView lvBTStakeholders;
        private System.Windows.Forms.ColumnHeader chBenefiters;
        private System.Windows.Forms.Label lblBenefitToTitle;
        private System.Windows.Forms.Button btnRemovePBStakeholder;
        private System.Windows.Forms.Button btnAddPBStakeholder;
        private System.Windows.Forms.ListView lvPBStakeholders;
        private System.Windows.Forms.ColumnHeader chProposers;
        private System.Windows.Forms.Label lblProposedByTitle;
        private System.Windows.Forms.Button btnRemoveHistoryItem;
        private System.Windows.Forms.ListView lvHistory;
        private System.Windows.Forms.ColumnHeader chDate;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chAction;
        private System.Windows.Forms.ColumnHeader chNotes;
        private System.Windows.Forms.Button btnStatusToHistory;
        private System.Windows.Forms.Button btnAcceptToHistory;
        private System.Windows.Forms.Button btnGoToDefinition;
    }
}