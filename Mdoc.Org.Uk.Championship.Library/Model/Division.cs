using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Division
    {
        #region Attributes

        [XmlIgnore]
        internal List<Double> PaceList;
        public String Name { get; set; }
        public Double? MedianPace { get; set; }
        public Double? MedianAbsoluteDeviationPace { get; set; }
        public Double? MeanPace { get; set; }
        public Double? StandardDeviationPace { get; set; }

        #endregion

        #region Constructor

        public Division()
        {
            PaceList = new List<Double>();
        }

        #endregion
    }
}