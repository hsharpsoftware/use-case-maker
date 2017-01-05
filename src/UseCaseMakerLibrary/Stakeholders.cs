using System;

namespace UseCaseMakerLibrary
{
    /**
	 * @brief Descrizione di riepilogo per Stakeholders.
	 */
    [Serializable]
	public class Stakeholders : IdentificableObjectCollection
	{
		internal Stakeholders(Package owner)
		{
			base.Owner = owner;
		}
	}
}
