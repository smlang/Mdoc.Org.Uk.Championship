using System;
using System.IO;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Forms.Properties;
using Mdoc.Org.Uk.Championship.Library;
using System.Drawing;
using System.Linq;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class Main : Form, IUserInteraction
    {
        private static readonly string _CupFilePathFormat = Settings.Default.DataDirectoryPath + @"\{0}\Cup.xml";
        private Cup _cup;
        private String _cupFilePath;
        private Boolean _unsavedChanges;

        public Main()
        {
            InitializeComponent();

            _cupFilePath = String.Format(_CupFilePathFormat, DateTime.Today.Year);
            // If this year's file does not exist, look for previous years
            // But if none exist, go back to current year
            if (!File.Exists(_cupFilePath))
            {
                foreach (string yearDirectory in Directory.GetDirectories(Settings.Default.DataDirectoryPath).OrderByDescending(d => d))
                {
                    string year = Path.GetFileName(yearDirectory);
                    string alternativeCupFilePath = String.Format(_CupFilePathFormat, year);
                    if (File.Exists(alternativeCupFilePath))
                    {
                        _cupFilePath = alternativeCupFilePath;
                        break;
                    }
                }
            }

            Cup.UI = this;
            if (!Cup.Initialise())
            {
                Close();
            }
            else
            {
                CupLoad();
            }
        }

        #region IUserInteraction
        public Int32 GetChoice(Int32 defaultValue, String prompt, params String[] options)
        {
            GetChoiceForm form = new GetChoiceForm(prompt, options, defaultValue);
            form.ShowDialog();
            return form.Value;
        }

        public String GetValue(String prompt, String defaultValue)
        {
            GetValueForm form = new GetValueForm(prompt, defaultValue);
            form.ShowDialog();
            return form.Value;
        }

        /// <summary>
        /// UI to display a warning message
        /// </summary>
        public void ShowInformation(String message)
        {
            if (debugRichTextBox.TextLength != 0)
            {
                debugRichTextBox.AppendText("\n");
            }
            int start = debugRichTextBox.TextLength;

            debugRichTextBox.AppendText(message);

            debugRichTextBox.Select(start, message.Length);
            debugRichTextBox.SelectionColor = Color.Black;
        }

        /// <summary>
        /// UI to display a warning message
        /// </summary>
        public void ShowWarning(String message)
        {
            if (debugRichTextBox.TextLength != 0)
            {
                debugRichTextBox.AppendText("\n");
            }
            int start = debugRichTextBox.TextLength;

            debugRichTextBox.AppendText(message);

            debugRichTextBox.Select(start, message.Length);
            debugRichTextBox.SelectionColor = Color.Red;

            //debugRichTextBox.Select(debugRichTextBox.TextLength, 0);
        }

        /// <summary>
        /// UI to display an error message
        /// </summary>
        public void ShowError(String message)
        {
            MessageBox.Show(message, Resources.Main_ShowError_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region File Menu Items
        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (CancelIfUnsavedChanges())
                {
                    return;
                }

                openFileDialog.FileName = Path.GetFileName(_cupFilePath);
                openFileDialog.InitialDirectory = Path.GetDirectoryName(_cupFilePath);
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _unsavedChanges = false;
                    _cupFilePath = openFileDialog.FileName;
                    CupLoad();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void CupLoad()
        {
            try
            {
                debugRichTextBox.Text = String.Empty;

                bool isNewFile;
                _cup = Cup.Load(_cupFilePath, out isNewFile);

                _unsavedChanges = isNewFile;

                PopulateRaceMenu();
                PopulateClubMenu();

                if (isNewFile)
                {
                    ShowInformation(String.Format("Created {0}", _cupFilePath));
                }
                else
                {
                    ShowInformation(String.Format("Opened {0}", _cupFilePath));
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void SaveAsCupToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (CancelIfUnsavedChanges())
                {
                    return;
                }

                string nextYearFilePath;
                int nextYear = _cup.Year;
                string oldTemplateFilePath = Path.Combine(Path.GetDirectoryName(_cupFilePath), "Template.html");
                do
                {
                    nextYear++;
                    nextYearFilePath = String.Format(Main._CupFilePathFormat, nextYear);
                } while (File.Exists(nextYearFilePath));

                CupForm form = new CupForm(Cup.NextYearCup(_cup, nextYear));

                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    debugRichTextBox.Text = String.Empty;

                    _cup = form.Cup;
                    _cupFilePath = nextYearFilePath;
                    string newTemplateFilePath = Path.Combine(Path.GetDirectoryName(_cupFilePath), "Template.html");

                    _cup.Save(_cupFilePath);
                    _unsavedChanges = false;
                    ShowInformation(String.Format("Saved next year cup file at {0}", _cupFilePath));

                    File.Copy(oldTemplateFilePath, newTemplateFilePath);
                    ShowInformation(String.Format("Saved next year template file at {0}", newTemplateFilePath));

                    PopulateRaceMenu();
                    PopulateClubMenu();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void EditCupToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                CupForm form = new CupForm(_cup);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                    _cup = form.Cup;
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                debugRichTextBox.Text = String.Empty;
                _cup.Save(_cupFilePath);
                _unsavedChanges = false;
                ShowInformation(String.Format("Saved {0}", _cupFilePath));
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void ExportHtmlToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                debugRichTextBox.Text = String.Empty;
                string templateFilePath = Path.Combine(Path.GetDirectoryName(_cupFilePath), "Template.html");
                string htmlFilePath = Path.Combine(Settings.Default.WebPageDirectoryPath, String.Format("{0}.html", _cup.Year));

                _cup.GenerateHtml(templateFilePath, htmlFilePath);
                ShowInformation(String.Format("Exported {0} results web page to {1}", _cup.Year, htmlFilePath));
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (CancelIfUnsavedChanges())
                {
                    return;
                }

                Close();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }
        #endregion

        #region Race Menu Items
        private void AddRaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                RaceForm form = new RaceForm(_cup);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                    _cup.RaceList.Add(form.Race);
                    _cup.RaceList.Sort(new Race.Comparer());
                    PopulateRaceMenu();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void EditRaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Race race = (Race)((ToolStripMenuItem)sender).Tag;
                RaceForm form = new RaceForm(_cup, race);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                    _cup.RaceList.Sort(new Race.Comparer());
                    PopulateRaceMenu();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void DeleteRaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Race race = (Race)((ToolStripMenuItem)sender).Tag;
                DialogResult result = MessageBox.Show(
                    String.Format("Are you sure, you want to delete {0} race?", race.Name),
                    Resources.Main_DeleteRaceToolStripMenuItemClick_ConfirmOption,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (result == DialogResult.OK)
                {
                    _unsavedChanges = true;
                    _cup.RaceList.Remove(race);
                    PopulateRaceMenu();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void DownloadRaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                debugRichTextBox.Text = String.Empty;
                _unsavedChanges = true;
                Race race = (Race)((ToolStripMenuItem)sender).Tag;
                string raceFilePath = Path.ChangeExtension(Path.Combine(Path.GetDirectoryName(_cupFilePath), race.Code), "txt");
                race.Download(_cup, raceFilePath);
                ShowInformation(String.Format("Downloaded results for {0}", race.Code));
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void ImportRaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                debugRichTextBox.Text = String.Empty;
                _unsavedChanges = true;
                Race race = (Race)((ToolStripMenuItem)sender).Tag;
                string raceFilePath = Path.ChangeExtension(Path.Combine(Path.GetDirectoryName(_cupFilePath), race.Code), "txt");
                race.Import(_cup, raceFilePath);
                ShowInformation(String.Format("Imported results from {0}", race.Code));
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void FindCompetitorsRaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                debugRichTextBox.Text = String.Empty;
                _unsavedChanges = true;
                Race race = (Race)((ToolStripMenuItem)sender).Tag;
                race.FindCompetitors(_cup);
                ShowInformation(String.Format("Looked for new competitors in {0}", race.Code));
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void CalculateScoresRaceToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                debugRichTextBox.Text = String.Empty;
                _unsavedChanges = true;
                Race race = (Race)((ToolStripMenuItem)sender).Tag;
                race.CalculateScore(_cup);
                ShowInformation(String.Format("Calculated scores for {0}", race.Code));
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void PopulateRaceMenu()
        {
            for (int index = raceToolStripMenuItem.DropDownItems.Count - 1; index > 1; index--)
            {
                raceToolStripMenuItem.DropDownItems.RemoveAt(index);
            }

            foreach (Race race in _cup.RaceList)
            {
                ToolStripMenuItem raceMenuItem = new ToolStripMenuItem
                                                     {
                                                         Size = new Size(183, 22),
                                                         Text = race.Name
                                                     };
                raceToolStripMenuItem.DropDownItems.Add(raceMenuItem);

                ToolStripMenuItem editRaceMenuItem = new ToolStripMenuItem
                                                         {
                                                             Size = new Size(183, 22),
                                                             Tag = race,
                                                             Text = Resources.Main_EditRaceMenuItem_Text
                                                         };
                editRaceMenuItem.Click += EditRaceToolStripMenuItemClick;
                raceMenuItem.DropDownItems.Add(editRaceMenuItem);

                ToolStripMenuItem deleteRaceMenuItem = new ToolStripMenuItem
                                                           {
                                                               Size = new Size(183, 22),
                                                               Tag = race,
                                                               Text = Resources.Main_DeleteRaceMenuItem_Text
                                                           };
                deleteRaceMenuItem.Click += DeleteRaceToolStripMenuItemClick;
                raceMenuItem.DropDownItems.Add(deleteRaceMenuItem);

                ToolStripSeparator separatorMenuItem = new ToolStripSeparator();
                raceMenuItem.DropDownItems.Add(separatorMenuItem);

                ToolStripMenuItem downloadRaceMenuItem = new ToolStripMenuItem
                                                             {
                                                                 Size = new Size(183, 22),
                                                                 Tag = race,
                                                                 Text = Resources.Main_DownloadRaceMenuItem_Text
                                                             };
                downloadRaceMenuItem.Click += DownloadRaceToolStripMenuItemClick;
                raceMenuItem.DropDownItems.Add(downloadRaceMenuItem);

                ToolStripMenuItem importRaceMenuItem = new ToolStripMenuItem
                                                           {
                                                               Size = new Size(183, 22),
                                                               Tag = race,
                                                               Text = Resources.Main_ImportRaceMenuItem_Text
                                                           };
                importRaceMenuItem.Click += ImportRaceToolStripMenuItemClick;
                raceMenuItem.DropDownItems.Add(importRaceMenuItem);

                ToolStripMenuItem findCompetitorsRaceMenuItem = new ToolStripMenuItem
                                                                    {
                                                                        Size = new Size(183, 22),
                                                                        Tag = race,
                                                                        Text = Resources.Main_FindCompetitorsRaceMenuItem_Text
                                                                    };
                findCompetitorsRaceMenuItem.Click += FindCompetitorsRaceToolStripMenuItemClick;
                raceMenuItem.DropDownItems.Add(findCompetitorsRaceMenuItem);

                ToolStripMenuItem calculateScoresRaceMenuItem = new ToolStripMenuItem
                                                                    {
                                                                        Size = new Size(183, 22),
                                                                        Tag = race,
                                                                        Text = Resources.Main_CalculateScoreRaceMenuItem_Text
                                                                    };
                calculateScoresRaceMenuItem.Click += CalculateScoresRaceToolStripMenuItemClick;
                raceMenuItem.DropDownItems.Add(calculateScoresRaceMenuItem);
            }
        }
        #endregion

        #region Club Menu Items
        private void AddClubToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                ClubForm form = new ClubForm(_cup);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                    _cup.ClubList.Add(form.Club);
                    _cup.ClubList.Sort(new Club.Comparer());
                    PopulateClubMenu();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void EditClubToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Club club = (Club)((ToolStripMenuItem)sender).Tag;
                ClubForm form = new ClubForm(_cup, club);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                    PopulateClubMenu();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void DeleteClubToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                Club club = (Club)((ToolStripMenuItem)sender).Tag;
                DialogResult result = MessageBox.Show(
                    String.Format("Are you sure, you want to delete {0} club?", club.Name),
                    Resources.Main_DeleteClubToolStripMenuItemClick_ConfirmCaption,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (result == DialogResult.OK)
                {
                    string message;
                    if (!club.Delete(_cup, out message))
                    {
                        Cup.UI.ShowError(message);
                        return;
                    }

                    _unsavedChanges = true;
                    PopulateClubMenu();
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void PopulateClubMenu()
        {
            for (int index = clubToolStripMenuItem.DropDownItems.Count - 1; index > 1; index--)
            {
                clubToolStripMenuItem.DropDownItems.RemoveAt(index);
            }

            foreach (Club club in _cup.ClubList)
            {
                ToolStripMenuItem clubMenuItem = new ToolStripMenuItem
                                                     {
                                                         Size = new Size(183, 22),
                                                         Text = club.Name
                                                     };
                clubToolStripMenuItem.DropDownItems.Add(clubMenuItem);

                ToolStripMenuItem editClubMenuItem = new ToolStripMenuItem
                                                         {
                                                             Size = new Size(183, 22),
                                                             Tag = club,
                                                             Text = Resources.Main_EditClubMenuItem_Text
                                                         };
                editClubMenuItem.Click += EditClubToolStripMenuItemClick;
                clubMenuItem.DropDownItems.Add(editClubMenuItem);

                ToolStripMenuItem deleteClubMenuItem = new ToolStripMenuItem
                                                           {
                                                               Size = new Size(183, 22),
                                                               Tag = club,
                                                               Text = Resources.Main_DeleteClubMenuItem_Text
                                                           };
                deleteClubMenuItem.Click += DeleteClubToolStripMenuItemClick;
                clubMenuItem.DropDownItems.Add(deleteClubMenuItem);
            }
        }
        #endregion

        #region Miscellaneous Menu Item
        private void AgeClassToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                AgeClassListForm form = new AgeClassListForm(_cup);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void CourseToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                CourseListForm form = new CourseListForm(_cup);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void CompetitorsToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                CompetitorListForm form = new CompetitorListForm(_cup);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void SkipResultLinesToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                SkipResultLineForm form = new SkipResultLineForm(_cup);
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    _unsavedChanges = true;
                }
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }
        #endregion

        private bool CancelIfUnsavedChanges()
        {
            if (_unsavedChanges)
            {
                return (MessageBox.Show(Resources.Main_CancelIfUnsavedChanges_Question, Resources.Main_CancelIfUnsavedChanges_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No);
            }
            return false;
        }

        private void DisplayException(Exception e)
        {
            try
            {
                MessageBox.Show(e.Message, Resources.Main_DisplayException_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Exception innerException = e;
                while (innerException != null)
                {
                    ShowError(innerException.Message);
                    ShowError(innerException.StackTrace);
                    innerException = innerException.InnerException;
                }
            }
            catch
            {
            }
        }
    }
}
