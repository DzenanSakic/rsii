namespace AMA.AdminClient
{
    partial class FrmHome
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
            this.linkMngUsers = new System.Windows.Forms.LinkLabel();
            this.linkMngCategories = new System.Windows.Forms.LinkLabel();
            this.linkQuestions = new System.Windows.Forms.LinkLabel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.PanelQuestions = new System.Windows.Forms.Panel();
            this.PanelAnswers = new System.Windows.Forms.Panel();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.cmbSubCategory = new System.Windows.Forms.ComboBox();
            this.linkReports = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // linkMngUsers
            // 
            this.linkMngUsers.AutoSize = true;
            this.linkMngUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkMngUsers.Location = new System.Drawing.Point(84, 9);
            this.linkMngUsers.Name = "linkMngUsers";
            this.linkMngUsers.Size = new System.Drawing.Size(110, 20);
            this.linkMngUsers.TabIndex = 0;
            this.linkMngUsers.TabStop = true;
            this.linkMngUsers.Text = "Manage users";
            this.linkMngUsers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMngUsers_LinkClicked);
            // 
            // linkMngCategories
            // 
            this.linkMngCategories.AutoSize = true;
            this.linkMngCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkMngCategories.Location = new System.Drawing.Point(200, 9);
            this.linkMngCategories.Name = "linkMngCategories";
            this.linkMngCategories.Size = new System.Drawing.Size(145, 20);
            this.linkMngCategories.TabIndex = 1;
            this.linkMngCategories.TabStop = true;
            this.linkMngCategories.Text = "Manage categories";
            this.linkMngCategories.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkMngCategories_LinkClicked);
            // 
            // linkQuestions
            // 
            this.linkQuestions.AutoSize = true;
            this.linkQuestions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkQuestions.Location = new System.Drawing.Point(12, 9);
            this.linkQuestions.Name = "linkQuestions";
            this.linkQuestions.Size = new System.Drawing.Size(66, 20);
            this.linkQuestions.TabIndex = 2;
            this.linkQuestions.TabStop = true;
            this.linkQuestions.Text = "Refresh";
            this.linkQuestions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkQuestions_LinkClicked);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(16, 65);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(257, 20);
            this.txtSearch.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(285, 65);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 44);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // PanelQuestions
            // 
            this.PanelQuestions.Location = new System.Drawing.Point(16, 115);
            this.PanelQuestions.Name = "PanelQuestions";
            this.PanelQuestions.Size = new System.Drawing.Size(344, 323);
            this.PanelQuestions.TabIndex = 7;
            // 
            // PanelAnswers
            // 
            this.PanelAnswers.Location = new System.Drawing.Point(388, 65);
            this.PanelAnswers.Name = "PanelAnswers";
            this.PanelAnswers.Size = new System.Drawing.Size(386, 373);
            this.PanelAnswers.TabIndex = 8;
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(16, 88);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 9;
            this.cmbCategory.SelectionChangeCommitted += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // cmbSubCategory
            // 
            this.cmbSubCategory.FormattingEnabled = true;
            this.cmbSubCategory.Location = new System.Drawing.Point(152, 88);
            this.cmbSubCategory.Name = "cmbSubCategory";
            this.cmbSubCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbSubCategory.TabIndex = 10;
            // 
            // linkReports
            // 
            this.linkReports.AutoSize = true;
            this.linkReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkReports.Location = new System.Drawing.Point(351, 9);
            this.linkReports.Name = "linkReports";
            this.linkReports.Size = new System.Drawing.Size(66, 20);
            this.linkReports.TabIndex = 11;
            this.linkReports.TabStop = true;
            this.linkReports.Text = "Reports";
            this.linkReports.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkReports_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(711, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(63, 20);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Log out";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 450);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.linkReports);
            this.Controls.Add(this.cmbSubCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.PanelAnswers);
            this.Controls.Add(this.PanelQuestions);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.linkQuestions);
            this.Controls.Add(this.linkMngCategories);
            this.Controls.Add(this.linkMngUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.FrmHome_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkMngUsers;
        private System.Windows.Forms.LinkLabel linkMngCategories;
        private System.Windows.Forms.LinkLabel linkQuestions;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel PanelQuestions;
        private System.Windows.Forms.Panel PanelAnswers;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.ComboBox cmbSubCategory;
        private System.Windows.Forms.LinkLabel linkReports;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}