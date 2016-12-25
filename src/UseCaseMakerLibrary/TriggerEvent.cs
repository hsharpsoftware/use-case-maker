using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
    public class TriggerEvent
    {
        #region Public Constants and Enumerators
        public enum EventTypeValue
        {
            [XmlEnum]External = 0,
            [XmlEnum]Temporal = 1,
            [XmlEnum]Internal = 2
        }
        #endregion

        #region Class Members
        private String description = String.Empty;
        private EventTypeValue eventType = EventTypeValue.External;
        #endregion

        #region Constructors
        #endregion

        #region Public Properties
        public EventTypeValue EventType
        {
            get
            {
                return this.eventType;
            }
            set
            {
                this.eventType = value;
            }
        }

        public String Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        #endregion
    }
}
