using System;
using System.Xml.Serialization;
using System.Globalization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per HistoryItem.
	/// </summary>
    [Serializable]
	public class HistoryItem
	{
		#region Public Constants and Enumerators
		public enum HistoryType
		{
			[XmlEnum]Implementation = 0,
			[XmlEnum]Status = 1,
            [XmlEnum]Acceptance = 2
		}
		#endregion

		#region Class Members
		private DateTime date;
		private HistoryType type;
		private Int32 action;
		private String notes = String.Empty;
		#endregion

		#region Constructors
		public HistoryItem()
		{
		}
		#endregion

		#region Public Properties
		[XmlIgnore]
		public String LocalizatedDateValue
		{
			get
			{
				return date.ToString("d",DateTimeFormatInfo.CurrentInfo);
			}
		}

		[XmlIgnore]
		public DateTime Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
			}
		}

		public String DateValue
		{
			get
			{
				return Convert.ToString(this.date,DateTimeFormatInfo.InvariantInfo);
			}
			set
			{
				this.date = Convert.ToDateTime(value,DateTimeFormatInfo.InvariantInfo);
			}
		}

		public HistoryType Type
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

		public Int32 Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		public String Notes
		{
			get
			{
				return this.notes;
			}
			set
			{
				this.notes = value;
			}
		}
		#endregion
	}
}
