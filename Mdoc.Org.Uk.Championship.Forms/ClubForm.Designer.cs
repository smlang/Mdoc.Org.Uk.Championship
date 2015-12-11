namespace Mdoc.Org.Uk.Championship.Forms
{
    partial class ClubForm
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.urlLabel = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.inCupCheckBox = new System.Windows.Forms.CheckBox();
            this.defaultScoreRegExLabel = new System.Windows.Forms.Label();
            this.defaultScoreRegexPatternTextBox = new System.Windows.Forms.TextBox();
            this.changeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 16);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(53, 13);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(367, 20);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.WordWrap = false;
            // 
            // urlLabel
            // 
            this.urlLabel.AutoSize = true;
            this.urlLabel.Location = new System.Drawing.Point(18, 43);
            this.urlLabel.Name = "urlLabel";
            this.urlLabel.Size = new System.Drawing.Size(29, 13);
            this.urlLabel.TabIndex = 2;
            this.urlLabel.Text = "URL";
            // 
            // urlTextBox
            // 
            this.urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.urlTextBox.Location = new System.Drawing.Point(53, 40);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(367, 20);
            this.urlTextBox.TabIndex = 3;
            this.urlTextBox.WordWrap = false;
            // 
            // inCupCheckBox
            // 
            this.inCupCheckBox.AutoSize = true;
            this.inCupCheckBox.Location = new System.Drawing.Point(53, 66);
            this.inCupCheckBox.Name = "inCupCheckBox";
            this.inCupCheckBox.Size = new System.Drawing.Size(57, 17);
            this.inCupCheckBox.TabIndex = 5;
            this.inCupCheckBox.Text = "In Cup";
            this.inCupCheckBox.UseVisualStyleBackColor = true;
            // 
            // defaultScoreRegExLabel
            // 
            this.defaultScoreRegExLabel.AutoSize = true;
            this.defaultScoreRegExLabel.Location = new System.Drawing.Point(12, 86);
            this.defaultScoreRegExLabel.Name = "defaultScoreRegExLabel";
            this.defaultScoreRegExLabel.Size = new System.Drawing.Size(166, 13);
            this.defaultScoreRegExLabel.TabIndex = 6;
            this.defaultScoreRegExLabel.Text = "Default Score Regular Expression";
            // 
            // defaultScoreRegexPatternTextBox
            // 
            this.defaultScoreRegexPatternTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultScoreRegexPatternTextBox.Location = new System.Drawing.Point(53, 103);
            this.defaultScoreRegexPatternTextBox.Multiline = true;
            this.defaultScoreRegexPatternTextBox.Name = "defaultScoreRegexPatternTextBox";
            this.defaultScoreRegexPatternTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.defaultScoreRegexPatternTextBox.Size = new System.Drawing.Size(368, 172);
            this.defaultScoreRegexPatternTextBox.TabIndex = 7;
            this.defaultScoreRegexPatternTextBox.WordWrap = false;
            // 
            // changeButton
            // 
            this.changeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeButton.Location = new System.Drawing.Point(54, 281);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(75, 23);
            this.changeButton.TabIndex = 8;
            this.changeButton.Text = "Add";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.ChangeButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(136, 281);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // ClubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(434, 316);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.defaultScoreRegexPatternTextBox);
            this.Controls.Add(this.defaultScoreRegExLabel);
            this.Controls.Add(this.inCupCheckBox);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.urlLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ClubForm";
            this.Text = "Club";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label urlLabel;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.CheckBox inCupCheckBox;
        private System.Windows.Forms.Label defaultScoreRegExLabel;
        private System.Windows.Forms.TextBox defaultScoreRegexPatternTextBox;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.Button cancelButton;
    }
}