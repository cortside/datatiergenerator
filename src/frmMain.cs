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
	private System.Windows.Forms.GroupBox groupBox2;
	private System.Windows.Forms.Label lblPassword;
	private System.Windows.Forms.TextBox txtPassword;
	private System.Windows.Forms.Label lblUserID;
	private System.Windows.Forms.TextBox txtUserID;
	private System.Windows.Forms.Label lblDatabaseName;
	private System.Windows.Forms.TextBox txtDatabaseName;
	private System.Windows.Forms.Label lblServerName;
	private System.Windows.Forms.TextBox txtServerName;
	private System.Windows.Forms.Button loadXml;
	private System.Windows.Forms.TextBox xmlConfigFilename;

	private Configuration config;
	private System.Windows.Forms.Button generateEntitesXML;

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
	    this.groupBox2 = new System.Windows.Forms.GroupBox();
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
	    this.groupBox2.SuspendLayout();
	    this.SuspendLayout();
	    // 
	    // btnOK
	    // 
	    this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.btnOK.Enabled = false;
	    this.btnOK.Location = new System.Drawing.Point(232, 168);
	    this.btnOK.Name = "btnOK";
	    this.btnOK.Size = new System.Drawing.Size(64, 23);
	    this.btnOK.TabIndex = 15;
	    this.btnOK.Text = "&OK";
	    this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
	    // 
	    // btnClose
	    // 
	    this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.btnClose.Location = new System.Drawing.Point(304, 168);
	    this.btnClose.Name = "btnClose";
	    this.btnClose.Size = new System.Drawing.Size(64, 23);
	    this.btnClose.TabIndex = 16;
	    this.btnClose.Text = "&Close";
	    this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
	    // 
	    // lnkEverythingSQL
	    // 
	    this.lnkEverythingSQL.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
	    this.lnkEverythingSQL.Location = new System.Drawing.Point(8, 192);
	    this.lnkEverythingSQL.Name = "lnkEverythingSQL";
	    this.lnkEverythingSQL.Size = new System.Drawing.Size(360, 23);
	    this.lnkEverythingSQL.TabIndex = 20;
	    this.lnkEverythingSQL.TabStop = true;
	    this.lnkEverythingSQL.Text = "By Adrian, Cort and Dave.  (Help, and your name could be here too)";
	    this.lnkEverythingSQL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	    this.lnkEverythingSQL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEverythingSQL_LinkClicked);
	    // 
	    // groupBox2
	    // 
	    this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
		| System.Windows.Forms.AnchorStyles.Right);
	    this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
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
	    this.groupBox2.Size = new System.Drawing.Size(362, 120);
	    this.groupBox2.TabIndex = 30;
	    this.groupBox2.TabStop = false;
	    this.groupBox2.Text = "Connection Information";
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
	    this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
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
	    this.txtUserID.TextChanged += new System.EventHandler(this.txtUserID_TextChanged);
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
	    this.txtDatabaseName.TextChanged += new System.EventHandler(this.txtDatabaseName_TextChanged);
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
	    this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
	    // 
	    // loadXml
	    // 
	    this.loadXml.Location = new System.Drawing.Point(304, 136);
	    this.loadXml.Name = "loadXml";
	    this.loadXml.Size = new System.Drawing.Size(64, 24);
	    this.loadXml.TabIndex = 31;
	    this.loadXml.Text = "Load XML";
	    this.loadXml.Click += new System.EventHandler(this.loadXml_Click);
	    // 
	    // xmlConfigFilename
	    // 
	    this.xmlConfigFilename.Location = new System.Drawing.Point(8, 136);
	    this.xmlConfigFilename.Name = "xmlConfigFilename";
	    this.xmlConfigFilename.Size = new System.Drawing.Size(288, 20);
	    this.xmlConfigFilename.TabIndex = 32;
	    this.xmlConfigFilename.Text = "";
	    // 
	    // generateEntitesXML
	    // 
	    this.generateEntitesXML.Location = new System.Drawing.Point(8, 168);
	    this.generateEntitesXML.Name = "generateEntitesXML";
	    this.generateEntitesXML.Size = new System.Drawing.Size(88, 23);
	    this.generateEntitesXML.TabIndex = 33;
	    this.generateEntitesXML.Text = "Generate XML";
	    this.generateEntitesXML.Click += new System.EventHandler(this.generateEntitesXML_Click);
	    // 
	    // frmMain
	    // 
	    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
	    this.ClientSize = new System.Drawing.Size(376, 216);
	    this.Controls.AddRange(new System.Windows.Forms.Control[] {
									  this.generateEntitesXML,
									  this.xmlConfigFilename,
									  this.loadXml,
									  this.groupBox2,
									  this.lnkEverythingSQL,
									  this.btnOK,
									  this.btnClose});
	    this.MaximumSize = new System.Drawing.Size(1800, 700);
	    this.MinimumSize = new System.Drawing.Size(366, 250);
	    this.Name = "frmMain";
	    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
	    this.Text = "Data Tier Generator";
	    this.Load += new System.EventHandler(this.frmMain_Load);
	    this.groupBox2.ResumeLayout(false);
	    this.ResumeLayout(false);

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

	    try {
		GetDataFromForm();

		objGenerator = new Generator(config);
		objGenerator.GenerateSource();
		objGenerator = null;

		// Alert the user everything went ok
		MessageBox.Show("Data tier generated successfully.");
	    } catch (Exception objException) {
		MessageBox.Show("An error occcurred while generating.\n\n" + objException.Message);
		Console.Out.WriteLine("An error occcurred while generating.\n\n" + objException.Message);
	    }
	}

	private void btnClose_Click(object sender, System.EventArgs e) {
	    this.Close();
	}

	private void lnkEverythingSQL_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
	    System.Diagnostics.Process.Start("http://www.EverythingSQL.com");
	}

	private void loadXml_Click(object sender, System.EventArgs e) {
	    XmlDocument doc = new XmlDocument();
			
	    doc.Load(xmlConfigFilename.Text);
	    XmlNode root = doc.DocumentElement["config"];
	    if (root != null) {
		config = new Configuration(root);
		config.XmlConfigFilename = xmlConfigFilename.Text;
		PopulateForm();
	    }
	}

	private void generateEntitesXML_Click(object sender, System.EventArgs e) {
	    Generator objGenerator;

	    GetDataFromForm();

	    objGenerator = new Generator(config);
	    objGenerator.GenerateXML();
	    objGenerator = null;

	    // Alert the user everything went ok
	    MessageBox.Show("Xml generated successfully.");
	}

	#endregion


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

	private void GetDataFromForm() {
	    // update with form settings
	    config.Server = txtServerName.Text;
	    config.Database = txtDatabaseName.Text;
	    config.User = txtUserID.Text;
	    config.Password = txtPassword.Text;
	    config.XmlConfigFilename = xmlConfigFilename.Text;
	}

	private void PopulateForm() {
	    // update with form settings
	    txtServerName.Text = config.Server;
	    txtDatabaseName.Text = config.Database;
	    txtUserID.Text = config.User;
	    txtPassword.Text = config.Password;
	    xmlConfigFilename.Text = config.XmlConfigFilename;
	    EnableOK();
	}

	private void frmMain_Load(object sender, System.EventArgs e) {
	    config = new Configuration();
	    //config.XmlConfigFilename = "..\\dtg-config.xml";
	    //config.XmlConfigFilename = "C:\\Data\\work\\seamlessweb\\manhattan\\src\\DataTierGenerator.config.xml";
	    PopulateForm();

	    if (config.XmlConfigFilename.Length>0) {
		loadXml_Click(null, null);
	    }
	}

    }
}
