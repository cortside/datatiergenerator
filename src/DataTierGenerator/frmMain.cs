using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

using Spring2.DataTierGenerator.Generator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.DataTierGenerator {
    /// <summary>
    /// Summary description for frmMain.
    /// </summary>
    public class frmMain : System.Windows.Forms.Form {
	private System.Windows.Forms.Button btnOK;
	private System.Windows.Forms.Button btnClose;
	private System.Windows.Forms.LinkLabel lnkEverythingSQL;
	private System.Windows.Forms.Button loadXml;
	private System.Windows.Forms.TextBox xmlConfigFilename;

        private System.Windows.Forms.OpenFileDialog openFileDialog;
	private Boolean isXmlLoaded = false;
	
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
	    this.loadXml = new System.Windows.Forms.Button();
	    this.xmlConfigFilename = new System.Windows.Forms.TextBox();
	    this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
	    this.SuspendLayout();
	    // 
	    // btnOK
	    // 
	    this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.btnOK.Enabled = false;
	    this.btnOK.Location = new System.Drawing.Point(368, 40);
	    this.btnOK.Name = "btnOK";
	    this.btnOK.Size = new System.Drawing.Size(64, 23);
	    this.btnOK.TabIndex = 15;
	    this.btnOK.Text = "&OK";
	    this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
	    // 
	    // btnClose
	    // 
	    this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.btnClose.Location = new System.Drawing.Point(440, 40);
	    this.btnClose.Name = "btnClose";
	    this.btnClose.Size = new System.Drawing.Size(64, 23);
	    this.btnClose.TabIndex = 16;
	    this.btnClose.Text = "&Close";
	    this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
	    // 
	    // lnkEverythingSQL
	    // 
	    this.lnkEverythingSQL.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
		| System.Windows.Forms.AnchorStyles.Right);
	    this.lnkEverythingSQL.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
	    this.lnkEverythingSQL.Location = new System.Drawing.Point(8, 69);
	    this.lnkEverythingSQL.Name = "lnkEverythingSQL";
	    this.lnkEverythingSQL.Size = new System.Drawing.Size(496, 23);
	    this.lnkEverythingSQL.TabIndex = 20;
	    this.lnkEverythingSQL.TabStop = true;
	    this.lnkEverythingSQL.Text = "By Adrian, Cort and Dave.  (Help, and your name could be here too)";
	    this.lnkEverythingSQL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
	    this.lnkEverythingSQL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEverythingSQL_LinkClicked);
	    // 
	    // loadXml
	    // 
	    this.loadXml.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.loadXml.Location = new System.Drawing.Point(480, 8);
	    this.loadXml.Name = "loadXml";
	    this.loadXml.Size = new System.Drawing.Size(24, 24);
	    this.loadXml.TabIndex = 31;
	    this.loadXml.Text = "...";
	    this.loadXml.Click += new System.EventHandler(this.fileBrowse_Click);
	    // 
	    // xmlConfigFilename
	    // 
	    this.xmlConfigFilename.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
		| System.Windows.Forms.AnchorStyles.Right);
	    this.xmlConfigFilename.Location = new System.Drawing.Point(8, 8);
	    this.xmlConfigFilename.Name = "xmlConfigFilename";
	    this.xmlConfigFilename.Size = new System.Drawing.Size(464, 20);
	    this.xmlConfigFilename.TabIndex = 32;
	    this.xmlConfigFilename.Text = "";
	    // 
	    // openFileDialog
	    // 
	    this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.BrowserSelection);
	    // 
	    // frmMain
	    // 
	    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
	    this.ClientSize = new System.Drawing.Size(512, 91);
	    this.Controls.AddRange(new System.Windows.Forms.Control[] {
									  this.xmlConfigFilename,
									  this.loadXml,
									  this.lnkEverythingSQL,
									  this.btnOK,
									  this.btnClose});
	    this.MaximumSize = new System.Drawing.Size(1800, 125);
	    this.MinimumSize = new System.Drawing.Size(366, 125);
	    this.Name = "frmMain";
	    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
	    this.Text = "Data Tier Generator";
	    this.Load += new System.EventHandler(this.frmMain_Load);
	    this.ResumeLayout(false);

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
		CodeGenerator g = new CodeGenerator(args[0]);
		g.Generate();
	    } else {
		Application.Run(new frmMain());
	    }

	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine("Done :: " + DateTime.Now.ToString());
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

	    sw.Close();
	}

	private void EnableOK()	{
	    if (!isXmlLoaded)
		btnOK.Enabled = false;
	    else
		btnOK.Enabled = true;
	}

	private void btnOK_Click(object sender, System.EventArgs e) {
	    try {
		CodeGenerator g = new CodeGenerator(xmlConfigFilename.Text);
		g.Generate();

		// Alert the user everything went ok
		MessageBox.Show("Data tier generated successfully.");
	    } catch (Exception ex) {
		MessageBox.Show("An error occcurred while generating.\n\n" + ex.ToString());
		Console.Out.WriteLine("An error occcurred while generating.\n\n" + ex.ToString());
	    }
	}

	private void btnClose_Click(object sender, System.EventArgs e) {
	    this.Close();
	}

	private void lnkEverythingSQL_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e) {
	    System.Diagnostics.Process.Start("http://www.EverythingSQL.com");
	}

	private void frmMain_Load(object sender, System.EventArgs e) {
	    xmlConfigFilename.Text = System.Environment.CurrentDirectory;
	}

	//start file browser 
	private void fileBrowse_Click(object sender, System.EventArgs e) {
	    openFileDialog.Multiselect = false;
	    openFileDialog.Filter = "XML(*.xml)|*.xml";
	    openFileDialog.InitialDirectory = xmlConfigFilename.Text;
	    openFileDialog.ShowDialog(this);
	}

	//clicked on from file browser, clicking cancel wil return from the showDialog
	private void BrowserSelection(object sender, System.ComponentModel.CancelEventArgs e) {			
	    xmlConfigFilename.Text = openFileDialog.FileName;
	    isXmlLoaded = true;
	    EnableOK();
	    openFileDialog.Dispose();
	}


    }
}
