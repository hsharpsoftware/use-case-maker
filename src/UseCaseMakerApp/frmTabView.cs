using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using UseCaseMakerLibrary;
using UseCaseMakerControls;
using WeifenLuo.WinFormsUI.Docking;

namespace UseCaseMaker
{
    public partial class frmTabView : DockContent
    {
        private object currentElement = null;
        private SepararatorCollection separators;
        private HighLightDescriptorCollection hdc;
        private Localizer localizer;
        private frmMain parent;

        private ToolTip lvRelatedDocsTooltip = new ToolTip();
        private ToolTip lvActorsTooltip = new ToolTip();

        public frmTabView(frmMain parent,
            object element,
            SepararatorCollection separators,
            HighLightDescriptorCollection hdc,
            Localizer localizer)
        {
            this.parent = parent;
            this.currentElement = element;
            this.localizer = localizer;
            this.hdc = hdc;
            this.separators = separators;

            InitializeComponent();

            localizer.LocalizeControls(this);
            
            this.ImeMode = ImeMode.On;
        }

        private void SetDefaultButtonsState()
        {
			// Flow of events buttons
			btnAddStep.Enabled = true;
			btnInsertStep.Enabled = false;
			btnAddAltStep.Enabled = false;
			btnAddRefStep.Enabled = true;
			btnInsertRefStep.Enabled = false;
			btnRemoveStep.Enabled = false;

			// General (Actor)
			btnAddGoal.Enabled = true;
			btnRemoveGoal.Enabled = false;

			// General (Use Case)
			btnAddActor.Enabled = true;
			btnRemoveActor.Enabled = false;
			btnSetPrimaryActor.Enabled = false;

			// Attributes
			btnAddRelatedDoc.Enabled = true;
			btnRemoveRelatedDoc.Enabled = false;
			btnOpenRelatedDoc.Enabled = false;

			// Glossary
			btnAddGlossaryItem.Enabled = true;
			btnChangeGlossaryItem.Enabled = false;
			btnRemoveGlossaryItem.Enabled = false;

            // Stakeholders
            btnAddStakeholder.Enabled = true;
            btnChangeStakeholder.Enabled = false;
            btnRemoveStakeholder.Enabled = false;

			// History
			btnRemoveHistoryItem.Enabled = false;

			// Details
			btnAddOpenIssue.Enabled = true;
			btnRemoveOpenIssue.Enabled = false;

			// Requirements
			btnAddRequirement.Enabled = true;
            btnShowReqDetails.Enabled = false;
			btnRemoveRequirement.Enabled = false;
        }

