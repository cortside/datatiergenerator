//by Joe Miguel on 4-14-2002
//use as you wish, just give me some credit if you
//improve it (and tell me too, i might want a copy)!

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Xml.Schema;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Summary description for frmMain.
    /// </summary>
    public class frmMain : System.Windows.Forms.Form {
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;
	private static bool isValid;
	private static string error;
	private static string header;

	private IList entities = new ArrayList();
	private IList sqlentities = new ArrayList();
	private ICollection types = new ArrayList();
	private ICollection sqltypes = new ArrayList();
	private IList enums = new ArrayList();
	private IList collections = new ArrayList();

	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Button validate;
	private System.Windows.Forms.Label result;
	private System.Windows.Forms.Button fileBrowse;
	private System.Windows.Forms.OpenFileDialog openFileDialog;
	private System.Windows.Forms.RichTextBox resultErrors;
	private System.Windows.Forms.RadioButton documentTypeSchema;
	private System.Windows.Forms.RadioButton documentTypeDTD;
	private System.Windows.Forms.RadioButton documentTypeNone;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
	private System.Windows.Forms.TextBox file;

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
		if (components != null) {
		    components.Dispose();
		}
	    }
	    base.Dispose( disposing );
	}

		#region Windows Form Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
	    this.label1 = new System.Windows.Forms.Label();
	    this.file = new System.Windows.Forms.TextBox();
	    this.validate = new System.Windows.Forms.Button();
	    this.result = new System.Windows.Forms.Label();
	    this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
	    this.fileBrowse = new System.Windows.Forms.Button();
	    this.resultErrors = new System.Windows.Forms.RichTextBox();
	    this.documentTypeSchema = new System.Windows.Forms.RadioButton();
	    this.documentTypeDTD = new System.Windows.Forms.RadioButton();
	    this.documentTypeNone = new System.Windows.Forms.RadioButton();
	    this.treeView1 = new System.Windows.Forms.TreeView();
	    this.splitter1 = new System.Windows.Forms.Splitter();
	    this.listView1 = new System.Windows.Forms.ListView();
	    this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
	    this.SuspendLayout();
	    // 
	    // label1
	    // 
	    this.label1.Location = new System.Drawing.Point(8, 16);
	    this.label1.Name = "label1";
	    this.label1.Size = new System.Drawing.Size(100, 16);
	    this.label1.TabIndex = 0;
	    this.label1.Text = "XML filename";
	    // 
	    // file
	    // 
	    this.file.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
		| System.Windows.Forms.AnchorStyles.Right);
	    this.file.Location = new System.Drawing.Point(8, 32);
	    this.file.Name = "file";
	    this.file.Size = new System.Drawing.Size(712, 20);
	    this.file.TabIndex = 1;
	    this.file.Text = "";
	    this.file.WordWrap = false;
	    // 
	    // validate
	    // 
	    this.validate.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
	    this.validate.Location = new System.Drawing.Point(640, 456);
	    this.validate.Name = "validate";
	    this.validate.Size = new System.Drawing.Size(112, 24);
	    this.validate.TabIndex = 2;
	    this.validate.Text = "&Validate";
	    this.validate.Click += new System.EventHandler(this.validate_Click);
	    // 
	    // result
	    // 
	    this.result.Location = new System.Drawing.Point(216, 8);
	    this.result.Name = "result";
	    this.result.Size = new System.Drawing.Size(232, 16);
	    this.result.TabIndex = 3;
	    // 
	    // openFileDialog
	    // 
	    this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.BrowserSelection);
	    // 
	    // fileBrowse
	    // 
	    this.fileBrowse.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
	    this.fileBrowse.Location = new System.Drawing.Point(728, 32);
	    this.fileBrowse.Name = "fileBrowse";
	    this.fileBrowse.Size = new System.Drawing.Size(24, 20);
	    this.fileBrowse.TabIndex = 5;
	    this.fileBrowse.Text = "...";
	    this.fileBrowse.Click += new System.EventHandler(this.fileBrowse_Click);
	    // 
	    // resultErrors
	    // 
	    this.resultErrors.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
		| System.Windows.Forms.AnchorStyles.Right);
	    this.resultErrors.Location = new System.Drawing.Point(8, 368);
	    this.resultErrors.Name = "resultErrors";
	    this.resultErrors.Size = new System.Drawing.Size(744, 80);
	    this.resultErrors.TabIndex = 6;
	    this.resultErrors.Text = "";
	    // 
	    // documentTypeSchema
	    // 
	    this.documentTypeSchema.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
	    this.documentTypeSchema.Checked = true;
	    this.documentTypeSchema.Location = new System.Drawing.Point(8, 456);
	    this.documentTypeSchema.Name = "documentTypeSchema";
	    this.documentTypeSchema.Size = new System.Drawing.Size(64, 24);
	    this.documentTypeSchema.TabIndex = 7;
	    this.documentTypeSchema.TabStop = true;
	    this.documentTypeSchema.Text = "Schema";
	    this.documentTypeSchema.Visible = false;
	    // 
	    // documentTypeDTD
	    // 
	    this.documentTypeDTD.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
	    this.documentTypeDTD.Location = new System.Drawing.Point(96, 456);
	    this.documentTypeDTD.Name = "documentTypeDTD";
	    this.documentTypeDTD.Size = new System.Drawing.Size(48, 24);
	    this.documentTypeDTD.TabIndex = 8;
	    this.documentTypeDTD.Text = "DTD";
	    this.documentTypeDTD.Visible = false;
	    // 
	    // documentTypeNone
	    // 
	    this.documentTypeNone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
	    this.documentTypeNone.Location = new System.Drawing.Point(168, 456);
	    this.documentTypeNone.Name = "documentTypeNone";
	    this.documentTypeNone.Size = new System.Drawing.Size(56, 24);
	    this.documentTypeNone.TabIndex = 9;
	    this.documentTypeNone.Text = "None";
	    this.documentTypeNone.Visible = false;
	    // 
	    // treeView1
	    // 
	    this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
		| System.Windows.Forms.AnchorStyles.Left);
	    this.treeView1.ImageIndex = -1;
	    this.treeView1.Location = new System.Drawing.Point(8, 56);
	    this.treeView1.Name = "treeView1";
	    this.treeView1.SelectedImageIndex = -1;
	    this.treeView1.Size = new System.Drawing.Size(160, 304);
	    this.treeView1.TabIndex = 10;
	    this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
	    // 
	    // splitter1
	    // 
	    this.splitter1.Name = "splitter1";
	    this.splitter1.Size = new System.Drawing.Size(3, 486);
	    this.splitter1.TabIndex = 12;
	    this.splitter1.TabStop = false;
	    // 
	    // listView1
	    // 
	    this.listView1.AccessibleName = "";
	    this.listView1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
		| System.Windows.Forms.AnchorStyles.Left) 
		| System.Windows.Forms.AnchorStyles.Right);
	    this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
											this.columnHeader1});
	    this.listView1.FullRowSelect = true;
	    this.listView1.GridLines = true;
	    this.listView1.Location = new System.Drawing.Point(176, 56);
	    this.listView1.Name = "listView1";
	    this.listView1.Size = new System.Drawing.Size(576, 304);
	    this.listView1.TabIndex = 13;
	    this.listView1.View = System.Windows.Forms.View.Details;
	    // 
	    // frmMain
	    // 
	    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
	    this.ClientSize = new System.Drawing.Size(760, 486);
	    this.Controls.AddRange(new System.Windows.Forms.Control[] {
									  this.listView1,
									  this.splitter1,
									  this.treeView1,
									  this.documentTypeNone,
									  this.documentTypeDTD,
									  this.documentTypeSchema,
									  this.resultErrors,
									  this.fileBrowse,
									  this.result,
									  this.validate,
									  this.file,
									  this.label1});
	    this.Name = "frmMain";
	    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
	    this.Text = "DTG Config Editor";
	    this.Load += new System.EventHandler(this.frmMain_Load);
	    this.ResumeLayout(false);

	}
		#endregion

	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main() {
	    Application.Run(new frmMain());
	}

	private void frmMain_Load(object sender, System.EventArgs e) {
	    file.Text = System.Environment.CurrentDirectory;
	}

	private void validate_Click(object sender, System.EventArgs e) {
	    String errors = ValidateSchema();
	    Boolean isValid = errors.Equals(String.Empty);

	    result.Text = isValid.ToString();
	    resultErrors.Text = errors;

	    LoadTree(treeView1, file.Text);
	} 

	private String ValidateSchema() {
	    error = "";

	    try {
		XmlTextReader xml = new XmlTextReader(file.Text);
		XmlValidatingReader xsd = new XmlValidatingReader(xml);

		//use schemas or DTDs
		if (documentTypeSchema.Checked == true) {
		    //schemas - YAAAAAAA
		    xsd.ValidationType = ValidationType.Schema;
		}
		else if (documentTypeNone.Checked == true) {
		    //so you just want to see if your XML is well formed?
		    xsd.ValidationType = ValidationType.None;
		}
		else {
		    //why do you want to learn this? its old, no one uses it and they are laughing behind your back. Shame on you!
		    xsd.ValidationType = ValidationType.DTD;
		}

		//and validation errors events go to...
		xsd.ValidationEventHandler += new ValidationEventHandler(MyValidationEventHandler);
				
		//wait until the read is over, its occuring in a different thread - kinda like when your walking to get a cup of coffee and your mind is in Hawaii
		while (xsd.Read()) {
		}
		xsd.Close();
	    }
	    catch(UnauthorizedAccessException a) {
		//dont have access permission
		error = a.Message;
	    }
	    catch(Exception a) {
		//and other things that could go wrong
		error = a.Message;
	    }
	    return error;
	}

	//handle our XML validation errors
	public static void MyValidationEventHandler(object sender, ValidationEventArgs args) {
	    isValid = false;
	    error += args.Message + "\n\n";
	}

	//start file browser 
	private void fileBrowse_Click(object sender, System.EventArgs e) {
	    openFileDialog.Multiselect = false;
	    openFileDialog.Filter = "XML(*.xml)|*.xml";
	    openFileDialog.InitialDirectory = file.Text;
	    openFileDialog.ShowDialog(this);
	}

	//clicked on from file browser, clicking cancel wil return from the showDialog
	private void BrowserSelection(object sender, System.ComponentModel.CancelEventArgs e) {			
	    file.Text = openFileDialog.FileName;
	    openFileDialog.Dispose();

	    String errors = ValidateSchema();
	    Boolean isValid = errors.Equals(String.Empty);

	    result.Text = isValid.ToString();
	    resultErrors.Text = errors;

	    LoadTree(treeView1, file.Text);
	}

    
	private void LoadTree(TreeView tree, String filename) {
	    tree.Nodes.Clear();

	    Generator g = new Generator(filename);
	    TreeNode node;

	    sqltypes = g.SqlTypes;
	    node = new TreeNode("SqlTypes");
	    foreach(SqlType sqltype in sqltypes) {
		node.Nodes.Add(sqltype.Name);
	    }
	    tree.Nodes.Add(node);

	    types = g.Types;
	    node = new TreeNode("Types");
	    foreach(Type type in types) {
		node.Nodes.Add(type.Name);
	    }
	    tree.Nodes.Add(node);

	    enums = g.Enums;
	    node = new TreeNode("Enums");
	    foreach(EnumType e in enums) {
		node.Nodes.Add(e.Name);
	    }
	    tree.Nodes.Add(node);

	    collections = g.Collections;
	    node = new TreeNode("Collections");
	    foreach(Collection c in collections) {
		TreeNode n = new TreeNode();
		n.CreateObjRef(typeof(SqlType));
		node.Nodes.Add(c.Name);
	    }
	    tree.Nodes.Add(node);

	    sqlentities = g.SqlEntities;
	    node = new TreeNode("SqlEntities");
	    foreach(SqlEntity sqlentity in sqlentities) {
		TreeNode n = new TreeNode(sqlentity.Name);
		if (sqlentity.Constraints.Count>0) {
		    TreeNode c = new TreeNode("contraints");
		    foreach (Constraint constraint in sqlentity.Constraints) {
			c.Nodes.Add(constraint.Name);
		    }
		    n.Nodes.Add(c);
		}
		if (sqlentity.Indexes.Count>0) {
		    TreeNode i = new TreeNode("indexes");
		    foreach (Index index in sqlentity.Indexes) {
			i.Nodes.Add(index.Name);
		    }
		    n.Nodes.Add(i);
		}

		node.Nodes.Add(n);
	    }
	    tree.Nodes.Add(node);

	    entities = g.Entities;
	    node = new TreeNode("Entities");
	    foreach(Entity entity in entities) {
		TreeNode n = new TreeNode(entity.Name);
		if (entity.Finders.Count>0) {
		    TreeNode f = new TreeNode("finders");
		    foreach (Finder finder in entity.Finders) {
			f.Nodes.Add(finder.Name);
		    }
		    n.Nodes.Add(f);
		}

		node.Nodes.Add(n);
	    }
	    tree.Nodes.Add(node);


	}

        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
	    TreeNode top = treeView1.SelectedNode;
	    Int32 level = 0;

	    while (top.Parent != null) {
		level++;
		top = top.Parent;
	    }

