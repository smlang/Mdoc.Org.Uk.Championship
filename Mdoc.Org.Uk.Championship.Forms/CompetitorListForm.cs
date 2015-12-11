using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class CompetitorListForm : Form
    {
        private readonly Cup _cup;
        private List<Competitor> _edittedCompetitorList;

        public CompetitorListForm(Cup cup)
        {
            InitializeComponent();

            _cup = cup;

            _edittedCompetitorList = new List<Competitor>();
            _edittedCompetitorList.AddRange(_cup.CompetitorList);

            searchTextBox_TextChanged(null, null);
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(searchTextBox.Text, RegexOptions.IgnoreCase);

            competitorListBox.Items.Clear();
            foreach (Competitor competitor in _edittedCompetitorList)
            {
                if (regex.IsMatch(competitor.Name))
                {
                    competitorListBox.Items.Add(competitor);
                }
                else
                {
                    foreach (string alternative in competitor.AlternativeNameList)
                    {
                        if (regex.IsMatch(alternative))
                        {
                            competitorListBox.Items.Add(competitor);
                            break;
                        }
                    }
                }
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            _edittedCompetitorList.Sort(new Competitor.Comparer());
            _cup.CompetitorList = _edittedCompetitorList;

            DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompetitorForm form = new CompetitorForm(_cup, null);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                Competitor competitor = new Competitor();
                competitor.AlternativeClubList = form.AlternativeClubList;
                competitor.AlternativeNameList = form.AlternativeNameList;
                competitor.Attribute = form.Attribute;
                competitor.Club = form.PrimaryClub;
                competitor.Name = form.PrimaryName;

                competitorListBox.Items.Add(competitor);
                competitorListBox.SelectedItem = competitor;

                _edittedCompetitorList.Add(competitor);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (competitorListBox.SelectedItem != null)
            {
                Competitor competitor = (Competitor)competitorListBox.SelectedItem;
                CompetitorForm form = new CompetitorForm(_cup, competitor);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    competitor.AlternativeClubList = form.AlternativeClubList;
                    competitor.AlternativeNameList = form.AlternativeNameList;
                    competitor.Attribute = form.Attribute;
                    competitor.Club  = form.PrimaryClub;
                    competitor.Name = form.PrimaryName;
                }
            }
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (competitorListBox.SelectedItem != null)
            {
                foreach (Competitor competitor in competitorListBox.SelectedItems)
                {
                    _edittedCompetitorList.Remove(competitor);
                }
                searchTextBox_TextChanged(null, null);
            }
        }

    }
}
