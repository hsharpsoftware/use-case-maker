using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/**
	 * @brief Descrizione di riepilogo per Stakeholder.
	 */
    [Serializable]
	public class Stakeholder : IdentificableObject
	{
		#region Class Members
		String description = String.Empty;
		#endregion

		#region Constructors
		internal Stakeholder()
			: base()
		{
		}

		internal Stakeholder(String name, String prefix, Int32 id)
			: base(name,prefix,id)
		{
		}

		internal Stakeholder(String name, String prefix, Int32 id, Package owner)
			: base(name,prefix,id,owner)
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
		#endregion
	}
}
