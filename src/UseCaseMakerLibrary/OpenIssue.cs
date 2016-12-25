using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class OpenIssue : IdentificableObject
	{
		#region Public Constants and Enumerators
		#endregion

		#region Class Members
		private String description = String.Empty;
		#endregion

		#region Constructors
		internal OpenIssue()
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
		#endregion
	}
}
