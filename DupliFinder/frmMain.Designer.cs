namespace DupliFinder
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblStart = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.btSearch = new System.Windows.Forms.Button();
            this.cbIncSubfolders = new System.Windows.Forms.CheckBox();
            this.fd = new System.Windows.Forms.FolderBrowserDialog();
            this.pbWorkingAll = new System.Windows.Forms.ProgressBar();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.btRemDup = new System.Windows.Forms.Button();
            this.btRemOrig = new System.Windows.Forms.Button();
            this.lvDuplicates = new System.Windows.Forms.ListView();
            this.chName = new System.Windows.Forms.ColumnHeader();
            this.chSize = new System.Windows.Forms.ColumnHeader();
            this.chSimilarity = new System.Windows.Forms.ColumnHeader();
            this.pbDup = new System.Windows.Forms.PictureBox();
            this.pbOrig = new System.Windows.Forms.PictureBox();
            this.lvResults = new System.Windows.Forms.ListView();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblWorking = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.gb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrig)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(12, 9);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(79, 13);
            this.lblStart.TabIndex = 0;
            this.lblStart.Text = "S&tart search at:";
            // 
            // tbPath
            // 
            this.tbPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPath.Location = new System.Drawing.Point(97, 6);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(235, 20);
            this.tbPath.TabIndex = 1;
            this.tbPath.TabStop = false;
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Location = new System.Drawing.Point(338, 4);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(24, 23);
            this.btBrowse.TabIndex = 2;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // btSearch
            // 
            this.btSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSearch.Enabled = false;
            this.btSearch.Location = new System.Drawing.Point(287, 59);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 4;
            this.btSearch.Text = "&Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // cbIncSubfolders
            // 
            this.cbIncSubfolders.AutoSize = true;
            this.cbIncSubfolders.Location = new System.Drawing.Point(15, 32);
            this.cbIncSubfolders.Name = "cbIncSubfolders";
            this.cbIncSubfolders.Size = new System.Drawing.Size(112, 17);
            this.cbIncSubfolders.TabIndex = 3;
            this.cbIncSubfolders.Text = "&Include subfolders";
            this.cbIncSubfolders.UseVisualStyleBackColor = true;
            // 
            // fd
            // 
            this.fd.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fd.ShowNewFolderButton = false;
            // 
            // pbWorkingAll
            // 
            this.pbWorkingAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWorkingAll.Location = new System.Drawing.Point(15, 70);
            this.pbWorkingAll.Name = "pbWorkingAll";
            this.pbWorkingAll.Size = new System.Drawing.Size(266, 12);
            this.pbWorkingAll.TabIndex = 5;
            this.pbWorkingAll.Visible = false;
            this.pbWorkingAll.VisibleChanged += new System.EventHandler(this.pbWorking_VisibleChanged);
            // 
            // gb1
            // 
            this.gb1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gb1.Controls.Add(this.btRemDup);
            this.gb1.Controls.Add(this.btRemOrig);
            this.gb1.Controls.Add(this.lvDuplicates);
            this.gb1.Controls.Add(this.pbDup);
            this.gb1.Controls.Add(this.pbOrig);
            this.gb1.Controls.Add(this.lvResults);
            this.gb1.Location = new System.Drawing.Point(12, 55);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(350, 0);
            this.gb1.TabIndex = 6;
            this.gb1.TabStop = false;
            this.gb1.Text = "Results - similar images group found";
            // 
            // btRemDup
            // 
            this.btRemDup.Image = global::DupliFinder.Properties.Resources.del;
            this.btRemDup.Location = new System.Drawing.Point(457, 160);
            this.btRemDup.Name = "btRemDup";
            this.btRemDup.Size = new System.Drawing.Size(36, 36);
            this.btRemDup.TabIndex = 8;
            this.btRemDup.UseVisualStyleBackColor = true;
            this.btRemDup.Click += new System.EventHandler(this.btRemDup_Click);
            // 
            // btRemOrig
            // 
            this.btRemOrig.Image = global::DupliFinder.Properties.Resources.del;
            this.btRemOrig.Location = new System.Drawing.Point(272, 160);
            this.btRemOrig.Name = "btRemOrig";
            this.btRemOrig.Size = new System.Drawing.Size(36, 36);
            this.btRemOrig.TabIndex = 7;
            this.btRemOrig.UseVisualStyleBackColor = true;
            this.btRemOrig.Click += new System.EventHandler(this.btRemOrig_Click);
            // 
            // lvDuplicates
            // 
            this.lvDuplicates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDuplicates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chSize,
            this.chSimilarity});
            this.lvDuplicates.FullRowSelect = true;
            this.lvDuplicates.GridLines = true;
            this.lvDuplicates.HideSelection = false;
            this.lvDuplicates.Location = new System.Drawing.Point(131, -89);
            this.lvDuplicates.Name = "lvDuplicates";
            this.lvDuplicates.Size = new System.Drawing.Size(213, 102);
            this.lvDuplicates.TabIndex = 6;
            this.lvDuplicates.UseCompatibleStateImageBehavior = false;
            this.lvDuplicates.View = System.Windows.Forms.View.Details;
            this.lvDuplicates.SelectedIndexChanged += new System.EventHandler(this.lvDuplicates_SelectedIndexChanged);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 170;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.Width = 90;
            // 
            // chSimilarity
            // 
            this.chSimilarity.Text = "Similarity";
            this.chSimilarity.Width = 80;
            // 
            // pbDup
            // 
            this.pbDup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbDup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDup.Location = new System.Drawing.Point(316, 19);
            this.pbDup.Name = "pbDup";
            this.pbDup.Size = new System.Drawing.Size(177, 177);
            this.pbDup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDup.TabIndex = 1;
            this.pbDup.TabStop = false;
            this.pbDup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbOrig_MouseDoubleClick);
            // 
            // pbOrig
            // 
            this.pbOrig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOrig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbOrig.Location = new System.Drawing.Point(131, 19);
            this.pbOrig.Name = "pbOrig";
            this.pbOrig.Size = new System.Drawing.Size(177, 177);
            this.pbOrig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOrig.TabIndex = 1;
            this.pbOrig.TabStop = false;
            this.pbOrig.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbOrig_MouseDoubleClick);
            this.pbOrig.SizeChanged += new System.EventHandler(this.pbOrig_SizeChanged);
            // 
            // lvResults
            // 
            this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lvResults.FullRowSelect = true;
            this.lvResults.GridLines = true;
            this.lvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(6, 19);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(119, 0);
            this.lvResults.TabIndex = 5;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.List;
            this.lvResults.SelectedIndexChanged += new System.EventHandler(this.lvResults_SelectedIndexChanged);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(133, 33);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(229, 13);
            this.lblNote.TabIndex = 7;
            this.lblNote.Text = "Browse or drop start directory to perform search";
            this.lblNote.UseMnemonic = false;
            // 
            // lblWorking
            // 
            this.lblWorking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblWorking.AutoSize = true;
            this.lblWorking.Location = new System.Drawing.Point(12, 55);
            this.lblWorking.Name = "lblWorking";
            this.lblWorking.Size = new System.Drawing.Size(0, 13);
            this.lblWorking.TabIndex = 8;
            this.lblWorking.UseMnemonic = false;
            this.lblWorking.Visible = false;
            this.lblWorking.VisibleChanged += new System.EventHandler(this.pbWorking_VisibleChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.label1.Location = new System.Drawing.Point(1, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Duplicate images finder by Tamir Khason";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.linkLabel1.Location = new System.Drawing.Point(203, 80);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Visit my blog";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 94);
            this.Controls.Add(this.lblWorking);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.pbWorkingAll);
            this.Controls.Add(this.cbIncSubfolders);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(390, 130);
            this.Name = "frmMain";
            this.Text = "Duplicate Images Finder";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.gb1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.CheckBox cbIncSubfolders;
        private System.Windows.Forms.FolderBrowserDialog fd;
        private System.Windows.Forms.ProgressBar pbWorkingAll;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.ListView lvDuplicates;
        private System.Windows.Forms.PictureBox pbDup;
        private System.Windows.Forms.PictureBox pbOrig;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.ColumnHeader chSimilarity;
        private System.Windows.Forms.Button btRemOrig;
        private System.Windows.Forms.Button btRemDup;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblWorking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

