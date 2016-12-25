using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class Requirement : IdentificableObject
	{
		#region Public Constants and Enumerators
        public enum CategoryValue
        {
            [XmlEnum]Functional = 0,
            [XmlEnum]NonFunctional = 1
        }

        public enum ImportanceValue
        {
            [XmlEnum]MustHave = 0, 
            [XmlEnum]ShouldHave = 1,
            [XmlEnum]NiceToHave = 2
        }

        public enum AcceptanceStatusValue
        {
            [XmlEnum]Proposed = 0,
            [XmlEnum]Evaluating = 1,
            [XmlEnum]Accepted = 2,
            [XmlEnum]Refused = 3,
            [XmlEnum]Cancelled = 4,
            [XmlEnum]Conflictual = 5
        }
		#endregion

		#region Class Members
		private String description = String.Empty;
        private CategoryValue category = CategoryValue.Functional;
        private ImportanceValue importance = ImportanceValue.MustHave;
        private UseCase.StatusValue status = UseCase.StatusValue.Named;
        private AcceptanceStatusValue acceptanceStaus = AcceptanceStatusValue.Proposed;
        private HistoryItems historyItems = new HistoryItems();
        private ReferencedObjectCollection proponents = new ReferencedObjectCollection();
        private ReferencedObjectCollection beneficiaries = new ReferencedObjectCollection();
        private ReferencedObjectCollection mappedUseCases = new ReferencedObjectCollection();
		#endregion

		#region Constructors
		internal Requirement()
		{
		}
		#endregion

		#region Public Properties
		public String Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		[XmlAttribute]
        public new String UniqueID
		{
			get
			{
				return base.UniqueID;
			}
			set
			{
				base.UniqueID = value;
			}
		}

        [XmlAttribute]
        public new String Name
		{
			get
			{
				return base.ID.ToString();
			}
			set
			{
				base.Name = value;
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

        [XmlArray]
        [XmlArrayItem(typeof(ReferencedObject))]
        public ReferencedObjectCollection Proponents
        {
            get
            {
                return this.proponents;
            }
        }

        [XmlArray]
        [XmlArrayItem(typeof(ReferencedObject))]
        public ReferencedObjectCollection Beneficiaries
        {
            get
            {
                return this.beneficiaries;
            }
        }

        [XmlArray]
        [XmlArrayItem(typeof(ReferencedObject))]
        public ReferencedObjectCollection MappedUseCases
        {
            get
            {
                return this.mappedUseCases;
            }
        }

        public CategoryValue Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        public ImportanceValue Importance
        {
            get
            {
                return this.importance;
            }
            set
            {
                this.importance = value;
            }
        }

        public UseCase.StatusValue Status
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

        public AcceptanceStatusValue AcceptanceStatus
        {
            get
            {
                return this.acceptanceStaus;
            }
            set
            {
                this.acceptanceStaus = value;
            }
        }
		#endregion

        #region Public Methods
        #region Proponents Handling
        public void AddProponent(Stakeholder stakeholder)
        {
            ReferencedObject refobj = new ReferencedObject();
            refobj.UniqueID = stakeholder.UniqueID;
            this.Proponents.Add(refobj);
        }

        public void RemoveProponent(Stakeholder stakeholder)
        {
            ReferencedObject refobj = (ReferencedObject)this.Proponents.FindByUniqueID(stakeholder.UniqueID);
            if(refobj != null)
            {
                this.Proponents.Remove(refobj);
            }
        }
        #endregion

        #region Beneficiaries Handling
        public void AddBeneficiary(Stakeholder stakeholder)
        {
            ReferencedObject refobj = new ReferencedObject();
            refobj.UniqueID = stakeholder.UniqueID;
            this.Beneficiaries.Add(refobj);
        }

        public void RemoveBeneficiary(Stakeholder stakeholder)
        {
            ReferencedObject refobj = (ReferencedObject)this.Beneficiaries.FindByUniqueID(stakeholder.UniqueID);
            if(refobj != null)
            {
                this.Beneficiaries.Remove(refobj);
            }
        }
        #endregion

        #region Mapped Use Cases Handling
        public void AddMappedUseCase(UseCase useCase)
        {
            ReferencedObject refobj = new ReferencedObject();
            refobj.UniqueID = useCase.UniqueID;
            this.MappedUseCases.Add(refobj);
        }

        public void RemoveMappedUseCase(UseCase useCase)
        {
            ReferencedObject refobj = (ReferencedObject)this.MappedUseCases.FindByUniqueID(useCase.UniqueID);
            if(refobj != null)
            {
                this.MappedUseCases.Remove(refobj);
            }
        }
        #endregion

        #region History Item Handling
        public void AddHistoryItem(DateTime date, HistoryItem.HistoryType type, Int32 action, String notes)
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
    }
}
