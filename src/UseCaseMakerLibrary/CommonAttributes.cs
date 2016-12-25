using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	[Serializable]
    public class CommonAttributes
	{
		#region Class Members
		private String description = String.Empty;
		private String notes = String.Empty;
		private RelatedDocuments relatedDocuments = new RelatedDocuments();
		#endregion

		#region Constructors
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

		public String Notes
		{
			get
			{
				return this.notes;
			}
			set
			{
				this.notes = value;
			}
		}

		[XmlArray]
        [XmlArrayItem(typeof(RelatedDocument))]
        public RelatedDocuments RelatedDocuments
		{
			get
			{
				return this.relatedDocuments;
			}
            set
            {
                this.relatedDocuments = value;
            }
		}
		#endregion
	}
}
