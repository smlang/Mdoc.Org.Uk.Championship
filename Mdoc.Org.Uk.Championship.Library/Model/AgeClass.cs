using System;
using System.Collections.Generic;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class AgeClass
    {
        #region Attributes

        public String Name { get; set; }
        public Sex Sex { get; set; }
        public Byte MinimumAge { get; set; }
        public Byte MaximumAge { get; set; }
        public Double SpeedRatio { get; set; }
        public Double DistanceRatio { get; set; }

        #endregion

        #region Comparer

        public class Comparer : IComparer<AgeClass>
        {
            #region IComparer<AgeClass> Members

            public Int32 Compare(AgeClass a, AgeClass b)
            {
                return a.Name.CompareTo(b.Name);
            }

            #endregion
        }

        #endregion
    }
}