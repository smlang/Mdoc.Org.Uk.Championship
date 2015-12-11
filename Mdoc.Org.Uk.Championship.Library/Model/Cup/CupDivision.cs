using System;
using System.Collections.Generic;

namespace Mdoc.Org.Uk.Championship.Library
{
    internal class CupDivision
    {
        #region Attributes

        internal Dictionary<String, DivisionCompetitor> CompetitorList { get; set; }

        #endregion

        #region Constructor

        internal CupDivision()
        {
            CompetitorList = new Dictionary<String, DivisionCompetitor>();
        }

        internal static void SetDivision(Cup cup, Course course, Score score)
        {
            if (course.TD == Settings.Default.OpenTechnicalDifficulty)
            {
                score.Division = Settings.Default.OpenTechnicalDifficulty;
                return;
            }

            if (score.Attribute.Age(cup.Year) < Settings.Default.MinimumSeniorAge)
            {
                score.Division = String.Format("{0}{1}", course.TD, (score.Attribute.Sex == Sex.Men) ? "M" : "W");
                return;
            }

            score.Division = null;
        }

        #endregion
    }
}