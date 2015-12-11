using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class CourseDefinition
    {
        private string _name;
        private Regex _nameRegEx;

        #region Attributes

        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _nameRegEx = new Regex(value, RegexOptions.IgnoreCase);
            }
        }
        public String TD { get; set; }

        public String DefaultAgeClass { get; set; }

        #endregion

        internal bool IsMatch(string courseName)
        {
            return _nameRegEx.IsMatch(courseName);
        }

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

        #region Comparer

        public class Comparer : IComparer<CourseDefinition>
        {
            #region IComparer<CourseDefinition> Members

            public Int32 Compare(CourseDefinition a, CourseDefinition b)
            {
                if (a.TD != b.TD)
                {
                    return a.TD.CompareTo(b.TD);
                }
                return (a.DefaultAgeClass == b.DefaultAgeClass)
                           ? a.Name.CompareTo(b.Name)
                           : a.DefaultAgeClass.CompareTo(b.DefaultAgeClass);
            }

            #endregion
        }

        #endregion
    }
}