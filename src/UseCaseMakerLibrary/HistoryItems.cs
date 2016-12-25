using System;
using System.Collections;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per HistoryItems.
	/// </summary>
	[Serializable]
    public class HistoryItems : ICollection
	{
		#region Private Enumerators and Constants
		#endregion

		#region Public Enumerators and Constants
		#endregion

		#region Class Members
		private ArrayList items = new ArrayList();
		#endregion

		#region Constructors
		internal HistoryItems()
		{
			//
			// TODO: aggiungere qui la logica del costruttore
			//
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Returns the number of elements in the MenuItemCollection
		/// </summary>
		[XmlIgnore]
		public int Count
		{
			get
			{
				return items.Count;
			}
		}

		[XmlIgnore]
		public bool IsSynchronized
		{
			get
			{
				return items.IsSynchronized;
			}
		}

		[XmlIgnore]
		public object SyncRoot
		{
			get
			{
				return items.SyncRoot;
			}
		}

		[XmlIgnore]
		public object this[int index]
		{
			get
			{
				return items[index];
			}
		}
		#endregion

		#region Public Methods
		public int Add(object item)
		{
			int result = items.Add(item);

			return result;
		}

		public void AddRange(RelatedDocuments items)
		{
			items.AddRange(items);
		}

		public void Clear()
		{
			items.Clear();
		}

		public bool Contains(object item)
		{
			return items.Contains(item);
		}

		public int IndexOf(object item)
		{
			return items.IndexOf(item);
		}

		public void Insert(int index, object item)
		{
			items.Insert(index, item);
		}

		public void Remove(object item)
		{
			items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			items.RemoveAt(index);
		}

		public void CopyTo(Array array, int index)
		{
			items.CopyTo(array, index);
		}

		public IEnumerator GetEnumerator()
		{
			return items.GetEnumerator();
		}
		#endregion

		#region Protected Methods
		#endregion

		#region Private Methods
		#endregion
	}
}
