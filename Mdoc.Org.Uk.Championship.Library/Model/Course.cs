using System;
using System.Collections.Generic;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Course
    {
        #region Attributes

        public String Name { get; set; }
        public String TD { get; set; }
        public String DefaultAgeClass { get; set; }
        public Double? Distance { get; set; }

        public List<Score> ScoreList { get; set; }

        #endregion

        #region Constructor

        public Course()
        {
            ScoreList = new List<Score>();
        }

        #endregion

        public bool UpdateAgeClassReference(string oldName, string newName, out string message)
        {
            if ((this.DefaultAgeClass != null) && (this.DefaultAgeClass.Equals(oldName)))
            {
                this.DefaultAgeClass = newName;
            }
            message = String.Empty;
            return true;
        }

        public bool DeleteAgeClassReference(string name, out string message)
        {
            if ((this.DefaultAgeClass != null) && (this.DefaultAgeClass.Equals(name)))
            {
                message = String.Format("Can not delete Age Class '{0}', it is default age class of course '{1}'", name, this.Name);
                return false;
            }
            message = String.Empty;
            return true;
        }

        public bool UpdateClubReference(string oldName, string newName, out string message)
        {
            foreach (Score score in this.ScoreList)
            {
                if (!score.UpdateClubReference(oldName, newName, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }

        public bool DeleteClubReference(string name, out string message)
        {
            foreach (Score score in this.ScoreList)
            {
                if (!score.DeleteClubReference(name, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }
    }
}