        /**
		 * @brief Visualizzazione del modello
		 * 
		 * Aggiorna gli elementi del modello riflettendo lo stato
		 * dell business e le modifiche apportate dall'utente
		 */
        public void UpdateView()
        {
            this.Cursor = Cursors.WaitCursor;

            Win32.SendMessage(this.Handle,Win32.WM_SETREDRAW,0,(IntPtr)0);
			parent.LockModified();

			this.SetDefaultButtonsState();

			tabUseCase.TabPages.Clear();

			if(currentElement != null)
			{
				if(this.currentElement.GetType() == typeof(Model))
				{
					// TabPage controls
                    tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = true;
					pnlActorsContainer.Visible = true;
					pnlUseCasesContainer.Visible = true;
                    pnlModelAdditionalInfo.Visible = true;
					lblPackages.Text = parent.Model.Packages.Count.ToString();
					lblActors.Text = parent.Model.Actors.Count.ToString();
					lblUseCases.Text = parent.Model.UseCases.Count.ToString();

                    tabUseCase.TabPages.Add(pgPGeneral);
					lblPOwner.Text = String.Empty;
					lblPID.Text = parent.Model.ElementID;
					lblPName.Text = parent.Model.Name;
                    tbAuthor.Text = parent.Model.Author;
                    tbCompany.Text = parent.Model.Company;
                    tbModelRelease.Text = parent.Model.Release;
                    lblCreationDate.Text = parent.Model.LocalizatedDateValue;

                    tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = parent.Model.Attributes.Description;
					tbDescription.ParseNow();
					tbNotes.Text = parent.Model.Attributes.Notes;
					tbNotes.ParseNow();
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in parent.Model.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;

                    tabUseCase.TabPages.Add(pgRequirements);
					RList.DataSource = parent.Model.Requirements;
					RList.IndexDataField = "Name";
					RList.TextDataField = "Description";
					RList.UniqueIDDataField = "UniqueID";
					RList.DataBind();
					if(parent.Model.Requirements.Count > 0)
					{
						RList.SelectedIndex = 0;
					}

					tabUseCase.TabPages.Add(pgGlossary);
					GList.Items.Clear();
					GList.DataSource = parent.Model.Glossary;
					GList.IndexDataField = "Name";
					GList.TextDataField = "Description";
					GList.UniqueIDDataField = "UniqueID";
					GList.DataBind();
					if(parent.Model.Glossary.Count > 0)
					{
						GList.SelectedIndex = 0;
					}

                    tabUseCase.TabPages.Add(pgStakeholders);
                    SList.Items.Clear();
                    SList.DataSource = parent.Model.Stakeholders;
                    SList.IndexDataField = "Name";
                    SList.TextDataField = "Description";
                    SList.UniqueIDDataField = "UniqueID";
                    SList.DataBind();
                    if(parent.Model.Stakeholders.Count > 0)
                    {
                        SList.SelectedIndex = 0;
                    }
				}
				else if(this.currentElement.GetType() == typeof(Package))
				{
					Package package = (Package)this.currentElement;

					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = true;
					pnlActorsContainer.Visible = true;
					pnlUseCasesContainer.Visible = true;
                    pnlModelAdditionalInfo.Visible = false;
					lblPackages.Text = package.Packages.Count.ToString();
					lblActors.Text = package.Actors.Count.ToString();
					lblUseCases.Text = package.UseCases.Count.ToString();

					tabUseCase.TabPages.Add(pgPGeneral);
					lblPOwner.Text = package.Owner.Name;
					lblPID.Text = package.ElementID;
					lblPName.Text = package.Name;

					tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = package.Attributes.Description;
					tbDescription.ParseNow();  
					tbNotes.Text = package.Attributes.Notes;
					tbNotes.ParseNow();
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in package.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;

					tabUseCase.TabPages.Add(pgRequirements);
					RList.DataSource = package.Requirements;
					RList.IndexDataField = "Name";
					RList.TextDataField = "Description";
					RList.UniqueIDDataField = "UniqueID";
					RList.DataBind();
					if(package.Requirements.Count > 0)
					{
						RList.SelectedIndex = 0;
					}
				}
				else if(this.currentElement.GetType() == typeof(Actors))
				{
					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = true;
					pnlUseCasesContainer.Visible = false;
					lblActors.Text = ((Actors)this.currentElement).Owner.Actors.Count.ToString();
				}
				else if(this.currentElement.GetType() == typeof(UseCases))
				{
					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = false;
					pnlUseCasesContainer.Visible = true;
					lblUseCases.Text = ((UseCases)this.currentElement).Owner.UseCases.Count.ToString();
				}
				else if(this.currentElement.GetType() == typeof(Actor))
				{
					Actor actor = (Actor)this.currentElement;

					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = false;
					pnlUseCasesContainer.Visible = false;

					tabUseCase.TabPages.Add(pgAGeneral);
					lblAOwner.Text = actor.Owner.Name;
					lblAID.Text = actor.ElementID;
					lblAName.Text = actor.Name;
					AGList.DataSource = actor.Goals;
					AGList.IndexDataField = "Name";
					AGList.TextDataField = "Description";
					AGList.UniqueIDDataField = "UniqueID";
					AGList.DataBind();
					if(actor.Goals.Count > 0)
					{
						AGList.SelectedIndex = 0;
					}
					
					tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = actor.Attributes.Description;
					tbDescription.ParseNow();
					tbNotes.Text = actor.Attributes.Notes;
					tbNotes.ParseNow();
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in actor.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;

				}
				else if(this.currentElement.GetType() == typeof(UseCase))
				{
					UseCase useCase = (UseCase)this.currentElement;

					// TabPage controls
					tabUseCase.TabPages.Add(pgMain);
					pnlFullPathContainer.Visible = true;
					pnlPackagesContainer.Visible = false;
					pnlActorsContainer.Visible = false;
					pnlUseCasesContainer.Visible = false;

					tabUseCase.TabPages.Add(pgUCGeneral);
					lblUCOwner.Text = useCase.Owner.Name;
					lblUCID.Text = useCase.ElementID;
					lblUCName.Text = useCase.Name;
					tbPreconditions.Text = useCase.Preconditions;
					tbPreconditions.ParseNow();
					tbPostconditions.Text = useCase.Postconditions;
					tbPostconditions.ParseNow();
                    cmbTriggerEvent.SelectedIndex = (int)useCase.Trigger.EventType;
                    tbTriggerDescription.Text = useCase.Trigger.Description;
                    tbTriggerDescription.ParseNow();
					lvActors.Items.Clear();
					foreach(ActiveActor aactor in useCase.ActiveActors)
					{
						Actor actor = (Actor)parent.Model.FindElementByUniqueID(aactor.ActorUniqueID);
						ListViewItem lvi = new ListViewItem();
						lvi.Text = actor.Name;
						if(aactor.IsPrimary)
						{
							lvi.SubItems.Add("X");
						}
						else
						{
							lvi.SubItems.Add("");
						}
						this.lvActors.Items.Add(lvi);
					}
					btnRemoveActor.Enabled = false;
					btnSetPrimaryActor.Enabled = false;

					tabUseCase.TabPages.Add(pgDetails);
					OIList.DataSource = useCase.OpenIssues;
					OIList.IndexDataField = "Name";
					OIList.TextDataField = "Description";
					OIList.UniqueIDDataField = "UniqueID";
					OIList.DataBind();
					if(useCase.OpenIssues.Count > 0)
					{
						OIList.SelectedIndex = 0;
					}
					cmbLevel.SelectedIndex = (int)useCase.Level;
					cmbComplexity.SelectedIndex = (int)useCase.Complexity;
					cmbStatus.SelectedIndex = (int)useCase.Status;
					cmbImplementation.SelectedIndex = (int)useCase.Implementation;
					tbPriority.Text = useCase.Priority.ToString();
					tbAssignedTo.Text = useCase.AssignedTo;
					tbRelease.Text = useCase.Release;
					
					tabUseCase.TabPages.Add(pgAttributes);
					tbDescription.Text = useCase.Attributes.Description;
					tbDescription.ParseNow();
					tbNotes.Text = useCase.Attributes.Notes;
					tbNotes.ParseNow();
					lvRelatedDocs.Items.Clear();
					foreach(RelatedDocument rd in useCase.Attributes.RelatedDocuments)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = rd.FileName;
						this.lvRelatedDocs.Items.Add(lvi);
					}
					btnRemoveRelatedDoc.Enabled = false;
					btnOpenRelatedDoc.Enabled = false;
					
					tabUseCase.TabPages.Add(pgFlowOfEvents);
					UCList.DataSource = useCase.Steps;
					UCList.IndexDataField = "Name";
					UCList.TextDataField = "Description";
					UCList.UniqueIDDataField = "UniqueID";
					UCList.DataBind();
					this.UpdateUCList(useCase);
					if(useCase.Steps.Count > 0)
					{
						UCList.SelectedIndex = 0;
					}
					
					tabUseCase.TabPages.Add(pgProse);
					tbProse.Text = useCase.Prose;
					tbProse.ParseNow();

					tabUseCase.TabPages.Add(pgHistory);
					lvHistory.Items.Clear();
					foreach(HistoryItem hi in useCase.HistoryItems)
					{
						ListViewItem lvi = new ListViewItem();
						lvi.Text = hi.LocalizatedDateValue;
						if(hi.Type == HistoryItem.HistoryType.Implementation)
						{
							lvi.SubItems.Add(this.localizer.GetValue("Globals","Implementation"));
							lvi.SubItems.Add((string)cmbImplementation.Items[hi.Action]);
							lvi.SubItems.Add(hi.Notes);
						}
						else
						{
							lvi.SubItems.Add(this.localizer.GetValue("Globals","Status"));
							lvi.SubItems.Add((string)cmbStatus.Items[hi.Action]);
							lvi.SubItems.Add(hi.Notes);
						}
						this.lvHistory.Items.Add(lvi);
					}
					btnRemoveHistoryItem.Enabled = false;
				}			
			}

			IdentificableObjectCollection coll = (this.currentElement as IdentificableObjectCollection);
			if(coll != null)
			{
				lblFullPath.Text = ((IdentificableObjectCollection)this.currentElement).Path;
			}
			else
			{
				lblFullPath.Text = ((IdentificableObject)this.currentElement).Path;
				TabPage tabPage = ((IdentificableObject)this.currentElement).ObjectUserViewStatus.CurrentTabPage;
				if(tabPage != null)
				{
					tabUseCase.SelectedTab = tabPage;
				}
			}

			parent.UnlockModified();

			Win32.SendMessage(this.Handle,Win32.WM_SETREDRAW,1,(IntPtr)0);
			this.Refresh();

            this.Cursor = Cursors.Default;
        }

