namespace WindowsFormsApplication2
{
    partial class SetAccountForm
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
            Globals.connStr = @"Data Source=(LocalDB)\ProjectsV12;Initial Catalog=FullerTV;Integrated Security=true";
            this.lbAvailable = new System.Windows.Forms.ListBox();
            this.lbSelected = new System.Windows.Forms.ListBox();
            this.btnSelectOne = new System.Windows.Forms.Button();
            this.btnReturnOne = new System.Windows.Forms.Button();
            this.btnReturnAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.rbSports = new System.Windows.Forms.RadioButton();
            this.lblPackages = new System.Windows.Forms.Label();
            this.llSports = new System.Windows.Forms.LinkLabel();
            this.rbDIY = new System.Windows.Forms.RadioButton();
            this.rbNews = new System.Windows.Forms.RadioButton();
            this.rbMovie = new System.Windows.Forms.RadioButton();
            this.rbAllAround = new System.Windows.Forms.RadioButton();
            this.llMovie = new System.Windows.Forms.LinkLabel();
            this.llNews = new System.Windows.Forms.LinkLabel();
            this.llDIY = new System.Windows.Forms.LinkLabel();
            this.llAllAround = new System.Windows.Forms.LinkLabel();
            this.rbNoPackage = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lbAvailable
            // 
            this.lbAvailable.FormattingEnabled = true;
            this.lbAvailable.Location = new System.Drawing.Point(88, 30);
            this.lbAvailable.Name = "lbAvailable";
            this.lbAvailable.Size = new System.Drawing.Size(116, 121);
            this.lbAvailable.TabIndex = 0;
            // 
            // lbSelected
            // 
            this.lbSelected.FormattingEnabled = true;
            this.lbSelected.Location = new System.Drawing.Point(310, 30);
            this.lbSelected.Name = "lbSelected";
            this.lbSelected.Size = new System.Drawing.Size(116, 121);
            this.lbSelected.TabIndex = 1;
            // 
            // btnSelectOne
            // 
            this.btnSelectOne.Location = new System.Drawing.Point(228, 65);
            this.btnSelectOne.Name = "btnSelectOne";
            this.btnSelectOne.Size = new System.Drawing.Size(56, 23);
            this.btnSelectOne.TabIndex = 2;
            this.btnSelectOne.Text = ">";
            this.btnSelectOne.UseVisualStyleBackColor = true;
            this.btnSelectOne.Click += new System.EventHandler(this.btnSelectOne_Click);
            // 
            // btnReturnOne
            // 
            this.btnReturnOne.Location = new System.Drawing.Point(228, 90);
            this.btnReturnOne.Name = "btnReturnOne";
            this.btnReturnOne.Size = new System.Drawing.Size(56, 23);
            this.btnReturnOne.TabIndex = 3;
            this.btnReturnOne.Text = "<";
            this.btnReturnOne.UseVisualStyleBackColor = true;
            this.btnReturnOne.Click += new System.EventHandler(this.btnReturnOne_Click);
            // 
            // btnReturnAll
            // 
            this.btnReturnAll.Location = new System.Drawing.Point(228, 119);
            this.btnReturnAll.Name = "btnReturnAll";
            this.btnReturnAll.Size = new System.Drawing.Size(56, 23);
            this.btnReturnAll.TabIndex = 4;
            this.btnReturnAll.Text = "<<<";
            this.btnReturnAll.UseVisualStyleBackColor = true;
            this.btnReturnAll.Click += new System.EventHandler(this.btnReturnAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(228, 36);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(56, 23);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = ">>>";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Stations Available";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(307, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Stations Selected";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(310, 172);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(43, 15);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total:";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalValue.Location = new System.Drawing.Point(375, 172);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(35, 15);
            this.lblTotalValue.TabIndex = 9;
            this.lblTotalValue.Text = "0.00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(359, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "$";
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(354, 204);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(72, 23);
            this.btnContinue.TabIndex = 11;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(354, 233);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(72, 23);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbSports
            // 
            this.rbSports.AutoSize = true;
            this.rbSports.Location = new System.Drawing.Point(88, 195);
            this.rbSports.Name = "rbSports";
            this.rbSports.Size = new System.Drawing.Size(93, 17);
            this.rbSports.TabIndex = 13;
            this.rbSports.TabStop = true;
            this.rbSports.Text = "Sports Fanatic";
            this.rbSports.UseVisualStyleBackColor = true;
            this.rbSports.CheckedChanged += new System.EventHandler(this.rbSports_CheckedChanged);
            // 
            // lblPackages
            // 
            this.lblPackages.AutoSize = true;
            this.lblPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackages.Location = new System.Drawing.Point(85, 172);
            this.lblPackages.Name = "lblPackages";
            this.lblPackages.Size = new System.Drawing.Size(131, 15);
            this.lblPackages.TabIndex = 18;
            this.lblPackages.Text = "Packages Available";
            // 
            // llSports
            // 
            this.llSports.AutoSize = true;
            this.llSports.Location = new System.Drawing.Point(177, 197);
            this.llSports.Name = "llSports";
            this.llSports.Size = new System.Drawing.Size(29, 13);
            this.llSports.TabIndex = 19;
            this.llSports.TabStop = true;
            this.llSports.Text = "view";
            this.llSports.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llSports_LinkClicked);
            // 
            // rbDIY
            // 
            this.rbDIY.AutoSize = true;
            this.rbDIY.Location = new System.Drawing.Point(88, 218);
            this.rbDIY.Name = "rbDIY";
            this.rbDIY.Size = new System.Drawing.Size(58, 17);
            this.rbDIY.TabIndex = 20;
            this.rbDIY.TabStop = true;
            this.rbDIY.Text = "DIY- er";
            this.rbDIY.UseVisualStyleBackColor = true;
            this.rbDIY.CheckedChanged += new System.EventHandler(this.rbDIY_CheckedChanged);
            // 
            // rbNews
            // 
            this.rbNews.AutoSize = true;
            this.rbNews.Location = new System.Drawing.Point(88, 241);
            this.rbNews.Name = "rbNews";
            this.rbNews.Size = new System.Drawing.Size(98, 17);
            this.rbNews.TabIndex = 21;
            this.rbNews.TabStop = true;
            this.rbNews.Text = "News Package";
            this.rbNews.UseVisualStyleBackColor = true;
            this.rbNews.CheckedChanged += new System.EventHandler(this.rbNews_CheckedChanged);
            // 
            // rbMovie
            // 
            this.rbMovie.AutoSize = true;
            this.rbMovie.Location = new System.Drawing.Point(88, 264);
            this.rbMovie.Name = "rbMovie";
            this.rbMovie.Size = new System.Drawing.Size(118, 17);
            this.rbMovie.TabIndex = 22;
            this.rbMovie.TabStop = true;
            this.rbMovie.Text = "Movie Buff w/ HBO";
            this.rbMovie.UseVisualStyleBackColor = true;
            this.rbMovie.CheckedChanged += new System.EventHandler(this.rbMovie_CheckedChanged);
            // 
            // rbAllAround
            // 
            this.rbAllAround.AutoSize = true;
            this.rbAllAround.Location = new System.Drawing.Point(88, 287);
            this.rbAllAround.Name = "rbAllAround";
            this.rbAllAround.Size = new System.Drawing.Size(95, 17);
            this.rbAllAround.TabIndex = 23;
            this.rbAllAround.TabStop = true;
            this.rbAllAround.Text = "The All-Around";
            this.rbAllAround.UseVisualStyleBackColor = true;
            this.rbAllAround.CheckedChanged += new System.EventHandler(this.rbAllAround_CheckedChanged);
            // 
            // llMovie
            // 
            this.llMovie.AutoSize = true;
            this.llMovie.Location = new System.Drawing.Point(204, 266);
            this.llMovie.Name = "llMovie";
            this.llMovie.Size = new System.Drawing.Size(29, 13);
            this.llMovie.TabIndex = 24;
            this.llMovie.TabStop = true;
            this.llMovie.Text = "view";
            this.llMovie.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMovie_LinkClicked);
            // 
            // llNews
            // 
            this.llNews.AutoSize = true;
            this.llNews.Location = new System.Drawing.Point(184, 243);
            this.llNews.Name = "llNews";
            this.llNews.Size = new System.Drawing.Size(29, 13);
            this.llNews.TabIndex = 25;
            this.llNews.TabStop = true;
            this.llNews.Text = "view";
            this.llNews.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llNews_LinkClicked);
            // 
            // llDIY
            // 
            this.llDIY.AutoSize = true;
            this.llDIY.Location = new System.Drawing.Point(143, 220);
            this.llDIY.Name = "llDIY";
            this.llDIY.Size = new System.Drawing.Size(29, 13);
            this.llDIY.TabIndex = 26;
            this.llDIY.TabStop = true;
            this.llDIY.Text = "view";
            this.llDIY.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDIY_LinkClicked);
            // 
            // llAllAround
            // 
            this.llAllAround.AutoSize = true;
            this.llAllAround.Location = new System.Drawing.Point(180, 289);
            this.llAllAround.Name = "llAllAround";
            this.llAllAround.Size = new System.Drawing.Size(29, 13);
            this.llAllAround.TabIndex = 27;
            this.llAllAround.TabStop = true;
            this.llAllAround.Text = "view";
            this.llAllAround.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llAllAround_LinkClicked);
            // 
            // rbNoPackage
            // 
            this.rbNoPackage.AutoSize = true;
            this.rbNoPackage.Location = new System.Drawing.Point(88, 310);
            this.rbNoPackage.Name = "rbNoPackage";
            this.rbNoPackage.Size = new System.Drawing.Size(85, 17);
            this.rbNoPackage.TabIndex = 28;
            this.rbNoPackage.TabStop = true;
            this.rbNoPackage.Text = "No Package";
            this.rbNoPackage.UseVisualStyleBackColor = true;
            this.rbNoPackage.CheckedChanged += new System.EventHandler(this.rbNoPackage_CheckedChanged);
            // 
            // SetAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 352);
            this.Controls.Add(this.rbNoPackage);
            this.Controls.Add(this.llAllAround);
            this.Controls.Add(this.llDIY);
            this.Controls.Add(this.llNews);
            this.Controls.Add(this.llMovie);
            this.Controls.Add(this.rbAllAround);
            this.Controls.Add(this.rbMovie);
            this.Controls.Add(this.rbNews);
            this.Controls.Add(this.rbDIY);
            this.Controls.Add(this.llSports);
            this.Controls.Add(this.lblPackages);
            this.Controls.Add(this.rbSports);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotalValue);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnReturnAll);
            this.Controls.Add(this.btnReturnOne);
            this.Controls.Add(this.btnSelectOne);
            this.Controls.Add(this.lbSelected);
            this.Controls.Add(this.lbAvailable);
            this.Name = "SetAccountForm";
            this.Text = "Setup My Account";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbAvailable;
        private System.Windows.Forms.ListBox lbSelected;
        private System.Windows.Forms.Button btnSelectOne;
        private System.Windows.Forms.Button btnReturnOne;
        private System.Windows.Forms.Button btnReturnAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.RadioButton rbSports;
        private System.Windows.Forms.Label lblPackages;
        private System.Windows.Forms.LinkLabel llSports;
        private System.Windows.Forms.RadioButton rbDIY;
        private System.Windows.Forms.RadioButton rbNews;
        private System.Windows.Forms.RadioButton rbMovie;
        private System.Windows.Forms.RadioButton rbAllAround;
        private System.Windows.Forms.LinkLabel llMovie;
        private System.Windows.Forms.LinkLabel llNews;
        private System.Windows.Forms.LinkLabel llDIY;
        private System.Windows.Forms.LinkLabel llAllAround;
        private System.Windows.Forms.RadioButton rbNoPackage;
    }
}