//	    MessageBox.Show(top.Text);
//	    MessageBox.Show(level.ToString());
//	    MessageBox.Show(treeView1.SelectedNode.Text);

	    String s = treeView1.SelectedNode.Text;
	    if (top.Text.Equals("Entities")) {
		if (level==1) {
		    listView1.Items.Clear();
		    Entity entity = Entity.FindEntityByName((ArrayList)entities, s);
		    listView1.Columns.Clear();
		    listView1.Columns.Add("Name", -1, HorizontalAlignment.Left);
		    listView1.Columns.Add("Type", -1, HorizontalAlignment.Left);
		    listView1.Columns.Add("Concrete Type", -1, HorizontalAlignment.Left);
		    listView1.Columns.Add("SqlEntity Column", -1, HorizontalAlignment.Left);
		    listView1.Columns.Add("Convert From SqlType Format", -1, HorizontalAlignment.Left);
		    listView1.Columns.Add("Access Modifier", -1, HorizontalAlignment.Left);
		    listView1.Columns.Add("Description", -1, HorizontalAlignment.Left);
		    foreach(Field field in entity.Fields) {
			ListViewItem lvi = new ListViewItem(field.Name);
			lvi.SubItems.Add(field.Type.Name);
			lvi.SubItems.Add(field.Type.ConcreteType);
			lvi.SubItems.Add(field.Column.Name);
			lvi.SubItems.Add(field.Type.ConvertFromSqlTypeFormat);
			lvi.SubItems.Add(field.AccessModifier);
			lvi.SubItems.Add(field.Description);
			listView1.Items.Add(lvi);
		    }
		    ResizeListViewColumns(listView1, -1);
		}
		if (level==3) {
		    listView1.Items.Clear();
		    Entity entity = Entity.FindEntityByName((ArrayList)entities, treeView1.SelectedNode.Parent.Parent.Text);
		    listView1.Columns.Clear();
		    listView1.Columns.Add("Finder Property Name", -1, HorizontalAlignment.Left);
		    Finder finder = entity.FindFinderByName(s);

		    foreach (Field field in finder.Fields) {
			listView1.Items.Add(field.Name);
		    }
		    ResizeListViewColumns(listView1, -2);
		}
	    }
	}

	private void ResizeListViewColumns(ListView lv, Int32 size) {
	    for (int i=0; i<lv.Columns.Count; i++) {
		lv.Columns[i].Width = size;
	    }
	}

    }

}
