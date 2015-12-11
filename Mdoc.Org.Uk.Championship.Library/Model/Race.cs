using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Race
    {
        #region Class Attributes

        private static Regex _distanceRegex;

        #endregion

        #region Attributes

        private String _scoreRegexPattern;
        public String Code { get; set; }
        public DateTime Date { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String Note { get; set; }
        public String Club { get; set; }

        public String ScoreRegexPattern
        {
            get { return _scoreRegexPattern; }
            set
            {
                _scoreRegexPattern = value;
                ScoreRegex = new Regex(_scoreRegexPattern, RegexOptions.IgnorePatternWhitespace);
            }
        }

        [XmlIgnore]
        internal Regex ScoreRegex { get; set; }

        public List<Division> DivisionList { get; set; }
        public List<Course> CourseList { get; set; }

        #endregion

        #region Constructor

        public Race()
        {
            CourseList = new List<Course>();
        }

        #endregion

        #region Methods

        internal static void Initialise()
        {
            _distanceRegex = new Regex(Settings.Default.DistanceRegex,
                                       RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
        }

        public void Download(Cup cup, string filePath)
        {
            #region Get defaults from club

            if (String.IsNullOrEmpty(_scoreRegexPattern) || String.IsNullOrEmpty(Url))
            {
                foreach (Club club in cup.ClubList)
                {
                    if (Club.Equals(club.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (String.IsNullOrEmpty(_scoreRegexPattern))
                        {
                            _scoreRegexPattern = club.DefaultScoreRegexPattern;
                        }
                        if (String.IsNullOrEmpty(Url))
                        {
                            Url = club.Url;
                        }
                        break;
                    }
                }
            }

            #endregion

            #region Get source path

            Url = Cup.UI.GetValue("Enter URL of Race Results", Url);
            if (String.IsNullOrEmpty(Url))
            {
                return;
            }

            #endregion

            #region Get source

            using (Stream stream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        try
                        {
                            foreach (String resultUrl in Web.Download(Url, Cup.UI))
                            {
                                Web.DownloadResults(writer, resultUrl, Cup.UI);
                            }
                        }
                        finally
                        {
                            writer.Close();
                        }
                    }
                }
                finally
                {
                    stream.Close();
                }
            }

            #endregion
        }

        public void Import(Cup cup, string filePath)
        {
            if (File.Exists(filePath))
            {
                #region Build Course Regex

                StringBuilder courseRegexPattern = new StringBuilder(@"\A\s*(.*Course\s+)?(");
                bool first = true;
                foreach (CourseDefinition courseDefinition in cup.CourseDefinitionList)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        courseRegexPattern.Append("|");
                    }
                    courseRegexPattern.Append(String.Format("(?<name>{0})", courseDefinition.Name));
                }
                courseRegexPattern.Append(@")(\s+Course.*)?");
                Regex courseRegex = new Regex(courseRegexPattern.ToString(), RegexOptions.IgnoreCase);

                #endregion

                #region Build Skip Line Regex

                StringBuilder skipResultLineRegexPattern = new StringBuilder();
                first = true;
                foreach (String skipResultLine in cup.SkipResultLineList)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        skipResultLineRegexPattern.Append("|");
                    }
                    skipResultLineRegexPattern.Append(skipResultLine);
                }
                Regex skipResultLineRegex = new Regex(skipResultLineRegexPattern.ToString(), RegexOptions.IgnoreCase);

                #endregion

                CourseList = new List<Course>();

                #region Read Race Results

                StreamReader reader = new StreamReader(filePath, Encoding.ASCII);
                try
                {
                    Course currentCourse = null;

                    while (true)
                    {
                        string line = reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }

                        #region Check for Lines to Skip
                        Match m = skipResultLineRegex.Match(line);
                        if (m.Success)
                        {
                            Cup.UI.ShowInformation(String.Format("Skipped line {0}", line));
                            continue;
                        }
                        #endregion

                        #region Check For New Score
                        if (currentCourse != null)
                        {
                            m = ScoreRegex.Match(line);
                            if (m.Success)
                            {
                                string name = m.Groups["name"].Value.Trim();
                                string club = m.Groups["club"].Value.Trim();
                                string ageClass = m.Groups["class"].Value.Trim();
                                string yb = m.Groups["yb"].Value.Trim();
                                string comment = m.Groups["comment"].Value.Trim();
                                if (comment == String.Empty)
                                {
                                    comment = m.Groups["comment2"].Value.Trim();
                                }
                                string durationText = (comment != String.Empty) ? String.Empty : m.Groups["duration"].Value.Trim();

                                // ignore Team that did not start
                                if (!comment.Equals("dns", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Score score = new Score();
                                    currentCourse.ScoreList.Add(score);

                                    if (String.IsNullOrEmpty(name))
                                    {
                                        Cup.UI.ShowWarning(String.Format("Missing Name: {0}", line));
                                    }
                                    else
                                    {
                                        score.Name = name;
                                    }

                                    if (String.IsNullOrEmpty(club))
                                    {
                                        Cup.UI.ShowWarning(String.Format("Missing Club: {0}", line));
                                    }
                                    else
                                    {
                                        score.Club = club;
                                    }

                                    Int32 result;
                                    if (Int32.TryParse(yb, out result))
                                    {
                                        score.Attribute = new Attribute(Sex.Men, result, cup.Year);
                                    }
                                    else if (!String.IsNullOrEmpty(ageClass))
                                    {
                                        score.Attribute = new Attribute(cup, ageClass);
                                    }
                                    else
                                    {
                                        Cup.UI.ShowWarning(String.Format("Missing Age Class: {0}", line));
                                        score.Attribute = new Attribute(cup, currentCourse.DefaultAgeClass);
                                    }

                                    if (String.IsNullOrEmpty(durationText))
                                    {
                                        if (String.IsNullOrEmpty(comment))
                                        {
                                            Cup.UI.ShowWarning(String.Format("Missing Duration/Comment: {0}", line));
                                        }
                                        else
                                        {
                                            score.Comment = comment;
                                        }
                                    }
                                    else
                                    {
                                        score.Duration = new Duration(durationText);
                                    }

                                }
                                continue;
                            }
                        }
                        #endregion

                        #region Check For New Course
                        bool ignored = true;
                        m = courseRegex.Match(line);
                        if (m.Success)
                        {
                            ignored = false;

                            string courseName = String.Empty;
                            foreach (Capture c in m.Groups["name"].Captures)
                            {
                                courseName = c.Value;
                            }

                            currentCourse = null;
                            foreach (Course c in CourseList)
                            {
                                if (String.Equals(c.Name, courseName, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Cup.UI.ShowWarning(String.Format("Repetition of course: {0}", courseName));
                                    currentCourse = c;
                                    break;
                                }
                            }

                            if (currentCourse == null)
                            {
                                currentCourse = new Course();
                                CourseList.Add(currentCourse);
                                currentCourse.Name = courseName;

                                foreach (CourseDefinition d in cup.CourseDefinitionList)
                                {
                                    if (d.IsMatch(courseName))
                                    {
                                        currentCourse.TD = d.TD;
                                        currentCourse.DefaultAgeClass = d.DefaultAgeClass;
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Check For Course Distance
                        if ((currentCourse != null) && (!currentCourse.Distance.HasValue))
                        {
                            m = _distanceRegex.Match(line);
                            if (m.Success)
                            {
                                ignored = false;
                                string distance = m.Groups["distance"].Value;
                                currentCourse.Distance = Double.Parse(distance);
                            }
                        }
                        #endregion

                        if (ignored)
                        {
                            Cup.UI.ShowWarning(String.Format("Unprocessed Line {0}", line));
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }

                #endregion
            }
        }

        public void FindCompetitors(Cup cup)
        {
            foreach (Course course in CourseList)
            {
                foreach (Score score in course.ScoreList)
                {
                    Competitor foundCompetitor = null;
                    foreach (Competitor competitor in cup.CompetitorList)
                    {
                        if (competitor.Matches(score.Name, score.Club))
                        {
                            foundCompetitor = competitor;
                            break;
                        }
                    }

                    if ((foundCompetitor == null) && (!String.IsNullOrEmpty(score.Club)))
                    {
                        foreach (Club club in cup.ClubList)
                        {
                            if (score.Club.Equals(club.Name, StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (club.InCup)
                                {
                                    // Look for partial match
                                    foundCompetitor = cup.FindCompetitor(score);

                                    // Otherwise create a new competitor
                                    if (foundCompetitor == null)
                                    {
                                        Competitor newCompetitor = new Competitor(score, course, cup);
                                        cup.CompetitorList.Add(newCompetitor);
                                    }
                                }
                                break;
                            }
                        }
                    }

                    if (foundCompetitor != null)
                    {
                        foundCompetitor.Update(score, course, cup);
                    }
                }
            }
        }

        public void CalculateScore(Cup cup)
        {
            #region Get Maximum Distance

            Double maxDistance = 0;
            foreach (Course course in CourseList)
            {
                if (!course.Distance.HasValue)
                {
                    Cup.UI.ShowWarning(String.Format("Missing Distance {0}", course.Name));
                    return;
                }
                if (course.Distance.Value > maxDistance)
                {
                    maxDistance = course.Distance.Value;
                }
            }

            #endregion

            #region Age Class List

            Dictionary<String, AgeClass> ageClassDictionary = new Dictionary<String, AgeClass>();
            foreach (AgeClass ageClass in cup.AgeClassList)
            {
                ageClassDictionary.Add(ageClass.Name, ageClass);
            }

            #endregion

            #region Calculate Pace

            foreach (Course course in CourseList)
            {
                if (course.TD == null)
                {
                    Cup.UI.ShowWarning(String.Format("Course {0} TD Unknown", course.Name));
                    continue;
                }

                if (!course.Distance.HasValue)
                {
                    Cup.UI.ShowWarning(String.Format("Course {0} Distance unknown", course.Name));
                    continue;
                }

                List<Double> coursePaceList = new List<Double>();
                foreach (Score score in course.ScoreList)
                {
                    CupDivision.SetDivision(cup, course, score);

                    if (score.Duration != null)
                    {
                        score.Pace = (score.Duration.TotalSeconds/course.Distance); // sec/km
                        coursePaceList.Add(score.Pace.Value);

                        if (course.TD == Settings.Default.OpenTechnicalDifficulty)
                        {
                            String scoreAgeClass = score.Attribute.AgeClass(cup);
                            AgeClass ageClass;
                            if (scoreAgeClass != null)
                            {
                                if (ageClassDictionary.ContainsKey(scoreAgeClass))
                                {
                                    ageClass = ageClassDictionary[scoreAgeClass];
                                }
                                else
                                {
                                    Cup.UI.ShowWarning(String.Format("Age Class {0} unknown for {1}", scoreAgeClass,
                                                                     score.Name));
                                    continue;
                                }
                            }
                            else if (course.DefaultAgeClass != null)
                            {
                                if (ageClassDictionary.ContainsKey(course.DefaultAgeClass))
                                {
                                    ageClass = ageClassDictionary[course.DefaultAgeClass];
                                }
                                else
                                {
                                    Cup.UI.ShowWarning(String.Format("Age Class {0} unknown for {1}",
                                                                     course.DefaultAgeClass, score.Name));
                                    continue;
                                }
                            }
                            else
                            {
                                Cup.UI.ShowWarning(String.Format("Age Class unknown for {0}", score.Name));
                                continue;
                            }


                            Double speedRatio = ageClass.SpeedRatio/100;
                            score.SpeedRatio = speedRatio;

                            Double distanceRatio = ageClass.DistanceRatio / 100;
                            Double courseRatioForAgeClass = course.Distance.Value/(distanceRatio*maxDistance);
                            Double coursePowerForAgeClass = Math.Log10(courseRatioForAgeClass)/Math.Log10(2);
                            score.DistanceRatio = 0.02*coursePowerForAgeClass;

                            //score.AdjustedPace = score.Pace * (speedRatio + (0.05m * (1 - (course.Distance.Value / (distanceRatio * maxDistance)))));
                            score.AdjustedPace = score.Pace*(score.SpeedRatio.Value - score.DistanceRatio.Value);
                        }

                        if (score.Division != null)
                        {
                            Division division = DivisionList.Find(x => (x.Name == score.Division));
                            if (division == null)
                            {
                                division = new Division();
                                DivisionList.Add(division);
                                division.Name = score.Division;
                            }
                            division.PaceList.Add(score.AdjustedPace.HasValue
                                                      ? score.AdjustedPace.Value
                                                      : score.Pace.Value);
                        }
                    }
                }
            }

            #endregion

            foreach (Division division in DivisionList)
            {
                #region Find Median Adjusted Pace

                division.PaceList.Sort();
                Int32 midPoint = division.PaceList.Count/2;
                if (division.PaceList.Count == 0)
                {
                    Cup.UI.ShowWarning(String.Format("No Scores for Division: {0}", Name));
                    continue;
                }
                if ((division.PaceList.Count%2) == 0)
                {
                    // Even
                    division.MedianPace = (division.PaceList[midPoint] + division.PaceList[midPoint - 1])/2;
                }
                else
                {
                    // Odd
                    division.MedianPace = division.PaceList[midPoint];
                }

                #endregion

                #region Find Adjusted MAD Pace

                List<Double> divisionDiffPaceList = new List<Double>();
                foreach (Double pace in division.PaceList)
                {
                    divisionDiffPaceList.Add(Math.Abs(division.MedianPace.Value - pace));
                }
                divisionDiffPaceList.Sort();
                if ((divisionDiffPaceList.Count%2) == 0)
                {
                    // Even
                    division.MedianAbsoluteDeviationPace = (divisionDiffPaceList[midPoint] +
                                                            divisionDiffPaceList[midPoint - 1])/2;
                }
                else
                {
                    // Odd
                    division.MedianAbsoluteDeviationPace = divisionDiffPaceList[midPoint];
                }

                #endregion

                #region Find Mean and Variance of Adjusted Pace

                Double total = 0;
                Int32 finishersCount = 0;
                foreach (Double pace in division.PaceList)
                {
                    finishersCount++;
                    total += pace;
                }

                if (total != 0)
                {
                    division.MeanPace = total/finishersCount;

                    Double total2 = division.PaceList.Sum(pace => Math.Pow(pace - division.MeanPace.Value, 2));
                    division.StandardDeviationPace = Math.Sqrt(total2 / finishersCount);
                }

                #endregion
            }

            #region Calculate Score

            foreach (Score score in CourseList.SelectMany(course => course.ScoreList))
            {
                score.Point = 0;
                if ((score.Pace.HasValue) && (score.Division != null))
                {
                    Division division = DivisionList.Find(x => (x.Name == score.Division));

                    if (division.StandardDeviationPace.HasValue && division.MeanPace.HasValue &&
                        (division.StandardDeviationPace.Value != 0))
                    {
                        Double scorePace = score.AdjustedPace.HasValue ? score.AdjustedPace.Value : score.Pace.Value;
                        score.Point =
                            Convert.ToInt32(50*
                                            ((division.MeanPace.Value - scorePace)/
                                             division.StandardDeviationPace.Value));
                    }
                    score.Point += 1000;

                    if (score.Point <= 100)
                    {
                        Cup.UI.ShowWarning(String.Format("Very slow runner: {0}", score.Name));
                        score.Point = 100;
                    }
                }
            }

            #endregion
        }

        public bool Update(Cup cup,
                             string name,
                             string code,
                             DateTime date,
                             string club,
                             string note,
                             string url,
                             string defaultScoreRegexPattern,
                             out string message)
        {
            #region Name

            if (String.IsNullOrEmpty(name))
            {
                message = "Race Name is mandatory";
                return false;
            }

            if ((!String.IsNullOrEmpty(Name)) &&
                (!Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) &&
                cup.RaceList.Any(race => race.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
            {
                message = "Race Name is already used";
                return false;
            }

            #endregion

            #region Code

            if (String.IsNullOrEmpty(code))
            {
                message = "Race Code is mandatory";
                return false;
            }

            if ((!String.IsNullOrEmpty(Code)) &&
                (!Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)))
            {
                foreach (Race race in cup.RaceList)
                {
                    if (race.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                    {
                        message = "Race Code is already used";
                        return false;
                    }
                }
            }

            #endregion

            if ((date < (new DateTime(cup.Year, 1, 1))) || (date >= (new DateTime(cup.Year + 1, 1, 1))))
            {
                message = String.Format("Race date is not in year {0}", cup.Year);
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

            this.Name = name;
            this.Code = code;
            this.Date = date;
            this.Club = club;
            this.Note = note;
            this.Url = url;
            this.ScoreRegexPattern = defaultScoreRegexPattern;

            message = null;
            return true;
        }


        public bool UpdateAgeClassReference(string oldName, string newName, out string message)
        {
            foreach (Course course in this.CourseList)
            {
                if (!course.UpdateAgeClassReference(oldName, newName, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }

        public bool DeleteAgeClassReference(string name, out string message)
        {
            foreach (Course course in this.CourseList)
            {
                if (!course.DeleteAgeClassReference(name, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }

        public bool UpdateClubReference(string oldName, string newName, out string message)
        {
            if ((this.Club != null) && (this.Club.Equals(oldName)))
            {
                this.Club = newName;
            }

            foreach (Course course in this.CourseList)
            {
                if (!course.UpdateClubReference(oldName, newName, out message))
                {
                    return false;
                }
            }
            
            message = String.Empty;
            return true;
        }

        public bool DeleteClubReference(string name, out string message)
        {
            if ((this.Club != null) && (this.Club.Equals(name)))
            {
                message = String.Format("Can not delete Club '{0}', it is the club of race '{1}'", name, this.Name);
                return false;
            }

            foreach (Course course in this.CourseList)
            {
                if (!course.DeleteClubReference(name, out message))
                {
                    return false;
                }
            }
            
            message = String.Empty;
            return true;
        }
        #endregion

        #region Comparer

        public class Comparer : IComparer<Race>
        {
            #region IComparer<Race> Members

            public Int32 Compare(Race a, Race b)
            {
                return a.Date.CompareTo(b.Date);
            }

            #endregion
        }

        #endregion
    }
}