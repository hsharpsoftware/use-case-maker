using System;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using UseCaseMakerLibrary.Support;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class UseCase : IdentificableObject
	{
		#region Public Constants and Enumerators
		public enum ComplexityValue
		{
			[XmlEnum]Low = 0,
            [XmlEnum]Medium = 1,
            [XmlEnum]High = 2
		}

		public enum ImplementationValue
		{
            [XmlEnum]Scheduled = 0,
            [XmlEnum]Started = 1,
            [XmlEnum]Partial = 2,
            [XmlEnum]Completed = 3,
            [XmlEnum]Deferred = 4,
		}

		public enum LevelValue
		{
            [XmlEnum]Summary = 0,
            [XmlEnum]User = 1,
            [XmlEnum]Subfunction = 2
		}

		public enum StatusValue
		{
            [XmlEnum]Named = 0,
            [XmlEnum]Initial = 1,
            [XmlEnum]Base = 2,
            [XmlEnum]Completed = 3,
            [XmlEnum]Deferred = 4,
            [XmlEnum]Tested = 5,
            [XmlEnum]Approved = 6
		}
		#endregion

		#region Class Members
		private ActiveActors activeActors = new ActiveActors();
		private String assignedTo = String.Empty;
		private CommonAttributes commonAttributes = new CommonAttributes();
		private ComplexityValue complexity = ComplexityValue.Low;
		private ImplementationValue implementation = ImplementationValue.Scheduled;
		private StatusValue status = StatusValue.Named;
		private LevelValue level = LevelValue.Summary;
		private String postconditions = String.Empty;
		private String preconditions = String.Empty;
		private Int32 priority = 1;
		private String prose = String.Empty;
		private String release = String.Empty;
		private Steps steps = new Steps();
		private OpenIssues openIssues = new OpenIssues();
		private HistoryItems historyItems = new HistoryItems();
        private TriggerEvent triggerEvent = new TriggerEvent();
		#endregion

		#region Constructors
		internal UseCase()
			: base()
		{
		}

		internal UseCase(String name, String prefix, Int32 id)
			: base(name,prefix,id)
		{
		}

		internal UseCase(String name, String prefix, Int32 id, Package owner)
			: base(name,prefix,id,owner)
		{
		}
		#endregion
	
		#region Public Properties
        [XmlElement]
		public CommonAttributes Attributes
		{
			get
			{
				return this.commonAttributes;
			}
            set
            {
                this.commonAttributes = value;
            }
		}

		[XmlArray]
        [XmlArrayItem(typeof(Step))]
        public Steps Steps
		{
			get
			{
				return this.steps;
			}
		}

        [XmlArray]
        [XmlArrayItem(typeof(OpenIssue))]
        public OpenIssues OpenIssues
		{
			get
			{
				return this.openIssues;
			}
		}

        [XmlArray]
        [XmlArrayItem(typeof(ActiveActor))]
        public ActiveActors ActiveActors
		{
			get
			{
				return this.activeActors;
			}
		}

        [XmlArray]
        [XmlArrayItem(typeof(HistoryItem))]
        public HistoryItems HistoryItems
		{
			get
			{
				return this.historyItems;
			}
		}

		public String Prose
		{
			get
			{
				return this.prose;
			}
			set
			{
				this.prose = value;
			}
		}

		public String Preconditions
		{
			get
			{
				return this.preconditions;
			}
			set
			{
				this.preconditions = value;
			}
		}

		public String Postconditions
		{
			get
			{
				return this.postconditions;
			}
			set
			{
				this.postconditions = value;
			}
		}

		public String Release
		{
			get
			{
				return this.release;
			}
			set
			{
				this.release = value;
			}
		}

		public String AssignedTo
		{
			get
			{
				return this.assignedTo;
			}
			set
			{
				this.assignedTo = value;
			}
		}

		public Int32 Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		public ComplexityValue Complexity
		{
			get
			{
				return this.complexity;
			}
			set
			{
				this.complexity = value;
			}
		}

		public ImplementationValue Implementation
		{
			get
			{
				return this.implementation;
			}
			set
			{
				this.implementation = value;
			}
		}

		public LevelValue Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		public StatusValue Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

        public TriggerEvent Trigger
        {
            get
            {
                return this.triggerEvent;
            }
            set
            {
                this.triggerEvent = value;
            }
        }
		#endregion

		#region Public Methods
        #region Common Handling
        public void TextSearch(
            SearchBookmarkQueue sbq,
            String searchedText,
            String replacedText,
            Boolean wholeWordsOnly,
            Boolean caseSensitivity,
            Boolean executeReplaceAll)
        {
            Int32 counter;
            String expr;
            Regex regex;

            if(wholeWordsOnly)
            {
                expr = @"\b" + searchedText + @"\b";
            }
            else
            {
                expr = searchedText;
            }

            if(caseSensitivity)
            {
                regex = new Regex(expr, RegexOptions.None);
            }
            else
            {
                regex = new Regex(expr, RegexOptions.IgnoreCase);
            }

            // STEPS
            for(counter = 0; counter < this.steps.Count; counter++)
            {
                Step step = (Step)this.steps[counter];

                if(!executeReplaceAll)
                {
                    foreach(Match match in regex.Matches(step.Description))
                    {
                        if(!executeReplaceAll)
                        {
                            this.AddBookmark(sbq, "Steps", counter, match.Index, match.Length);
                        }
                    }
                }
                else
                {
                    step.Description = regex.Replace(step.Description, replacedText);
                }
            }

            // OPEN ISSUES
            for(counter = 0; counter < this.openIssues.Count; counter++)
            {
                OpenIssue openIssue = (OpenIssue)this.openIssues[counter];

                if(!executeReplaceAll)
                {
                    foreach(Match match in regex.Matches(openIssue.Description))
                    {
                        if(!executeReplaceAll)
                        {
                            this.AddBookmark(sbq, "OpenIssues", counter, match.Index, match.Length);
                        }
                    }
                }
                else
                {
                    openIssue.Description = regex.Replace(openIssue.Description, replacedText);
                }
            }

            // PRECONDITIONS
            if(!executeReplaceAll)
            {
                foreach(Match match in regex.Matches(this.preconditions))
                {
                    if(!executeReplaceAll)
                    {
                        this.AddBookmark(sbq, "Preconditions", counter, match.Index, match.Length);
                    }
                }
            }
            else
            {
                this.preconditions = regex.Replace(this.preconditions, replacedText);
            }

            // POSTCONDITIONS
            if(!executeReplaceAll)
            {
                foreach(Match match in regex.Matches(this.postconditions))
                {
                    if(!executeReplaceAll)
                    {
                        this.AddBookmark(sbq, "Postconditions", counter, match.Index, match.Length);
                    }
                }
            }
            else
            {
                this.postconditions = regex.Replace(this.postconditions, replacedText);
            }

            // PROSE
            if(!executeReplaceAll)
            {
                foreach(Match match in regex.Matches(this.prose))
                {
                    if(!executeReplaceAll)
                    {
                        this.AddBookmark(sbq, "Prose", counter, match.Index, match.Length);
                    }
                }
            }
            else
            {
                this.prose = regex.Replace(this.prose, replacedText);
            }

            // ATTRIBUTES -> DESCRIPTION
            if(!executeReplaceAll)
            {
                foreach(Match match in regex.Matches(this.Attributes.Description))
                {
                    if(!executeReplaceAll)
                    {
                        this.AddBookmark(sbq, "Attributes.Description", counter, match.Index, match.Length);
                    }
                }
            }
            else
            {
                this.Attributes.Description = regex.Replace(this.Attributes.Description, replacedText);
            }

            // ATTRIBUTES -> NOTES
            if(!executeReplaceAll)
            {
                foreach(Match match in regex.Matches(this.Attributes.Notes))
                {
                    if(!executeReplaceAll)
                    {
                        this.AddBookmark(sbq, "Attributes.Notes", counter, match.Index, match.Length);
                    }
                }
            }
            else
            {
                this.Attributes.Notes = regex.Replace(this.Attributes.Notes, replacedText);
            }
        }
        #endregion

		#region Step Handling
		public Int32 AddStep(
			Step previousStep,
			Step.StepType type,
			String stereotype,
			UseCase referencedUseCase,
			DependencyItem.ReferenceType referenceType)
		{
			Step step = new Step();
			Int32 index;
			Int32 ret;

			if(referenceType != DependencyItem.ReferenceType.None)
			{
				step.Dependency.Stereotype = stereotype;
				step.Dependency.PartnerUniqueID = referencedUseCase.UniqueID;
				step.Dependency.Type = referenceType;
				step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
				step.Description += " \"";
				step.Description += referencedUseCase.Name;
				step.Description += "\"";
			}

			if(previousStep == null)
			{
				step.ID = 1;
				ret = this.steps.Add(step);
				return ret;
			}
			else
			{
				switch(type)
				{
					case Step.StepType.Default:
						if(previousStep.Type == Step.StepType.Default)
						{
							step.ID = previousStep.ID;
                            foreach(Step tmpStep in this.steps)
                            {
                                if(tmpStep.ID > step.ID)
                                {
                                    step.ID = tmpStep.ID;
                                }
                            }
                            previousStep = (Step)this.Steps[this.Steps.Count - 1];
                            step.ID += 1;
						}
						else if(previousStep.Type == Step.StepType.Alternative)
						{
							step.ID = previousStep.ID;
							step.Type = Step.StepType.Alternative;

							index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
							while(true)
							{
								if(index == this.steps.Count)
								{
									previousStep = (Step)this.steps[index - 1];
									break;
								}
								Step tmpStep = (Step)this.steps[index];
								if(tmpStep.ID != step.ID || tmpStep.Prefix == String.Empty)
								{
									previousStep = (Step)this.steps[index - 1];
									break;
								}
								index += 1;
							}
							step.Prefix = previousStep.Prefix;
							if(step.Prefix != String.Empty)
							{
								Char nextChar = step.Prefix[0];
								nextChar++;
								step.Prefix = new String(nextChar,1);
							}
							else
							{
								step.Prefix = "A";
							}
						
							foreach(Step tmpStep in this.steps)
							{
								if(tmpStep.ID == step.ID)
								{
									if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
									{
										Char nextChar = tmpStep.Prefix[0];
										nextChar++;
										tmpStep.Prefix = new String(nextChar,1);
									}
								}
							}
						}
                        else if(previousStep.Type == Step.StepType.Child)
                        {
                            step.Type = Step.StepType.Child;
                            step.ID = previousStep.ID;
                            step.Prefix = previousStep.Prefix;

                            index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
                            while(true)
                            {
                                if(index == this.steps.Count)
                                {
                                    previousStep = (Step)this.steps[index - 1];
                                    break;
                                }
                                Step tmpStep = (Step)this.steps[index];
                                if(tmpStep.ID != step.ID || tmpStep.Prefix != step.Prefix)
                                {
                                    previousStep = (Step)this.steps[index - 1];
                                    break;
                                }
                                index += 1;
                            }

                            step.Prefix = previousStep.Prefix;
                            step.ChildID = previousStep.ChildID + 1;
                        }
						else if(previousStep.Type == Step.StepType.AlternativeChild)
						{
							step.Type = Step.StepType.AlternativeChild;
							step.ID = previousStep.ID;
							step.Prefix = previousStep.Prefix;

							index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
							while(true)
							{
								if(index == this.steps.Count)
								{
									previousStep = (Step)this.steps[index - 1];
									break;
								}
								Step tmpStep = (Step)this.steps[index];
								if(tmpStep.ID != step.ID || tmpStep.Prefix != step.Prefix)
								{
									previousStep = (Step)this.steps[index - 1];
									break;
								}
								index += 1;
							}

							step.Prefix = previousStep.Prefix;
							step.ChildID = previousStep.ChildID + 1;
						}
						break;
					case Step.StepType.Alternative:
						if(previousStep.Type == Step.StepType.Default)
						{
							step.ID = previousStep.ID;
							step.Type = Step.StepType.Alternative;

							index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
							while(true)
							{
								if(index == this.steps.Count)
								{
									previousStep = (Step)this.steps[index - 1];
									break;
								}
								Step tmpStep = (Step)this.steps[index];
								if(tmpStep.ID != step.ID)
								{
									previousStep = (Step)this.steps[index - 1];
									break;
								}
								index += 1;
							}
							step.Prefix = previousStep.Prefix;
							if(step.Prefix != String.Empty)
							{
								Char nextChar = step.Prefix[0];
								nextChar++;
								step.Prefix = new String(nextChar,1);
							}
							else
							{
								step.Prefix = "A";
							}
						
							foreach(Step tmpStep in this.steps)
							{
								if(tmpStep.ID == step.ID)
								{
									if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
									{
										Char nextChar = tmpStep.Prefix[0];
										nextChar++;
										tmpStep.Prefix = new String(nextChar,1);
									}
								}
							}
						}
                        break;
                    case Step.StepType.Child:
                        if(previousStep.Type == Step.StepType.Default)
                        {
                            step.Type = Step.StepType.Child;
                            step.ID = previousStep.ID;
                            step.Prefix = previousStep.Prefix;
                            step.ChildID = 1;
                        }
                        else if(previousStep.Type == Step.StepType.Alternative)
						{
							step.Type = Step.StepType.AlternativeChild;
							step.ID = previousStep.ID;
							step.Prefix = previousStep.Prefix;
							step.ChildID = 1;
						}
						break;
				}
			}

			index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
			if(index == this.steps.Count)
			{
				ret = this.steps.Add(step);
			}
			else
			{
				this.steps.Insert(index, step);
				ret = index;
			}

			return ret;
		}

		public Int32 InsertStep(
			Step previousStep,
			String stereotype,
			UseCase referencedUseCase,
			DependencyItem.ReferenceType referenceType)
		{
			Step step = new Step();
			Int32 ret;

			if(referenceType != DependencyItem.ReferenceType.None)
			{
				step.Dependency.Stereotype = stereotype;
				step.Dependency.PartnerUniqueID = referencedUseCase.UniqueID;
				step.Dependency.Type = referenceType;
				step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
				step.Description += " \"";
				step.Description += referencedUseCase.Name;
				step.Description += "\"";
			}

			if(previousStep.Type == Step.StepType.Default)
			{
				step.ID = previousStep.ID;
				foreach(Step tmpStep in this.steps)
				{
					if(tmpStep.ID >= step.ID)
					{
						tmpStep.ID += 1;
					}
				}
			}
			else if(previousStep.Type == Step.StepType.Alternative)
			{
				step.Prefix = previousStep.Prefix;
				if(step.Prefix == String.Empty)
				{
					step.Prefix = "A";
				}
				step.ID = previousStep.ID;
				step.Type = Step.StepType.Alternative;

				foreach(Step tmpStep in this.steps)
				{
					if(tmpStep.ID == step.ID)
					{
						if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
						{
							Char nextChar = tmpStep.Prefix[0];
							nextChar++;
							tmpStep.Prefix = new String(nextChar,1);
						}
					}
				}
			}
            else if(previousStep.Type == Step.StepType.Child)
            {
                step.Type = Step.StepType.Child;
                step.ID = previousStep.ID;
                step.Prefix = previousStep.Prefix;
                step.ChildID = previousStep.ChildID;
                foreach(Step tmpStep in this.steps)
                {
                    if(tmpStep.ID == step.ID)
                    {
                        if(tmpStep.ChildID >= step.ChildID)
                        {
                            tmpStep.ChildID += 1;
                        }
                    }
                }
            }
			else if(previousStep.Type == Step.StepType.AlternativeChild)
			{
				step.Type = Step.StepType.AlternativeChild;
				step.ID = previousStep.ID;
				step.Prefix = previousStep.Prefix;
				step.ChildID = previousStep.ChildID;
				foreach(Step tmpStep in this.steps)
				{
					if(tmpStep.ID == step.ID && tmpStep.Prefix == step.Prefix)
					{
						if(tmpStep.ChildID >= step.ChildID)
						{
							tmpStep.ChildID += 1;
						}
					}
				}
			}

			int index = this.FindStepIndexByUniqueID(previousStep.UniqueID);
			this.steps.Insert(index,step);
			ret = index;

			return ret;
		}

		public void RemoveStep(Step step)
		{
			for(int i = this.steps.Count - 1; i >= 0; i--)
			{
				Step tmpStep = (Step)this.steps[i];
				switch(step.Type)
				{
					case Step.StepType.Default:
						if(tmpStep.ID == step.ID)
						{
							this.steps.Remove(tmpStep);
						}
						if(tmpStep.ID > step.ID)
						{
							tmpStep.ID -= 1;
						}
						break;
					case Step.StepType.Alternative:
						if(tmpStep.ID == step.ID)
						{
							if(tmpStep.Prefix == step.Prefix)
							{
								this.steps.Remove(tmpStep);
							}
							if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) > 0)
							{
								Char nextChar = tmpStep.Prefix[0];
								nextChar--;
								tmpStep.Prefix = new String(nextChar,1);
							}
						}
						break;
                    case Step.StepType.Child:
                        if(tmpStep.ID == step.ID)
                        {
                            if(tmpStep.ChildID == step.ChildID)
                            {
                                this.steps.Remove(step);
                            }
                            if(tmpStep.ChildID > step.ChildID)
                            {
                                tmpStep.ChildID -= 1;
                            }
                        }
                        break;
					case Step.StepType.AlternativeChild:
						if(tmpStep.ID == step.ID && tmpStep.Prefix == step.Prefix)
						{
							if(tmpStep.ChildID == step.ChildID)
							{
								this.steps.Remove(step);
							}
							if(tmpStep.ChildID > step.ChildID)
							{
								tmpStep.ChildID -= 1;
							}
						}
						break;
				}
			}
		}

		public Step FindStepByUniqueID(String uniqueID)
		{
			Step step = null;

			foreach(Step tmpStep in this.Steps)
			{
				if(tmpStep.UniqueID == uniqueID)
				{
					step = tmpStep;
				}
			}

			return step;
		}

		public bool StepHasAlternatives(Step step)
		{
			Int32 index;
			Step tmpStep;
            Boolean retVal = true;

			if(step.Type != Step.StepType.Default)
			{
				return false;
			}

			for(index = this.Steps.Count - 1; index >= 0; index--)
			{
				tmpStep = (Step)this.Steps[index];
				if(tmpStep.UniqueID == step.UniqueID)
				{
					break;
				}
			}

            while(true)
            {
                // Step is not found or it is the last step in collection
                if(index >= this.Steps.Count - 1)
                {
                    retVal = false;
                    break;
                }
                tmpStep = (Step)this.Steps[index + 1];
                if(step.ID == tmpStep.ID && tmpStep.Type == Step.StepType.Alternative)
                {
                    retVal = true;
                    break;
                }
                index += 1;
            }

			return retVal;
		}

        public bool StepHasChildren(Step step)
        {
            Int32 index;
            Step tmpStep;

            if(step.Type != Step.StepType.Default &&
                step.Type != Step.StepType.Alternative)
            {
                return false;
            }

            for(index = this.Steps.Count - 1; index >= 0; index--)
            {
                tmpStep = (Step)this.Steps[index];
                if(tmpStep.UniqueID == step.UniqueID)
                {
                    break;
                }
            }
            // Step is not found or it is the last step in collection
            if(index >= this.Steps.Count - 1)
            {
                return false;
            }
            tmpStep = (Step)this.Steps[index + 1];
            if(step.Type == Step.StepType.Default && tmpStep.Type == Step.StepType.Child)
            {
                return true;
            }
            if(step.Type == Step.StepType.Alternative && tmpStep.Type == Step.StepType.AlternativeChild)
            {
                return true;
            }
            return false;
        }
		#endregion

		#region OpenIssues Handling
		public Int32 AddOpenIssue()
		{
			OpenIssue openIssue = new OpenIssue();
			Int32 index = this.openIssues.Count;
			Int32 ret;

			if(index == 0)
			{
				openIssue.ID = 1;
			}
			else
			{
				openIssue.ID = ((OpenIssue)this.openIssues[index - 1]).ID + 1;
			}

			ret = this.openIssues.Add(openIssue);

			return ret;
		}

		public void RemoveOpenIssue(OpenIssue openIssue)
		{
			foreach(OpenIssue tmpOpenIssue in this.openIssues)
			{
				if(tmpOpenIssue.ID > openIssue.ID)
				{
					tmpOpenIssue.ID -= 1;
				}
			}
			this.openIssues.Remove(openIssue);
		}

		public OpenIssue FindOpenIssueByUniqueID(String uniqueID)
		{
			OpenIssue openIssue = null;

			foreach(OpenIssue tmpOpenIssue in this.openIssues)
			{
				if(tmpOpenIssue.UniqueID == uniqueID)
				{
					openIssue = tmpOpenIssue;
				}
			}

			return openIssue;
		}
		#endregion

		#region ActiveActors Handling
		public void AddActiveActor(Actor actor)
		{
			ActiveActor aactor = new ActiveActor();
			aactor.ActorUniqueID = actor.UniqueID;
			aactor.IsPrimary = false;
			this.ActiveActors.Add(aactor);
		}

		public void RemoveActiveActor(Actor actor)
		{
			ActiveActor aactor = (ActiveActor)this.ActiveActors.FindByUniqueID(actor.UniqueID);
			if(aactor != null)
			{
				this.ActiveActors.Remove(aactor);
			}
		}
		#endregion

		#region History Item Handling
		public void AddHistoryItem(DateTime date,HistoryItem.HistoryType type,Int32 action,String notes)
		{
			HistoryItem hi = new HistoryItem();
			hi.Date = date;
			hi.Type = type;
			hi.Action = action;
			hi.Notes = notes;
			this.HistoryItems.Add(hi);
		}

		public void RemoveHistoryItem(Int32 index)
		{
			this.HistoryItems.RemoveAt(index);
		}
		#endregion
		#endregion

        #region Protected Methods
        protected void AddBookmark(
            SearchBookmarkQueue sbq,
            String propertyName,
            Int32 propertyIndex,
            Int32 start,
            Int32 length)
        {
            SearchBookmark sb = new SearchBookmark();
            sb.Element = this;
            sb.PropertyName = propertyName;
            sb.Index = propertyIndex;
            sb.Start = start;
            sb.Length = length;
            sbq.Enqueue(sb);
        }
        #endregion

        #region Private Methods
        private Int32 FindStepIndexByUniqueID(String uniqueID)
		{
			Int32 ret = -1;

			for(int i = 0; i < this.Steps.Count; i++)
			{
				if(((Step)this.Steps[i]).UniqueID == uniqueID)
				{
					ret = i;
				}
			}

			return ret;
		}
		#endregion
	}
}
