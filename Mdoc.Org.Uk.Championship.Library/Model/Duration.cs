using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Mdoc.Org.Uk.Championship.Library
{
    [Serializable]
    public class Duration
    {
        private static Regex _durationRegex;

        #region Attributes

        private Int32 _hour;

        private Int32 _minute;

        private Int32 _second;

        public Int32 Hour
        {
            get { return _hour; }
            set
            {
                TotalSeconds -= (_hour*3600);
                _hour = value;
                TotalSeconds += (_hour*3600);
            }
        }

        public Int32 Minute
        {
            get { return _minute; }
            set
            {
                TotalSeconds -= (_minute*60);
                _minute = value;
                TotalSeconds += (_minute*60);
            }
        }

        public Int32 Second
        {
            get { return _second; }
            set
            {
                TotalSeconds -= _second;
                _second = value;
                TotalSeconds += _second;
            }
        }

        [XmlIgnore]
        internal Int32 TotalSeconds { get; set; }

        [XmlIgnore]
        internal Boolean HasValue { get; set; }

        #endregion

        #region Constructor

        internal Duration(string text)
        {
            TotalSeconds = 0;
            Match durationMatch = _durationRegex.Match(text);
            HasValue = durationMatch.Success;
            if (durationMatch.Success)
            {
                foreach (Capture durationCapture in durationMatch.Groups["unit"].Captures)
                {
                    TotalSeconds = (TotalSeconds*60) + Int32.Parse(durationCapture.Value);
                }
            }
            else
            {
                Cup.UI.ShowWarning(String.Format("Duration Unknown: {0}", text));
            }
            _second = TotalSeconds%60;
            _minute = ((TotalSeconds%3600) - _second)/60;
            _hour = (TotalSeconds - (_minute*60) - _second)/3600;
        }

        public Duration()
        {
        }

        #endregion

        #region Methods

        internal static void Initialise()
        {
            _durationRegex = new Regex(Settings.Default.DurationRegex,
                                       RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
        }

        #endregion
    }
}