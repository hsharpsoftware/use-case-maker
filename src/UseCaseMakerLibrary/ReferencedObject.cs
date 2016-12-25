using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class ReferencedObject
	{
		#region Class Members
		private String uniqueID = String.Empty;
		#endregion

		#region Constructors
        internal ReferencedObject()
		{
		}
		#endregion

		#region Public Properties
		public String UniqueID
		{
			get
			{
				return this.uniqueID;
			}
			set
			{
				this.uniqueID = value;
			}
        }
        #endregion
    }
}
