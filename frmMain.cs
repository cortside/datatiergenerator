using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

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
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Button loadXml;
	private System.Windows.Forms.TextBox xmlConfigFilename;

	private Configuration config;
	private System.Windows.Forms.Button generateEntitesXML;
	private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtRootNamespace;

		#region stuff that cort does not want to see
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
	    this.label2 = new System.Windows.Forms.Label();
	    this.txtRootNamespace = new System.Windows.Forms.TextBox();
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
	    this.loadXml = new System.Windows.Forms.Button();
	    this.xmlConfigFilename = new System.Windows.Forms.TextBox();
	    this.generateEntitesXML = new System.Windows.Forms.Button();
	    this.button1 = new System.Windows.Forms.Button();
	    this.groupBox1.SuspendLayout();
	    this.groupBox2.SuspendLayout();
	    this.SuspendLayout();
	    // 
	    // btnOK
	    // 
	    this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.btnOK.Enabled = false;
	    this.btnOK.Location = new System.Drawing.Point(202, 608);
	    this.btnOK.Name = "btnOK";
	    this.btnOK.Size = new System.Drawing.Size(72, 23);
	    this.btnOK.TabIndex = 15;
	    this.btnOK.Text = "&OK";
	    this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
	    // 
	    // btnClose
	    // 
	    this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.btnClose.Location = new System.Drawing.Point(282, 608);
	    this.btnClose.Name = "btnClose";
	    this.btnClose.Size = new System.Drawing.Size(72, 23);
	    this.btnClose.TabIndex = 16;
	    this.btnClose.Text = "&Close";
	    this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
	    // 
	    // lnkEverythingSQL
	    // 
	    this.lnkEverythingSQL.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
	    this.lnkEverythingSQL.Location = new System.Drawing.Point(8, 648);
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
										    this.txtRootNamespace,
										    this.chkScriptDropStatement,
										    this.chkGenerateDataObjects,
										    this.chkUseViewsInStoreProc,
										    this.chkCreateViews,
										    this.label1,
										    this.txtStoreProcNameFormatString,
										    this.chkSingleFile});
	    this.groupBox1.Location = new System.Drawing.Point(8, 168);
	    this.groupBox1.Name = "groupBox1";
	    this.groupBox1.Size = new System.Drawing.Size(362, 296);
	    this.groupBox1.TabIndex = 29;
	    this.groupBox1.TabStop = false;
	    this.groupBox1.Text = "Generation Options";
	    // 
	    // label2
	    // 
	    this.label2.Location = new System.Drawing.Point(8, 192);
	    this.label2.Name = "label2";
	    this.label2.Size = new System.Drawing.Size(104, 16);
	    this.label2.TabIndex = 37;
	    this.label2.Text = "Project Namespace";
	    // 
	    // txtRootNamespace
	    // 
	    this.txtRootNamespace.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
		| System.Windows.Forms.AnchorStyles.Right);
	    this.txtRootNamespace.Location = new System.Drawing.Point(120, 184);
	    this.txtRootNamespace.Name = "txtRootNamespace";
	    this.txtRootNamespace.Size = new System.Drawing.Size(234, 20);
	    this.txtRootNamespace.TabIndex = 36;
	    this.txtRootNamespace.Text = "";
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
	    this.txtStoreProcNameFormatString.Size = new System.Drawing.Size(350, 20);
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
	    this.groupBox2.Size = new System.Drawing.Size(362, 136);
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
	    this.txtPassword.Size = new System.Drawing.Size(250, 22);
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
	    this.txtUserID.Size = new System.Drawing.Size(250, 22);
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
	    this.txtDatabaseName.Size = new System.Drawing.Size(250, 22);
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
	    this.txtServerName.Size = new System.Drawing.Size(250, 22);
	    this.txtServerName.TabIndex = 22;
	    this.txtServerName.Text = "";
	    // 
	    // loadXml
	    // 
	    this.loadXml.Location = new System.Drawing.Point(272, 480);
	    this.loadXml.Name = "loadXml";
	    this.loadXml.TabIndex = 31;
	    this.loadXml.Text = "Load XML";
	    this.loadXml.Click += new System.EventHandler(this.loadXml_Click);
	    // 
	    // xmlConfigFilename
	    // 
	    this.xmlConfigFilename.Location = new System.Drawing.Point(8, 480);
	    this.xmlConfigFilename.Name = "xmlConfigFilename";
	    this.xmlConfigFilename.Size = new System.Drawing.Size(256, 20);
	    this.xmlConfigFilename.TabIndex = 32;
	    this.xmlConfigFilename.Text = "";
	    // 
	    // generateEntitesXML
	    // 
	    this.generateEntitesXML.Location = new System.Drawing.Point(256, 552);
	    this.generateEntitesXML.Name = "generateEntitesXML";
	    this.generateEntitesXML.Size = new System.Drawing.Size(88, 23);
	    this.generateEntitesXML.TabIndex = 33;
	    this.generateEntitesXML.Text = "Generate XML";
	    this.generateEntitesXML.Click += new System.EventHandler(this.generateEntitesXML_Click);
	    // 
	    // button1
	    // 
	    this.button1.Location = new System.Drawing.Point(64, 552);
	    this.button1.Name = "button1";
	    this.button1.TabIndex = 34;
	    this.button1.Text = "button1";
	    this.button1.Click += new System.EventHandler(this.button1_Click);
	    // 
	    // frmMain
	    // 
	    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
	    this.ClientSize = new System.Drawing.Size(376, 666);
	    this.Controls.AddRange(new System.Windows.Forms.Control[] {
									  this.button1,
									  this.generateEntitesXML,
									  this.xmlConfigFilename,
									  this.loadXml,
									  this.groupBox2,
									  this.groupBox1,
									  this.lnkEverythingSQL,
									  this.btnOK,
									  this.btnClose});
	    this.MaximumSize = new System.Drawing.Size(1800, 700);
	    this.MinimumSize = new System.Drawing.Size(366, 700);
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
	static void Main(string[] args) {

	    FileStream fs = new FileStream("DataTierGenerator.log", FileMode.Create);
	    StreamWriter sw = new StreamWriter(fs);
	    Console.SetOut(sw);

	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine("Start :: " + DateTime.Now.ToString());
	    Console.WriteLine("Number of command line parameters = {0}", args.Length);
	    foreach(string s in args) {
		Console.WriteLine(s);
	    }
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

	    // decide whether to parse XML file and go or bring up form
	    if (args.Length==1) {
		XmlDocument doc = new XmlDocument();
			
		doc.Load(args[0]);
		XmlNode root = doc.DocumentElement["config"];
		Configuration config;
		if (root != null) {
		    config = new Configuration(root);
		    config.XmlConfigFilename = args[0];
		    Generator generator = new Generator(config);
		    generator.GenerateSource();
		} else {
		    Console.Out.WriteLine("No configuration section found in config file.");
		}
	    } else {
		Application.Run(new frmMain());
	    }

	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine("Done :: " + DateTime.Now.ToString());
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

	    sw.Close();
	}
