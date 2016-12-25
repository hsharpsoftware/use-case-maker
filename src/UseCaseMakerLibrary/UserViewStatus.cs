using System;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace UseCaseMakerLibrary.Support
{
    [Serializable]
	public class UserViewStatus
	{
		#region Class Members
		private TabPage currentPage = null;
		#endregion

		#region Constructors
		public UserViewStatus()
		{
		}
		#endregion

		#region Public Properties (Run-Time)
        [XmlIgnore]
		public TabPage CurrentTabPage
		{
			get
			{
				return this.currentPage;
			}
			set
			{
				this.currentPage = value;
			}
		}
		#endregion
	}
}
