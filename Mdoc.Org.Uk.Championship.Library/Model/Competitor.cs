using System;
using System.Collections.Generic;
using System.Linq;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Competitor
    {
        #region Attributes

        public String Name { get; set; }
        public String Club { get; set; }
        public Attribute Attribute { get; set; }
        public List<String> AlternativeClubList { get; set; }
        public List<String> AlternativeNameList { get; set; }

        #endregion

        #region Constructor

        public Competitor()
        {
            AlternativeClubList = new List<String>();
            AlternativeNameList = new List<String>();
        }

        internal Competitor(Score score, Course course, Cup cup)
        {
            Cup.UI.ShowWarning(String.Format("New Competitor: {0}", score.Name));

            #region Ask for main name

            // Get a list of possible perminations of the competitor's name
            List<String> nameCollection = Cup.SplitName(score.Name);
            List<String> perminationCollection = new List<String>();
            GetPerminations(null, nameCollection, perminationCollection);

            Int32 choice2 = Cup.UI.GetChoice(
                1,
                String.Format("What is the primary name of the new competitor '{0}'", score.Name),
                perminationCollection.ToArray());
            string primaryName = perminationCollection[choice2-1];

            #endregion

            Name = primaryName;
            Club = score.Club;
            AlternativeClubList = new List<String>();
            AlternativeNameList = new List<String>();
            if (primaryName != score.Name)
            {
                AlternativeNameList.Add(score.Name);
            }

            // Set Attribute
            if (score.Attribute != null)
            {
                Attribute = score.Attribute;
            }
            else
            {
                SetAttributeToCourseDefault(cup, course);
            }
        }

        #endregion

        #region Methods

        // Found all perminations of the competitor name (using recursion)
        private static void GetPerminations(string name, List<String> nameCollection, List<String> perminationCollection)
        {
            for (int i = 0; i < nameCollection.Count; i++)
            {
                string word = nameCollection[i];
                // Add word to the end of the name
                string newName = (name == null) ? word : name + " " + word;
                // If the word is the last word in the collection, then we have
                // completed a permination and add the permination in the 
                // collection.  Otherwise recursively call GetPermination with
                // a updated name and a shortern name collection
                if (nameCollection.Count == 1)
                {
                    perminationCollection.Add(newName);
                }
                else
                {
                    // Create a new name collection, shortern by excluding word
                    List<String> newNameCollection = nameCollection.Where((t, j) => i != j).ToList();
                    GetPerminations(newName, newNameCollection, perminationCollection);
                }
            }
        }

        internal bool Matches(string name, string club)
        {
            bool matchName = false;
            if ((Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ||
                (AlternativeNameList.Any(alternativeName => alternativeName.Equals(name, StringComparison.InvariantCultureIgnoreCase))))
            {
                matchName = true;
            }
            if (!matchName)
            {
                return false;
            }

            bool matchClub = false;
            if ((Club.Equals(club, StringComparison.InvariantCultureIgnoreCase)) ||
                (AlternativeClubList.Any(alternativeClub => alternativeClub.Equals(club, StringComparison.InvariantCultureIgnoreCase))))
            {
                matchClub = true;
            }

            return matchClub;
        }

        internal void Update(Score score, Course course, Cup cup)
        {
            // Check/Set Gender
            if ((Attribute != null) && (score.Attribute != null))
            {
                if (!Attribute.AgeClass(cup).Equals(score.Attribute.AgeClass(cup)))
                {
                    Int32 choice = Cup.UI.GetChoice(
                        1,
                        String.Format("Mismatch in Age Class of {0}", score.Name),
                        String.Format("Cup Age Class {0} {1}", Attribute.Sex, Attribute.YearOfBirth),
                        String.Format("Event Age Class {0} {1}", score.Attribute.Sex, score.Attribute.YearOfBirth));

                    switch (choice)
                    {
                        case 1:
                            score.Attribute = Attribute;
                            break;
                        case 2:
                            Attribute = score.Attribute;
                            break;
                    }
                }
            }
            else if (score.Attribute != null)
            {
                Attribute = score.Attribute;
            }
            else if (course.DefaultAgeClass != null)
            {
                SetAttributeToCourseDefault(cup, course);
            }
        }

        internal void SetAttributeToCourseDefault(Cup cup, Course course)
        {
            string ageClass = Cup.UI.GetValue(
                String.Format("Please enter a Age Class for {0}", Name),
                course.DefaultAgeClass);
            Attribute = new Attribute(cup, ageClass);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool UpdateClubReference(string oldName, string newName, out string message)
        {
            if ((this.Club != null) && (this.Club.Equals(oldName)))
            {
                this.Club = newName;
            }
            message = String.Empty;
            return true;
        }

        public bool DeleteClubReference(string name, out string message)
        {
            if ((this.Club != null) && (this.Club.Equals(name)))
            {
                message = String.Format("Can not delete Club '{0}', it is the club of competitor '{1}'", name, this.Name);
                return false;
            }
            message = String.Empty;
            return true;
        }
        #endregion

        #region Comparer

        public class Comparer : IComparer<Competitor>
        {
            #region IComparer<Competitor> Members

            public Int32 Compare(Competitor a, Competitor b)
            {
                return a.Name.CompareTo(b.Name);
            }

            #endregion
        }

        #endregion
    }
}