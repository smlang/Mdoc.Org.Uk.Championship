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
    public partial class CompetitorForm : Form
    {        
        private readonly Cup _cup;
        private readonly Competitor _competitor;
        private string _primaryName;
        private string _primaryClub;
        private Mdoc.Org.Uk.Championship.Library.Attribute _attribute;

        internal string PrimaryName { get { return _primaryName; } }
        internal string PrimaryClub { get { return _primaryClub; } }
        internal Mdoc.Org.Uk.Championship.Library.Attribute Attribute { 
            get {
                _attribute.Actual = !this.estimateCheckBox.Checked;
                return _attribute; 
            } 
        }
        internal List<string> AlternativeNameList { 
            get {
                List<string> nameList = new List<string>();
                foreach (CompetitorName name in nameListBox.Items)
                {
                    if (!name.Name.Equals(_primaryName))
                    {
                        nameList.Add(name.Name);
                    }
                }
                return nameList; 
            } 
        }
        internal List<string> AlternativeClubList
        {
            get
            {
                List<string> clubList = new List<string>();
                foreach (CompetitorClub club in clubListBox.Items)
                {
                    if (!club.Club.Equals(_primaryClub))
                    {
                        clubList.Add(club.Club);
                    }
                }
                return clubList;
            }
        }

        public CompetitorForm(Cup cup, Competitor competitor)
        {
            InitializeComponent();

            _cup = cup;

            if (competitor != null)
            {
                _competitor = competitor;
                _attribute = competitor.Attribute;

                _primaryName = _competitor.Name;
                nameListBox.Items.Add(new CompetitorName(_competitor.Name, this));
                foreach (string name in _competitor.AlternativeNameList)
                {
                    nameListBox.Items.Add(new CompetitorName(name, this));
                }

                _primaryClub = _competitor.Club;
                clubListBox.Items.Add(new CompetitorClub(_competitor.Club, this));
                foreach (string club in _competitor.AlternativeClubList)
                {
                    clubListBox.Items.Add(new CompetitorClub(club, this));
                }
            }

            if (_attribute == null)
            {
                _attribute = new Mdoc.Org.Uk.Championship.Library.Attribute();
            }

            sexComboBox.SelectedItem = _attribute.Sex.ToString();
            yobMaskedTextBox.Text = _attribute.YearOfBirth.ToString();
            estimateCheckBox.Checked = (!_attribute.Actual);
            ageClassTextBox.Text = _attribute.AgeClass(_cup);
        }

        private void changeButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

        }

        #region Manage Names
        internal class CompetitorName
        {
            internal string Name { get; set; }
            private CompetitorForm _parent;

            internal CompetitorName(string name, CompetitorForm parent)
            {
                Name = name;
                _parent = parent;
            }

            public override string ToString()
            {
                if (Name.Equals(_parent._primaryName))
                {
                    return Name + " (Primary)";
                }
                else
                {
                    return Name;
                }
            }
        }

        private void addNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetValueForm getValue = new GetValueForm("Add Competitor's Name", String.Empty);
            if (getValue.ShowDialog() == DialogResult.OK)
            {
                string newName = getValue.Value.Trim();
                if (newName != String.Empty)
                {
                    if (nameListBox.Items.Count == 0)
                    {
                        _primaryName = newName;
                    }
                    foreach (CompetitorName others in nameListBox.Items)
                    {
                        if (others.Name.Equals(newName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            MessageBox.Show("The name is already listed", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    nameListBox.Items.Add(new CompetitorName(newName, this));
                }
            }
        }

        private void editNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nameListBox.SelectedItem != null)
            {
                CompetitorName cName = (CompetitorName)nameListBox.SelectedItem;
                GetValueForm getValue = new GetValueForm("Edit Competitor's Name", cName.Name);
                if (getValue.ShowDialog() == DialogResult.OK)
                {
                    string newName = getValue.Value.Trim();
                    if (newName != String.Empty)
                    {
                        foreach (CompetitorName others in nameListBox.Items)
                        {
                            if ((others != cName) && (others.Name.Equals(newName, StringComparison.InvariantCultureIgnoreCase)))
                            {
                                MessageBox.Show("The name is already listed", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        nameListBox.Items.Remove(cName);
                        if (cName.Name.Equals(_primaryName))
                        {
                            _primaryName = newName;
                        }
                        nameListBox.Items.Add(new CompetitorName(newName, this));
                    }
                }
            }
        }

        private void deleteNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CompetitorName> removeList = new List<CompetitorName>();
            foreach (CompetitorName cName in nameListBox.SelectedItems)
            {
                removeList.Add(cName);
                if (cName.Name == _primaryName)
                {
                    _primaryName = null;
                }
            }
            foreach (CompetitorName cName in removeList)
            {
                nameListBox.Items.Remove(cName);
            }

            if (nameListBox.Items.Count != 0)
            {
                CompetitorName other = (CompetitorName)nameListBox.Items[0];
                _primaryName = other.Name;

                nameListBox.Items.Remove(other);
                nameListBox.Items.Add(new CompetitorName(_primaryName, this));
            }
        }

        private void makePrimaryNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((nameListBox.SelectedItem != null) && (((CompetitorName)nameListBox.SelectedItem).Name != _primaryName))
            {
                CompetitorName removeName = null;
                foreach (CompetitorName cName in nameListBox.Items)
                {
                    if (cName.Name == _primaryName)
                    {
                        removeName = cName;
                        break;
                    }
                }
                
                _primaryName = ((CompetitorName)nameListBox.SelectedItem).Name;
                nameListBox.Items.Remove(nameListBox.SelectedItem);
                nameListBox.Items.Add(new CompetitorName(_primaryName, this));

                if (removeName != null)
                {
                    nameListBox.Items.Remove(removeName);
                    nameListBox.Items.Add(new CompetitorName(removeName.Name, this));
                }
            }
        }
        #endregion

        #region Manage Club
        internal class CompetitorClub
        {
            internal string Club { get; set; }
            private CompetitorForm _parent;

            internal CompetitorClub(string club, CompetitorForm parent)
            {
                Club = club;
                _parent = parent;
            }

            public override string ToString()
            {
                if (Club.Equals(_parent._primaryClub))
                {
                    return Club + " (Primary)";
                }
                else
                {
                    return Club;
                }
            }
        }

        private void addClubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetValueForm getValue = new GetValueForm("Add Competitor's Club", String.Empty);
            if (getValue.ShowDialog() == DialogResult.OK)
            {
                string newClub = getValue.Value.Trim();
                if (newClub != String.Empty)
                {
                    if (clubListBox.Items.Count == 0)
                    {
                        _primaryClub = newClub;
                    }
                    foreach (CompetitorClub others in clubListBox.Items)
                    {
                        if (others.Club.Equals(newClub, StringComparison.InvariantCultureIgnoreCase))
                        {
                            MessageBox.Show("The club is already listed", "Duplicate Club", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    clubListBox.Items.Add(new CompetitorClub(newClub, this));
                }
            }
        }

        private void editClubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clubListBox.SelectedItem != null)
            {
                CompetitorClub cClub = (CompetitorClub)clubListBox.SelectedItem;
                GetValueForm getValue = new GetValueForm("Edit Competitor's Club", cClub.Club);
                if (getValue.ShowDialog() == DialogResult.OK)
                {
                    string newClub = getValue.Value.Trim();
                    if (newClub != String.Empty)
                    {
                        foreach (CompetitorClub others in clubListBox.Items)
                        {
                            if ((others != cClub) && (others.Club.Equals(newClub, StringComparison.InvariantCultureIgnoreCase)))
                            {
                                MessageBox.Show("The club is already listed", "Duplicate Club", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        clubListBox.Items.Remove(cClub);
                        if (cClub.Club.Equals(_primaryClub))
                        {
                            _primaryClub = newClub;
                        }
                        clubListBox.Items.Add(new CompetitorClub(newClub, this));
                    }
                }
            }
        }

        private void deleteClubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CompetitorClub> removeList = new List<CompetitorClub>();
            foreach (CompetitorClub cClub in clubListBox.SelectedItems)
            {
                removeList.Add(cClub);
                if (cClub.Club == _primaryClub)
                {
                    _primaryClub = null;
                }
            }
            foreach (CompetitorClub cClub in removeList)
            {
                clubListBox.Items.Remove(cClub);
            }

            if (clubListBox.Items.Count != 0)
            {
                CompetitorClub other = (CompetitorClub)clubListBox.Items[0];
                _primaryClub = other.Club;

                clubListBox.Items.Remove(other);
                clubListBox.Items.Add(new CompetitorClub(_primaryClub, this));
            }
        }

        private void makePrimaryClubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((clubListBox.SelectedItem != null) && (((CompetitorClub)clubListBox.SelectedItem).Club != _primaryClub))
            {
                CompetitorClub removeClub = null;
                foreach (CompetitorClub cClub in clubListBox.Items)
                {
                    if (cClub.Club == _primaryClub)
                    {
                        removeClub = cClub;
                        break;
                    }
                }

                _primaryClub = ((CompetitorClub)clubListBox.SelectedItem).Club;
                clubListBox.Items.Remove(clubListBox.SelectedItem);
                clubListBox.Items.Add(new CompetitorClub(_primaryClub, this));

                if (removeClub != null)
                {
                    clubListBox.Items.Remove(removeClub);
                    clubListBox.Items.Add(new CompetitorClub(removeClub.Club, this));
                }
            }
        }
        #endregion

        private void sexComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            _attribute.Sex = (sexComboBox.Text == Sex.Men.ToString()) ? Sex.Men : Sex.Women;
            ageClassTextBox.Text = _attribute.AgeClass(_cup);
        }

        private void yobMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            _attribute.YearOfBirth = 0;

            Int32 yob;
            if (Int32.TryParse(yobMaskedTextBox.Text, out yob))
            {
                if ((yob <= _cup.Year) && (yob >= _cup.Year - 100))
                {
                    _attribute.YearOfBirth = yob;
                }
            }

            ageClassTextBox.Text = _attribute.AgeClass(_cup);
        }
    }
}
