namespace ChemicalStructureSearchingDemo.SearcherClient
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIndexPath = new System.Windows.Forms.TextBox();
            this.pictureBoxQuery = new System.Windows.Forms.PictureBox();
            this.radioButtonExact = new System.Windows.Forms.RadioButton();
            this.radioButtonSimilarity = new System.Windows.Forms.RadioButton();
            this.radioButtonSubstructure = new System.Windows.Forms.RadioButton();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.labelResults = new System.Windows.Forms.Label();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.pictureBoxSelectedResult = new System.Windows.Forms.PictureBox();
            this.labelInstructions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectedResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Query:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Index Path:";
            // 
            // textBoxIndexPath
            // 
            this.textBoxIndexPath.Location = new System.Drawing.Point(80, 10);
            this.textBoxIndexPath.Name = "textBoxIndexPath";
            this.textBoxIndexPath.Size = new System.Drawing.Size(452, 20);
            this.textBoxIndexPath.TabIndex = 2;
            // 
            // pictureBoxQuery
            // 
            this.pictureBoxQuery.BackColor = System.Drawing.Color.White;
            this.pictureBoxQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxQuery.Location = new System.Drawing.Point(80, 50);
            this.pictureBoxQuery.Name = "pictureBoxQuery";
            this.pictureBoxQuery.Size = new System.Drawing.Size(216, 136);
            this.pictureBoxQuery.TabIndex = 3;
            this.pictureBoxQuery.TabStop = false;
            this.pictureBoxQuery.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxQuery_DragDrop);
            this.pictureBoxQuery.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxQuery_DragEnter);
            // 
            // radioButtonExact
            // 
            this.radioButtonExact.AutoSize = true;
            this.radioButtonExact.Checked = true;
            this.radioButtonExact.Location = new System.Drawing.Point(80, 192);
            this.radioButtonExact.Name = "radioButtonExact";
            this.radioButtonExact.Size = new System.Drawing.Size(52, 17);
            this.radioButtonExact.TabIndex = 5;
            this.radioButtonExact.TabStop = true;
            this.radioButtonExact.Text = "Exact";
            this.radioButtonExact.UseVisualStyleBackColor = true;
            // 
            // radioButtonSimilarity
            // 
            this.radioButtonSimilarity.AutoSize = true;
            this.radioButtonSimilarity.Location = new System.Drawing.Point(138, 192);
            this.radioButtonSimilarity.Name = "radioButtonSimilarity";
            this.radioButtonSimilarity.Size = new System.Drawing.Size(65, 17);
            this.radioButtonSimilarity.TabIndex = 6;
            this.radioButtonSimilarity.Text = "Similarity";
            this.radioButtonSimilarity.UseVisualStyleBackColor = true;
            // 
            // radioButtonSubstructure
            // 
            this.radioButtonSubstructure.AutoSize = true;
            this.radioButtonSubstructure.Location = new System.Drawing.Point(209, 192);
            this.radioButtonSubstructure.Name = "radioButtonSubstructure";
            this.radioButtonSubstructure.Size = new System.Drawing.Size(85, 17);
            this.radioButtonSubstructure.TabIndex = 7;
            this.radioButtonSubstructure.Text = "Substructure";
            this.radioButtonSubstructure.UseVisualStyleBackColor = true;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(80, 215);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(216, 47);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // labelResults
            // 
            this.labelResults.AutoSize = true;
            this.labelResults.Location = new System.Drawing.Point(12, 280);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(45, 13);
            this.labelResults.TabIndex = 9;
            this.labelResults.Text = "Results:";
            // 
            // listBoxResults
            // 
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.Location = new System.Drawing.Point(80, 280);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(214, 199);
            this.listBoxResults.TabIndex = 10;
            this.listBoxResults.SelectedIndexChanged += new System.EventHandler(this.listBoxResults_SelectedIndexChanged);
            // 
            // pictureBoxSelectedResult
            // 
            this.pictureBoxSelectedResult.BackColor = System.Drawing.Color.White;
            this.pictureBoxSelectedResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxSelectedResult.Location = new System.Drawing.Point(318, 280);
            this.pictureBoxSelectedResult.Name = "pictureBoxSelectedResult";
            this.pictureBoxSelectedResult.Size = new System.Drawing.Size(214, 199);
            this.pictureBoxSelectedResult.TabIndex = 11;
            this.pictureBoxSelectedResult.TabStop = false;
            // 
            // labelInstructions
            // 
            this.labelInstructions.AutoSize = true;
            this.labelInstructions.BackColor = System.Drawing.Color.White;
            this.labelInstructions.Location = new System.Drawing.Point(126, 103);
            this.labelInstructions.Name = "labelInstructions";
            this.labelInstructions.Size = new System.Drawing.Size(126, 26);
            this.labelInstructions.TabIndex = 12;
            this.labelInstructions.Text = "Drag and drop a *.mol file\r\nfrom Windows Explorer";
            this.labelInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 494);
            this.Controls.Add(this.labelInstructions);
            this.Controls.Add(this.pictureBoxSelectedResult);
            this.Controls.Add(this.listBoxResults);
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.radioButtonSubstructure);
            this.Controls.Add(this.radioButtonSimilarity);
            this.Controls.Add(this.radioButtonExact);
            this.Controls.Add(this.pictureBoxQuery);
            this.Controls.Add(this.textBoxIndexPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chemical Structure Search Client";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectedResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIndexPath;
        private System.Windows.Forms.PictureBox pictureBoxQuery;
        private System.Windows.Forms.RadioButton radioButtonExact;
        private System.Windows.Forms.RadioButton radioButtonSimilarity;
        private System.Windows.Forms.RadioButton radioButtonSubstructure;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.PictureBox pictureBoxSelectedResult;
        private System.Windows.Forms.Label labelInstructions;
    }
}

