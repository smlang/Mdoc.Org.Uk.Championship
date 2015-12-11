using System;
using System.Windows.Forms;

namespace Mdoc.Org.Uk.Championship.Forms
{
    public partial class GetValueForm : Form
    {
        public string Value { get { return valueTextBox.Text; } }

        public GetValueForm(string prompt, string defaultValue)
        {
            InitializeComponent();

            promptLabel.Text = prompt;
            valueTextBox.Text = defaultValue;
        }

        private void OkayButtonClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
