using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    [Serializable]
    [XmlRoot("UCM-Document")]
    public class UCMDocument
    {
        #region Class Members
        private Model model = null;
        private String version = String.Empty;
        #endregion

        #region Constructors
        public UCMDocument()
        {
        }
        #endregion

        #region Public Properties
        public Model Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
            }
        }

        [XmlAttribute]
        public String Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
