using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class AgeClassListForm : Form
    {
        private const int NameColumn = 0;
        private const int OriginalNameColumn = 1;
        private const int SexColumn = 2;
        private const int MinAgeColumn = 3;
        private const int MaxAgeColumn = 4;        
        private const int SpeedRatioColumn = 5;
        private const int DistanceRatioColumn = 6;

        private readonly Cup _cup;

        public AgeClassListForm(Cup cup)
        {
            InitializeComponent();

            _cup = cup;

            foreach (AgeClass ageClass in cup.AgeClassList)
            {
                ageClassDataGridView.Rows.Add(ageClass.Name, ageClass.Name, ageClass.Sex.ToString(), ageClass.MinimumAge, ageClass.MaximumAge, ageClass.SpeedRatio.ToString(), ageClass.DistanceRatio.ToString());
            }
        }

        private void ChangeButtonClick(object sender, EventArgs e)
        {
            List<AgeClass> newAgeClassList = new List<AgeClass>();
            foreach (DataGridViewRow row in ageClassDataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    AgeClass ageClass = new AgeClass
                                            {
                                                Name = row.Cells[NameColumn].Value.ToString(),
                                                Sex = row.Cells[SexColumn].Value.Equals("Men") ? Sex.Men : Sex.Women,
                                                MinimumAge = Byte.Parse(row.Cells[MinAgeColumn].Value.ToString()),
                                                MaximumAge = Byte.Parse(row.Cells[MaxAgeColumn].Value.ToString()),
                                                SpeedRatio = Double.Parse(row.Cells[SpeedRatioColumn].Value.ToString()),
                                                DistanceRatio =
                                                    Double.Parse(row.Cells[DistanceRatioColumn].Value.ToString())
                                            };
                    if (row.Cells[OriginalNameColumn].Value != null)
                    {
                        string originalName = row.Cells[OriginalNameColumn].Value.ToString();
                        if (!ageClass.Name.Equals(originalName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            #region Update Age Class References
                            string message;
                            if (!_cup.UpdateAgeClassReference(originalName, ageClass.Name, out message))
                            {
                                Cup.UI.ShowError(message);
                                return;
                            }
                            #endregion
                        }
                    }
                    newAgeClassList.Add(ageClass);
                }
            }

            #region Is any deleted age class in use?
            foreach (AgeClass oldAgeClass in _cup.AgeClassList)
            {
                bool found = false;
                foreach (AgeClass newAgeClass in newAgeClassList)
                {
                    if (oldAgeClass.Name.Equals(newAgeClass.Name))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    string message;
                    if (!_cup.DeleteAgeClassReference(oldAgeClass.Name, out message))
                    {
                        Cup.UI.ShowError(message);
                        return;
                    }
                }
            }
            #endregion

            newAgeClassList.Sort(new AgeClass.Comparer());
            _cup.AgeClassList = newAgeClassList;

            DialogResult = DialogResult.OK;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #region Validate
        private void AgeClassDataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = ageClassDataGridView.Rows[e.RowIndex];
            row.ErrorText = String.Empty;

            string errorText = null;
            switch (e.ColumnIndex)
            {
                case NameColumn:
                    errorText = "Name is not unique";
                    break;
                case MinAgeColumn:
                    errorText = "Minimum Age must be a positive integer";
                    break;
                case MaxAgeColumn:
                    errorText = "Maximum Age must be a positive integer";
                    break;
                case SpeedRatioColumn:
                    errorText = "Speed Ratio is not between 0% and 100%";
                    break;
                case DistanceRatioColumn:
                    errorText = "Distance Ratio is not between 0% and 100%";
                    break;
            }

            switch (e.ColumnIndex)
            {
                case NameColumn:
                    string name = e.FormattedValue.ToString();
                    if (ageClassDataGridView != null)
                    {
                        if ((from DataGridViewRow otherRow in ageClassDataGridView.Rows
                             where otherRow != row
                             select (string) otherRow.Cells[e.ColumnIndex].Value).Any(
                                 otherName => name.Equals(otherName, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            row.ErrorText = errorText;
                            e.Cancel = true;
                        }
                    }
                    break;
                case MinAgeColumn:
                case MaxAgeColumn:
                    Byte ageResult;
                    if (!Byte.TryParse(e.FormattedValue.ToString(), out ageResult))
                    {
                        row.ErrorText = errorText;
                        e.Cancel = true;
                    }
                    break;
                case SpeedRatioColumn:
                case DistanceRatioColumn:
                    Double ratioResult;
                    if (!Double.TryParse(e.FormattedValue.ToString(), out ratioResult))
                    {
                        row.ErrorText = errorText;
                        e.Cancel = true;
                    }
                    else if (ratioResult < 0)
                    {
                        row.ErrorText = errorText;
                        e.Cancel = true;
                    }
                    else if (ratioResult > 100)
                    {
                        row.ErrorText = errorText;
                        e.Cancel = true;
                    }
                    break;
            }
        }

        private void AgeClassDataGridViewRowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow row = ageClassDataGridView.Rows[e.RowIndex];
            if (!row.IsNewRow)
            {
                if (row.Cells[NameColumn].Value == null)
                {
                    row.ErrorText = "Name is missing";
                    e.Cancel = true;
                    return;
                }
                if (row.Cells[SexColumn].Value == null)
                {
                    row.ErrorText = "Sex is missing";
                    e.Cancel = true;
                    return;
                }
                if (row.Cells[MinAgeColumn].Value == null)
                {
                    row.ErrorText = "Minimum Age is missing";
                    e.Cancel = true;
                    return;
                }
                if (row.Cells[MaxAgeColumn].Value == null)
                {
                    row.ErrorText = "Maximum Age is missing";
                    e.Cancel = true;
                    return;
                }
                if (row.Cells[SpeedRatioColumn].Value == null)
                {
                    row.ErrorText = "Speed Ratio is missing";
                    e.Cancel = true;
                    return;
                }
                if (row.Cells[DistanceRatioColumn].Value == null)
                {
                    row.ErrorText = "Distance Ratio is missing";
                    e.Cancel = true;
                    return;
                }

                string sex = row.Cells[SexColumn].Value.ToString();
                Byte minAge = Byte.Parse(row.Cells[MinAgeColumn].Value.ToString());
                Byte maxAge = Byte.Parse(row.Cells[MaxAgeColumn].Value.ToString());

                if (ageClassDataGridView != null)
                {
                    foreach (DataGridViewRow otherRow in ageClassDataGridView.Rows)
                    {
                        if ((!otherRow.IsNewRow) && (otherRow != row))
                        {
                            string otherSex = otherRow.Cells[SexColumn].Value.ToString();
                            if (sex.Equals(otherSex))
                            {
                                Byte otherMinAge = Byte.Parse(otherRow.Cells[MinAgeColumn].Value.ToString());
                                Byte otherMaxAge = Byte.Parse(otherRow.Cells[MaxAgeColumn].Value.ToString());
                                if (((minAge >= otherMinAge) && (minAge <= otherMaxAge)) ||
                                    ((maxAge >= otherMinAge) && (maxAge <= otherMaxAge)))
                                {
                                    e.Cancel = true;
                                    row.ErrorText = String.Format("Age Class is overlapping with {0}",
                                                                  otherRow.Cells[NameColumn].Value);
                                    return;
                                }
                            }
                        }
                    }
                }
                row.ErrorText = String.Empty;
            }
        }
        #endregion
    }
}
