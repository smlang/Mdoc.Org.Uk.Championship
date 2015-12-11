using System;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Forms.Properties;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class ClubForm : Form
    {
        private readonly Cup _cup;
        private readonly Club _club;
        public Club Club { get { return _club; } }

        public ClubForm(Cup cup)
        {
            InitializeComponent();

            _cup = cup;
            _club = new Club();
            changeButton.Text = Resources.ClubForm_ChangeButton_Add;

            Initialise();
        }

        public ClubForm(Cup cup, Club club)
        {
            InitializeComponent();

            _cup = cup;
            _club = club;
            changeButton.Text = Resources.ClubForm_ChangeButton_Edit;

            Initialise();
        }

        private void Initialise()
        {
            nameTextBox.Text = _club.Name;
            urlTextBox.Text = _club.Url;
            inCupCheckBox.Checked = _club.InCup;
            defaultScoreRegexPatternTextBox.Text = RegExUtility.ToTextBox(_club.DefaultScoreRegexPattern);
        }

        private void ChangeButtonClick(object sender, EventArgs e)
        {
            string message;
            if (!_club.Update(
                _cup,
                nameTextBox.Text,
                urlTextBox.Text,
                inCupCheckBox.Checked,
                RegExUtility.FromTextBox(defaultScoreRegexPatternTextBox.Text),
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
    }
}
