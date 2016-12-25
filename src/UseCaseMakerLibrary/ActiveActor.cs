using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class ActiveActor
	{
		#region Class Members
		private Boolean isPrimary = false;
		private String actorUniqueID = String.Empty;
		#endregion

		#region Constructors
		internal ActiveActor()
		{
		}
		#endregion

		#region Public Properties
		public String ActorUniqueID
		{
			get
			{
				return this.actorUniqueID;
			}
			set
			{
				this.actorUniqueID = value;
			}
		}

		public Boolean IsPrimary
		{
			get
			{
				return this.isPrimary;
			}
			set
			{
				this.isPrimary = value;
			}
		}
		#endregion
	}
}
