using System;
using System.Collections.Generic;

namespace Mdoc.Org.Uk.Championship.Library
{
    internal class DivisionCompetitor
    {
        #region Attributes

        internal Competitor Competitor { get; set; }
        internal Dictionary<String, Score> RacePointList { get; set; }
        internal Int32 AveragePoint { get; set; }
        internal Int32 CountingRuns { get; set; }
        internal Int32 TotalPoint { get; set; }

        #endregion

        #region Constructor

        internal DivisionCompetitor(Competitor competitor)
        {
            Competitor = competitor;
            RacePointList = new Dictionary<String, Score>();
            CountingRuns = 0;
            AveragePoint = 0;
            TotalPoint = 0;
        }

        #endregion

        #region Methods

        internal void AddRace(String raceName, Score score)
        {
            if (RacePointList.ContainsKey(raceName))
            {
                Cup.UI.ShowWarning(String.Format("{0} ran twice at {1}", score.Name, raceName));
                if ((!RacePointList[raceName].Point.HasValue) ||
                    ((score.Point.HasValue) && (score.Point.Value > RacePointList[raceName].Point.Value)))
                {
                    RacePointList[raceName] = score;
                }
            }
            else
            {
                RacePointList.Add(raceName, score);
            }
        }

        internal void CalculateTotalPoint(Int32 maxRuns)
        {
            CountingRuns = 0;
            AveragePoint = 0;
            TotalPoint = 0;
            
            // Reset scores to include
            foreach (Score score in RacePointList.Values)
            {
                score.Include = false;
            }

            // Identify scores to include
            for (int i = 0; i < maxRuns; i++)
            {
                Score maxScore = null;
                foreach (Score score in RacePointList.Values)
                {
                    if (!score.Include && ((maxScore == null) ||
                                           (!maxScore.Point.HasValue) ||
                                           (score.Point.HasValue && (score.Point.Value > maxScore.Point.Value))))
                    {
                        maxScore = score;
                    }
                }
                if ((maxScore == null) || (!maxScore.Point.HasValue) || (!String.IsNullOrEmpty(maxScore.Comment)))
                {
                    break;
                }
                maxScore.Include = true;
                TotalPoint += maxScore.Point.Value;
                CountingRuns++;
            }
            if (CountingRuns != 0)
            {
                AveragePoint = TotalPoint / CountingRuns;
            }
        }

        #endregion
    }
}