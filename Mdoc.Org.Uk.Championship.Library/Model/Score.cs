using System;
using System.Xml.Serialization;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Score
    {
        #region Attributes

        public String Name { get; set; }
        public String Club { get; set; }
        public Attribute Attribute { get; set; }
        public String Division { get; set; }

        public String Comment { get; set; }
        public Duration Duration { get; set; }
        public Double? Pace { get; set; }

        public Double? DistanceRatio { get; set; }
        public Double? SpeedRatio { get; set; }

        public Double? AdjustedPace { get; set; }
        public Int32? Point { get; set; }

        [XmlIgnore]
        internal Boolean Include { get; set; }

        [XmlIgnore]
        internal String Text
        {
            get
            {
                if (Pace.HasValue && Point.HasValue)
                {
                    return Include ? Point.Value.ToString() : String.Format("[{0}]", Point.Value);
                }
                return String.Format("[{0}]", Comment);
            }
        }

        [XmlIgnore]
        internal String ToolTip
        {
            get
            {
                if (Pace.HasValue)
                {
                    if (AdjustedPace.HasValue && SpeedRatio.HasValue && DistanceRatio.HasValue)
                    {
                        return String.Format("{2} m/km = Pace {0} m/km * Adjustment {1}",
                                             Math.Round(Pace.Value/60, 1, MidpointRounding.AwayFromZero),
                                             Math.Round(SpeedRatio.Value + DistanceRatio.Value, 2,
                                                        MidpointRounding.AwayFromZero),
                                             Math.Round(AdjustedPace.Value/60, 1, MidpointRounding.AwayFromZero));
                    }
                    return String.Format("Pace {0} m/km",
                                         Math.Round(Pace.Value/60, 1, MidpointRounding.AwayFromZero));
                }
                return String.Format("[{0}]", Comment);
            }
        }

        #endregion

        #region Constructor

        public Score()
        {
            Include = false;
        }

        #endregion

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
                message = String.Format("Can not delete Club '{0}', it is club of runner '{1}'", name, this.Name);
                return false;
            }
            message = String.Empty;
            return true;
        }
    }
}