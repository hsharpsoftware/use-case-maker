using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per DependencyItem.
	/// </summary>
	public class DependencyItem
	{
		#region Public Constants and Enumerators
		public enum ReferenceType
		{
			[XmlEnum]None = 0,
            [XmlEnum]Client = 1,
            [XmlEnum]Supplier = 2
		}
		#endregion

		#region Class Members
		private String stereotype = "";
		private ReferenceType type = ReferenceType.None;
		private String partnerUniqueID = "";
		#endregion

		#region Constructors
		public DependencyItem()
		{
			//
			// TODO: aggiungere qui la logica del costruttore
			//
		}
		#endregion

		#region Public Properties
		public String Stereotype
		{
			get
			{
				return this.stereotype;
			}
			set
			{
				this.stereotype = value;
			}
		}

        public ReferenceType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		public String PartnerUniqueID
		{
			get
			{
				return this.partnerUniqueID;
			}
			set 
			{
				this.partnerUniqueID = value;
			}
		}
		#endregion
	}
}
