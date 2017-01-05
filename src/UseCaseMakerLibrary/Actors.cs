using System;

namespace UseCaseMakerLibrary
{
    [Serializable]
    public class Actors : IdentificableObjectCollection
	{
		internal Actors(Package owner)
		{
			base.Owner = owner;
		}

		#region Public Properties
		#endregion

		#region Public Methods
		#endregion
	}
}
