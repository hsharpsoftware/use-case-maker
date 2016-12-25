using System;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using UseCaseMakerLibrary.Support;

namespace UseCaseMakerLibrary
{
    [Serializable]
	public class Actor : IdentificableObject
	{
		#region Class Members
		private CommonAttributes commonAttributes = new CommonAttributes();
		private Goals goals = new Goals();
		#endregion

		#region Constructors
		internal Actor()
			: base()
		{
		}

		internal Actor(String name, String prefix, Int32 id)
			: base(name,prefix,id)
		{
		}

		internal Actor(String name, String prefix, Int32 id, Package owner)
			: base(name,prefix,id,owner)
		{
		}
		#endregion

		#region Public Properties
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

        [XmlArray]
        [XmlArrayItem(typeof(Goal))]
		public Goals Goals
		{
			get
			{
				return this.goals;
			}
		}
		#endregion

		#region Public Methods
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

            // GOALS
            for(counter = 0; counter < this.goals.Count; counter++)
            {
                Goal goal = (Goal)this.goals[counter];

                if(!executeReplaceAll)
                {
                    foreach(Match match in regex.Matches(goal.Description))
                    {
                        if(!executeReplaceAll)
                        {
                            this.AddBookmark(sbq, "Goals", counter, match.Index, match.Length);
                        }
                    }
                }
                else
                {
                    goal.Description = regex.Replace(goal.Description, replacedText);
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
        }
        #endregion

        #region Goals Handling
        public Int32 AddGoal()
		{
			Goal goal = new Goal();
			Int32 index = this.goals.Count;
			Int32 ret;

			if(index == 0)
			{
				goal.ID = 1;
			}
			else
			{
				goal.ID = ((Goal)this.goals[index - 1]).ID + 1;
			}

			ret = this.goals.Add(goal);

			return ret;
		}

		public void RemoveGoal(Goal goal)
		{
			foreach(Goal tmpGoal in this.goals)
			{
				if(tmpGoal.ID > goals.ID)
				{
					tmpGoal.ID -= 1;
				}
			}
			this.goals.Remove(goal);
		}

		public Goal FindGoalByUniqueID(String uniqueID)
		{
			Goal goal = null;

			foreach(Goal tmpGoal in this.goals)
			{
				if(tmpGoal.UniqueID == uniqueID)
				{
					goal = tmpGoal;
				}
			}

			return goal;
		}
		#endregion
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

        #region Static Members
        #endregion
    }
}
