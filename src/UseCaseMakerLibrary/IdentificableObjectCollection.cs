using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per IdentificableObjectCollection.
	/// </summary>
    [Serializable]
    public class IdentificableObjectCollection : IdentificableObject, ICollection
	{
		#region Private Enumerators and Constants
		#endregion

		#region Public Enumerators and Constants
		#endregion

		#region Class Members
		private ArrayList items = new ArrayList();
		private IdentificableObject ia = new IdentificableObject();
		#endregion

		#region Constructors
		internal IdentificableObjectCollection()
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

		#region IdentificableObject Properties Override
		[XmlAttribute]
		public new String Path
		{
			get
			{
				if(Owner == null)
				{
					return String.Empty;
				}
				return Owner.Path;
			}
		}

		[XmlIgnore]
		public new String ElementID
		{
			get
			{
                if(Owner == null)
                {
                    return String.Empty;
                }
				return Owner.ElementID;
			}
		}
		#endregion
		#endregion

		#region Public Methods
		public int Add(object item)
		{
			int result = items.Add(item);

			return result;
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

		public ICollection Sorted(string propertyName)
		{
			ArrayList sorter = new ArrayList(this);
			sorter.Sort(new PropertySorter(propertyName,"ASC"));
			this.Clear();
			foreach(object element in sorter)
			{
				this.Add(element);
			}
			return this;
		}

		public object FindByName(String name)
		{
			IdentificableObject element = null;
			foreach(IdentificableObject tmpElement in this)
			{
				if(tmpElement.Name == name)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public object FindByUniqueID(String uniqueID)
		{
			IdentificableObject element = null;
			foreach(IdentificableObject tmpElement in this)
			{
				if(tmpElement.UniqueID == uniqueID)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public object FindByElementID(String elementID)
		{
			IdentificableObject element = null;
			foreach(IdentificableObject tmpElement in this)
			{
				if(tmpElement.ElementID == elementID)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public object FindByPath(String path)
		{
			IdentificableObject element = null;
			foreach(IdentificableObject tmpElement in this)
			{
				if(tmpElement.Path == path)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public Int32 GetNextFreeID()
		{
			int id = 0;
			foreach(IdentificableObject tmpElement in this)
			{
				if(tmpElement.ID > id)
				{
					id = tmpElement.ID;
				}
			}

			return (id + 1);
		}
		#endregion

		#region Protected Methods
		#endregion

		#region Private Methods
		#endregion
	}
}
