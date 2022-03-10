namespace TestClient.WinForms
{
    partial class EmpiQuery
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "5.6",
            "9123456789",
            "SMITH, John Edward Franklin",
            "1982-01-02",
            "123 Oak St"}, -1);
            this.btnGetDemographics = new System.Windows.Forms.Button();
            this.btnFindCandidates = new System.Windows.Forms.Button();
            this.txtPhn = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this.lvCandidates = new System.Windows.Forms.ListView();
            this.colScore = new System.Windows.Forms.ColumnHeader();
            this.colPhn = new System.Windows.Forms.ColumnHeader();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colDateOfBirth = new System.Windows.Forms.ColumnHeader();
            this.colAddress = new System.Windows.Forms.ColumnHeader();
            this.txtGivenNames = new System.Windows.Forms.TextBox();
            this.txtDateOfBirth = new System.Windows.Forms.TextBox();
            this.txtPostalCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGetDemographics
            // 
            this.btnGetDemographics.Location = new System.Drawing.Point(27, 51);
            this.btnGetDemographics.Name = "btnGetDemographics";
            this.btnGetDemographics.Size = new System.Drawing.Size(133, 23);
            this.btnGetDemographics.TabIndex = 1;
            this.btnGetDemographics.Text = "Get Demogaphics";
            this.btnGetDemographics.UseVisualStyleBackColor = true;
            this.btnGetDemographics.Click += new System.EventHandler(this.btnGetDemographics_Click);
            // 
            // btnFindCandidates
            // 
            this.btnFindCandidates.Location = new System.Drawing.Point(186, 51);
            this.btnFindCandidates.Name = "btnFindCandidates";
            this.btnFindCandidates.Size = new System.Drawing.Size(133, 23);
            this.btnFindCandidates.TabIndex = 7;
            this.btnFindCandidates.Text = "Find Candidates";
            this.btnFindCandidates.UseVisualStyleBackColor = true;
            this.btnFindCandidates.Click += new System.EventHandler(this.btnFindCandidates_Click);
            // 
            // txtPhn
            // 
            this.txtPhn.Location = new System.Drawing.Point(27, 22);
            this.txtPhn.MaxLength = 10;
            this.txtPhn.Name = "txtPhn";
            this.txtPhn.PlaceholderText = "9#########";
            this.txtPhn.Size = new System.Drawing.Size(133, 23);
            this.txtPhn.TabIndex = 0;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(186, 22);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.PlaceholderText = "Surname";
            this.txtSurname.Size = new System.Drawing.Size(133, 23);
            this.txtSurname.TabIndex = 2;
            // 
            // txtAddressLine1
            // 
            this.txtAddressLine1.Location = new System.Drawing.Point(603, 22);
            this.txtAddressLine1.Name = "txtAddressLine1";
            this.txtAddressLine1.PlaceholderText = "Address Line 1";
            this.txtAddressLine1.Size = new System.Drawing.Size(133, 23);
            this.txtAddressLine1.TabIndex = 5;
            // 
            // lvCandidates
            // 
            this.lvCandidates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colScore,
            this.colPhn,
            this.colName,
            this.colDateOfBirth,
            this.colAddress});
            this.lvCandidates.FullRowSelect = true;
            this.lvCandidates.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lvCandidates.Location = new System.Drawing.Point(27, 89);
            this.lvCandidates.Name = "lvCandidates";
            this.lvCandidates.Size = new System.Drawing.Size(848, 349);
            this.lvCandidates.TabIndex = 8;
            this.lvCandidates.UseCompatibleStateImageBehavior = false;
            this.lvCandidates.View = System.Windows.Forms.View.Details;
            // 
            // colScore
            // 
            this.colScore.Text = "Score";
            this.colScore.Width = 45;
            // 
            // colPhn
            // 
            this.colPhn.Text = "PHN";
            this.colPhn.Width = 80;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 200;
            // 
            // colDateOfBirth
            // 
            this.colDateOfBirth.Text = "Date of Birth";
            this.colDateOfBirth.Width = 90;
            // 
            // colAddress
            // 
            this.colAddress.Text = "Address";
            this.colAddress.Width = 300;
            // 
            // txtGivenNames
            // 
            this.txtGivenNames.Location = new System.Drawing.Point(325, 22);
            this.txtGivenNames.Name = "txtGivenNames";
            this.txtGivenNames.PlaceholderText = "Given Names";
            this.txtGivenNames.Size = new System.Drawing.Size(133, 23);
            this.txtGivenNames.TabIndex = 3;
            // 
            // txtDateOfBirth
            // 
            this.txtDateOfBirth.Location = new System.Drawing.Point(464, 22);
            this.txtDateOfBirth.Name = "txtDateOfBirth";
            this.txtDateOfBirth.PlaceholderText = "Date of Birth";
            this.txtDateOfBirth.Size = new System.Drawing.Size(133, 23);
            this.txtDateOfBirth.TabIndex = 4;
            // 
            // txtPostalCode
            // 
            this.txtPostalCode.Location = new System.Drawing.Point(742, 22);
            this.txtPostalCode.Name = "txtPostalCode";
            this.txtPostalCode.PlaceholderText = "Postal Code";
            this.txtPostalCode.Size = new System.Drawing.Size(133, 23);
            this.txtPostalCode.TabIndex = 6;
            // 
            // EmpiQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 463);
            this.Controls.Add(this.txtPostalCode);
            this.Controls.Add(this.txtDateOfBirth);
            this.Controls.Add(this.txtGivenNames);
            this.Controls.Add(this.lvCandidates);
            this.Controls.Add(this.txtAddressLine1);
            this.Controls.Add(this.txtSurname);
            this.Controls.Add(this.txtPhn);
            this.Controls.Add(this.btnFindCandidates);
            this.Controls.Add(this.btnGetDemographics);
            this.Name = "EmpiQuery";
            this.Text = "EMPI Query";
            this.SizeChanged += new System.EventHandler(this.EmpiQuery_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnGetDemographics;
        private Button btnFindCandidates;
        private TextBox txtPhn;
        private TextBox txtSurname;
        private TextBox txtAddressLine1;
        private ListView lvCandidates;
        private ColumnHeader colScore;
        private ColumnHeader colPhn;
        private ColumnHeader colName;
        private ColumnHeader colAddress;
        private TextBox txtGivenNames;
        private TextBox txtDateOfBirth;
        private TextBox txtPostalCode;
        private ColumnHeader colDateOfBirth;
    }
}