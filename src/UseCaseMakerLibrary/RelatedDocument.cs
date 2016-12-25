using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per RelatedDocument.
	/// </summary>
    [Serializable]
	public class RelatedDocument
	{
		#region Class Members
		String fileName = String.Empty;
		#endregion

		#region Constructors
		public RelatedDocument()
		{
		}
		#endregion

		#region Public Properties
		public String FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}
		#endregion
	}
}
