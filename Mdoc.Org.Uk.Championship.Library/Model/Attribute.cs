using System;
using System.Linq;

namespace Mdoc.Org.Uk.Championship.Library
{
    public enum Sex
    {
        Men,
        Women
    }

    [Serializable]
    public class Attribute
    {
        #region Attributes

        public Sex Sex { get; set; }
        public Int32 YearOfBirth { get; set; }
        public Boolean Actual { get; set; }

        public Byte Age(Int32 yearOfCup)
        {
            return (Byte) (yearOfCup - YearOfBirth);
        }

        public String AgeClass(Cup cup)
        {
            Byte age = Age(cup.Year);
            return (from ageClass in cup.AgeClassList
                    where (ageClass.Sex == Sex) && (ageClass.MinimumAge <= age) && (ageClass.MaximumAge >= age)
                    select ageClass.Name).FirstOrDefault();
        }

        #endregion

        #region Constructors

        public Attribute()
        {
        }

        public Attribute(Sex sex, Int32 yearOfBirth, Int32 yearOfCup)
        {
            Actual = true;

            Sex = sex;

            if (yearOfBirth > 100)
            {
                Int32 century = yearOfCup%100;
                yearOfBirth += ((yearOfCup - century) > yearOfBirth) ? century : century - 100;
            }
            YearOfBirth = yearOfBirth;
        }

        public Attribute(Cup cup, String ageClassName)
        {
            Actual = false;

            if (String.IsNullOrEmpty(ageClassName))
            {

            }
            ageClassName = ageClassName.Trim().ToUpper();

            Sex = (ageClassName[0] == 'M') ? Sex.Men : Sex.Women;
            YearOfBirth = cup.Year - Int32.Parse(ageClassName.Substring(1));
        }

        #endregion

        #region Methods

        public bool Equals(Attribute obj)
        {
            return YearOfBirth.Equals(obj.YearOfBirth) && Sex.Equals(obj.Sex);
        }

        #endregion
    }
}