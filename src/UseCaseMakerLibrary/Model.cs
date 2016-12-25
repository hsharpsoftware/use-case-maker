using System;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.RegularExpressions;

using UseCaseMakerLibrary.Support;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class Model : Package
	{
		#region Class Members
		private readonly GlossaryItems glossary = null;
        private readonly Stakeholders stakeholders = null;
        private DateTime creationDate = DateTime.Now;
        private String author = String.Empty;
        private String company = String.Empty;
        private String release = String.Empty;
		#endregion

		#region Constructors
		public Model() : base()
		{
			this.glossary = new GlossaryItems(this);
            this.stakeholders = new Stakeholders(this);
            this.creationDate = DateTime.Now;
		}

		public Model(String name, String prefix, Int32 id) : base(name,prefix,id)
		{
			this.glossary = new GlossaryItems(this);
            this.stakeholders = new Stakeholders(this);
            this.creationDate = DateTime.Now;
		}

		public Model(String name, String prefix, Int32 id, Package owner) : base(name,prefix,id,owner)
		{
			this.glossary = new GlossaryItems(this);
            this.stakeholders = new Stakeholders(this);
            this.creationDate = DateTime.Now;
		}
		#endregion

		#region Public Properties
        [XmlAttribute]
        public String Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }

        [XmlAttribute]
        public String Company
        {
            get
            {
                return this.company;
            }
            set
            {
                this.company = value;
            }
        }

        [XmlAttribute]
        public String Release
        {
            get
            {
                return this.release;
            }
            set
            {
                this.release = value;
            }
        }

        [XmlAttribute]
        public String CreationDateValue
        {
            get
            {
                return Convert.ToString(this.creationDate, DateTimeFormatInfo.InvariantInfo);
            }
            set
            {
                this.creationDate = Convert.ToDateTime(value, DateTimeFormatInfo.InvariantInfo);
            }
        }

        [XmlArray]
        [XmlArrayItem(typeof(GlossaryItem))]
		public GlossaryItems Glossary
		{
			get
			{
				return this.glossary;
			}
		}

        [XmlArray]
        [XmlArrayItem(typeof(Stakeholder))]
        public Stakeholders Stakeholders
        {
            get
            {
                return this.stakeholders;
            }
        }

        [XmlIgnore]
        public String LocalizatedDateValue
        {
            get
            {
                return creationDate.ToString("d", DateTimeFormatInfo.CurrentInfo);
            }
        }

        [XmlIgnore]
        public DateTime CreationDate
        {
            get
            {
                return this.creationDate;
            }
            set
            {
                this.creationDate = value;
            }
        }
		#endregion

		#region Public Methods
        #region Common Handling
        public void ReplaceElementName(
            String oldName,
            String oldNameStartTag,
            String oldNameEndTag,
            String newName,
            String newNameStartTag,
            String newNameEndTag)
        {
            this.ChangeReferences(
                oldName,
                oldNameStartTag,
                oldNameEndTag,
                newName,
                newNameStartTag,
                newNameEndTag,
                true);
        }

        public void ReplaceElementPath(
            String oldPath,
            String oldPathStartTag,
            String oldPathEndTag,
            String newPath,
            String newPathStartTag,
            String newPathEndTag)
        {
            this.ChangeReferences(
                oldPath,
                oldPathStartTag,
                oldPathEndTag,
                newPath,
                newPathStartTag,
                newPathEndTag,
                true);
        }
        #endregion

        #region Glossary Handling
        public GlossaryItem NewGlossaryItem(String name, String prefix, Int32 id)
		{
			GlossaryItem gi = new GlossaryItem(name,prefix,id,this);
			return gi;
		}

		public void AddGlossaryItem(GlossaryItem gi)
		{
			gi.Owner = this;
			this.glossary.Add(gi);
		}

		public void RemoveGlossaryItem(
			GlossaryItem gi,
			String oldNameStartTag,
			String oldNameEndTag,
			String newNameStartTag,
			String newNameEndTag,
			Boolean dontMarkOccurrences)
		{
			if(!dontMarkOccurrences)
			{
				this.ChangeReferences(
					gi.Name,
					oldNameStartTag,
					oldNameEndTag,
					gi.Name,
					newNameStartTag,
					newNameEndTag,
					false);
			}

			foreach(GlossaryItem tmpGi in this.glossary)
			{
				if(tmpGi.ID > gi.ID)
				{
					tmpGi.ID -= 1;
				}
			}
			this.glossary.Remove(gi);
		}

		public GlossaryItem GetGlossaryItem(String uniqueID)
		{
			return (GlossaryItem)this.glossary.FindByUniqueID(uniqueID);
		}
		#endregion // Glossary Handling

        #region Stakeholders Handling
        public Stakeholder NewStakeholder(String name, String prefix, Int32 id)
        {
            Stakeholder stakeholder = new Stakeholder(name, prefix, id, this);
            return stakeholder;
        }

        public void AddStakeholder(Stakeholder stakeholder)
        {
            stakeholder.Owner = this;
            this.stakeholders.Add(stakeholder);
        }

        public void RemoveStakeholder(
            Stakeholder stakeholder,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences)
        {
            this.PurgeReferences(
                stakeholder,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                dontMarkOccurrences);

            foreach(Stakeholder tmpStakeholder in this.stakeholders)
            {
                if(tmpStakeholder.ID > stakeholder.ID)
                {
                    tmpStakeholder.ID -= 1;
                }
            }
            this.stakeholders.Remove(stakeholder);
        }

        public Stakeholder GetStakeholder(String uniqueID)
        {
            return (Stakeholder)this.stakeholders.FindByUniqueID(uniqueID);
        }

        public String[] GetStakeholderNames()
        {
            StringCollection sc = new StringCollection();

            foreach(Stakeholder stakeholder in this.stakeholders)
            {
                sc.Add(stakeholder.Name);
            }

            String[] names = new String[sc.Count];
            sc.CopyTo(names, 0);

            return names;
        }
        #endregion // Stakeholders Handling

        #region Actors Handling
        public new void RemoveActor(
            Actor actor,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences)
        {
            this.PurgeReferences(
                actor,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                dontMarkOccurrences);
            Actors.Remove(actor);
        }
        #endregion

        #region Packages Handling
        public new void RemovePackage(
            Package package,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences)
        {
            this.PurgeReferences(
                package,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                dontMarkOccurrences);
            Packages.Remove(package);
        }
        #endregion

        #region UseCases Handling
        public new void RemoveUseCase(
            UseCase useCase,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences)
        {
            this.PurgeReferences(
                useCase,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                dontMarkOccurrences);
            UseCases.Remove(useCase);
        }
        #endregion

        public object FindElementByUniqueID(String uniqueID)
		{
			object element = null;

			if(this.UniqueID == uniqueID)
			{
				return this;
			}

			element = this.glossary.FindByUniqueID(uniqueID);
			if(element != null)
			{
				return element;
			}

            element = this.stakeholders.FindByUniqueID(uniqueID);
            if(element != null)
            {
                return element;
            }

			if(this.Requirements.UniqueID == uniqueID)
			{
				return this.Requirements;
			}

			if(this.Actors.UniqueID == uniqueID)
			{
				return this.Actors;
			}
			element = this.Actors.FindByUniqueID(uniqueID);
			if(element != null)
			{
				return element;
			}

			if(this.UseCases.UniqueID == uniqueID)
			{
				return this.UseCases;
			}
			element = this.UseCases.FindByUniqueID(uniqueID);
			if(element != null)
			{
				return element;
			}

			return this.Packages.FindElementByUniqueID(uniqueID);
		}

		public object FindElementByName(String name)
		{
			object element = null;

			if(this.Name == name)
			{
				return this;
			}

			element = this.glossary.FindByName(name);
			if(element != null)
			{
				return element;
			}

            element = this.stakeholders.FindByName(name);
            if(element != null)
            {
                return element;
            }

			if(this.Actors.Name == name)
			{
				return this.Actors;
			}
			element = this.Actors.FindByName(name);
			if(element != null)
			{
				return element;
			}

			if(this.UseCases.Name == name)
			{
				return this.UseCases;
			}
			element = this.UseCases.FindByName(name);
			if(element != null)
			{
				return element;
			}

			return this.Packages.FindElementByName(name);
		}

		public object FindElementByPath(String path)
		{
			object element = null;

			if(this.Path == path)
			{
				return this;
			}

			element = this.glossary.FindByPath(path);
			if(element != null)
			{
				return element;
			}

            element = this.stakeholders.FindByPath(path);
            if(element != null)
            {
                return element;
            }

			if(this.Actors.Path == path)
			{
				return this.Actors;
			}
			element = this.Actors.FindByPath(path);
			if(element != null)
			{
				return element;
			}

			if(this.UseCases.Path == path)
			{
				return this.UseCases;
			}
			element = this.UseCases.FindByPath(path);
			if(element != null)
			{
				return element;
			}

			return this.Packages.FindElementByPath(path);
		}

        public new void TextSearch(
            SearchBookmarkQueue sbq,
            String searchedText,
            String replacedText,
            Boolean wholeWordsOnly,
            Boolean caseSensitivity,
            Boolean executeReplaceAll)
        {
            Int32 counter;
            String expr;
            Regex regex;

            if(wholeWordsOnly)
            {
                expr = @"\b" + searchedText + @"\b";
            }
            else
            {
                expr = searchedText;
            }

            if(caseSensitivity)
            {
                regex = new Regex(expr, RegexOptions.None);
            }
            else
            {
                regex = new Regex(expr, RegexOptions.IgnoreCase);
            }

            // GLOSSARY
            for(counter = 0; counter < this.glossary.Count; counter++)
            {
                GlossaryItem gi = (GlossaryItem)this.glossary[counter];

                if(!executeReplaceAll)
                {
                    foreach(Match match in regex.Matches(gi.Description))
                    {
                        if(!executeReplaceAll)
                        {
                            this.AddBookmark(sbq, "Glossary", counter, match.Index, match.Length);
                        }
                    }
                }
                else
                {
                    gi.Description = regex.Replace(gi.Description, replacedText);
                }
            }

            // STAKEHOLDERS
            for(counter = 0; counter < this.stakeholders.Count; counter++)
            {
                Stakeholder stakeholder = (Stakeholder)this.stakeholders[counter];

                if(!executeReplaceAll)
                {
                    foreach(Match match in regex.Matches(stakeholder.Description))
                    {
                        if(!executeReplaceAll)
                        {
                            this.AddBookmark(sbq, "Stakeholders", counter, match.Index, match.Length);
                        }
                    }
                }
                else
                {
                    stakeholder.Description = regex.Replace(stakeholder.Description, replacedText);
                }
            }

            base.TextSearch(sbq,
                    searchedText,
                    replacedText,
                    wholeWordsOnly,
                    caseSensitivity,
                    executeReplaceAll);
        }
		#endregion

		#region Private Methods
		#endregion

        #region Internal Methods
        #endregion
    }
}
