using System;
using System.IO;
using System.Windows.Forms;
using Mdoc.Org.Uk.Championship.Library;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class CupForm : Form
    {
        private readonly Cup _cup;
        public Cup Cup { get { return _cup; } }

        public CupForm(Cup cup)
        {
            InitializeComponent();

            _cup = cup;
            Initialise();
        }

        private void Initialise()
        {
            if (_cup.Year != 0)
            {
                yearTextBox.Text = _cup.Year.ToString();
            }
            if (_cup.MaximumScores != 0)
            {
                maxTextBox.Text = _cup.MaximumScores.ToString();
            }
        }

        private void ChangeButtonClick(object sender, EventArgs e)
        {
            string message;
            if (!_cup.Update(
                maxTextBox.Text,
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
