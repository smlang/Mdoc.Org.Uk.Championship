using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class SkipResultLineForm : Form
    {
        private const int RegexColumn = 0;

        private readonly Cup _cup;

        public SkipResultLineForm(Cup cup)
        {
            InitializeComponent();

            _cup = cup;

            foreach (String skipResultLine in cup.SkipResultLineList)
            {
                skipResultLineDataGridView.Rows.Add(RegExUtility.ToTextBox(skipResultLine));
            }
        }

        private void ChangeButtonClick(object sender, EventArgs e)
        {
            List<String> newSkipResultLineList = new List<String>();
            foreach (DataGridViewRow row in skipResultLineDataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    String skipResultLine = RegExUtility.FromTextBox(row.Cells[RegexColumn].Value.ToString());
                    newSkipResultLineList.Add(skipResultLine);
                }
            }

            _cup.SkipResultLineList = newSkipResultLineList;

            DialogResult = DialogResult.OK;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #region Validate
        private void SkipResultLineDataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = skipResultLineDataGridView.Rows[e.RowIndex];
            row.ErrorText = String.Empty;

            switch (e.ColumnIndex)
            {
                case RegexColumn:
                    string regexPattern = e.FormattedValue.ToString();
                    try
                    {
                        new System.Text.RegularExpressions.Regex(regexPattern);
                    }
                    catch
                    {
                        row.ErrorText = String.Format("Regular Expression '{0}' in row {1} is not valid", regexPattern, e.RowIndex + 1);
                        e.Cancel = true;
                        return;
                    }

                    if (skipResultLineDataGridView != null)
                    {
                        if ((from DataGridViewRow otherRow in skipResultLineDataGridView.Rows
                             where otherRow != row
                             select (string) otherRow.Cells[e.ColumnIndex].Value).Any(
                                 otherRegexPattern => regexPattern.Equals(otherRegexPattern, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            row.ErrorText = "Regular Expression is not unique";
                            e.Cancel = true;
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}
