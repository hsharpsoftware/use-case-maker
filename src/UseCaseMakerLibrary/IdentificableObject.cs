using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Support;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class IdentificableObject : IIdentificableObject
	{
		#region Class Members
		private Int32 id = -1;
		private String name = String.Empty;
		private String prefix = String.Empty;
		private String uniqueID = String.Empty;
		private Package owner = null;
		private UserViewStatus userViewStatus = new UserViewStatus();
		#endregion

		#region Constructors
		internal IdentificableObject()
		{
			MakeUniqueID();
		}

		internal IdentificableObject(String name, String prefix, Int32 id)
		{
			MakeUniqueID();
			this.name = name;
			this.prefix = prefix;
			this.id = id;
		}

		internal IdentificableObject(String name, String prefix, Int32 id, Package owner)
		{
			MakeUniqueID();
			this.name = name;
			this.prefix = prefix;
			this.id = id;
			this.owner = owner;
		}
		#endregion

		#region Public Properties
		[XmlAttribute]
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

		[XmlIgnore]
		public Package Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		[XmlAttribute]
		public String Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		[XmlAttribute]
		public Int32 ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		[XmlAttribute]
		public String Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		[XmlAttribute]
		public String Path
		{
			get
			{
				string path = this.Prefix + this.ID.ToString();
				IdentificableObject owner = this.Owner;
				while(owner != null)
				{
					path = owner.Prefix + owner.ID.ToString() + "." + path;
					owner = owner.Owner;
				}
				return path;
			}
		}

		[XmlIgnore]
		public String ElementID
		{
			get
			{
				return this.prefix + this.id.ToString();
			}
		}

		public UserViewStatus ObjectUserViewStatus
		{
			get
			{
				return this.userViewStatus;
			}
		}
		#endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void MakeUniqueID()
		{
			Guid guid = Guid.NewGuid();
			uniqueID = guid.ToString();
		}
		#endregion
	}
}
