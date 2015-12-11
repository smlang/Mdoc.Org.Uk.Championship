namespace Mdoc.Org.Uk.Championship.Forms
{
    partial class CompetitorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.nameListLabel = new System.Windows.Forms.Label();
            this.nameListBox = new System.Windows.Forms.ListBox();
            this.nameContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makePrimaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clubListLabel = new System.Windows.Forms.Label();
            this.clubListBox = new System.Windows.Forms.ListBox();
            this.sexLabel = new System.Windows.Forms.Label();
            this.yobLabel = new System.Windows.Forms.Label();
            this.ageClassLabel = new System.Windows.Forms.Label();
            this.sexComboBox = new System.Windows.Forms.ComboBox();
            this.yobMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.estimateCheckBox = new System.Windows.Forms.CheckBox();
            this.ageClassTextBox = new System.Windows.Forms.TextBox();
            this.changeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.clubContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.makePrimaryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nameContextMenuStrip.SuspendLayout();
            this.clubContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameListLabel
            // 
            this.nameListLabel.AutoSize = true;
            this.nameListLabel.Location = new System.Drawing.Point(10, 9);
            this.nameListLabel.Name = "nameListLabel";
            this.nameListLabel.Size = new System.Drawing.Size(40, 13);
            this.nameListLabel.TabIndex = 0;
            this.nameListLabel.Text = "Names";
            // 
            // nameListBox
            // 
            this.nameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameListBox.ContextMenuStrip = this.nameContextMenuStrip;
            this.nameListBox.FormattingEnabled = true;
            this.nameListBox.Location = new System.Drawing.Point(12, 25);
            this.nameListBox.Name = "nameListBox";
            this.nameListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.nameListBox.Size = new System.Drawing.Size(524, 108);
            this.nameListBox.Sorted = true;
            this.nameListBox.TabIndex = 1;
            // 
            // nameContextMenuStrip
            // 
            this.nameContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.makePrimaryToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.nameContextMenuStrip.Name = "nameContextMenuStrip";
            this.nameContextMenuStrip.Size = new System.Drawing.Size(148, 92);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addToolStripMenuItem.Text = "Add…";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addNameToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.editToolStripMenuItem.Text = "Edit…";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editNameToolStripMenuItem_Click);
            // 
            // makePrimaryToolStripMenuItem
            // 
            this.makePrimaryToolStripMenuItem.Name = "makePrimaryToolStripMenuItem";
            this.makePrimaryToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.makePrimaryToolStripMenuItem.Text = "Make Primary";
            this.makePrimaryToolStripMenuItem.Click += new System.EventHandler(this.makePrimaryNameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.deleteToolStripMenuItem.Text = "Delete…";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteNameToolStripMenuItem_Click);
            // 
            // clubListLabel
            // 
            this.clubListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clubListLabel.AutoSize = true;
            this.clubListLabel.Location = new System.Drawing.Point(9, 135);
            this.clubListLabel.Name = "clubListLabel";
            this.clubListLabel.Size = new System.Drawing.Size(33, 13);
            this.clubListLabel.TabIndex = 2;
            this.clubListLabel.Text = "Clubs";
            // 
            // clubListBox
            // 
            this.clubListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.clubListBox.ContextMenuStrip = this.clubContextMenuStrip;
            this.clubListBox.FormattingEnabled = true;
            this.clubListBox.Location = new System.Drawing.Point(12, 151);
            this.clubListBox.Name = "clubListBox";
            this.clubListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.clubListBox.Size = new System.Drawing.Size(524, 95);
            this.clubListBox.Sorted = true;
            this.clubListBox.TabIndex = 3;
            // 
            // sexLabel
            // 
            this.sexLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sexLabel.AutoSize = true;
            this.sexLabel.Location = new System.Drawing.Point(12, 255);
            this.sexLabel.Name = "sexLabel";
            this.sexLabel.Size = new System.Drawing.Size(25, 13);
            this.sexLabel.TabIndex = 4;
            this.sexLabel.Text = "Sex";
            // 
            // yobLabel
            // 
            this.yobLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yobLabel.AutoSize = true;
            this.yobLabel.Location = new System.Drawing.Point(12, 282);
            this.yobLabel.Name = "yobLabel";
            this.yobLabel.Size = new System.Drawing.Size(65, 13);
            this.yobLabel.TabIndex = 5;
            this.yobLabel.Text = "Year of Birth";
            // 
            // ageClassLabel
            // 
            this.ageClassLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ageClassLabel.AutoSize = true;
            this.ageClassLabel.Location = new System.Drawing.Point(12, 308);
            this.ageClassLabel.Name = "ageClassLabel";
            this.ageClassLabel.Size = new System.Drawing.Size(54, 13);
            this.ageClassLabel.TabIndex = 7;
            this.ageClassLabel.Text = "Age Class";
            // 
            // sexComboBox
            // 
            this.sexComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sexComboBox.FormattingEnabled = true;
            this.sexComboBox.Items.AddRange(new object[] {
            "Men",
            "Women"});
            this.sexComboBox.Location = new System.Drawing.Point(78, 252);
            this.sexComboBox.Name = "sexComboBox";
            this.sexComboBox.Size = new System.Drawing.Size(103, 21);
            this.sexComboBox.TabIndex = 8;
            this.sexComboBox.SelectedValueChanged += new System.EventHandler(this.sexComboBox_SelectedValueChanged);
            // 
            // yobMaskedTextBox
            // 
            this.yobMaskedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yobMaskedTextBox.Location = new System.Drawing.Point(78, 279);
            this.yobMaskedTextBox.Mask = "0000";
            this.yobMaskedTextBox.Name = "yobMaskedTextBox";
            this.yobMaskedTextBox.Size = new System.Drawing.Size(31, 20);
            this.yobMaskedTextBox.TabIndex = 9;
            this.yobMaskedTextBox.TextChanged += new System.EventHandler(this.yobMaskedTextBox_TextChanged);
            // 
            // estimateCheckBox
            // 
            this.estimateCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.estimateCheckBox.AutoSize = true;
            this.estimateCheckBox.Location = new System.Drawing.Point(115, 281);
            this.estimateCheckBox.Name = "estimateCheckBox";
            this.estimateCheckBox.Size = new System.Drawing.Size(66, 17);
            this.estimateCheckBox.TabIndex = 10;
            this.estimateCheckBox.Text = "Estimate";
            this.estimateCheckBox.UseVisualStyleBackColor = true;
            // 
            // ageClassTextBox
            // 
            this.ageClassTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ageClassTextBox.Enabled = false;
            this.ageClassTextBox.Location = new System.Drawing.Point(78, 305);
            this.ageClassTextBox.Name = "ageClassTextBox";
            this.ageClassTextBox.Size = new System.Drawing.Size(103, 20);
            this.ageClassTextBox.TabIndex = 11;
            // 
            // changeButton
            // 
            this.changeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.changeButton.Location = new System.Drawing.Point(12, 332);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(75, 23);
            this.changeButton.TabIndex = 12;
            this.changeButton.Text = "Okay";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(94, 331);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // clubContextMenuStrip
            // 
            this.clubContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem1,
            this.editToolStripMenuItem1,
            this.makePrimaryToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.clubContextMenuStrip.Name = "clubContextMenuStrip";
            this.clubContextMenuStrip.Size = new System.Drawing.Size(148, 92);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem1.Text = "Add…";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addClubToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem1.Text = "Edit…";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.editClubToolStripMenuItem_Click);
            // 
            // makePrimaryToolStripMenuItem1
            // 
            this.makePrimaryToolStripMenuItem1.Name = "makePrimaryToolStripMenuItem1";
            this.makePrimaryToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.makePrimaryToolStripMenuItem1.Text = "Make Primary";
            this.makePrimaryToolStripMenuItem1.Click += new System.EventHandler(this.makePrimaryClubToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem1.Text = "Delete…";
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteClubToolStripMenuItem_Click);
            // 
            // CompetitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 367);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.ageClassTextBox);
            this.Controls.Add(this.estimateCheckBox);
            this.Controls.Add(this.yobMaskedTextBox);
            this.Controls.Add(this.sexComboBox);
            this.Controls.Add(this.ageClassLabel);
            this.Controls.Add(this.yobLabel);
            this.Controls.Add(this.sexLabel);
            this.Controls.Add(this.clubListBox);
            this.Controls.Add(this.clubListLabel);
            this.Controls.Add(this.nameListBox);
            this.Controls.Add(this.nameListLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CompetitorForm";
            this.Text = "Competitor";
            this.nameContextMenuStrip.ResumeLayout(false);
            this.clubContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameListLabel;
        private System.Windows.Forms.ListBox nameListBox;
        private System.Windows.Forms.Label clubListLabel;
        private System.Windows.Forms.ListBox clubListBox;
        private System.Windows.Forms.Label sexLabel;
        private System.Windows.Forms.Label yobLabel;
        private System.Windows.Forms.Label ageClassLabel;
        private System.Windows.Forms.ComboBox sexComboBox;
        private System.Windows.Forms.MaskedTextBox yobMaskedTextBox;
        private System.Windows.Forms.CheckBox estimateCheckBox;
        private System.Windows.Forms.TextBox ageClassTextBox;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ContextMenuStrip nameContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makePrimaryToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip clubContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem makePrimaryToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
    }
}