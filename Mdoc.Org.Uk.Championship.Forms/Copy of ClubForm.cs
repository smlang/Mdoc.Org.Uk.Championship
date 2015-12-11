using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class ClubForm : Form
    {
        private Cup cup;
        private Club club;
        public Club Club { get { return this.club; } }

        public ClubForm(Cup cup)
        {
            InitializeComponent();

            this.cup = cup;
            this.club = new Club();
            this.changeButton.Text = "Add";

            this.Initialise();
        }

        public ClubForm(Cup cup, Club club)
        {
            InitializeComponent();

            this.cup = cup;
            this.club = club;
            this.changeButton.Text = "Edit";

            this.Initialise();
        }

        private void Initialise()
        {
            this.nameTextBox.Text = this.club.Name;
            this.urlTextBox.Text = this.club.Url;
            this.inCupCheckBox.Checked = this.club.InCup;
            this.defaultScoreRegexPatternTextBox.Text = this.club.DefaultScoreRegexPattern;
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            string message;
            if (!club.Validate(
                this.cup,
                this.nameTextBox.Text,
                this.urlTextBox.Text,
                this.defaultScoreRegexPatternTextBox.Text,
                out message))
            {
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK);
                return;
            }

            this.club.Name = this.nameTextBox.Text;
            this.club.Url = this.urlTextBox.Text;
            this.club.InCup = this.inCupCheckBox.Checked;
            this.club.DefaultScoreRegexPattern = this.defaultScoreRegexPatternTextBox.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
