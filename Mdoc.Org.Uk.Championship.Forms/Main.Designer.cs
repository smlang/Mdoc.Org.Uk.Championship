namespace Mdoc.Org.Uk.Championship.Forms
{
    partial class Main
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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editCupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsCupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportHtmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addClubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.miscellanouesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ageClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.courseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.competitorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipResultLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.debugRichTextBox = new System.Windows.Forms.RichTextBox();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(102, 6);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.raceToolStripMenuItem,
            this.clubToolStripMenuItem,
            this.miscellanouesToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(811, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCupToolStripMenuItem,
            this.toolStripSeparator1,
            this.editCupToolStripMenuItem,
            this.saveCupToolStripMenuItem,
            this.saveAsCupToolStripMenuItem,
            this.exportHtmlToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCupToolStripMenuItem
            // 
            this.openCupToolStripMenuItem.Name = "openCupToolStripMenuItem";
            this.openCupToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.openCupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openCupToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.openCupToolStripMenuItem.Text = "Open…";
            this.openCupToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(240, 6);
            // 
            // editCupToolStripMenuItem
            // 
            this.editCupToolStripMenuItem.Name = "editCupToolStripMenuItem";
            this.editCupToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.editCupToolStripMenuItem.Text = "Edit…";
            this.editCupToolStripMenuItem.Click += new System.EventHandler(this.EditCupToolStripMenuItemClick);
            // 
            // saveCupToolStripMenuItem
            // 
            this.saveCupToolStripMenuItem.Name = "saveCupToolStripMenuItem";
            this.saveCupToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.saveCupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveCupToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveCupToolStripMenuItem.Text = "Save";
            this.saveCupToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // saveAsCupToolStripMenuItem
            // 
            this.saveAsCupToolStripMenuItem.Name = "saveAsCupToolStripMenuItem";
            this.saveAsCupToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.saveAsCupToolStripMenuItem.Text = "Save As Next Year…";
            this.saveAsCupToolStripMenuItem.Click += new System.EventHandler(this.SaveAsCupToolStripMenuItemClick);
            // 
            // exportHtmlToolStripMenuItem
            // 
            this.exportHtmlToolStripMenuItem.Name = "exportHtmlToolStripMenuItem";
            this.exportHtmlToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportHtmlToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.exportHtmlToolStripMenuItem.Text = "Export Results Web Page";
            this.exportHtmlToolStripMenuItem.Click += new System.EventHandler(this.ExportHtmlToolStripMenuItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(240, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // raceToolStripMenuItem
            // 
            this.raceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRaceToolStripMenuItem,
            toolStripSeparator3});
            this.raceToolStripMenuItem.Name = "raceToolStripMenuItem";
            this.raceToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.raceToolStripMenuItem.Text = "Race";
            // 
            // addRaceToolStripMenuItem
            // 
            this.addRaceToolStripMenuItem.Name = "addRaceToolStripMenuItem";
            this.addRaceToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.addRaceToolStripMenuItem.Text = "Add…";
            this.addRaceToolStripMenuItem.Click += new System.EventHandler(this.AddRaceToolStripMenuItemClick);
            // 
            // clubToolStripMenuItem
            // 
            this.clubToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addClubToolStripMenuItem,
            this.toolStripSeparator4});
            this.clubToolStripMenuItem.Name = "clubToolStripMenuItem";
            this.clubToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.clubToolStripMenuItem.Text = "Club";
            // 
            // addClubToolStripMenuItem
            // 
            this.addClubToolStripMenuItem.Name = "addClubToolStripMenuItem";
            this.addClubToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.addClubToolStripMenuItem.Text = "Add…";
            this.addClubToolStripMenuItem.Click += new System.EventHandler(this.AddClubToolStripMenuItemClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(102, 6);
            // 
            // miscellanouesToolStripMenuItem
            // 
            this.miscellanouesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ageClassToolStripMenuItem,
            this.courseToolStripMenuItem,
            this.competitorsToolStripMenuItem,
            this.skipResultLinesToolStripMenuItem});
            this.miscellanouesToolStripMenuItem.Name = "miscellanouesToolStripMenuItem";
            this.miscellanouesToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.miscellanouesToolStripMenuItem.Text = "Miscellaneous";
            // 
            // ageClassToolStripMenuItem
            // 
            this.ageClassToolStripMenuItem.Name = "ageClassToolStripMenuItem";
            this.ageClassToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ageClassToolStripMenuItem.Text = "Age Classes…";
            this.ageClassToolStripMenuItem.Click += new System.EventHandler(this.AgeClassToolStripMenuItemClick);
            // 
            // courseToolStripMenuItem
            // 
            this.courseToolStripMenuItem.Name = "courseToolStripMenuItem";
            this.courseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.courseToolStripMenuItem.Text = "Course Definitions…";
            this.courseToolStripMenuItem.Click += new System.EventHandler(this.CourseToolStripMenuItemClick);
            // 
            // competitorsToolStripMenuItem
            // 
            this.competitorsToolStripMenuItem.Name = "competitorsToolStripMenuItem";
            this.competitorsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.competitorsToolStripMenuItem.Text = "Competitors…";
            this.competitorsToolStripMenuItem.Click += new System.EventHandler(this.CompetitorsToolStripMenuItemClick);
            // 
            // skipResultLinesToolStripMenuItem
            // 
            this.skipResultLinesToolStripMenuItem.Name = "skipResultLinesToolStripMenuItem";
            this.skipResultLinesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.skipResultLinesToolStripMenuItem.Text = "Skip Result Lines…";
            this.skipResultLinesToolStripMenuItem.Click += new System.EventHandler(this.SkipResultLinesToolStripMenuItemClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "xml";
            this.openFileDialog.Filter = "Cup Files (*.xml)|*.xml";
            this.openFileDialog.RestoreDirectory = true;
            this.openFileDialog.Title = "Select a cup to process";
            // 
            // debugRichTextBox
            // 
            this.debugRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugRichTextBox.Location = new System.Drawing.Point(0, 27);
            this.debugRichTextBox.Name = "debugRichTextBox";
            this.debugRichTextBox.ReadOnly = true;
            this.debugRichTextBox.Size = new System.Drawing.Size(811, 362);
            this.debugRichTextBox.TabIndex = 1;
            this.debugRichTextBox.Text = "";
            this.debugRichTextBox.WordWrap = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 389);
            this.Controls.Add(this.debugRichTextBox);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.ShowIcon = false;
            this.Text = "MDOC Championship";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem raceToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.RichTextBox debugRichTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportHtmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addClubToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem miscellanouesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ageClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem courseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem competitorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsCupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skipResultLinesToolStripMenuItem;
    }
}