        private void UpdateUCList(UseCase useCase)
        {
            for (int counter = 0; counter < useCase.Steps.Count; counter++)
            {
                Step step = (Step)useCase.Steps[counter];
                if (step.Dependency.Type != DependencyItem.ReferenceType.None)
                {
                    UCList.Items[counter].ReadOnly = true;
                    UCList.Items[counter].IndexImage = imgListSteps.Images[3];
                }
                else
                {
                    switch (step.Type)
                    {
                        case Step.StepType.Default:
                            UCList.Items[counter].IndexImage = imgListSteps.Images[0];
                            break;
                        case Step.StepType.Alternative:
                            UCList.Items[counter].IndexImage = imgListSteps.Images[2];
                            break;
                        case Step.StepType.Child:
                        case Step.StepType.AlternativeChild:
                            UCList.Items[counter].IndexImage = imgListSteps.Images[1];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void ShowGlossaryItem(String token)
        {
            tabUseCase.SelectedTab = pgGlossary;
            foreach(IndexedListItem ili in GList.Items)
            {
                if(token == ili.Index)
                {
                    ili.Selected = true;
                }
            }
        }

        public void ShowStakeholder(String token)
        {
            tabUseCase.SelectedTab = pgStakeholders;
            foreach(IndexedListItem ili in SList.Items)
            {
                if(token == ili.Index)
                {
                    ili.Selected = true;
                }
            }
        }

        private void btnAddStep_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            int currentSelectedIndex = UCList.SelectedIndex;
            IndexedListItem ili = null;

            if (currentSelectedIndex != -1)
            {
                ili = UCList.Items[UCList.SelectedIndex];
                Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
                currentSelectedIndex = useCase.AddStep(
                    previousStep,
                    Step.StepType.Default,
                    "",
                    null,
                    DependencyItem.ReferenceType.None);
            }
            else
            {
                currentSelectedIndex = useCase.AddStep(
                    null,
                    Step.StepType.Default,
                    "",
                    null,
                    DependencyItem.ReferenceType.None);
            }

            this.Cursor = Cursors.WaitCursor;
            UCList.DataBind();
            this.Cursor = Cursors.Default;
            this.UpdateUCList(useCase);

            UCList.SelectedIndex = currentSelectedIndex;
            parent.SetModified(true);
        }

        private void btnAddAltStep_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            int currentSelectedIndex = UCList.SelectedIndex;
            IndexedListItem ili = null;

            ili = UCList.Items[UCList.SelectedIndex];
            Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
            currentSelectedIndex = useCase.AddStep(
                previousStep,
                Step.StepType.Alternative,
                "",
                null,
                DependencyItem.ReferenceType.None);

            this.Cursor = Cursors.WaitCursor;
            UCList.DataBind();
            this.Cursor = Cursors.Default;
            this.UpdateUCList(useCase);

            UCList.SelectedIndex = currentSelectedIndex;
            parent.SetModified(true);
        }

        private void btnAddSubStep_Click(object sender, EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            int currentSelectedIndex = UCList.SelectedIndex;
            IndexedListItem ili = null;

            ili = UCList.Items[UCList.SelectedIndex];
            Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
            currentSelectedIndex = useCase.AddStep(
                previousStep,
                Step.StepType.Child,
                "",
                null,
                DependencyItem.ReferenceType.None);

            this.Cursor = Cursors.WaitCursor;
            UCList.DataBind();
            this.Cursor = Cursors.Default;
            this.UpdateUCList(useCase);

            UCList.SelectedIndex = currentSelectedIndex;
            parent.SetModified(true);
        }

        private void btnInsertStep_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            int currentSelectedIndex = UCList.SelectedIndex;
            IndexedListItem ili = null;

            ili = UCList.Items[UCList.SelectedIndex];
            Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
            currentSelectedIndex = useCase.InsertStep(
                previousStep,
                "",
                null,
                DependencyItem.ReferenceType.None);

            this.Cursor = Cursors.WaitCursor;
            UCList.DataBind();
            this.Cursor = Cursors.Default;
            this.UpdateUCList(useCase);

            UCList.SelectedIndex = currentSelectedIndex;
            parent.SetModified(true);
        }


        private void btnAddRefStep_Click(object sender, System.EventArgs e)
        {
            frmRefSelector frm = new frmRefSelector((UseCase)this.currentElement, parent.Model, this.localizer);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                UseCase useCase = (UseCase)this.currentElement;
                int currentSelectedIndex = UCList.SelectedIndex;
                IndexedListItem ili = null;

                if (currentSelectedIndex != -1)
                {
                    ili = UCList.Items[UCList.SelectedIndex];
                    Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
                    currentSelectedIndex = useCase.AddStep(
                        previousStep,
                        Step.StepType.Default,
                        frm.Stereotype,
                        frm.SelectedUseCase,
                        frm.ReferenceType);
                }
                else
                {
                    currentSelectedIndex = useCase.AddStep(
                        null,
                        Step.StepType.Default,
                        frm.Stereotype,
                        frm.SelectedUseCase,
                        frm.ReferenceType);
                }

                this.Cursor = Cursors.WaitCursor;
                UCList.DataBind();
                this.Cursor = Cursors.Default;
                this.UpdateUCList(useCase);

                UCList.SelectedIndex = currentSelectedIndex;
                parent.SetModified(true);
            }
        }

