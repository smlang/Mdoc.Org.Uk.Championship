namespace Mdoc.Org.Uk.Championship.Forms
{
    partial class AgeClassListForm
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
            this.ageClassDataGridView = new System.Windows.Forms.DataGridView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.ageClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.originalAgeClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageClassSex = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ageClassMinAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageClassMaxAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageClassSpeedRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ageClassDistanceRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ageClassDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ageClassDataGridView
            // 
            this.ageClassDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ageClassDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ageClassDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ageClassName,
            this.originalAgeClassName,
            this.ageClassSex,
            this.ageClassMinAge,
            this.ageClassMaxAge,
            this.ageClassSpeedRatio,
            this.ageClassDistanceRatio});
            this.ageClassDataGridView.Location = new System.Drawing.Point(0, 0);
            this.ageClassDataGridView.Name = "ageClassDataGridView";
            this.ageClassDataGridView.Size = new System.Drawing.Size(660, 396);
            this.ageClassDataGridView.TabIndex = 0;
            this.ageClassDataGridView.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.AgeClassDataGridViewRowValidating);
            this.ageClassDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.AgeClassDataGridViewCellValidating);
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
            // ageClassName
            // 
            this.ageClassName.HeaderText = "Name";
            this.ageClassName.Name = "ageClassName";
            // 
            // originalAgeClassName
            // 
            this.originalAgeClassName.HeaderText = "Original Name";
            this.originalAgeClassName.Name = "originalAgeClassName";
            this.originalAgeClassName.ReadOnly = true;
            this.originalAgeClassName.Visible = false;
            // 
            // ageClassSex
            // 
            this.ageClassSex.HeaderText = "Sex";
            this.ageClassSex.Items.AddRange(new object[] {
            "Men",
            "Women"});
            this.ageClassSex.Name = "ageClassSex";
            // 
            // ageClassMinAge
            // 
            this.ageClassMinAge.HeaderText = "Min Age";
            this.ageClassMinAge.Name = "ageClassMinAge";
            // 
            // ageClassMaxAge
            // 
            this.ageClassMaxAge.HeaderText = "Max Age";
            this.ageClassMaxAge.Name = "ageClassMaxAge";
            // 
            // ageClassSpeedRatio
            // 
            this.ageClassSpeedRatio.HeaderText = "Speed Ratio";
            this.ageClassSpeedRatio.Name = "ageClassSpeedRatio";
            // 
            // ageClassDistanceRatio
            // 
            this.ageClassDistanceRatio.HeaderText = "Distance Ratio";
            this.ageClassDistanceRatio.Name = "ageClassDistanceRatio";
            // 
            // AgeClassListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 437);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.ageClassDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "AgeClassListForm";
            this.Text = "Age Classes";
            ((System.ComponentModel.ISupportInitialize)(this.ageClassDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.DataGridView ageClassDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn originalAgeClassName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ageClassSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageClassMinAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageClassMaxAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageClassSpeedRatio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ageClassDistanceRatio;
    }
}