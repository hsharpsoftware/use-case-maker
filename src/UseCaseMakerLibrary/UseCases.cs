using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class UseCases : IdentificableObjectCollection
	{
		internal UseCases(Package owner)
		{
			base.Owner = owner;
		}

		#region Public Properties
		#endregion

		#region Public Methods
		#endregion
	}
}
