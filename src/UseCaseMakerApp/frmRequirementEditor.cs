using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UseCaseMakerLibrary;

namespace UseCaseMaker
{
    public partial class frmRequirementEditor : Form
    {
        private bool modified = false;
        private bool lockModified = false;
        private Requirement requirement = null;
        private Model model = null;
        private Localizer localizer = null;
        private String token = null;

        public frmRequirementEditor(Requirement requirement, Model model, Localizer localizer)
        {
            InitializeComponent();

            this.requirement = requirement;
            this.model = model;
            this.localizer = localizer;

            this.localizer.LocalizeControls(this);

            this.lockModified = true;
            
            // Details page
            foreach(ReferencedObject refobj in this.requirement.Proponents)
            {
                Stakeholder stakeholder = this.model.GetStakeholder(refobj.UniqueID);
                if(stakeholder != null)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = stakeholder.Name;
                    this.lvPBStakeholders.Items.Add(lvi);
                }
            }

            foreach(ReferencedObject refobj in this.requirement.Beneficiaries)
            {
                Stakeholder stakeholder = this.model.GetStakeholder(refobj.UniqueID);
                if(stakeholder != null)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = stakeholder.Name;
                    this.lvBTStakeholders.Items.Add(lvi);
                }
            }

            foreach(ReferencedObject refobj in this.requirement.MappedUseCases)
            {
                UseCase useCase = (UseCase)this.model.FindElementByUniqueID(refobj.UniqueID);
                if(useCase != null)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = useCase.Name;
                    this.lvMappedOntoUCs.Items.Add(lvi);
                }
            }

            cmbCategory.SelectedIndex = (Int32)this.requirement.Category;
            cmbImportance.SelectedIndex = (Int32)this.requirement.Importance;
            cmbStatus.SelectedIndex = (Int32)this.requirement.Status;
            cmbAcceptanceStatus.SelectedIndex = (Int32)this.requirement.AcceptanceStatus;

            this.UpdateHistoryView();

            this.SetButtons();

            this.lockModified = false;

            this.ImeMode = ImeMode.On;
        }

        public Boolean Modified
        {
            get
            {
                return this.modified;
            }
        }

        public String Token
        {
            get
            {
                return this.token;
            }
        }

        private void SetButtons()
        {
            if(this.requirement.Proponents.Count > 0 && this.lvPBStakeholders.SelectedIndices.Count > 0)
            {
                btnRemovePBStakeholder.Enabled = true;
            }
            else
            {
                btnRemovePBStakeholder.Enabled = false;
            }

            if(this.requirement.Beneficiaries.Count > 0 && this.lvBTStakeholders.SelectedIndices.Count > 0)
            {
                btnRemoveBTStakeholder.Enabled = true;
            }
            else
            {
                btnRemoveBTStakeholder.Enabled = false;
            }

            if(this.requirement.MappedUseCases.Count > 0 && this.lvMappedOntoUCs.SelectedIndices.Count > 0)
            {
                btnRemoveMappedUC.Enabled = true;
                btnGoToDefinition.Enabled = true;
            }
            else
            {
                btnRemoveMappedUC.Enabled = false;
                btnGoToDefinition.Enabled = false;
            }

            if(this.requirement.HistoryItems.Count > 0 && this.lvHistory.SelectedIndices.Count > 0)
            {
                btnRemoveHistoryItem.Enabled = true;
            }
            else
            {
                btnRemoveHistoryItem.Enabled = false;
            }
        }

        private void UpdateHistoryView()
        {
            // History page
            lvHistory.Items.Clear();
            foreach(HistoryItem hi in this.requirement.HistoryItems)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = hi.LocalizatedDateValue;
                if(hi.Type == HistoryItem.HistoryType.Acceptance)
                {
                    lvi.SubItems.Add(this.localizer.GetValue("Globals", "Acceptance"));
                    lvi.SubItems.Add((string)cmbAcceptanceStatus.Items[hi.Action]);
                    lvi.SubItems.Add(hi.Notes);
                }
                else
                {
                    lvi.SubItems.Add(this.localizer.GetValue("Globals", "Status"));
                    lvi.SubItems.Add((string)cmbStatus.Items[hi.Action]);
                    lvi.SubItems.Add(hi.Notes);
                }
                this.lvHistory.Items.Add(lvi);
            }
            btnRemoveHistoryItem.Enabled = false;
        }

        private void lvPBStakeholders_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetButtons();
        }

        private void lvBTStakeholders_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetButtons();
        }

        private void lvMappedOntoUCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetButtons();
        }

        private void lvHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetButtons();
        }

        private void btnAddPBStakeholder_Click(object sender, EventArgs e)
        {
            String[] names = this.model.GetStakeholderNames();

            frmNameListChooser frm = new frmNameListChooser(names, this.localizer);

            if(frm.ShowDialog(this) == DialogResult.OK)
            {
                String name = (String)frm.SelectedItem;
                Stakeholder stakeholder = (Stakeholder)this.model.FindElementByName(name);
                if(this.requirement.Proponents.FindByUniqueID(stakeholder.UniqueID) != null)
                {
                    // [Element already present!]
                    MessageBox.Show(this, this.localizer.GetValue("UserMessages", "elementAlreadyPresent"));
                    return;
                }

                this.requirement.AddProponent(stakeholder);

                ListViewItem lvi = new ListViewItem();
                lvi.Text = stakeholder.Name;
                this.lvPBStakeholders.Items.Add(lvi);
                this.modified = true;
            }

            frm.Dispose();
        }
        
        private void btnRemovePBStakeholder_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvPBStakeholders.SelectedItems[0];
            Stakeholder stakeholder = (Stakeholder)this.model.FindElementByName(lvi.Text);
            this.requirement.RemoveProponent(stakeholder);
            this.lvPBStakeholders.Items.Remove(lvi);
            this.modified = true;
        }

        private void btnAddBTStakeholder_Click(object sender, EventArgs e)
        {
            String[] names = this.model.GetStakeholderNames();

            frmNameListChooser frm = new frmNameListChooser(names, this.localizer);

            if(frm.ShowDialog(this) == DialogResult.OK)
            {
                String name = (String)frm.SelectedItem;
                Stakeholder stakeholder = (Stakeholder)this.model.FindElementByName(name);
                if(this.requirement.Beneficiaries.FindByUniqueID(stakeholder.UniqueID) != null)
                {
                    // [Element already present!]
                    MessageBox.Show(this, this.localizer.GetValue("UserMessages", "elementAlreadyPresent"));
                    return;
                }

                this.requirement.AddBeneficiary(stakeholder);

                ListViewItem lvi = new ListViewItem();
                lvi.Text = stakeholder.Name;
                this.lvBTStakeholders.Items.Add(lvi);
                this.modified = true;
            }

            frm.Dispose();
        }

        private void btnRemoveBTStakeholder_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvBTStakeholders.SelectedItems[0];
            Stakeholder stakeholder = (Stakeholder)this.model.FindElementByName(lvi.Text);
            this.requirement.RemoveBeneficiary(stakeholder);
            this.lvBTStakeholders.Items.Remove(lvi);
            this.modified = true;
        }

        private void btnStatusToHistory_Click(object sender, EventArgs e)
        {
            frmHistoryNotes frm = new frmHistoryNotes(this.localizer);

            if(frm.ShowDialog() == DialogResult.OK)
            {
                this.requirement.AddHistoryItem(
                    DateTime.Now,
                    HistoryItem.HistoryType.Status,
                    cmbStatus.SelectedIndex,
                    frm.tbNotes.Text.Replace("\r\n", " "));
                this.UpdateHistoryView();
                this.modified = true;
            }
            frm.Dispose();
        }

        private void btnAcceptToHistory_Click(object sender, EventArgs e)
        {
            frmHistoryNotes frm = new frmHistoryNotes(this.localizer);

            if(frm.ShowDialog() == DialogResult.OK)
            {
                this.requirement.AddHistoryItem(
                    DateTime.Now,
                    HistoryItem.HistoryType.Acceptance,
                    cmbAcceptanceStatus.SelectedIndex,
                    frm.tbNotes.Text.Replace("\r\n", " "));
                this.UpdateHistoryView();
                this.modified = true;
            }
            frm.Dispose();
        }

        private void btnRemoveHistoryItem_Click(object sender, EventArgs e)
        {
            this.requirement.RemoveHistoryItem(lvHistory.SelectedIndices[0]);
            lvHistory.Items.Remove(lvHistory.SelectedItems[0]);
            this.modified = true;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(Requirement.CategoryValue));
            foreach(Int32 _value in _values)
            {
                if(_value == cmbCategory.SelectedIndex)
                {
                    this.requirement.Category = (Requirement.CategoryValue)cmbCategory.SelectedIndex;
                    found = true;
                }
            }

            if(!found)
            {
                cmbCategory.SelectedIndex = (int)this.requirement.Category;
            }

            if(!this.lockModified)
            {
                this.modified = true;
            }
        }

        private void cmbImportance_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(Requirement.ImportanceValue));
            foreach(Int32 _value in _values)
            {
                if(_value == cmbImportance.SelectedIndex)
                {
                    this.requirement.Importance = (Requirement.ImportanceValue)cmbImportance.SelectedIndex;
                    found = true;
                }
            }

            if(!found)
            {
                cmbImportance.SelectedIndex = (int)this.requirement.Importance;
            }

            if(!this.lockModified)
            {
                this.modified = true;
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(UseCase.StatusValue));
            foreach(Int32 _value in _values)
            {
                if(_value == cmbStatus.SelectedIndex)
                {
                    this.requirement.Status = (UseCase.StatusValue)cmbStatus.SelectedIndex;
                    found = true;
                }
            }

            if(!found)
            {
                cmbStatus.SelectedIndex = (int)this.requirement.Status;
            }

            if(!this.lockModified)
            {
                this.modified = true;
            }
        }

        private void cmbAcceptanceStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(Requirement.AcceptanceStatusValue));
            foreach(Int32 _value in _values)
            {
                if(_value == cmbAcceptanceStatus.SelectedIndex)
                {
                    this.requirement.AcceptanceStatus =
                        (Requirement.AcceptanceStatusValue)cmbAcceptanceStatus.SelectedIndex;
                    found = true;
                }
            }

            if(!found)
            {
                cmbAcceptanceStatus.SelectedIndex = (int)this.requirement.AcceptanceStatus;
            }

            if(!this.lockModified)
            {
                this.modified = true;
            }
        }

        private void btnAddMappedUC_Click(object sender, EventArgs e)
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
                UseCase useCase = (UseCase)frm.SelectedElement;
                if(this.requirement.MappedUseCases.FindByUniqueID(useCase.UniqueID) != null)
                {
                    // [Element already present!]
                    MessageBox.Show(this, this.localizer.GetValue("UserMessages", "elementAlreadyPresent"));
                    return;
                }
                this.requirement.AddMappedUseCase(useCase);

                ListViewItem lvi = new ListViewItem();
                lvi.Text = useCase.Name;
                this.lvMappedOntoUCs.Items.Add(lvi);
                this.modified = true;
            }

            frm.Dispose();
        }

        private void btnRemoveMappedUC_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvMappedOntoUCs.SelectedItems[0];
            UseCase useCase = (UseCase)this.model.FindElementByName(lvi.Text);
            this.requirement.RemoveMappedUseCase(useCase) ;
            this.lvMappedOntoUCs.Items.Remove(lvi);
            this.modified = true;
        }

        private void btnGoToDefinition_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = lvMappedOntoUCs.SelectedItems[0];
            this.token = lvi.Text;
            this.Close();
        }
    }
}