using System;
using System.Collections;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class ActiveActors : ICollection
	{
		#region Class Members
		private ArrayList items = new ArrayList();
		#endregion

		#region Constructors
		internal ActiveActors()
		{
		}
		#endregion

		#region Public Properties
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

		public object FindByUniqueID(String uniqueID)
		{
			ActiveActor aactor = null;
			foreach(ActiveActor tmpAActor in this.items)
			{
				if(tmpAActor.ActorUniqueID == uniqueID)
				{
					aactor = tmpAActor;
				}
			}

			return aactor;
		}
		#endregion
	}
}
