namespace Mdoc.Org.Uk.Championship.Forms
{
    partial class CourseListForm
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
            this.courseDefinitionDataGridView = new System.Windows.Forms.DataGridView();
            this.regEx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.td = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ageClassList = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ageClassContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editAgeClassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.editAgeClassbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.courseDefinitionDataGridView)).BeginInit();
            this.ageClassContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // courseDefinitionDataGridView
            // 
            this.courseDefinitionDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.courseDefinitionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.courseDefinitionDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.regEx,
            this.td,
            this.ageClassList});
            this.courseDefinitionDataGridView.Location = new System.Drawing.Point(0, 0);
            this.courseDefinitionDataGridView.Name = "courseDefinitionDataGridView";
            this.courseDefinitionDataGridView.Size = new System.Drawing.Size(435, 396);
            this.courseDefinitionDataGridView.TabIndex = 0;
            this.courseDefinitionDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.CourseDefinitionDataGridViewCellValidating);
            // 
            // regEx
            // 
            this.regEx.HeaderText = "Regular Expression";
            this.regEx.Name = "regEx";
            this.regEx.Width = 125;
            // 
            // td
            // 
            this.td.HeaderText = "Technical Difficulty";
            this.td.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.td.Name = "td";
            this.td.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.td.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.td.Width = 125;
            // 
            // ageClassList
            // 
            this.ageClassList.ContextMenuStrip = this.ageClassContextMenuStrip;
            this.ageClassList.HeaderText = "Default Age Class";
            this.ageClassList.Name = "ageClass";
            this.ageClassList.Width = 125;
            // 
            // ageClassContextMenuStrip
            // 
            this.ageClassContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editAgeClassesToolStripMenuItem});
            this.ageClassContextMenuStrip.Name = "ageClassContextMenuStrip";
            this.ageClassContextMenuStrip.Size = new System.Drawing.Size(169, 26);
            // 
            // editAgeClassesToolStripMenuItem
            // 
            this.editAgeClassesToolStripMenuItem.Name = "editAgeClassesToolStripMenuItem";
            this.editAgeClassesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.editAgeClassesToolStripMenuItem.Text = "Edit Age Classes…";
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
            // editAgeClassbutton
            // 
            this.editAgeClassbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editAgeClassbutton.Location = new System.Drawing.Point(290, 402);
            this.editAgeClassbutton.Name = "editAgeClassbutton";
            this.editAgeClassbutton.Size = new System.Drawing.Size(124, 23);
            this.editAgeClassbutton.TabIndex = 3;
            this.editAgeClassbutton.TabStop = false;
            this.editAgeClassbutton.Text = "Age Classes…";
            this.editAgeClassbutton.UseVisualStyleBackColor = true;
            this.editAgeClassbutton.Click += new System.EventHandler(this.EditAgeClassbuttonClick);
            // 
            // CourseListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 437);
            this.Controls.Add(this.editAgeClassbutton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.courseDefinitionDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CourseListForm";
            this.Text = "Course Definitions";
            ((System.ComponentModel.ISupportInitialize)(this.courseDefinitionDataGridView)).EndInit();
            this.ageClassContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.DataGridView courseDefinitionDataGridView;
        private System.Windows.Forms.ContextMenuStrip ageClassContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editAgeClassesToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn regEx;
        private System.Windows.Forms.DataGridViewComboBoxColumn td;
        private System.Windows.Forms.DataGridViewComboBoxColumn ageClassList;
        private System.Windows.Forms.Button editAgeClassbutton;
    }
}