        private void btnInsertRefStep_Click(object sender, System.EventArgs e)
        {
            frmRefSelector frm = new frmRefSelector((UseCase)this.currentElement, parent.Model, this.localizer);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                UseCase useCase = (UseCase)this.currentElement;
                int currentSelectedIndex = UCList.SelectedIndex;
                IndexedListItem ili = null;

                ili = UCList.Items[UCList.SelectedIndex];
                Step previousStep = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
                currentSelectedIndex = useCase.InsertStep(
                    previousStep,
                    frm.Stereotype,
                    frm.SelectedUseCase,
                    frm.ReferenceType);

                this.Cursor = Cursors.WaitCursor;
                UCList.DataBind();
                this.Cursor = Cursors.Default;
                this.UpdateUCList(useCase);

                UCList.SelectedIndex = currentSelectedIndex;
                parent.SetModified(true);
            }
        }

        private void btnRemoveStep_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            int currentSelectedIndex = UCList.SelectedIndex;

            IndexedListItem ili = UCList.Items[currentSelectedIndex];
            Step step = (Step)useCase.FindStepByUniqueID((String)ili.Tag);
            useCase.RemoveStep(step);

            this.Cursor = Cursors.WaitCursor;
            UCList.DataBind();
            this.Cursor = Cursors.Default;
            this.UpdateUCList(useCase);

            if (currentSelectedIndex < UCList.Items.Count)
            {
                UCList.SelectedIndex = currentSelectedIndex;
            }
            else
            {
                if (UCList.Items.Count > 0)
                {
                    UCList.SelectedIndex = UCList.Items.Count - 1;
                }
                else
                {
                    btnAddStep.Enabled = true;
                    btnAddSubStep.Enabled = false;
                    btnAddAltStep.Enabled = false;
                    btnInsertStep.Enabled = false;
                    btnAddRefStep.Enabled = true;
                    btnInsertRefStep.Enabled = false;
                    btnRemoveStep.Enabled = false;
                }
            }
            parent.SetModified(true);
        }

        private void UCList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void UCList_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            IndexedListItem item = (IndexedListItem)sender;
            Step step = (Step)useCase.FindStepByUniqueID((String)item.Tag);
            if (step != null)
            {
                step.Description = item.Text;
            }
            parent.SetModified(true);
        }

        private void UCList_SelectedChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            Step step = (Step)useCase.FindStepByUniqueID((String)((IndexedListItem)sender).Tag);
            switch (step.Type)
            {
                case Step.StepType.Default:
                    btnAddStep.Enabled = true;
                    btnInsertStep.Enabled = true;
                    if(useCase.StepHasChildren(step))
                    {
                        btnAddSubStep.Enabled = false;
                    }
                    else
                    {
                        btnAddSubStep.Enabled = true;
                    }
                    if(useCase.StepHasAlternatives(step))
                    {
                        btnAddAltStep.Enabled = false;
                    }
                    else
                    {
                        btnAddAltStep.Enabled = true;
                    }
                    btnAddRefStep.Enabled = true;
                    btnInsertRefStep.Enabled = true;
                    btnRemoveStep.Enabled = true;
                    break;
                case Step.StepType.Alternative:
                    btnAddStep.Enabled = true;
                    btnInsertStep.Enabled = true;
                    if(useCase.StepHasChildren(step))
                    {
                        btnAddSubStep.Enabled = false;
                    }
                    else
                    {
                        btnAddSubStep.Enabled = true;
                    }
                    btnAddAltStep.Enabled = false;
                    btnAddRefStep.Enabled = true;
                    btnInsertRefStep.Enabled = true;
                    btnRemoveStep.Enabled = true;
                    break;
                case Step.StepType.Child:
                    btnAddStep.Enabled = true;
                    btnInsertStep.Enabled = true;
                    btnAddSubStep.Enabled = false;
                    btnAddAltStep.Enabled = false;
                    btnAddRefStep.Enabled = true;
                    btnInsertRefStep.Enabled = true;
                    btnRemoveStep.Enabled = true;
                    break;
                case Step.StepType.AlternativeChild:
                    btnAddStep.Enabled = true;
                    btnInsertStep.Enabled = true;
                    btnAddSubStep.Enabled = false;
                    btnAddAltStep.Enabled = false;
                    btnAddRefStep.Enabled = true;
                    btnInsertRefStep.Enabled = true;
                    btnRemoveStep.Enabled = true;
                    break;
            }
        }

        private void tbProse_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void tbProse_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            UseCaseMakerControls.LinkEnabledRTB item = (UseCaseMakerControls.LinkEnabledRTB)sender;
            useCase.Prose = item.Text;
            parent.SetModified(true);
        }

        private void btnAddOpenIssue_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            int currentSelectedIndex = useCase.AddOpenIssue();

            this.Cursor = Cursors.WaitCursor;
            OIList.DataBind();
            this.Cursor = Cursors.Default;

            OIList.SelectedIndex = currentSelectedIndex;
            parent.SetModified(true);
        }

        private void btnRemoveOpenIssue_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            int currentSelectedIndex = OIList.SelectedIndex;

            IndexedListItem ili = OIList.Items[currentSelectedIndex];
            OpenIssue openIssue = (OpenIssue)useCase.FindOpenIssueByUniqueID((String)ili.Tag);
            useCase.RemoveOpenIssue(openIssue);

            this.Cursor = Cursors.WaitCursor;
            OIList.DataBind();
            this.Cursor = Cursors.Default;

            if (currentSelectedIndex < OIList.Items.Count)
            {
                OIList.SelectedIndex = currentSelectedIndex;
            }
            else
            {
                if (OIList.Items.Count > 0)
                {
                    OIList.SelectedIndex = OIList.Items.Count - 1;
                }
                else
                {
                    btnAddOpenIssue.Enabled = true;
                    btnRemoveOpenIssue.Enabled = false;
                }
            }
            parent.SetModified(true);
        }

        private void OIList_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            IndexedListItem item = (IndexedListItem)sender;
            OpenIssue openIssue = (OpenIssue)useCase.FindOpenIssueByUniqueID((String)item.Tag);
            if (openIssue != null)
            {
                openIssue.Description = item.Text;
            }
            parent.SetModified(true);
        }

        private void OIList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void OIList_SelectedChanged(object sender, System.EventArgs e)
        {
            btnAddOpenIssue.Enabled = true;
            btnRemoveOpenIssue.Enabled = true;
        }

        private void btnAddRequirement_Click(object sender, System.EventArgs e)
        {
            Package package = (Package)this.currentElement;
            int currentSelectedIndex = package.AddRequrement();

            this.Cursor = Cursors.WaitCursor;
            RList.DataBind();
            this.Cursor = Cursors.Default;

            RList.SelectedIndex = currentSelectedIndex;
            parent.SetModified(true);
        }

        private void btnRemoveRequirement_Click(object sender, System.EventArgs e)
        {
            Package package = (Package)this.currentElement;
            int currentSelectedIndex = RList.SelectedIndex;

            IndexedListItem ili = RList.Items[currentSelectedIndex];
            Requirement requirement = (Requirement)package.FindRequirementByUniqueID((String)ili.Tag);
            package.RemoveRequirement(requirement);

            this.Cursor = Cursors.WaitCursor;
            RList.DataBind();
            this.Cursor = Cursors.Default;

            if (currentSelectedIndex < RList.Items.Count)
            {
                RList.SelectedIndex = currentSelectedIndex;
            }
            else
            {
                if (RList.Items.Count > 0)
                {
                    RList.SelectedIndex = RList.Items.Count - 1;
                }
                else
                {
                    btnAddRequirement.Enabled = true;
                    btnShowReqDetails.Enabled = false;
                    btnRemoveRequirement.Enabled = false;
                }
            }

            this.UpdateView();
            parent.SetModified(true);
        }

        private void btnShowReqDetails_Click(object sender, EventArgs e)
        {
            Package package = (Package)this.currentElement;
            int currentSelectedIndex = RList.SelectedIndex;

            IndexedListItem ili = RList.Items[currentSelectedIndex];
            Requirement requirement = (Requirement)package.FindRequirementByUniqueID((String)ili.Tag);

            if(requirement != null)
            {
                frmRequirementEditor frm = new frmRequirementEditor(requirement,parent.Model,this.localizer);
                frm.ShowDialog(this);
                if(frm.Modified)
                {
                    parent.SetModified(true);
                }
                if(frm.Token != null)
                {
                    parent.GoToDefinition(frm.Token);
                }
            }
        }

        private void RList_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            Package package = (Package)this.currentElement;
            IndexedListItem item = (IndexedListItem)sender;
            Requirement requirement = (Requirement)package.FindRequirementByUniqueID((String)item.Tag);
            if (requirement != null)
            {
                requirement.Description = item.Text;
            }
            parent.SetModified(true);
        }

        private void RList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void RList_SelectedChange(object sender, System.EventArgs e)
        {
            btnAddRequirement.Enabled = true;
            btnShowReqDetails.Enabled = true;
            btnRemoveRequirement.Enabled = true;
        }

        private void btnANameChange_Click(object sender, System.EventArgs e)
        {
            parent.ElementNameChange(lblAName);
        }

        private void btnUCNameChange_Click(object sender, System.EventArgs e)
        {
            parent.ElementNameChange(lblUCName);
        }

        private void btnPNameChange_Click(object sender, System.EventArgs e)
        {
            parent.ElementNameChange(lblPName);
        }

        private void btnAddGlossaryItem_Click(object sender, System.EventArgs e)
        {
            frmCreator frm = new frmCreator(this.localizer, this.localizer.GetValue("Globals", "GlossaryItem"));
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                Model model = (Model)this.currentElement;
                GlossaryItem gi =
                    parent.Model.NewGlossaryItem(frm.tbName.Text, frmMain.defaultGPrefix, parent.Model.Glossary.GetNextFreeID());
                parent.Model.AddGlossaryItem(gi);
                parent.Model.Glossary.Sorted("Name");
                parent.AddGlossaryItemHD(gi.Name);

                this.UpdateView();
                for(int counter = 0; counter < parent.Model.Glossary.Count; counter++)
                {
                    if((string)GList.Items[counter].Tag == gi.UniqueID)
                    {
                        GList.SelectedIndex = counter;
                    }
                }

                parent.SetModified(true);
            }
            frm.Dispose();
        }

        private void btnChangeGlossaryItem_Click(object sender, System.EventArgs e)
        {
            Model model = (Model)this.currentElement;

            IndexedListItem ili = GList.Items[GList.SelectedIndex];
            ili.Index = parent.ElementNameChange((IdentificableObject)parent.Model.GetGlossaryItem((String)ili.Tag));

            for (int counter = 0; counter < parent.Model.Glossary.Count; counter++)
            {
                if ((string)GList.Items[counter].Tag == (String)ili.Tag)
                {
                    GList.SelectedIndex = counter;
                }
            }
        }

        private void btnRemoveGlossaryItem_Click(object sender, System.EventArgs e)
        {
            Model model = (Model)this.currentElement;
            int currentSelectedIndex = GList.SelectedIndex;

            IndexedListItem ili = GList.Items[currentSelectedIndex];
            GlossaryItem gi = parent.Model.GetGlossaryItem((String)ili.Tag);

            frmDeleter frm = new frmDeleter(this.localizer, this.localizer.GetValue("Globals", "GlossaryItem"));

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                parent.Model.RemoveGlossaryItem(
                    gi,
                    "\"",
                    "\"",
                    "{",
                    "}",
                    frm.cbDontMark.Checked);

                parent.RemoveGlossaryItemHD(gi.Name);

                this.Cursor = Cursors.WaitCursor;
                GList.DataBind();
                this.Cursor = Cursors.Default;

                if (currentSelectedIndex < GList.Items.Count)
                {
                    GList.SelectedIndex = currentSelectedIndex;
                }
                else
                {
                    if (GList.Items.Count > 0)
                    {
                        GList.SelectedIndex = GList.Items.Count - 1;
                    }
                    else
                    {
                        btnAddGlossaryItem.Enabled = true;
                        btnRemoveGlossaryItem.Enabled = false;
                        btnChangeGlossaryItem.Enabled = false;
                    }
                }
                this.UpdateView();
                parent.SetModified(true);
            }
            frm.Dispose();
        }

        private void GList_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void GList_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            Model model = (Model)this.currentElement;
            IndexedListItem item = (IndexedListItem)sender;
            GlossaryItem gi = (GlossaryItem)parent.Model.GetGlossaryItem((String)item.Tag);
            if (gi != null)
            {
                gi.Description = item.Text;
            }
            parent.SetModified(true);
        }

        private void GList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void GList_SelectedChanged(object sender, System.EventArgs e)
        {
            btnAddGlossaryItem.Enabled = true;
            btnRemoveGlossaryItem.Enabled = true;
            btnChangeGlossaryItem.Enabled = true;
        }


        private void btnAddStakeholder_Click(object sender, EventArgs e)
        {
            frmCreator frm = new frmCreator(this.localizer, this.localizer.GetValue("Globals", "Stakeholder"));
            if(frm.ShowDialog(this) == DialogResult.OK)
            {
                Model model = (Model)this.currentElement;
                Stakeholder stakeholder =
                    parent.Model.NewStakeholder(frm.tbName.Text, frmMain.defaultSPrefix, parent.Model.Stakeholders.GetNextFreeID());
                parent.Model.AddStakeholder(stakeholder);
                parent.Model.Stakeholders.Sorted("Name");
                parent.AddStakeholderHD(stakeholder.Name);

                this.UpdateView();
                for(int counter = 0; counter < parent.Model.Stakeholders.Count; counter++)
                {
                    if((string)SList.Items[counter].Tag == stakeholder.UniqueID)
                    {
                        SList.SelectedIndex = counter;
                    }
                }

                parent.SetModified(true);
            }
            frm.Dispose();
        }

        private void btnChangeStakeholder_Click(object sender, EventArgs e)
        {
            Model model = (Model)this.currentElement;

            IndexedListItem ili = SList.Items[SList.SelectedIndex];
            ili.Index = parent.ElementNameChange((IdentificableObject)parent.Model.GetStakeholder((String)ili.Tag));

            for(int counter = 0; counter < parent.Model.Stakeholders.Count; counter++)
            {
                if((string)SList.Items[counter].Tag == (String)ili.Tag)
                {
                    SList.SelectedIndex = counter;
                }
            }
        }

        private void btnRemoveStakeholder_Click(object sender, EventArgs e)
        {
            Model model = (Model)this.currentElement;
            int currentSelectedIndex = SList.SelectedIndex;

            IndexedListItem ili = SList.Items[currentSelectedIndex];
            Stakeholder stakeholder = parent.Model.GetStakeholder((String)ili.Tag);

            frmDeleter frm = new frmDeleter(this.localizer, this.localizer.GetValue("Globals", "Stakeholder"));

            if(frm.ShowDialog(this) == DialogResult.OK)
            {
                parent.Model.RemoveStakeholder(
                    stakeholder,
                    "\"",
                    "\"",
                    "{",
                    "}",
                    frm.cbDontMark.Checked);

                parent.RemoveStakeholderHD(stakeholder.Name);

                this.Cursor = Cursors.WaitCursor;
                SList.DataBind();
                this.Cursor = Cursors.Default;

                if(currentSelectedIndex < SList.Items.Count)
                {
                    SList.SelectedIndex = currentSelectedIndex;
                }
                else
                {
                    if(SList.Items.Count > 0)
                    {
                        SList.SelectedIndex = SList.Items.Count - 1;
                    }
                    else
                    {
                        btnAddStakeholder.Enabled = true;
                        btnRemoveStakeholder.Enabled = false;
                        btnChangeStakeholder.Enabled = false;
                    }
                }
                this.UpdateView();
                parent.SetModified(true);
            }
            frm.Dispose();
        }

        private void SList_ItemClick(object sender, MouseOverTokenEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void SList_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            Model model = (Model)this.currentElement;
            IndexedListItem item = (IndexedListItem)sender;
            Stakeholder stakeholder = (Stakeholder)parent.Model.GetStakeholder((String)item.Tag);
            if(stakeholder != null)
            {
                stakeholder.Description = item.Text;
            }
            parent.SetModified(true);
        }

        private void SList_MouseOverToken(object sender, MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if(element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if(element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void SList_SelectedChanged(object sender, EventArgs e)
        {
            btnAddStakeholder.Enabled = true;
            btnRemoveStakeholder.Enabled = true;
            btnChangeStakeholder.Enabled = true;
        }

        private void btnAddActor_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;

            frmChooser frm = new frmChooser(
                parent.Model,
                this.localizer,
                this.localizer.GetValue("Globals", "Actor"));
            frm.ShowActors = true;
            frm.ShowUseCases = false;
            frm.PackageSelectionIsValid = false;
            frm.UseCaseSelectionIsValid = false;
            frm.ActorSelectionIsValid = true;
            frm.Initialize();

            if(frm.ShowDialog(this) == DialogResult.OK)
            {
                Actor actor = (Actor)frm.SelectedElement;
                if(useCase.ActiveActors.FindByUniqueID(actor.UniqueID) != null)
                {
                    // [Element already present!]
                    MessageBox.Show(this, this.localizer.GetValue("UserMessages", "elementAlreadyPresent"));
                    return;
                }
                useCase.AddActiveActor(actor);
                this.UpdateView();
                tabUseCase.SelectedTab = pgUCGeneral;
                parent.SetModified(true);
            }

            frm.Dispose();
        }

        private void btnRemoveActor_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            Actor actor = (Actor)parent.Model.FindElementByName(lvActors.SelectedItems[0].Text);
            useCase.RemoveActiveActor(actor);
            this.UpdateView();
            tabUseCase.SelectedTab = pgUCGeneral;
            parent.SetModified(true);
        }

        private void btnSetPrimaryActor_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;

            foreach (ActiveActor aactor in useCase.ActiveActors)
            {
                aactor.IsPrimary = false;
            }

            ActiveActor selectedAActor = (ActiveActor)useCase.ActiveActors[lvActors.SelectedIndices[0]];
            selectedAActor.IsPrimary = true;
            this.UpdateView();
            tabUseCase.SelectedTab = pgUCGeneral;
            parent.SetModified(true);
        }

        private void tbPriority_TextChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            if (tbPriority.Text != string.Empty)
            {
                useCase.Priority = Convert.ToInt32(tbPriority.Text);
            }
            parent.SetModified(true);
        }

        private void tbPriority_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9')
                || e.KeyChar == 0x1E || e.KeyChar == 0x08 || e.KeyChar == 0x1B)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void tbPriority_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;

            if (tbPriority.Text == string.Empty)
            {
                tbPriority.Text = useCase.Priority.ToString();
            }
            if (Convert.ToInt32(tbPriority.Text) == 0)
            {
                tbPriority.Text = Convert.ToString(1);
            }
            tbPriority.Text = useCase.Priority.ToString();
        }

        private void tbAssignedTo_TextChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            useCase.AssignedTo = tbAssignedTo.Text;
            parent.SetModified(true);
        }

        private void tbRelease_TextChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            useCase.Release = tbRelease.Text;
            parent.SetModified(true);
        }

        private void cmbLevel_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(UseCase.LevelValue));
            foreach (Int32 _value in _values)
            {
                if (_value == cmbLevel.SelectedIndex)
                {
                    useCase.Level = (UseCase.LevelValue)cmbLevel.SelectedIndex;
                    found = true;
                }
            }

            if (!found)
            {
                cmbLevel.SelectedIndex = (int)useCase.Level;
            }
            parent.SetModified(true);
        }

        private void cmbComplexity_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(UseCase.ComplexityValue));
            foreach (Int32 _value in _values)
            {
                if (_value == cmbComplexity.SelectedIndex)
                {
                    useCase.Complexity = (UseCase.ComplexityValue)cmbComplexity.SelectedIndex;
                    found = true;
                }
            }

            if (!found)
            {
                cmbComplexity.SelectedIndex = (int)useCase.Complexity;
            }
            parent.SetModified(true);
        }

        private void cmbStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(UseCase.StatusValue));
            foreach (Int32 _value in _values)
            {
                if (_value == cmbStatus.SelectedIndex)
                {
                    useCase.Status = (UseCase.StatusValue)cmbStatus.SelectedIndex;
                    found = true;
                }
            }

            if (!found)
            {
                cmbStatus.SelectedIndex = (int)useCase.Status;
            }
            parent.SetModified(true);
        }

        private void cmbImplementation_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(UseCase.ImplementationValue));
            foreach (Int32 _value in _values)
            {
                if (_value == cmbImplementation.SelectedIndex)
                {
                    useCase.Implementation = (UseCase.ImplementationValue)cmbImplementation.SelectedIndex;
                    found = true;
                }
            }

            if (!found)
            {
                cmbImplementation.SelectedIndex = (int)useCase.Implementation;
            }
            parent.SetModified(true);
        }

        private void lvActors_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvActors.SelectedIndices.Count == 0)
            {
                btnRemoveActor.Enabled = false;
                btnSetPrimaryActor.Enabled = false;
            }
            else
            {
                btnRemoveActor.Enabled = true;
                btnSetPrimaryActor.Enabled = true;
            }
        }

        private void lvRelatedDocs_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvRelatedDocs.SelectedIndices.Count == 0)
            {
                btnRemoveRelatedDoc.Enabled = false;
                btnOpenRelatedDoc.Enabled = false;
            }
            else
            {
                btnRemoveRelatedDoc.Enabled = true;
                btnOpenRelatedDoc.Enabled = true;
            }
        }

        private void btnAddRelatedDoc_Click(object sender, System.EventArgs e)
        {
            if (selectDocFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                CommonAttributes attributes = null;
                if (this.currentElement.GetType() == typeof(Model) ||
                    this.currentElement.GetType() == typeof(Package))
                {
                    attributes = ((Package)this.currentElement).Attributes;
                }
                if (this.currentElement.GetType() == typeof(UseCase))
                {
                    attributes = ((UseCase)this.currentElement).Attributes;
                }
                if (this.currentElement.GetType() == typeof(Actor))
                {
                    attributes = ((Actor)this.currentElement).Attributes;
                }

                foreach (RelatedDocument rd in attributes.RelatedDocuments)
                {
                    if (rd.FileName == selectDocFileDialog.FileName)
                    {
                        // [File already present!]
                        MessageBox.Show(this, this.localizer.GetValue("UserMessages", "fileAlreadyPresent"));
                        return;
                    }
                }
                RelatedDocument newRd = new RelatedDocument();
                newRd.FileName = selectDocFileDialog.FileName;
                attributes.RelatedDocuments.Add(newRd);

                this.UpdateView();
                tabUseCase.SelectedTab = pgAttributes;
                parent.SetModified(true);
            }
        }

        private void btnRemoveRelatedDoc_Click(object sender, System.EventArgs e)
        {
            CommonAttributes attributes = null;
            if (this.currentElement.GetType() == typeof(Model) ||
                this.currentElement.GetType() == typeof(Package))
            {
                attributes = ((Package)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(UseCase))
            {
                attributes = ((UseCase)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(Actor))
            {
                attributes = ((Actor)this.currentElement).Attributes;
            }

            for (int index = attributes.RelatedDocuments.Count - 1; index >= 0; index--)
            {
                RelatedDocument rd = (RelatedDocument)attributes.RelatedDocuments[index];
                if (rd.FileName == lvRelatedDocs.SelectedItems[0].Text)
                {
                    attributes.RelatedDocuments.RemoveAt(index);
                }
            }

            this.UpdateView();
            tabUseCase.SelectedTab = pgAttributes;
            parent.SetModified(true);
        }

        private void lvRelatedDocs_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)lvRelatedDocs.GetItemAt(e.X, e.Y);
            if (lvi != null)
            {
                lvRelatedDocsTooltip.Active = true;
                lvRelatedDocsTooltip.SetToolTip(lvRelatedDocs, lvi.Text);
            }
            else
            {
                lvRelatedDocsTooltip.Active = false;
            }
        }

        private void lvActors_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)lvActors.GetItemAt(e.X, e.Y);
            if (lvi != null)
            {
                lvActorsTooltip.Active = true;
                lvActorsTooltip.SetToolTip(lvActors, lvi.Text);
            }
            else
            {
                lvActorsTooltip.Active = false;
            }
        }

        private void btnOpenRelatedDoc_Click(object sender, System.EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = lvRelatedDocs.SelectedItems[0].Text;
            try
            {
                process.Start();
            }
            catch (Win32Exception ex)
            {
                // [Cannot open file!]
                MessageBox.Show(this, this.localizer.GetValue("UserMessages", "cannotOpenFile") + "\r\n" +
                    ex.Message);
            }
        }

        private void btnStatusToHistory_Click(object sender, System.EventArgs e)
        {
            frmHistoryNotes frm = new frmHistoryNotes(this.localizer);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                UseCase useCase = (UseCase)this.currentElement;
                useCase.AddHistoryItem(
                    DateTime.Now,
                    HistoryItem.HistoryType.Status,
                    cmbStatus.SelectedIndex,
                    frm.tbNotes.Text.Replace("\r\n", " "));
                this.UpdateView();
                tabUseCase.SelectedTab = pgDetails;
                parent.SetModified(true);
            }
            frm.Dispose();
        }

        private void btnImplToHistory_Click(object sender, System.EventArgs e)
        {
            frmHistoryNotes frm = new frmHistoryNotes(this.localizer);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                UseCase useCase = (UseCase)this.currentElement;
                useCase.AddHistoryItem(
                    DateTime.Now,
                    HistoryItem.HistoryType.Implementation,
                    cmbImplementation.SelectedIndex,
                    frm.tbNotes.Text.Replace("\r\n", " "));
                this.UpdateView();
                tabUseCase.SelectedTab = pgDetails;
                parent.SetModified(true);
            }
            frm.Dispose();
        }

        private void btnRemoveHistoryItem_Click(object sender, System.EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;

            useCase.RemoveHistoryItem(lvHistory.SelectedIndices[0]);

            this.UpdateView();
            tabUseCase.SelectedTab = pgHistory;
            parent.SetModified(true);
        }

        private void lvHistory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lvHistory.SelectedIndices.Count == 0)
            {
                btnRemoveHistoryItem.Enabled = false;
            }
            else
            {
                btnRemoveHistoryItem.Enabled = true;
            }
        }

        private void lvHistory_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            lvHistory.Columns[3].Width =
                lvHistory.ClientRectangle.Width -
                lvHistory.Columns[0].Width -
                lvHistory.Columns[1].Width -
                lvHistory.Columns[2].Width;
        }

        private void lvActors_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            lvActors.Columns[0].Width = lvActors.ClientRectangle.Width - lvActors.Columns[1].Width;
        }

        private void lvRelatedDocs_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
        {
            lvRelatedDocs.Columns[0].Width = lvRelatedDocs.ClientRectangle.Width;
        }


        private void OIList_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void UCList_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void tbProse_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void RList_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void tbDescription_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void tbDescription_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            CommonAttributes attributes = null;

            if (this.currentElement.GetType() == typeof(Model))
            {
                attributes = ((Model)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(Package))
            {
                attributes = ((Package)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(Actor))
            {
                attributes = ((Actor)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(UseCase))
            {
                attributes = ((UseCase)this.currentElement).Attributes;
            }

            attributes.Description = tbDescription.Text;
            parent.ElementAttributesDescriptionChanged(this.currentElement);
            parent.SetModified(true);
        }

        private void tbDescription_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void tbNotes_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void tbNotes_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            CommonAttributes attributes = null;

            if (this.currentElement.GetType() == typeof(Model))
            {
                attributes = ((Model)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(Package))
            {
                attributes = ((Package)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(Actor))
            {
                attributes = ((Actor)this.currentElement).Attributes;
            }
            if (this.currentElement.GetType() == typeof(UseCase))
            {
                attributes = ((UseCase)this.currentElement).Attributes;
            }

            attributes.Notes = tbNotes.Text;
            parent.SetModified(true);
        }

        private void tbNotes_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void tbPreconditions_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void tbPreconditions_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            useCase.Preconditions = tbPreconditions.Text;
            parent.SetModified(true);
        }

        private void tbPreconditions_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void tbPostconditions_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void tbPostconditions_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            useCase.Postconditions = tbPostconditions.Text;
            parent.SetModified(true);
        }

        private void tbPostconditions_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void tbTriggerDescription_MouseOverToken(object sender, MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if(element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if(element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void tbTriggerDescription_ItemTextChanged(object sender, ItemTextChangedEventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            useCase.Trigger.Description = tbTriggerDescription.Text;
            parent.SetModified(true);
        }

        private void tbTriggerDescription_ItemClick(object sender, MouseOverTokenEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void cmbTriggerEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseCase useCase = (UseCase)this.currentElement;
            Boolean found = false;

            Array _values = Enum.GetValues(typeof(TriggerEvent.EventTypeValue));
            foreach(Int32 _value in _values)
            {
                if(_value == cmbTriggerEvent.SelectedIndex)
                {
                    useCase.Trigger.EventType = (TriggerEvent.EventTypeValue)cmbTriggerEvent.SelectedIndex;
                    found = true;
                }
            }

            if(!found)
            {
                cmbTriggerEvent.SelectedIndex = (int)useCase.Trigger.EventType;
            }
            parent.SetModified(true);
        }

        private void tabUseCase_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TabControl tc = (TabControl)sender;

            if (!parent.IsModifiedLocked())
            {
                ((IdentificableObject)this.currentElement).ObjectUserViewStatus.CurrentTabPage = tc.SelectedTab;
            }
        }

        private void AGList_ItemClick(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                parent.EnableElementTokenContextMenu(e.Item, e.OverToken, new Point(e.X, e.Y));
            }
        }

        private void AGList_ItemTextChanged(object sender, UseCaseMakerControls.ItemTextChangedEventArgs e)
        {
            Actor actor = (Actor)this.currentElement;
            IndexedListItem item = (IndexedListItem)sender;
            Goal goal = (Goal)actor.FindGoalByUniqueID((String)item.Tag);
            if (goal != null)
            {
                goal.Description = item.Text;
            }
            parent.SetModified(true);
        }

        private void AGList_MouseOverToken(object sender, UseCaseMakerControls.MouseOverTokenEventArgs e)
        {
            UseCaseMakerControls.LinkEnabledRTB rtb = ((UseCaseMakerControls.LinkEnabledRTB)e.Item);

            object element = null;

            element = parent.Model.FindElementByName(e.Token);
            if (element == null)
            {
                element = parent.Model.FindElementByPath(e.Token);
            }
            if (element != null)
            {
                rtb.ToolTip.SetToolTip(rtb, parent.GetElementInfo(element));
            }
        }

        private void AGList_SelectedChanged(object sender, System.EventArgs e)
        {
            btnAddGoal.Enabled = true;
            btnRemoveGoal.Enabled = true;
        }

        private void btnAddGoal_Click(object sender, System.EventArgs e)
        {
            Actor actor = (Actor)this.currentElement;
            int currentSelectedIndex = actor.AddGoal();

            this.Cursor = Cursors.WaitCursor;
            AGList.DataBind();
            this.Cursor = Cursors.Default;

            AGList.SelectedIndex = currentSelectedIndex;
            parent.SetModified(true);
        }

        private void btnRemoveGoal_Click(object sender, System.EventArgs e)
        {
            Actor actor = (Actor)this.currentElement;
            int currentSelectedIndex = AGList.SelectedIndex;

            IndexedListItem ili = AGList.Items[currentSelectedIndex];
            Goal goal = (Goal)actor.FindGoalByUniqueID((String)ili.Tag);
            actor.RemoveGoal(goal);

            this.Cursor = Cursors.WaitCursor;
            AGList.DataBind();
            this.Cursor = Cursors.Default;

            if (currentSelectedIndex < AGList.Items.Count)
            {
                AGList.SelectedIndex = currentSelectedIndex;
            }
            else
            {
                if (AGList.Items.Count > 0)
                {
                    AGList.SelectedIndex = AGList.Items.Count - 1;
                }
                else
                {
                    btnAddGoal.Enabled = true;
                    btnRemoveGoal.Enabled = false;
                }
            }
            parent.SetModified(true);
        }

        private void tbAuthor_TextChanged(object sender, EventArgs e)
        {
            parent.Model.Author = tbAuthor.Text;
            parent.SetModified(true);
        }

        private void tbCompany_TextChanged(object sender, EventArgs e)
        {
            parent.Model.Company = tbCompany.Text;
            parent.SetModified(true);
        }

        private void tbModelRelease_TextChanged(object sender, EventArgs e)
        {
            parent.Model.Release = tbModelRelease.Text;
            parent.SetModified(true);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            ((IdentificableObject)this.currentElement).ObjectUserViewStatus.CurrentTabPage = null;
            base.OnClosing(e);
        }

        private void frmTabView_Activated(object sender, EventArgs e)
        {
            this.Text = ((IdentificableObject)this.currentElement).Name;
            this.TabText = this.Text;
            this.UpdateView();
        }

        private void frmTabView_Load(object sender, EventArgs e)
        {
            UCList.Items.Clear();
            UCList.Separators = this.separators;
            UCList.HighlightDescriptors = this.hdc;

            OIList.Items.Clear();
            OIList.Separators = this.separators;
            OIList.HighlightDescriptors = this.hdc;

            RList.Items.Clear();
            RList.Separators = this.separators;
            RList.HighlightDescriptors = this.hdc;

            GList.Items.Clear();
            GList.Separators = this.separators;
            GList.HighlightDescriptors = this.hdc;

            SList.Items.Clear();
            SList.Separators = this.separators;
            SList.HighlightDescriptors = this.hdc;

            AGList.Items.Clear();
            AGList.Separators = this.separators;
            AGList.HighlightDescriptors = this.hdc;

            tbProse.CaseSensitive = true;
            tbProse.Separators = this.separators;
            tbProse.HighlightDescriptors = this.hdc;

            tbDescription.CaseSensitive = true;
            tbDescription.Separators = this.separators;
            tbDescription.HighlightDescriptors = this.hdc;

            tbNotes.CaseSensitive = true;
            tbNotes.Separators = this.separators;
            tbNotes.HighlightDescriptors = this.hdc;

            tbPreconditions.CaseSensitive = true;
            tbPreconditions.Separators = this.separators;
            tbPreconditions.HighlightDescriptors = this.hdc;

            tbPostconditions.CaseSensitive = true;
            tbPostconditions.Separators = this.separators;
            tbPostconditions.HighlightDescriptors = this.hdc;

            tbTriggerDescription.CaseSensitive = true;
            tbTriggerDescription.Separators = this.separators;
            tbTriggerDescription.HighlightDescriptors = this.hdc;

            lblVersion.Text = Application.ProductVersion;
        }

        private void OnEditableStateChanged(object sender, ItemTextChangedEventArgs e)
        {
            parent.OnEditableStateChanged(sender, e);
        }
    }
}