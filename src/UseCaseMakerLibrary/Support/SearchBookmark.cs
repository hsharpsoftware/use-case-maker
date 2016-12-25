using System;
using System.Collections.Generic;
using System.Text;

namespace UseCaseMakerLibrary.Support
{
    public class SearchBookmark
    {
        #region Public Constants and Enumerators
        #endregion

        #region Class Members
        private IdentificableObject element = null;
        private String propertyName;
        private Int32 index = -1;
        private Int32 start;
        private Int32 length;
        #endregion

        #region Constructors
        public SearchBookmark()
        {
        }
        #endregion

        #region Public Properties
        public IdentificableObject Element
        {
            get
            {
                return this.element;
            }
            set
            {
                this.element = value;
            }
        }

        public String PropertyName
        {
            get
            {
                return this.propertyName;
            }
            set
            {
                this.propertyName = value;
            }
        }

        public Int32 Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }

        public Int32 Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.start = value;
            }
        }

        public Int32 Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }
        #endregion
    }
}
