using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;

namespace Spring2.DataTierGenerator {
	/// <summary>
	/// Summary description for frmMain.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form {
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.LinkLabel lnkEverythingSQL;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkScriptDropStatement;
		private System.Windows.Forms.CheckBox chkGenerateDataObjects;
		private System.Windows.Forms.CheckBox chkUseViewsInStoreProc;
		private System.Windows.Forms.CheckBox chkCreateViews;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtStoreProcNameFormatString;
		private System.Windows.Forms.CheckBox chkSingleFile;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkBlankPassword;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblUserID;
		private System.Windows.Forms.TextBox txtUserID;
		private System.Windows.Forms.Label lblDatabaseName;
		private System.Windows.Forms.TextBox txtDatabaseName;
		private System.Windows.Forms.Label lblServerName;
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.TextBox txtProjectNamespace;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmMain() {
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.btnOK = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.lnkEverythingSQL = new System.Windows.Forms.LinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkScriptDropStatement = new System.Windows.Forms.CheckBox();
			this.chkGenerateDataObjects = new System.Windows.Forms.CheckBox();
			this.chkUseViewsInStoreProc = new System.Windows.Forms.CheckBox();
			this.chkCreateViews = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtStoreProcNameFormatString = new System.Windows.Forms.TextBox();
			this.chkSingleFile = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkBlankPassword = new System.Windows.Forms.CheckBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblUserID = new System.Windows.Forms.Label();
			this.txtUserID = new System.Windows.Forms.TextBox();
			this.lblDatabaseName = new System.Windows.Forms.Label();
			this.txtDatabaseName = new System.Windows.Forms.TextBox();
			this.lblServerName = new System.Windows.Forms.Label();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.txtProjectNamespace = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(192, 408);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 23);
			this.btnOK.TabIndex = 15;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.Location = new System.Drawing.Point(272, 408);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(72, 23);
			this.btnClose.TabIndex = 16;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lnkEverythingSQL
			// 
			this.lnkEverythingSQL.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkEverythingSQL.Location = new System.Drawing.Point(16, 448);
			this.lnkEverythingSQL.Name = "lnkEverythingSQL";
			this.lnkEverythingSQL.Size = new System.Drawing.Size(336, 23);
			this.lnkEverythingSQL.TabIndex = 20;
			this.lnkEverythingSQL.TabStop = true;
			this.lnkEverythingSQL.Text = "By Adrian Anttila.  For more information, visit EverythingSQL.com.";
			this.lnkEverythingSQL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEverythingSQL_LinkClicked);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.label2,
																					this.txtProjectNamespace,
																					this.chkScriptDropStatement,
																					this.chkGenerateDataObjects,
																					this.chkUseViewsInStoreProc,
																					this.chkCreateViews,
																					this.label1,
																					this.txtStoreProcNameFormatString,
																					this.chkSingleFile});
			this.groupBox1.Location = new System.Drawing.Point(8, 168);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(344, 232);
			this.groupBox1.TabIndex = 29;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Generation Options";
			// 
			// chkScriptDropStatement
			// 
			this.chkScriptDropStatement.Location = new System.Drawing.Point(4, 168);
			this.chkScriptDropStatement.Name = "chkScriptDropStatement";
			this.chkScriptDropStatement.Size = new System.Drawing.Size(216, 16);
			this.chkScriptDropStatement.TabIndex = 35;
			this.chkScriptDropStatement.Text = "Script drop statements";
			// 
			// chkGenerateDataObjects
			// 
			this.chkGenerateDataObjects.Location = new System.Drawing.Point(4, 144);
			this.chkGenerateDataObjects.Name = "chkGenerateDataObjects";
			this.chkGenerateDataObjects.Size = new System.Drawing.Size(192, 16);
			this.chkGenerateDataObjects.TabIndex = 34;
			this.chkGenerateDataObjects.Text = "Generate Data Objects";
			// 
			// chkUseViewsInStoreProc
			// 
			this.chkUseViewsInStoreProc.Enabled = false;
			this.chkUseViewsInStoreProc.Location = new System.Drawing.Point(28, 120);
			this.chkUseViewsInStoreProc.Name = "chkUseViewsInStoreProc";
			this.chkUseViewsInStoreProc.Size = new System.Drawing.Size(232, 16);
			this.chkUseViewsInStoreProc.TabIndex = 33;
			this.chkUseViewsInStoreProc.Text = "Use Views in Stored Procedures";
			// 
			// chkCreateViews
			// 
			this.chkCreateViews.Location = new System.Drawing.Point(4, 96);
			this.chkCreateViews.Name = "chkCreateViews";
			this.chkCreateViews.Size = new System.Drawing.Size(104, 16);
			this.chkCreateViews.TabIndex = 32;
			this.chkCreateViews.Text = "Create Views";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(200, 16);
			this.label1.TabIndex = 31;
			this.label1.Text = "Stored Procedure Name format string:";
			// 
			// txtStoreProcNameFormatString
			// 
			this.txtStoreProcNameFormatString.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtStoreProcNameFormatString.Location = new System.Drawing.Point(4, 64);
			this.txtStoreProcNameFormatString.Name = "txtStoreProcNameFormatString";
			this.txtStoreProcNameFormatString.Size = new System.Drawing.Size(332, 20);
			this.txtStoreProcNameFormatString.TabIndex = 30;
			this.txtStoreProcNameFormatString.Text = "proc{%TABLE_NAME%}{%PROC_TYPE%}";
			// 
			// chkSingleFile
			// 
			this.chkSingleFile.Location = new System.Drawing.Point(4, 24);
			this.chkSingleFile.Name = "chkSingleFile";
			this.chkSingleFile.Size = new System.Drawing.Size(176, 16);
			this.chkSingleFile.TabIndex = 29;
			this.chkSingleFile.Text = "Script as single file";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.chkBlankPassword,
																					this.lblPassword,
																					this.txtPassword,
																					this.lblUserID,
																					this.txtUserID,
																					this.lblDatabaseName,
																					this.txtDatabaseName,
																					this.lblServerName,
																					this.txtServerName});
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(344, 136);
			this.groupBox2.TabIndex = 30;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Connection Information";
			// 
			// chkBlankPassword
			// 
			this.chkBlankPassword.Location = new System.Drawing.Point(104, 112);
			this.chkBlankPassword.Name = "chkBlankPassword";
			this.chkBlankPassword.Size = new System.Drawing.Size(136, 16);
			this.chkBlankPassword.TabIndex = 30;
			this.chkBlankPassword.Text = "Use blank password";
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(8, 96);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(88, 13);
			this.lblPassword.TabIndex = 29;
			this.lblPassword.Text = "Password:";
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtPassword.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPassword.Location = new System.Drawing.Point(104, 88);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(232, 22);
			this.txtPassword.TabIndex = 26;
			this.txtPassword.Text = "";
			// 
			// lblUserID
			// 
			this.lblUserID.Location = new System.Drawing.Point(8, 72);
			this.lblUserID.Name = "lblUserID";
			this.lblUserID.Size = new System.Drawing.Size(88, 13);
			this.lblUserID.TabIndex = 28;
			this.lblUserID.Text = "User ID:";
			// 
			// txtUserID
			// 
			this.txtUserID.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtUserID.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtUserID.Location = new System.Drawing.Point(104, 64);
			this.txtUserID.Name = "txtUserID";
			this.txtUserID.Size = new System.Drawing.Size(232, 22);
			this.txtUserID.TabIndex = 25;
			this.txtUserID.Text = "";
			// 
			// lblDatabaseName
			// 
			this.lblDatabaseName.Location = new System.Drawing.Point(8, 48);
			this.lblDatabaseName.Name = "lblDatabaseName";
			this.lblDatabaseName.Size = new System.Drawing.Size(88, 13);
			this.lblDatabaseName.TabIndex = 27;
			this.lblDatabaseName.Text = "Database Name:";
			// 
			// txtDatabaseName
			// 
			this.txtDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtDatabaseName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtDatabaseName.Location = new System.Drawing.Point(104, 40);
			this.txtDatabaseName.Name = "txtDatabaseName";
			this.txtDatabaseName.Size = new System.Drawing.Size(232, 22);
			this.txtDatabaseName.TabIndex = 24;
			this.txtDatabaseName.Text = "";
			// 
			// lblServerName
			// 
			this.lblServerName.Location = new System.Drawing.Point(8, 24);
			this.lblServerName.Name = "lblServerName";
			this.lblServerName.Size = new System.Drawing.Size(88, 13);
			this.lblServerName.TabIndex = 23;
			this.lblServerName.Text = "Server Name:";
			// 
			// txtServerName
			// 
			this.txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtServerName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtServerName.Location = new System.Drawing.Point(104, 16);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(232, 22);
			this.txtServerName.TabIndex = 22;
			this.txtServerName.Text = "";
			// 
			// txtProjectNamespace
			// 
			this.txtProjectNamespace.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtProjectNamespace.Location = new System.Drawing.Point(120, 184);
			this.txtProjectNamespace.Name = "txtProjectNamespace";
			this.txtProjectNamespace.Size = new System.Drawing.Size(216, 20);
			this.txtProjectNamespace.TabIndex = 36;
			this.txtProjectNamespace.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 192);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 37;
			this.label2.Text = "Project Namespace";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(358, 466);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupBox2,
																		  this.groupBox1,
																		  this.lnkEverythingSQL,
																		  this.btnOK,
																		  this.btnClose});
			this.MaximumSize = new System.Drawing.Size(1600, 500);
			this.MinimumSize = new System.Drawing.Size(366, 500);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Data Tier Generator";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new frmMain());
		}

		private void EnableOK()	{
			if (txtServerName.Text.Length == 0 || txtDatabaseName.Text.Length == 0 || txtUserID.Text.Length == 0 || (!chkBlankPassword.Checked & txtPassword.Text.Length == 0))
				btnOK.Enabled = false;
			else
				btnOK.Enabled = true;
		}

		private void txtServerName_TextChanged(object sender, System.EventArgs e) {
			EnableOK();
		}

		private void txtDatabaseName_TextChanged(object sender, System.EventArgs e) {
			EnableOK();
		}

		private void txtUserID_TextChanged(object sender, System.EventArgs e) {
			EnableOK();
		}

		private void txtPassword_TextChanged(object sender, System.EventArgs e) {
			EnableOK();
		}

		private void btnOK_Click(object sender, System.EventArgs e) {
			StringBuilder	objStringBuilder;
			Generator	objGenerator;

			try {
				// Build the database connection string
				objStringBuilder = new StringBuilder(255);
				objStringBuilder.Append("Data Source = " + txtServerName.Text + ";");
				objStringBuilder.Append("Initial Catalog = " + txtDatabaseName.Text + ";");
				objStringBuilder.Append("User ID = " + txtUserID.Text + ";");
				objStringBuilder.Append("Password = " + txtPassword.Text + ";");
				
				// Generate the SQL and C#
                Configuration options = new Configuration();
                options.ConnectionString = objStringBuilder.ToString();
                options.StoredProcNameFormat = txtStoreProcNameFormatString.Text;
                options.SingleFile = chkSingleFile.Checked;
                options.CreateViews = chkCreateViews.Checked;
                options.UseViews = chkUseViewsInStoreProc.Checked;
                options.CreateDataObjects = chkGenerateDataObjects.Checked;
                options.ScriptDropStatement = chkScriptDropStatement.Checked;
                options.ProjectNameSpace = txtProjectNamespace.Text;

				objGenerator = new Generator(options);
				objGenerator.ProcessTables();
				objGenerator = null;

				// Alert the user everything went ok
				MessageBox.Show("Data tier generated successfully.");
			} catch (Exception objException) {
				MessageBox.Show("An error occcurred while generating the data tier.\n\n" + objException.Message);
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e) {
			this.Close();
		}

		private void lnkEverythingSQL_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("http://www.EverythingSQL.com");
		}

		private void chkBlankPassword_CheckedChanged(object sender, System.EventArgs e) {
			if (chkBlankPassword.Checked)
				txtPassword.Enabled = false;
			else
				txtPassword.Enabled = true;
			EnableOK();
		}

		private void chkCreateViews_CheckedChanged(object sender, System.EventArgs e) {
			if (chkCreateViews.Checked)
				chkUseViewsInStoreProc.Enabled = true;
			else
				chkUseViewsInStoreProc.Enabled = false;
		}

		private void frmMain_Load(object sender, System.EventArgs e) {
			txtServerName.Text = "hal";
			txtDatabaseName.Text = "cort_seamless";
			txtUserID.Text = "sa";
            txtPassword.Text = "";
			chkBlankPassword.Checked = true;

			txtProjectNamespace.Text = "Seamless.Manhattan";

			chkCreateViews.Checked = true;
			chkUseViewsInStoreProc.Checked = true;
			chkGenerateDataObjects.Checked = true;
			chkScriptDropStatement.Checked = true;

			EnableOK();
			chkCreateViews_CheckedChanged(null, null);
		}

	}
}
