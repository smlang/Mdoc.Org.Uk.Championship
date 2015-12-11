using System;
using System.Windows.Forms;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class GetChoiceForm : Form
    {
        public int Value { get { return choiceListBox.SelectedIndex + 1; } }

        public GetChoiceForm(string prompt, string[] options, int defaultValue)
        {
            InitializeComponent();

            promptLabel.Text = prompt;
            choiceListBox.Items.AddRange(options);
            choiceListBox.SelectedIndex = defaultValue - 1;
        }

        private void OkayButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
