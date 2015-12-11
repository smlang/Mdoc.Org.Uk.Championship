using System;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Forms.Properties;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class RaceForm : Form
    {
        private readonly Cup _cup;
        private readonly Race _race;
        public Race Race { get { return _race; } }

        public RaceForm(Cup cup)
        {
            InitializeComponent();

            _cup = cup;
            _race = new Race { Date = new DateTime(cup.Year, 1, 1), Club = _cup.ClubList[0].Name };

            changeButton.Text = Resources.RaceForm_ChangeButton_Add;

            Initialise(cup);
        }

        public RaceForm(Cup cup, Race race)
        {
            InitializeComponent();

            _cup = cup;
            _race = race;
            changeButton.Text = Resources.RaceForm_ChangeButton_Edit;

            Initialise(cup);
        }

        private void Initialise(Cup cup)
        {
            PopulateClubMenu(_race.Club);

            nameTextBox.Text = _race.Name;
            codeTextBox.Text = _race.Code;
            dateTimePicker.MinDate = new DateTime(cup.Year, 1, 1);
            dateTimePicker.MaxDate = new DateTime(cup.Year, 12, 31);
            dateTimePicker.Value = _race.Date;
            noteTextBox.Text = _race.Note;
            urlTextBox.Text = _race.Url;
            scoreRegexPatternTextBox.Text = RegExUtility.ToTextBox(_race.ScoreRegexPattern);
        }

        private void PopulateClubMenu(string selectedClubName)
        {
            clubComboBox.Items.Clear();
            foreach (Club club in _cup.ClubList)
            {
                clubComboBox.Items.Add(club.Name);
                if (club.Name.Equals(selectedClubName, StringComparison.InvariantCultureIgnoreCase))
                {
                    clubComboBox.SelectedItem = club.Name;
                }
            }
        }

        private void ChangeButtonClick(object sender, EventArgs e)
        {
            string message;
            if (!_race.Update(
                _cup,
                nameTextBox.Text,
                codeTextBox.Text,
                dateTimePicker.Value,
                (string)clubComboBox.SelectedItem,
                noteTextBox.Text,
                urlTextBox.Text,
                RegExUtility.FromTextBox(scoreRegexPatternTextBox.Text),
                out message))
            {
                Cup.UI.ShowError(message);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ImportButtonClick(object sender, EventArgs e)
        {
            foreach (Club club in _cup.ClubList)
            {
                if (club.Name.Equals(_race.Club, StringComparison.InvariantCultureIgnoreCase))
                {
                    scoreRegexPatternTextBox.Text = RegExUtility.ToTextBox(club.DefaultScoreRegexPattern);
                }
            }
        }

        private void ExportButtonClick(object sender, EventArgs e)
        {
            foreach (Club club in _cup.ClubList)
            {
                if (club.Name.Equals(_race.Club, StringComparison.InvariantCultureIgnoreCase))
                {
                    club.DefaultScoreRegexPattern = RegExUtility.FromTextBox(scoreRegexPatternTextBox.Text);
                }
            }
        }

        private void AddClubButtonClick(object sender, EventArgs e)
        {
            ClubForm form = new ClubForm(_cup);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                _cup.ClubList.Add(form.Club);
                _cup.ClubList.Sort(new Club.Comparer());
                PopulateClubMenu(form.Club.Name);
            }
        }

    }
}
