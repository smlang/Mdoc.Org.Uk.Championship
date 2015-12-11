using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Club
    {
        #region Attributes

        public String DefaultScoreRegexPattern { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public Boolean InCup { get; set; }

        #endregion

        #region Methods

        public bool Update(Cup cup, string newName, string url, bool inCup, string defaultScoreRegexPattern, out string message)
        {
            if (String.IsNullOrEmpty(newName))
            {
                message = "Club Name is mandatory";
                return false;
            }

            if ((!String.IsNullOrEmpty(Name)) &&
                (!Name.Equals(newName, StringComparison.InvariantCultureIgnoreCase)) &&
                cup.ClubList.Any(club => club.Name.Equals(newName, StringComparison.InvariantCultureIgnoreCase)))
            {
                message = "Club Name is already used";
                return false;
            }

            if (!String.IsNullOrEmpty(url))
            {
                if (!Web.Validate(url, out message))
                {
                    return false;
                }
            }

            if (!String.IsNullOrEmpty(defaultScoreRegexPattern))
            {
                try
                {
                    new Regex(defaultScoreRegexPattern);
                }
                catch
                {
                    message = "Regular Expression is invalid";
                    return false;
                }
            }

            string oldName = this.Name;
            this.Name = newName;
            this.Url = url;
            this.InCup = inCup;
            this.DefaultScoreRegexPattern = defaultScoreRegexPattern;

            #region Update Club references
            if (!cup.UpdateClubReference(oldName, newName, out message))
            {
                return false;
            }
            #endregion

            message = null;
            return true;
        }

        public bool Delete(Cup cup, out string message)
        {
            #region Delete Club references
            if (!cup.DeleteClubReference(this.Name, out message))
            {
                return false;
            }
            #endregion

            cup.ClubList.Remove(this);

            message = null;
            return true;
        }

        #endregion

        #region Comparer

        public class Comparer : IComparer<Club>
        {
            #region IComparer<Club> Members

            public Int32 Compare(Club a, Club b)
            {
                return a.Name.CompareTo(b.Name);
            }

            #endregion
        }

        #endregion
    }
}