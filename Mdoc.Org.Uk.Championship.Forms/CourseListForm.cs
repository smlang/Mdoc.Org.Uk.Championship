using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class CourseListForm : Form
    {
        private const int RegExColumn = 0;
        private const int TDColumn = 1;
        private const int DefaultAgeClassColumn = 2;

        private readonly Cup _cup;

        public CourseListForm(Cup cup)
        {
            InitializeComponent();

            _cup = cup;

            PopulateAgeClasses();

            foreach (CourseDefinition courseDefinition in cup.CourseDefinitionList)
            {
                courseDefinitionDataGridView.Rows.Add(courseDefinition.Name, courseDefinition.TD, courseDefinition.DefaultAgeClass);
            }
        }

        private void ChangeButtonClick(object sender, EventArgs e)
        {
            List<CourseDefinition> newCourseDefinitionList = new List<CourseDefinition>();
            foreach (DataGridViewRow row in courseDefinitionDataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    CourseDefinition courseDefinition = new CourseDefinition
                                                            {
                                                                Name = row.Cells[RegExColumn].Value.ToString(),
                                                                TD = row.Cells[TDColumn].Value.ToString(),
                                                                DefaultAgeClass =
                                                                    row.Cells[DefaultAgeClassColumn].Value.ToString()
                                                            };
                    newCourseDefinitionList.Add(courseDefinition);
                }
            }

            newCourseDefinitionList.Sort(new CourseDefinition.Comparer());
            _cup.CourseDefinitionList = newCourseDefinitionList;

            DialogResult = DialogResult.OK;
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #region Validate
        private void CourseDefinitionDataGridViewCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewRow row = courseDefinitionDataGridView.Rows[e.RowIndex];
            row.ErrorText = String.Empty;

            if (e.ColumnIndex == RegExColumn)
            {
                try
                {
                    new System.Text.RegularExpressions.Regex(e.FormattedValue.ToString());
                }
                catch
                {
                    row.ErrorText = "Name is not a valid Regular Expression";
                    e.Cancel = true;
                }
            }
        }
        #endregion

        private void PopulateAgeClasses()
        {
            ageClassList.Items.Clear();
            foreach (AgeClass ageClass in _cup.AgeClassList)
            {
                ageClassList.Items.Add(ageClass.Name);
            }
        }

        private void EditAgeClassbuttonClick(object sender, EventArgs e)
        {
            AgeClassListForm form = new AgeClassListForm(_cup);
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                PopulateAgeClasses();
            }
        }
    }
}
