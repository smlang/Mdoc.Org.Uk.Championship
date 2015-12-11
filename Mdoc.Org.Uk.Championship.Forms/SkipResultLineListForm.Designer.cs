namespace Mdoc.Org.Uk.Championship.Forms
{
    partial class SkipResultLineForm
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
            this.skipResultLineDataGridView = new System.Windows.Forms.DataGridView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.regexName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.skipResultLineDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // skipResultLineDataGridView
            // 
            this.skipResultLineDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skipResultLineDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.skipResultLineDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.regexName});
            this.skipResultLineDataGridView.Location = new System.Drawing.Point(0, 0);
            this.skipResultLineDataGridView.Name = "skipResultLineDataGridView";
            this.skipResultLineDataGridView.Size = new System.Drawing.Size(645, 396);
            this.skipResultLineDataGridView.TabIndex = 0;
            this.skipResultLineDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.SkipResultLineDataGridViewCellValidating);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.CausesValidation = false;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(93, 402);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // changeButton
            // 
            this.changeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.changeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.changeButton.Location = new System.Drawing.Point(12, 402);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(75, 23);
            this.changeButton.TabIndex = 1;
            this.changeButton.Text = "Okay";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.ChangeButtonClick);
            // 
            // regexName
            // 
            this.regexName.HeaderText = "Regular Expression";
            this.regexName.Name = "regexName";
            this.regexName.Width = 600;
            // 
            // SkipResultLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 437);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.skipResultLineDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SkipResultLineForm";
            this.Text = "Skip Result Line";
            ((System.ComponentModel.ISupportInitialize)(this.skipResultLineDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.DataGridView skipResultLineDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn regexName;
    }
}