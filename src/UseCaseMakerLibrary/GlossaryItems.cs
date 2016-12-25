using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/**
	 * @brief Descrizione di riepilogo per GlossaryItems.
	 */
    [Serializable]
	public class GlossaryItems : IdentificableObjectCollection
	{
		internal GlossaryItems(Package owner)
		{
			base.Owner = owner;
		}
	}
}
