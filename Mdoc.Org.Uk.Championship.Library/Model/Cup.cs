using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml.Serialization;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Cup
    {
        private static Regex _nameRegex;

        #region Attributes

        [XmlIgnore]
        public static IUserInteraction UI { get; set; }

        public Int32 Year { get; set; }
        public Int32 MaximumScores { get; set; }

        public List<Club> ClubList { get; internal set; }
        public List<CourseDefinition> CourseDefinitionList { get; set; }
        public List<AgeClass> AgeClassList { get; set; }
        public List<String> SkipResultLineList { get; set; }
        public List<Competitor> CompetitorList { get; set; }
        public List<Race> RaceList { get; set; }

        #endregion

        #region Constructor

        public Cup()
        {
            AgeClassList = new List<AgeClass>();
            ClubList = new List<Club>();
            CompetitorList = new List<Competitor>();
            CourseDefinitionList = new List<CourseDefinition>();
            MaximumScores = 6;
            RaceList = new List<Race>();
            SkipResultLineList = new List<String>();
            Year = DateTime.Today.Year;
        }

        #endregion

        public static Cup NextYearCup(Cup currentCup, int nextYear)
        {
            Cup nextYearCup = new Cup();
            nextYearCup.AgeClassList = currentCup.AgeClassList;
            nextYearCup.ClubList = currentCup.ClubList;
            nextYearCup.CompetitorList = currentCup.CompetitorList;
            nextYearCup.CourseDefinitionList = currentCup.CourseDefinitionList;
            nextYearCup.MaximumScores = currentCup.MaximumScores;
            nextYearCup.RaceList = new List<Race>();
            nextYearCup.SkipResultLineList = currentCup.SkipResultLineList;
            nextYearCup.Year = nextYear;
            return nextYearCup;
        }

        #region Methods

        public bool Update(string maxScores, out string message)
        {
            if (String.IsNullOrEmpty(maxScores))
            {
                message = "Maximum Races per Competitor is mandatory";
                return false;
            }

            int maxScoresResult;
            if (!Int32.TryParse(maxScores, out maxScoresResult))
            {
                message = "Maximum Races per Competitor is not valid";
                return false;
            }

            MaximumScores = maxScoresResult;

            message = null;
            return true;
        }

        public Race GetRace()
        {
            // Get list of possible races to process
            List<String> raceList = new List<String>();
            Dictionary<int, Race> raceDictionary = new Dictionary<int, Race>();

            int raceCount = 0;
            foreach (Race race in RaceList)
            {
                raceCount++;
                raceList.Add(race.Code);
                raceDictionary.Add(raceCount, race);
            }

            // Ask user to select a race to process
            Int32 choice = UI.GetChoice(
                raceCount,
                "Please select a race to process",
                raceList.ToArray()
                );

            return (raceDictionary[choice]);
        }

        public void Save(string filePath)
        {
            if (!System.IO.Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                System.IO.Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            XmlSerializer serializer = new XmlSerializer(GetType());
            using (StreamWriter reader = new StreamWriter(filePath))
            {
                try
                {
                    serializer.Serialize(reader, this);
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        public void GenerateHtml(string templateFilePath, string htmlFilePath)
        {
            #region Statistics

            int overallTotal = 0;
            int overallRuns = 0;
            SortedDictionary<string, int> ageClassTotalScore = new SortedDictionary<string, int>();
            Dictionary<string, int> ageClassTotalRuns = new Dictionary<string, int>();

            #endregion

            #region Handle Club

            List<String> clubList = (from club in ClubList where club.InCup select club.Name).ToList();

            #endregion

            #region Calculate Scores

            #region Iterate through all races in championship

            int runs = 0;
            Dictionary<String, CupDivision> divisionList = new Dictionary<String, CupDivision>();
            foreach (Race race in RaceList)
            {
                if (race.DivisionList == null)
                {
                    continue;
                }
                Division raceDivision =
                    race.DivisionList.Find(d => (d.Name == Settings.Default.OpenTechnicalDifficulty));
                if (raceDivision == null)
                {
                    continue;
                }
                runs++;
                foreach (Score score in race.CourseList.SelectMany(course => course.ScoreList))
                {
                    #region Statistics

                    if ((score.Division == Settings.Default.OpenTechnicalDifficulty) &&
                        (score.Attribute.AgeClass(this) != null) && (score.Point.HasValue) && (score.Point.Value > 0))
                    {
                        int total = 0;
                        int entries = 0;
                        if (ageClassTotalScore.ContainsKey(score.Attribute.AgeClass(this)))
                        {
                            total = ageClassTotalScore[score.Attribute.AgeClass(this)];
                            entries = ageClassTotalRuns[score.Attribute.AgeClass(this)];
                        }

                        overallTotal += score.Point.Value;
                        overallRuns++;

                        ageClassTotalScore[score.Attribute.AgeClass(this)] = total + score.Point.Value;
                        ageClassTotalRuns[score.Attribute.AgeClass(this)] = entries + 1;
                    }

                    #endregion

                    #region Match competitor in race against competitor in championship 

                    foreach (Competitor competitor in CompetitorList)
                    {
                        if (competitor.Matches(score.Name, score.Club))
                        {
                            // member of club in competition
                            bool memberOfClubInChampionship = false;
                            foreach (string club in clubList)
                            {
                                if (club.Equals(competitor.Club, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    memberOfClubInChampionship = true;
                                }
                                else
                                {
                                    if (competitor.AlternativeClubList.Any(alternativeClub => club.Equals(alternativeClub, StringComparison.InvariantCultureIgnoreCase)))
                                    {
                                        memberOfClubInChampionship = true;
                                    }
                                }
                                if (memberOfClubInChampionship)
                                {
                                    break;
                                }
                            }

                            if ((score.Division != null) && memberOfClubInChampionship)
                            {
                                #region Handle Division

                                CupDivision division;
                                if (divisionList.ContainsKey(score.Division))
                                {
                                    division = divisionList[score.Division];
                                }
                                else
                                {
                                    division = new CupDivision();
                                    divisionList.Add(score.Division, division);
                                }

                                #endregion

                                #region Handle Competitor

                                DivisionCompetitor divisionCompetitor;
                                string key = String.Format("{0}|{1}", competitor.Name, competitor.Club);
                                if (division.CompetitorList.ContainsKey(key))
                                {
                                    divisionCompetitor = division.CompetitorList[key];
                                }
                                else
                                {
                                    divisionCompetitor = new DivisionCompetitor(competitor);
                                    division.CompetitorList.Add(key, divisionCompetitor);
                                }
                                divisionCompetitor.AddRace(race.Code, score);

                                #endregion
                            }
                        }
                    }

                    #endregion
                }
            }

            #endregion

            #region Statistics

            if (overallRuns != 0)
            {
                double overallAverage = ((double)overallTotal)/overallRuns;
                foreach (string ageClass in ageClassTotalRuns.Keys)
                {
                    double ageClassAverage = ((double)ageClassTotalScore[ageClass])/ageClassTotalRuns[ageClass];
                    double percentage = (100*ageClassAverage/overallAverage) - 100;

                    UI.ShowInformation(string.Format("{0}: {1} ({2}%)", ageClass, ageClassAverage, percentage));
                }
                UI.ShowInformation(string.Format("Total: {0}", overallAverage));
            }

            #endregion

            #region Total Scores

            foreach (DivisionCompetitor divisionCompetitor in
                divisionList.Values.SelectMany(division => division.CompetitorList.Values))
            {
                divisionCompetitor.CalculateTotalPoint(MaximumScores);
            }

            #endregion

            #endregion

            #region Read HTML Template from File
            if (!File.Exists(templateFilePath))
            {
                UI.ShowError(String.Format("HTML Template '{0}' does not exist.", templateFilePath));
                return;
            }

            StreamReader reader = new StreamReader(templateFilePath);
            string htmlSource = reader.ReadToEnd();
            reader.Close();

            #endregion

            #region Event HTML

            StringWriter writer = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);

            if (RaceList.Count == 0)
            {
                htmlWriter.Write("To be selected");
            }
            else
            {
                #region Event Table

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "event");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Border, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);

                #region Event Table Header

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Thead);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Code");
                htmlWriter.RenderEndTag();
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Date");
                htmlWriter.RenderEndTag();
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Club");
                htmlWriter.RenderEndTag();
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Event");
                htmlWriter.RenderEndTag();
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Notes");
                htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();

                #endregion

                #region Event Table Body

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);
                foreach (Race race in RaceList)
                {
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                    htmlWriter.Write(race.Code);
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    htmlWriter.Write(race.Date.ToString("d MMM"));
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    bool foundUrl = false;
                    if (!(String.IsNullOrEmpty(race.Club)))
                    {
                        foreach (Club club in ClubList)
                        {
                            if ((club.Name == race.Club) && (!String.IsNullOrEmpty(club.Url)))
                            {
                                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Href, club.Url);
                                htmlWriter.RenderBeginTag(HtmlTextWriterTag.A);
                                foundUrl = true;
                                break;
                            }
                        }
                    }
                    htmlWriter.Write(race.Club);
                    if (foundUrl)
                    {
                        htmlWriter.RenderEndTag();
                    }
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!String.IsNullOrEmpty(race.Url))
                    {
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Href, race.Url);
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.A);
                    }
                    htmlWriter.Write(race.Name);
                    if (!String.IsNullOrEmpty(race.Url))
                    {
                        htmlWriter.RenderEndTag();
                    }
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                    htmlWriter.Write(race.Note);
                    htmlWriter.RenderEndTag();
                    htmlWriter.RenderEndTag();
                }
                htmlWriter.RenderEndTag();

                #endregion

                htmlWriter.RenderEndTag();

                #endregion
            }

            htmlWriter.Close();
            string htmlEventSource = writer.ToString();
            htmlSource = htmlSource.Replace("<!-- EVENTS HERE -->", htmlEventSource);

            #endregion

            #region Results HTML

            writer = new StringWriter();
            htmlWriter = new HtmlTextWriter(writer);

            if (runs == 0)
            {
                htmlWriter.Write("There aren't any results");
            }
            else
            {
                if (MaximumScores == 1)
                {
                    htmlWriter.Write("A competitor's total includes his/her best event");
                }
                else
                {
                    htmlWriter.Write(String.Format("A competitor's total includes his/her best {0} events",
                                                   MaximumScores));
                }

                #region Results Table

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "score");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Border, "2");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Table);

                #region Results Table Header
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Thead);

                #region First Line

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "sdivision");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Division");
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "sposition");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("#");
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "sname");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Name");
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "sh1race");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Colspan, runs.ToString());
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Events");
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "saverage");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Avgr");
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "stotal");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, "2");
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                htmlWriter.Write("Total");
                htmlWriter.RenderEndTag();

                htmlWriter.RenderEndTag();

                #endregion

                #region Second Line

                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                foreach (Race race in RaceList)
                {
                    Division raceDivision =
                        race.DivisionList.Find(d => (d.Name == Settings.Default.OpenTechnicalDifficulty));
                    if (raceDivision == null)
                    {
                        continue;
                    }

                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "sh2race srace");
                    if (raceDivision.MeanPace.HasValue)
                    {
                        Double meanAdjustedPace = Math.Round(raceDivision.MeanPace.Value / 60, 1,
                                                               MidpointRounding.AwayFromZero);
                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Title,
                                                String.Format("{0} {1} - Mean Adjusted Pace {2} m/km", race.Club,
                                                              race.Name, meanAdjustedPace));
                    }
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);
                    htmlWriter.Write(race.Code);
                    htmlWriter.RenderEndTag();
                }
                htmlWriter.RenderEndTag();

                #endregion

                htmlWriter.RenderEndTag();
                #endregion

                #region Results Table Body
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tbody);

                bool isFirstDivision = true;
                foreach (String divisionName in divisionList.Keys.OrderBy(d => d))
                {
                    CupDivision division = divisionList[divisionName];

                    #region Division Sub-Table

                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);

                    string topClass = isFirstDivision ? "slevel1" : "slevel2";
                    isFirstDivision = false;

                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, String.Format("sdivision {0}", topClass));
                    htmlWriter.AddAttribute(HtmlTextWriterAttribute.Rowspan, division.CompetitorList.Count.ToString());
                    htmlWriter.RenderBeginTag(HtmlTextWriterTag.Th);

                    #region Division Name
                    if (divisionName.Equals(Settings.Default.OpenTechnicalDifficulty))
                    {
                        htmlWriter.Write("Open");
                    }
                    else
                    {
                        string br = HtmlTextWriterTag.Br.ToString();
                        htmlWriter.Write("Junior");
                        htmlWriter.WriteFullBeginTag(br);

                        char[] divisionElements = divisionName.ToCharArray();
                        switch (divisionElements[1])
                        {
                            case 'M':
                                htmlWriter.Write("Men");
                                break;
                            default:
                                htmlWriter.Write("Women");
                                break;
                        }
                        htmlWriter.WriteFullBeginTag(br);

                        switch (divisionElements[0])
                        {
                            case '1':
                                htmlWriter.Write("White");
                                break;
                            case '2':
                                htmlWriter.Write("Yellow");
                                break;
                            case '3':
                                htmlWriter.Write("Orange");
                                break;
                            default:
                                htmlWriter.Write("Light Green");
                                break;
                        }
                    }
                    #endregion

                    htmlWriter.RenderEndTag();

                    #endregion

                    bool isFirstCompetitor = true;
                    int lastPoint = Int32.MaxValue;
                    int count = 0;

                    #region Competitor
                    foreach (
                        DivisionCompetitor divisionCompetitor in
                            division.CompetitorList.Values.OrderByDescending(c => c.TotalPoint))
                    {
                        if (!isFirstCompetitor)
                        {
                            topClass = "slevel3";
                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Tr);
                        }
                        isFirstCompetitor = false;

                        #region Position

                        count++;
                        string positionStr;
                        if (lastPoint != divisionCompetitor.TotalPoint)
                        {
                            lastPoint = divisionCompetitor.TotalPoint;
                            positionStr = count.ToString();
                        }
                        else
                        {
                            positionStr = "=";
                        }

                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class,
                                                String.Format("sposition {0}", topClass));
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write(positionStr);
                        htmlWriter.RenderEndTag();

                        #endregion

                        #region Name

                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, String.Format("sname {0}", topClass));
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        if (divisionName == Settings.Default.OpenTechnicalDifficulty)
                        {
                            htmlWriter.Write(String.Format("{0} ({1})", divisionCompetitor.Competitor.Name,
                                                           divisionCompetitor.Competitor.Attribute.AgeClass(this)));
                        }
                        else
                        {
                            htmlWriter.Write(divisionCompetitor.Competitor.Name);
                        }
                        htmlWriter.RenderEndTag();

                        #endregion

                        #region Individual Score Cell

                        foreach (Race race in RaceList)
                        {
                            Division raceDivision =
                                race.DivisionList.Find(d => (d.Name == Settings.Default.OpenTechnicalDifficulty));
                            if (raceDivision == null)
                            {
                                continue;
                            }

                            string cellText;
                            string toolTip = null;
                            if (divisionCompetitor.RacePointList.ContainsKey(race.Code))
                            {
                                Score score = divisionCompetitor.RacePointList[race.Code];
                                cellText = score.Text;
                                toolTip = score.ToolTip;
                            }
                            else
                            {
                                cellText = String.Empty;
                            }

                            htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class,
                                                    String.Format("snumber srace {0}", topClass));
                            if (toolTip != null)
                            {
                                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Title, toolTip);
                            }
                            htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                            htmlWriter.Write(cellText);
                            htmlWriter.RenderEndTag();
                        }

                        #endregion

                        #region Average Score Cell

                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class,
                                                String.Format("snumber saverage {0}", topClass));
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write(divisionCompetitor.AveragePoint.ToString());
                        htmlWriter.RenderEndTag();

                        #endregion

                        #region Total Score Cell

                        htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class,
                                                String.Format("snumber stotal {0}", topClass));
                        htmlWriter.RenderBeginTag(HtmlTextWriterTag.Td);
                        htmlWriter.Write(divisionCompetitor.TotalPoint.ToString());
                        htmlWriter.RenderEndTag();

                        #endregion

                        htmlWriter.RenderEndTag();
                    }
                    #endregion
                }
                htmlWriter.RenderEndTag();
                #endregion

                htmlWriter.RenderEndTag();

                #endregion
            }
            htmlWriter.Close();
            string htmlResultSource = writer.ToString();
            htmlSource = htmlSource.Replace("<!-- RESULTS HERE -->", htmlResultSource);

            #endregion

            #region Date HTML

            htmlSource = htmlSource.Replace("#YEAR#", Convert.ToString(Year));
            htmlSource = htmlSource.Replace("#YEAR-1#", Convert.ToString(Year - 1));
            htmlSource = htmlSource.Replace("#YEAR+1#", Convert.ToString(Year + 1));
            htmlSource = htmlSource.Replace("<!-- DATE HERE -->", DateTime.Today.ToString("dd MMM yyyy"));

            #endregion

            #region Expires

            writer = new StringWriter();
            htmlWriter = new HtmlTextWriter(writer);

            DateTime expires = DateTime.MaxValue;
            foreach (Race race in RaceList)
            {
                if ((race.Date > DateTime.Now) && (race.Date < expires))
                {
                    expires = race.Date;
                }
            }

            if (expires != DateTime.MaxValue)
            {
                htmlWriter.AddAttribute("http-equiv", "Expires");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Content, expires.ToString("r"));
                htmlWriter.RenderBeginTag(HtmlTextWriterTag.Meta);
                htmlWriter.RenderEndTag();
            }

            htmlWriter.Close();
            string htmlExpiresSource = writer.ToString();
            htmlSource = htmlSource.Replace("<!-- EXPIRES HERE -->", htmlExpiresSource);

            #endregion

            #region Write HTML to File

            StreamWriter htmlStreamWriter = new StreamWriter(htmlFilePath, false, Encoding.UTF8);
            htmlStreamWriter.Write(htmlSource);
            htmlStreamWriter.Close();

            #endregion
        }

        public static bool Initialise()
        {
            try
            {
                _nameRegex = new Regex(Settings.Default.WordRegex,
                                      RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);

                Race.Initialise();
                Duration.Initialise();

                return true;
            }
            catch (Exception e)
            {
                UI.ShowError(e.Message);

                return false;
            }
        }

        #region Find Competitor with similiar name

        internal Competitor FindCompetitor(Score score)
        {
            // Look a competitor whose primary name shares at least two words with input
            // name.
            foreach (Competitor otherCompetitor in CompetitorList)
            {
                #region Must match club

                bool matchClub = false;
                if ((score.Club.Equals(otherCompetitor.Club, StringComparison.InvariantCultureIgnoreCase)) ||
                    (otherCompetitor.AlternativeClubList.Any(club => score.Club.Equals(club, StringComparison.InvariantCultureIgnoreCase))))
                {
                    matchClub = true;
                }
                if (!matchClub)
                {
                    return null;
                }

                #endregion

                #region Look for near match

                List<String> nameCollection = SplitName(score.Name);
                if ((NearMatch(nameCollection, otherCompetitor.Name)) ||
                    (otherCompetitor.AlternativeNameList.Any(otherName => NearMatch(nameCollection, otherName))))
                {

                    // A possible match has been found, ask user if the match is good
                    Int32 choice = UI.GetChoice(
                        2,
                        String.Format("Is '{0}' an alternative name from '{1}'", otherCompetitor.Name, score.Name),
                        "Yes",
                        "No");


                    if (choice == 1)
                    {
                        otherCompetitor.AlternativeNameList.Add(score.Name);
                        return otherCompetitor;
                    }
                }

                #endregion
            }

            return null;
        }

        // Split the name into a collection of words
        internal static List<String> SplitName(string name)
        {
            return (from Match nameMatch in _nameRegex.Matches(name) select nameMatch.Captures[0].Value).ToList();
        }

        private static bool NearMatch(IEnumerable<String> nameCollection, String otherName)
        {
            #region Are two (or more) words shared

            List<String> otherNameCollection = SplitName(otherName);
            int foundCount = 0;
            foreach (string word in nameCollection)
            {
                bool found = false;
                if (otherNameCollection.Any(otherWord => word == otherWord))
                {
                    foundCount++;
                    if (foundCount == 2)
                    {
                        return true;
                    }
                    found = true;
                }
                if (found)
                {
                    otherNameCollection.Remove(word);
                }
            }

            #endregion

            return false;
        }

        #endregion

        #region Load

        public static Cup Load(string filePath, out bool isNewFile)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(Type.GetType("Mdoc.Org.Uk.Championship.Library.Cup"));
                        Cup cup = (Cup)serializer.Deserialize(reader);
                        isNewFile = false;
                        return cup;
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            else
            {
                Cup cup = new Cup();
                isNewFile = true;
                return cup;
            }
        }

        #endregion

        public bool UpdateAgeClassReference(string oldName, string newName, out string message)
        {
            foreach (CourseDefinition courseDefinition in this.CourseDefinitionList)
            {
                if (!courseDefinition.UpdateAgeClassReference(oldName, newName, out message))
                {
                    return false;
                }
            }

            foreach (Race race in this.RaceList)
            {
                if (!race.UpdateAgeClassReference(oldName, newName, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }

        public bool DeleteAgeClassReference(string name, out string message)
        {
            foreach (CourseDefinition courseDefinition in this.CourseDefinitionList)
            {
                if (!courseDefinition.DeleteAgeClassReference(name, out message))
                {
                    return false;
                }
            }

            foreach (Race race in this.RaceList)
            {
                if (!race.DeleteAgeClassReference(name, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }

        public bool UpdateClubReference(string oldName, string newName, out string message)
        {
            foreach (Competitor competitor in this.CompetitorList)
            {
                if (!competitor.UpdateClubReference(oldName, newName, out message))
                {
                    return false;
                }
            }

            foreach (Race race in this.RaceList)
            {
                if (!race.UpdateClubReference(oldName, newName, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }

        public bool DeleteClubReference(string name, out string message)
        {
            foreach (Competitor competitor in this.CompetitorList)
            {
                if (!competitor.DeleteClubReference(name, out message))
                {
                    return false;
                }
            }

            foreach (Race race in this.RaceList)
            {
                if (!race.DeleteClubReference(name, out message))
                {
                    return false;
                }
            }

            message = String.Empty;
            return true;
        }
        #endregion
    }
}