#endregion


		#region Button click handlers
	private void EnableOK()	{
	    if (txtServerName.Text.Length == 0 || txtDatabaseName.Text.Length == 0 || txtUserID.Text.Length == 0)
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
	    Generator objGenerator;

	    //			try {
	    GetDataFromForm();

	    objGenerator = new Generator(config);
	    objGenerator.GenerateSource();
	    objGenerator = null;

	    // Alert the user everything went ok
	    MessageBox.Show("Data tier generated successfully.");
	    //			} catch (Exception objException) {
	    //				MessageBox.Show("An error occcurred while generating the data tier.\n\n" + objException.Message);
	    //			}
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

		#endregion

	private void GetDataFromForm() {
	    // update with form settings
	    config.Server = txtServerName.Text;
	    config.Database = txtDatabaseName.Text;
	    config.User = txtUserID.Text;
	    config.Password = txtPassword.Text;
	    config.StoredProcNameFormat = txtStoreProcNameFormatString.Text;
	    config.SingleFile = chkSingleFile.Checked;
	    config.GenerateSqlViewScripts = chkCreateViews.Checked;
	    config.UseViews = chkUseViewsInStoreProc.Checked;
	    config.GenerateDataObjectClasses = chkGenerateDataObjects.Checked;
	    config.ScriptDropStatement = chkScriptDropStatement.Checked;
	    config.RootNameSpace = txtRootNamespace.Text;
	    config.XmlConfigFilename = xmlConfigFilename.Text;
	}

	private void PopulateForm() {
	    // update with form settings
	    txtServerName.Text = config.Server;
	    txtDatabaseName.Text = config.Database;
	    txtUserID.Text = config.User;
	    txtPassword.Text = config.Password;
	    txtStoreProcNameFormatString.Text = config.StoredProcNameFormat;
	    chkSingleFile.Checked = config.SingleFile;
	    chkCreateViews.Checked = config.GenerateSqlViewScripts;
	    chkUseViewsInStoreProc.Checked = config.UseViews;
	    chkGenerateDataObjects.Checked = config.GenerateDataObjectClasses;
	    chkScriptDropStatement.Checked = config.ScriptDropStatement;
	    txtRootNamespace.Text = config.RootNameSpace;
	    xmlConfigFilename.Text = config.XmlConfigFilename;
	    EnableOK();
	}

	private void frmMain_Load(object sender, System.EventArgs e) {
	    config = new Configuration();
	    //config.XmlConfigFilename = "..\\..\\dtg-config.xml";
	    //config.XmlConfigFilename = "C:\\Data\\work\\seamlessweb\\manhattan\\src\\DataTierGenerator.config.xml";
	    PopulateForm();

	    if (config.XmlConfigFilename.Length>0) {
		loadXml_Click(null, null);
	    }
	}

	private void loadXml_Click(object sender, System.EventArgs e) {
	    //			XmlDocument doc = new XmlDocument();
	    //
	    //			doc.Load("..\\..\\orders.xml");
	    //			XslTransform tfm = new XslTransform();
	    //			System.IO.StringWriter newDoc = new System.IO.StringWriter();
	    //			tfm.Load("..\\..\\transform.xslt");
	    //			tfm.Transform(doc.CreateNavigator(), new System.Xml.Xsl.XsltArgumentList(), newDoc);
	    //Console.Out.WriteLine(newDoc.ToString());

	    //doc.Load("..\\..\\config.xml");
	    //EnumerateEntities(doc.DocumentElement["entities"]);
	    //EnumerateEntities(doc.DocumentElement["enums"]);

	    //Configuration config = GetDataFromForm();
	    //Console.Out.WriteLine(config.ToString());

	    XmlDocument doc = new XmlDocument();
			
	    doc.Load(xmlConfigFilename.Text);
	    XmlNode root = doc.DocumentElement["config"];
	    if (root != null) {
		config = new Configuration(root);
		config.XmlConfigFilename = xmlConfigFilename.Text;
		PopulateForm();
	    }
	}

	private void EnumerateEntities(XmlNode root) {
	    for (Int32 i = 0; i< root.ChildNodes.Count; i++) {
		XmlNode nodeOuter = root.ChildNodes[i];
		Console.Out.WriteLine(nodeOuter.Name);
		if (nodeOuter.HasChildNodes) {
		    for (Int32 j =0; j<nodeOuter.ChildNodes.Count; j++) {
			XmlNode nodeInner = nodeOuter.ChildNodes[j];
			Console.Out.WriteLine("\t" + nodeInner.Name + " " + nodeInner.InnerText);
			if (nodeInner.Attributes != null) {
			    for (Int32 k =0; k<nodeInner.Attributes.Count; k++) {
				Console.Out.WriteLine("\t\t" + nodeInner.Attributes[k].Name + " " + nodeInner.Attributes[k].Value);
			    }
			}
		    }
		}
	    }
	}

	private void generateEntitesXML_Click(object sender, System.EventArgs e) {
	    Generator objGenerator;

	    GetDataFromForm();

	    objGenerator = new Generator(config);
	    objGenerator.GenerateXML();
	    objGenerator = null;

	    // Alert the user everything went ok
	    MessageBox.Show("Data tier generated successfully.");
	}

	private void button1_Click(object sender, System.EventArgs e) {
	    
	}



    }
}
