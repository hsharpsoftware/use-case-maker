using System;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using UseCaseMakerLibrary.Support;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class Package : IdentificableObject
	{
		#region Class Members
		private readonly Actors actors = null;
		private readonly Packages packages = null;
		private readonly UseCases useCases = null;
		private CommonAttributes commonAttributes = new CommonAttributes();
		private Requirements requirements = new Requirements();
		#endregion

		#region Constructors
		internal Package()
			: base()
		{
			this.actors = new Actors(this);
			this.packages = new Packages(this);
			this.useCases = new UseCases(this);
		}

		internal Package(String name, String prefix, Int32 id)
			: base(name,prefix,id)
		{
			this.actors = new Actors(this);
			this.packages = new Packages(this);
			this.useCases = new UseCases(this);
		}

		internal Package(String name, String prefix, Int32 id, Package owner)
			: base(name,prefix,id,owner)
		{
			this.actors = new Actors(this);
			this.packages = new Packages(this);
			this.useCases = new UseCases(this);
		}
		#endregion

		#region Public Methods
		#region Actors Handling
		public Actor NewActor(String name, String prefix, Int32 id)
		{
			Actor actor = new Actor(name,prefix,id,this);
			return actor;
		}

		public void AddActor(Actor actor)
		{
			actor.Owner = this;
			Actors.Add(actor);
		}

		public void RemoveActor(
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

		public Actor GetActor(String uniqueID)
		{
			return (Actor)Actors.FindByUniqueID(uniqueID);
		}

		public String [] GetActorNames()
		{
			StringCollection sc = new StringCollection();

			this.RecursiveGetActorNameList(sc);

			String [] actorNames = new String[sc.Count];
			sc.CopyTo(actorNames,0);

			return actorNames;
		}
		#endregion // Actors Handling

		#region Packages Handling
		public Package NewPackage(String name, String prefix, Int32 id)
		{
			Package package = new Package(name,prefix,id,this);
			return package;
		}

		public void AddPackage(Package package)
		{
			package.Owner = this;
			Packages.Add(package);
		}

		public void RemovePackage(
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

		public Package GetPackage(String uniqueID)
		{
			return (Package)Packages.FindByUniqueID(uniqueID);
		}
		#endregion // Packages Handling

		#region UseCases Handling
		public UseCase NewUseCase(String name, String prefix, Int32 id)
		{
			UseCase useCase = new UseCase(name,prefix,id,this);
			return useCase;
		}

		public void AddUseCase(UseCase useCase)
		{
			useCase.Owner = this;
			UseCases.Add(useCase);
		}

		public void RemoveUseCase(
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

		public UseCase GetUseCase(String uniqueID)
		{
			return (UseCase)UseCases.FindByUniqueID(uniqueID);
		}
		#endregion // UseCase Handling

		#region Requirements Handling
		public Int32 AddRequrement()
		{
			Requirement requirement = new Requirement();
			Int32 index = this.requirements.Count;
			Int32 ret;

			if(index == 0)
			{
				requirement.ID = 1;
			}
			else
			{
				requirement.ID = ((Requirement)this.requirements[index - 1]).ID + 1;
			}

			ret = this.requirements.Add(requirement);

			return ret;
		}

		public void RemoveRequirement(Requirement requirement)
		{
			foreach(Requirement tmpRequirement in this.requirements)
			{
				if(tmpRequirement.ID > requirement.ID)
				{
					tmpRequirement.ID -= 1;
				}
			}
			this.requirements.Remove(requirement);
		}

		public Requirement FindRequirementByUniqueID(String uniqueID)
		{
			Requirement requirement = null;

			foreach(Requirement tmpRequirement in this.requirements)
			{
				if(tmpRequirement.UniqueID == uniqueID)
				{
					requirement = tmpRequirement;
				}
			}

			return requirement;
		}
		#endregion

		#region Common Handling
        public void TextSearch(
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

            // REQUIREMENTS
            for(counter = 0; counter < this.Requirements.Count; counter++)
            {
                Requirement requirement = (Requirement)this.Requirements[counter];

                if(!executeReplaceAll)
                {
                    foreach(Match match in regex.Matches(requirement.Description))
                    {
                        if(!executeReplaceAll)
                        {
                            this.AddBookmark(sbq, "Requirements", counter, match.Index, match.Length);
                        }
                    }
                }
                else
                {
                    requirement.Description = regex.Replace(requirement.Description, replacedText);
                }
            }

            // ATTRIBUTES -> DESCRIPTION
            if(!executeReplaceAll)
            {
                foreach(Match match in regex.Matches(this.Attributes.Description))
                {
                    if(!executeReplaceAll)
                    {
                        this.AddBookmark(sbq, "Attributes.Description", counter, match.Index, match.Length);
                    }
                }
            }
            else
            {
                this.Attributes.Description = regex.Replace(this.Attributes.Description, replacedText);
            }

            // ATTRIBUTES -> NOTES
            if(!executeReplaceAll)
            {
                foreach(Match match in regex.Matches(this.Attributes.Notes))
                {
                    if(!executeReplaceAll)
                    {
                        this.AddBookmark(sbq, "Attributes.Notes", counter, match.Index, match.Length);
                    }
                }
            }
            else
            {
                this.Attributes.Notes = regex.Replace(this.Attributes.Notes, replacedText);
            }


            foreach(Actor actor in this.Actors)
            {
                actor.TextSearch(
                    sbq,
                    searchedText,
                    replacedText,
                    wholeWordsOnly,
                    caseSensitivity,
                    executeReplaceAll);
            }

            foreach(UseCase useCase in this.UseCases)
            {
                useCase.TextSearch(
                    sbq,
                    searchedText,
                    replacedText,
                    wholeWordsOnly,
                    caseSensitivity,
                    executeReplaceAll);
            }

            foreach(Package package in this.Packages)
            {
                package.TextSearch(
                    sbq,
                    searchedText,
                    replacedText,
                    wholeWordsOnly,
                    caseSensitivity,
                    executeReplaceAll);
            }
        }
		#endregion
		#endregion

		#region Public Properties
        [XmlArray]
        [XmlArrayItem(typeof(Actor))]
		public Actors Actors
		{
			get
			{
				return this.actors;
			}
		}

        [XmlArray]
        [XmlArrayItem(typeof(Package))]
        public Packages Packages
		{
			get
			{
				return this.packages;
			}
		}

        [XmlArray]
        [XmlArrayItem(typeof(UseCase))]
        public UseCases UseCases
		{
			get
			{
				return this.useCases;
			}
		}

        [XmlArray]
        [XmlArrayItem(typeof(Requirement))]
        public Requirements Requirements
		{
			get
			{
				return this.requirements;
			}
		}

        public CommonAttributes Attributes
		{
			get
			{
				return this.commonAttributes;
			}
            set
            {
                this.commonAttributes = value;
            }
		}
		#endregion

        #region Protected Methods
        protected void AddBookmark(
            SearchBookmarkQueue sbq,
            String propertyName,
            Int32 propertyIndex,
            Int32 start,
            Int32 length)
        {
            SearchBookmark sb = new SearchBookmark();
            sb.Element = this;
            sb.PropertyName = propertyName;
            sb.Index = propertyIndex;
            sb.Start = start;
            sb.Length = length;
            sbq.Enqueue(sb);
        }
        #endregion

        #region Private Methods
        void RecursiveGetActorNameList(StringCollection sc)
		{
			foreach(Actor actor in this.actors)
			{
				sc.Add(actor.Name);
			}

			foreach(Package subPackage in this.packages)
			{
				subPackage.RecursiveGetActorNameList(sc);
			}
		}
		#endregion

		#region Internal Methods
		internal void ChangeReferences(
			String oldName,
			String oldNameStartTag,
			String oldNameEndTag,
			String newName,
			String newNameStartTag,
			String newNameEndTag,
			Boolean deep)
		{
            String oldFullName = oldNameStartTag + @"\b" + oldName + @"\b" + oldNameEndTag;
			String newFullName = newNameStartTag + newName + newNameEndTag;

            Regex regex = new Regex(oldFullName, RegexOptions.None);

            if(this is Model)
            {
                foreach(GlossaryItem gi in ((Model)this).Glossary)
                {
                    gi.Description = regex.Replace(gi.Description, newFullName);
                }

                foreach(Stakeholder stakeholder in ((Model)this).Stakeholders)
                {
                    stakeholder.Description = regex.Replace(stakeholder.Description, newFullName);
                }
            }

			foreach(Requirement requirement in this.requirements)
			{
				requirement.Description = regex.Replace(requirement.Description, newFullName);
			}

			foreach(UseCase useCase in this.UseCases)
			{
				foreach(OpenIssue openIssue in useCase.OpenIssues)
				{
					openIssue.Description = regex.Replace(openIssue.Description, newFullName);
				}
				foreach(Step step in useCase.Steps)
				{
					step.Description = regex.Replace(step.Description, newFullName);
				}
				if(useCase.Prose != null)
				{
					useCase.Prose = regex.Replace(useCase.Prose, newFullName);
				}
			}

            foreach (Actor actor in this.Actors)
            {
                foreach (Goal goal in actor.Goals)
                {
                    goal.Description = regex.Replace(goal.Description, newFullName);
                }
            }

			if(deep)
			{
				foreach(Package package in this.Packages)
				{
					package.ChangeReferences(
						oldName,
						oldNameStartTag,
						oldNameEndTag,
						newName,
						newNameStartTag,
						oldNameEndTag,
						deep);
				}
			}
		}

		internal void PurgeReferences(
			object element,
			Package targetPackage,
			String oldNameStartTag,
			String oldNameEndTag,
			String newNameStartTag,
			String newNameEndTag,
			Boolean dontMark)
		{
			Package currentPackage = this;

            if(element.GetType() == typeof(Stakeholder))
            {
                foreach(Package package in this.Packages)
                {
                    package.PurgeReferences(
                        (Stakeholder)element,
                        null,
                        oldNameStartTag,
                        oldNameEndTag,
                        newNameStartTag,
                        newNameEndTag,
                        dontMark);
                }
                if(!dontMark)
                {
                    this.ChangeReferences(
                        ((Stakeholder)element).Name,
                        oldNameStartTag,
                        oldNameEndTag,
                        ((Stakeholder)element).Name,
                        newNameStartTag,
                        newNameEndTag,
                        false);
                    this.ChangeReferences(
                        ((Stakeholder)element).Path,
                        oldNameStartTag,
                        oldNameEndTag,
                        ((Stakeholder)element).Path,
                        newNameStartTag,
                        newNameEndTag,
                        false);
                }
                foreach(Requirement requirement in this.Requirements)
                {
                    foreach(ReferencedObject refobj in requirement.Proponents)
                    {
                        if(refobj.UniqueID == ((Stakeholder)element).UniqueID)
                        {
                            requirement.RemoveProponent((Stakeholder)element);
                            break;
                        }
                    }
                    foreach(ReferencedObject refobj in requirement.Beneficiaries)
                    {
                        if(refobj.UniqueID == ((Stakeholder)element).UniqueID)
                        {
                            requirement.RemoveBeneficiary((Stakeholder)element);
                            break;
                        }
                    }
                }
            }

			if(element.GetType() == typeof(Package))
			{
				foreach(Actor actor in ((Package)element).Actors)
				{
					currentPackage.PurgeReferences(
						actor,
						null,
						oldNameStartTag,
						oldNameEndTag,
						newNameStartTag,
						newNameEndTag,
						dontMark);
				}
				foreach(UseCase useCase in ((Package)element).UseCases)
				{
					currentPackage.PurgeReferences(
						useCase,
						null,
						oldNameStartTag,
						oldNameEndTag,
						newNameStartTag,
						newNameEndTag,
						dontMark);
				}
				foreach(Package package in ((Package)element).Packages)
				{
					currentPackage.PurgeReferences(
						package,
						null,
						oldNameStartTag,
						oldNameEndTag,
						newNameStartTag,
						newNameEndTag,
						dontMark);
				}
				if(!dontMark)
				{
					this.ChangeReferences(
						((Package)element).Name,
						oldNameStartTag,
						oldNameEndTag,
						((Package)element).Name,
						newNameStartTag,
						newNameEndTag,
						false);
					this.ChangeReferences(
						((Package)element).Path,
						oldNameStartTag,
						oldNameEndTag,
						((Package)element).Path,
						newNameStartTag,
						newNameEndTag,
						false);
				}
			}

			if(element.GetType() == typeof(UseCase))
			{
				foreach(Package package in this.Packages)
				{
					package.PurgeReferences(
						element,
						null,
						oldNameStartTag,
						oldNameEndTag,
						newNameStartTag,
						newNameEndTag,
						dontMark);
				}
				if(!dontMark)
				{
					this.ChangeReferences(
						((UseCase)element).Name,
						oldNameStartTag,
						oldNameEndTag,
						((UseCase)element).Name,
						newNameStartTag,
						newNameEndTag,
						false);
					this.ChangeReferences(
						((UseCase)element).Path,
						oldNameStartTag,
						oldNameEndTag,
						((UseCase)element).Path,
						newNameStartTag,
						newNameEndTag,
						false);
				}
				// remove use case references in use case's steps
				foreach(UseCase useCase in this.UseCases)
				{
					foreach(Step step in useCase.Steps)
					{
						if(step.Dependency.PartnerUniqueID == ((UseCase)element).UniqueID)
						{
							step.Dependency.Type = DependencyItem.ReferenceType.None;
							step.Dependency.PartnerUniqueID = "";
							step.Dependency.Stereotype = "";
						}
					}
				}
                // remove use case references in use case's requirements
                foreach(Requirement requirement in this.Requirements)
                {
                    foreach(ReferencedObject refobj in requirement.MappedUseCases)
                    {
                        if(refobj.UniqueID == ((UseCase)element).UniqueID)
                        {
                            requirement.RemoveMappedUseCase((UseCase)element);
                            break;
                        }
                    }
                }
			}

			if(element.GetType() == typeof(Actor))
			{
				foreach(UseCase useCase in this.UseCases)
				{
					ActiveActor tmpAActor = null;
					foreach(ActiveActor aactor in useCase.ActiveActors)
					{
						if(aactor.ActorUniqueID == ((Actor)element).UniqueID)
						{
							tmpAActor = aactor;
						}
					}
					if(tmpAActor != null)
					{
						useCase.ActiveActors.Remove(tmpAActor);
					}
				}
				foreach(Package package in this.Packages)
				{
					package.PurgeReferences(
						element,
						null,
						oldNameStartTag,
						oldNameEndTag,
						newNameStartTag,
						newNameEndTag,
						dontMark);
				}
				if(!dontMark)
				{
					this.ChangeReferences(
						((Actor)element).Name,
						oldNameStartTag,
						oldNameEndTag,
						((Actor)element).Name,
						newNameStartTag,
						newNameEndTag,
						false);
					this.ChangeReferences(
						((Actor)element).Path,
						oldNameStartTag,
						oldNameEndTag,
						((Actor)element).Path,
						newNameStartTag,
						newNameEndTag,
						false);
				}
			}
		}
		#endregion
	}
}
