using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Infragistics.Win.UltraWinTree;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.DTGEditor {
    /// <summary>
    /// Summary description for Editor.
    /// </summary>
    public class Editor : System.Windows.Forms.Form {
	private IList entities = new ArrayList();
	private IList databases = new ArrayList();
	private Hashtable types = new Hashtable();
	private ICollection sqltypes = new ArrayList();
	private IList enums = new ArrayList();
	private IList collections = new ArrayList();

	private Infragistics.Win.UltraWinDock.UnpinnedTabArea _EditorUnpinnedTabAreaLeft;
	private Infragistics.Win.UltraWinDock.UnpinnedTabArea _EditorUnpinnedTabAreaRight;
	private Infragistics.Win.UltraWinDock.UnpinnedTabArea _EditorUnpinnedTabAreaTop;
	private Infragistics.Win.UltraWinDock.UnpinnedTabArea _EditorUnpinnedTabAreaBottom;
	private Infragistics.Win.UltraWinDock.UltraDockManager DockManager;
	private Infragistics.Win.UltraWinTree.UltraTree tvSchema;
	private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
	private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea2;
	private System.Windows.Forms.RichTextBox txtOutput;
	private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow2;
	private Infragistics.Win.UltraWinDock.AutoHideControl _EditorAutoHideControl;
	private Infragistics.Win.UltraWinGrid.UltraGrid dbgImport;
	private System.Windows.Forms.Panel pnlProperties;
	private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow1;
	private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow4;
	private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea6;
	private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea3;
	private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _Editor_Toolbars_Dock_Area_Left;
	private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _Editor_Toolbars_Dock_Area_Right;
	private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _Editor_Toolbars_Dock_Area_Top;
	private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _Editor_Toolbars_Dock_Area_Bottom;
	private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager Toolbars;
	private System.Windows.Forms.ImageList ilImages;
	private Infragistics.Win.UltraWinStatusBar.UltraStatusBar sbStatus;
	private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Infragistics.Win.UltraWinDataSource.UltraDataSource ultraDataSource1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea4;
	private System.ComponentModel.IContainer components;

	public Editor() {
	    InitializeComponent();
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

	#region Windows Form Designer generated code
	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent() {
	    this.components = new System.ComponentModel.Container();
	    Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
	    Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
	    Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedBottom, new System.Guid("e729e03e-9a55-4602-809a-9b9863f966c1"));
	    Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("39066357-1a30-4b44-9d8b-ac8258a8e604"), new System.Guid("f31972b9-1dcf-4e26-9f79-e259fc540972"), -1, new System.Guid("e729e03e-9a55-4602-809a-9b9863f966c1"), 0);
	    Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
	    Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane2 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedLeft, new System.Guid("79e3b2eb-7c0a-4b79-b7fe-e2cdb4dce6ca"));
	    Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane2 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("2dcf001e-3b6b-433c-b4f3-fe5d02903f90"), new System.Guid("a42fbad8-597d-4100-b5aa-3e97c8c5c3da"), -1, new System.Guid("79e3b2eb-7c0a-4b79-b7fe-e2cdb4dce6ca"), -1);
	    Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
	    Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane3 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, new System.Guid("a42fbad8-597d-4100-b5aa-3e97c8c5c3da"));
	    Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane4 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedBottom, new System.Guid("4dee2e7b-088d-4f81-a0ff-0ed09aab8593"));
	    Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane3 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("fba5669b-3662-4ea5-b652-a65355aa5776"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("4dee2e7b-088d-4f81-a0ff-0ed09aab8593"), -1);
	    Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
	    Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane5 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, new System.Guid("f31972b9-1dcf-4e26-9f79-e259fc540972"));
	    System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Editor));
	    Infragistics.Win.UltraWinToolbars.OptionSet optionSet1 = new Infragistics.Win.UltraWinToolbars.OptionSet("Type");
	    Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("tbMain");
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool1 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("File");
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool2 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Commands");
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool3 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Type");
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool4 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("View");
	    Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar2 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("tbTools");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Open");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Validate");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Generate");
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool5 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("View");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Outline");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Output");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Properties");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Outline");
	    Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Properties");
	    Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Output");
	    Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool6 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("File");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Open");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool12 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Open");
	    Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Exit");
	    Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool7 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Type");
	    Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool1 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Schema", "Type");
	    Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool2 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DTD", "Type");
	    Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool3 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("None", "Type");
	    Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool4 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("Schema", "Type");
	    Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool5 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("DTD", "Type");
	    Infragistics.Win.UltraWinToolbars.StateButtonTool stateButtonTool6 = new Infragistics.Win.UltraWinToolbars.StateButtonTool("None", "Type");
	    Infragistics.Win.UltraWinToolbars.PopupMenuTool popupMenuTool8 = new Infragistics.Win.UltraWinToolbars.PopupMenuTool("Commands");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool14 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Generate");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Validate");
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool16 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Generate");
	    Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Validate");
	    Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
	    Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
	    Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
	    Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
	    Infragistics.Win.UltraWinDataSource.UltraDataBand ultraDataBand1 = new Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 1");
	    Infragistics.Win.UltraWinDataSource.UltraDataBand ultraDataBand2 = new Infragistics.Win.UltraWinDataSource.UltraDataBand("Band 2");
	    this.tvSchema = new Infragistics.Win.UltraWinTree.UltraTree();
	    this.pnlProperties = new System.Windows.Forms.Panel();
	    this.dbgImport = new Infragistics.Win.UltraWinGrid.UltraGrid();
	    this.txtOutput = new System.Windows.Forms.RichTextBox();
	    this.DockManager = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
	    this.ilImages = new System.Windows.Forms.ImageList(this.components);
	    this._EditorUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
	    this._EditorUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
	    this._EditorUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
	    this._EditorUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
	    this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
	    this.dockableWindow2 = new Infragistics.Win.UltraWinDock.DockableWindow();
	    this.windowDockingArea2 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
	    this._EditorAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
	    this.dockableWindow1 = new Infragistics.Win.UltraWinDock.DockableWindow();
	    this.dockableWindow4 = new Infragistics.Win.UltraWinDock.DockableWindow();
	    this.windowDockingArea6 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
	    this.windowDockingArea3 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
	    this.Toolbars = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
	    this._Editor_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
	    this._Editor_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
	    this._Editor_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
	    this._Editor_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
	    this.sbStatus = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
	    this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
	    this.ultraDataSource1 = new Infragistics.Win.UltraWinDataSource.UltraDataSource();
	    this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
	    this.windowDockingArea4 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
	    ((System.ComponentModel.ISupportInitialize)(this.tvSchema)).BeginInit();
	    this.pnlProperties.SuspendLayout();
	    ((System.ComponentModel.ISupportInitialize)(this.dbgImport)).BeginInit();
	    ((System.ComponentModel.ISupportInitialize)(this.DockManager)).BeginInit();
	    this.windowDockingArea1.SuspendLayout();
	    this.dockableWindow2.SuspendLayout();
	    this.dockableWindow1.SuspendLayout();
	    this.dockableWindow4.SuspendLayout();
	    this.windowDockingArea6.SuspendLayout();
	    ((System.ComponentModel.ISupportInitialize)(this.Toolbars)).BeginInit();
	    ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).BeginInit();
	    this.windowDockingArea4.SuspendLayout();
	    this.SuspendLayout();
	    // 
	    // tvSchema
	    // 
	    this.tvSchema.Location = new System.Drawing.Point(0, 20);
	    this.tvSchema.Name = "tvSchema";
	    this.tvSchema.Size = new System.Drawing.Size(307, 322);
	    this.tvSchema.TabIndex = 5;
	    this.tvSchema.AfterSelect += new Infragistics.Win.UltraWinTree.AfterNodeSelectEventHandler(this.tvSchema_AfterSelect);
	    // 
	    // pnlProperties
	    // 
	    this.pnlProperties.Controls.Add(this.dbgImport);
	    this.pnlProperties.Controls.Add(this.propertyGrid1);
	    this.pnlProperties.Location = new System.Drawing.Point(0, 20);
	    this.pnlProperties.Name = "pnlProperties";
	    this.pnlProperties.Size = new System.Drawing.Size(556, 322);
	    this.pnlProperties.TabIndex = 17;
	    // 
	    // dbgImport
	    // 
	    this.dbgImport.Cursor = System.Windows.Forms.Cursors.Default;
	    this.dbgImport.DisplayLayout.AddNewBox.Prompt = "Add ...";
	    appearance1.BackColor = System.Drawing.SystemColors.Window;
	    this.dbgImport.DisplayLayout.Appearance = appearance1;
	    this.dbgImport.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
	    appearance2.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
	    appearance2.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
	    this.dbgImport.DisplayLayout.GroupByBox.Appearance = appearance2;
	    this.dbgImport.DisplayLayout.GroupByBox.Hidden = true;
	    this.dbgImport.DisplayLayout.GroupByBox.Prompt = "Drag a column header here to group by that column.";
	    this.dbgImport.DisplayLayout.LoadStyle = Infragistics.Win.UltraWinGrid.LoadStyle.LoadOnDemand;
	    this.dbgImport.DisplayLayout.MaxBandDepth = 10;
	    this.dbgImport.DisplayLayout.MaxColScrollRegions = 1;
	    this.dbgImport.DisplayLayout.MaxRowScrollRegions = 1;
	    this.dbgImport.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
	    this.dbgImport.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
	    this.dbgImport.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
	    this.dbgImport.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
	    this.dbgImport.DisplayLayout.Override.NullText = "";
	    appearance3.BackColor = System.Drawing.SystemColors.ControlLight;
	    this.dbgImport.DisplayLayout.Override.RowAlternateAppearance = appearance3;
	    this.dbgImport.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
	    this.dbgImport.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
	    this.dbgImport.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
	    this.dbgImport.DisplayLayout.Override.SelectTypeGroupByRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
	    this.dbgImport.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
	    this.dbgImport.Dock = System.Windows.Forms.DockStyle.Fill;
	    this.dbgImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this.dbgImport.Location = new System.Drawing.Point(0, 0);
	    this.dbgImport.Name = "dbgImport";
	    this.dbgImport.Size = new System.Drawing.Size(284, 322);
	    this.dbgImport.TabIndex = 18;
	    // 
	    // txtOutput
	    // 
	    this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
	    this.txtOutput.Location = new System.Drawing.Point(0, 20);
	    this.txtOutput.Name = "txtOutput";
	    this.txtOutput.Size = new System.Drawing.Size(868, 120);
	    this.txtOutput.TabIndex = 6;
	    this.txtOutput.Text = "";
	    // 
	    // DockManager
	    // 
	    dockableControlPane1.Control = this.txtOutput;
	    dockableControlPane1.FlyoutSize = new System.Drawing.Size(-1, 230);
	    dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(76, 220, 100, 96);
	    appearance4.Image = 2;
	    dockableControlPane1.Settings.CaptionAppearance = appearance4;
	    appearance5.Image = 2;
	    dockableControlPane1.Settings.TabAppearance = appearance5;
	    dockableControlPane1.Size = new System.Drawing.Size(642, 140);
	    dockableControlPane1.Text = "Output";
	    dockableControlPane1.TextTab = "Output";
	    dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
												  dockableControlPane1});
	    dockAreaPane1.Size = new System.Drawing.Size(868, 140);
	    dockAreaPane1.UnfilledSize = new System.Drawing.Size(627, 487);
	    dockAreaPane2.DockedBefore = new System.Guid("a42fbad8-597d-4100-b5aa-3e97c8c5c3da");
	    dockAreaPane2.FloatingLocation = new System.Drawing.Point(516, 246);
	    dockableControlPane2.Control = this.tvSchema;
	    dockableControlPane2.FlyoutSize = new System.Drawing.Size(100, -1);
	    dockableControlPane2.OriginalControlBounds = new System.Drawing.Rectangle(328, 72, 196, 324);
	    appearance6.Image = 1;
	    dockableControlPane2.Settings.CaptionAppearance = appearance6;
	    appearance7.Image = 1;
	    dockableControlPane2.Settings.TabAppearance = appearance7;
	    dockableControlPane2.Size = new System.Drawing.Size(188, 278);
	    dockableControlPane2.Text = "Outline";
	    dockableControlPane2.TextTab = "Outline";
	    dockAreaPane2.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
												  dockableControlPane2});
	    dockAreaPane2.Size = new System.Drawing.Size(307, 342);
	    dockAreaPane3.DockedBefore = new System.Guid("4dee2e7b-088d-4f81-a0ff-0ed09aab8593");
	    dockAreaPane3.FloatingLocation = new System.Drawing.Point(516, 246);
	    dockAreaPane3.Size = new System.Drawing.Size(100, 100);
	    dockAreaPane4.DockedBefore = new System.Guid("f31972b9-1dcf-4e26-9f79-e259fc540972");
	    dockableControlPane3.Control = this.pnlProperties;
	    dockableControlPane3.OriginalControlBounds = new System.Drawing.Rectangle(152, 60, 200, 100);
	    appearance8.Image = 8;
	    dockableControlPane3.Settings.CaptionAppearance = appearance8;
	    appearance9.Image = 8;
	    dockableControlPane3.Settings.TabAppearance = appearance9;
	    dockableControlPane3.Size = new System.Drawing.Size(642, 341);
	    dockableControlPane3.Text = "Properties";
	    dockableControlPane3.TextTab = "Properties";
	    dockAreaPane4.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
												  dockableControlPane3});
	    dockAreaPane4.Size = new System.Drawing.Size(556, 342);
	    dockAreaPane4.UnfilledSize = new System.Drawing.Size(627, 487);
	    dockAreaPane5.FloatingLocation = new System.Drawing.Point(688, 482);
	    dockAreaPane5.Size = new System.Drawing.Size(675, 95);
	    this.DockManager.DockAreas.AddRange(new Infragistics.Win.UltraWinDock.DockAreaPane[] {
												     dockAreaPane1,
												     dockAreaPane2,
												     dockAreaPane3,
												     dockAreaPane4,
												     dockAreaPane5});
	    this.DockManager.HostControl = this;
	    this.DockManager.ImageList = this.ilImages;
	    this.DockManager.ImageSizeCaption = new System.Drawing.Size(16, 16);
	    this.DockManager.ImageSizeTab = new System.Drawing.Size(16, 16);
	    this.DockManager.ImageTransparentColor = System.Drawing.Color.Magenta;
	    this.DockManager.LayoutStyle = Infragistics.Win.UltraWinDock.DockAreaLayoutStyle.FillContainer;
	    this.DockManager.ShowCloseButton = false;
	    this.DockManager.ShowDisabledButtons = false;
	    // 
	    // ilImages
	    // 
	    this.ilImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
	    this.ilImages.ImageSize = new System.Drawing.Size(16, 16);
	    this.ilImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilImages.ImageStream")));
	    this.ilImages.TransparentColor = System.Drawing.Color.Magenta;
	    // 
	    // _EditorUnpinnedTabAreaLeft
	    // 
	    this._EditorUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
	    this._EditorUnpinnedTabAreaLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this._EditorUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 52);
	    this._EditorUnpinnedTabAreaLeft.Name = "_EditorUnpinnedTabAreaLeft";
	    this._EditorUnpinnedTabAreaLeft.Owner = this.DockManager;
	    this._EditorUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 487);
	    this._EditorUnpinnedTabAreaLeft.TabIndex = 0;
	    // 
	    // _EditorUnpinnedTabAreaRight
	    // 
	    this._EditorUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
	    this._EditorUnpinnedTabAreaRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this._EditorUnpinnedTabAreaRight.Location = new System.Drawing.Point(868, 52);
	    this._EditorUnpinnedTabAreaRight.Name = "_EditorUnpinnedTabAreaRight";
	    this._EditorUnpinnedTabAreaRight.Owner = this.DockManager;
	    this._EditorUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 487);
	    this._EditorUnpinnedTabAreaRight.TabIndex = 1;
	    // 
	    // _EditorUnpinnedTabAreaTop
	    // 
	    this._EditorUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
	    this._EditorUnpinnedTabAreaTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this._EditorUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 52);
	    this._EditorUnpinnedTabAreaTop.Name = "_EditorUnpinnedTabAreaTop";
	    this._EditorUnpinnedTabAreaTop.Owner = this.DockManager;
	    this._EditorUnpinnedTabAreaTop.Size = new System.Drawing.Size(868, 0);
	    this._EditorUnpinnedTabAreaTop.TabIndex = 2;
	    // 
	    // _EditorUnpinnedTabAreaBottom
	    // 
	    this._EditorUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
	    this._EditorUnpinnedTabAreaBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this._EditorUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 539);
	    this._EditorUnpinnedTabAreaBottom.Name = "_EditorUnpinnedTabAreaBottom";
	    this._EditorUnpinnedTabAreaBottom.Owner = this.DockManager;
	    this._EditorUnpinnedTabAreaBottom.Size = new System.Drawing.Size(868, 0);
	    this._EditorUnpinnedTabAreaBottom.TabIndex = 3;
	    // 
	    // windowDockingArea1
	    // 
	    this.windowDockingArea1.Controls.Add(this.dockableWindow2);
	    this.windowDockingArea1.Dock = System.Windows.Forms.DockStyle.Left;
	    this.windowDockingArea1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this.windowDockingArea1.Location = new System.Drawing.Point(0, 52);
	    this.windowDockingArea1.Name = "windowDockingArea1";
	    this.windowDockingArea1.Owner = this.DockManager;
	    this.windowDockingArea1.Size = new System.Drawing.Size(312, 342);
	    this.windowDockingArea1.TabIndex = 0;
	    // 
	    // dockableWindow2
	    // 
	    this.dockableWindow2.Controls.Add(this.tvSchema);
	    this.dockableWindow2.Location = new System.Drawing.Point(0, 0);
	    this.dockableWindow2.Name = "dockableWindow2";
	    this.dockableWindow2.Owner = this.DockManager;
	    this.dockableWindow2.Size = new System.Drawing.Size(307, 342);
	    this.dockableWindow2.TabIndex = 0;
	    // 
	    // windowDockingArea2
	    // 
	    this.windowDockingArea2.Dock = System.Windows.Forms.DockStyle.Fill;
	    this.windowDockingArea2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this.windowDockingArea2.Location = new System.Drawing.Point(0, 52);
	    this.windowDockingArea2.Name = "windowDockingArea2";
	    this.windowDockingArea2.Owner = this.DockManager;
	    this.windowDockingArea2.Size = new System.Drawing.Size(100, 100);
	    this.windowDockingArea2.TabIndex = 5;
	    // 
	    // _EditorAutoHideControl
	    // 
	    this._EditorAutoHideControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this._EditorAutoHideControl.Location = new System.Drawing.Point(0, 52);
	    this._EditorAutoHideControl.Name = "_EditorAutoHideControl";
	    this._EditorAutoHideControl.Owner = this.DockManager;
	    this._EditorAutoHideControl.Size = new System.Drawing.Size(105, 487);
	    this._EditorAutoHideControl.TabIndex = 15;
	    // 
	    // dockableWindow1
	    // 
	    this.dockableWindow1.Controls.Add(this.pnlProperties);
	    this.dockableWindow1.Location = new System.Drawing.Point(0, 0);
	    this.dockableWindow1.Name = "dockableWindow1";
	    this.dockableWindow1.Owner = this.DockManager;
	    this.dockableWindow1.Size = new System.Drawing.Size(556, 342);
	    this.dockableWindow1.TabIndex = 0;
	    // 
	    // dockableWindow4
	    // 
	    this.dockableWindow4.Controls.Add(this.txtOutput);
	    this.dockableWindow4.Location = new System.Drawing.Point(0, 5);
	    this.dockableWindow4.Name = "dockableWindow4";
	    this.dockableWindow4.Owner = this.DockManager;
	    this.dockableWindow4.Size = new System.Drawing.Size(868, 140);
	    this.dockableWindow4.TabIndex = 0;
	    // 
	    // windowDockingArea6
	    // 
	    this.windowDockingArea6.Controls.Add(this.dockableWindow1);
	    this.windowDockingArea6.Dock = System.Windows.Forms.DockStyle.Fill;
	    this.windowDockingArea6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this.windowDockingArea6.Location = new System.Drawing.Point(312, 52);
	    this.windowDockingArea6.Name = "windowDockingArea6";
	    this.windowDockingArea6.Owner = this.DockManager;
	    this.windowDockingArea6.Size = new System.Drawing.Size(556, 342);
	    this.windowDockingArea6.TabIndex = 0;
	    // 
	    // windowDockingArea3
	    // 
	    this.windowDockingArea3.Dock = System.Windows.Forms.DockStyle.Fill;
	    this.windowDockingArea3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this.windowDockingArea3.Location = new System.Drawing.Point(193, 52);
	    this.windowDockingArea3.Name = "windowDockingArea3";
	    this.windowDockingArea3.Owner = this.DockManager;
	    this.windowDockingArea3.Size = new System.Drawing.Size(675, 95);
	    this.windowDockingArea3.TabIndex = 19;
	    // 
	    // Toolbars
	    // 
	    this.Toolbars.DockWithinContainer = this;
	    this.Toolbars.ImageListSmall = this.ilImages;
	    this.Toolbars.LockToolbars = true;
	    optionSet1.AllowAllUp = false;
	    this.Toolbars.OptionSets.Add(optionSet1);
	    this.Toolbars.ShowFullMenusDelay = 500;
	    this.Toolbars.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
	    ultraToolbar1.DockedColumn = 0;
	    ultraToolbar1.DockedRow = 0;
	    ultraToolbar1.IsMainMenuBar = true;
	    ultraToolbar1.Text = "Toolbar";
	    ultraToolbar1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
											      popupMenuTool1,
											      popupMenuTool2,
											      popupMenuTool3,
											      popupMenuTool4});
	    ultraToolbar2.DockedColumn = 0;
	    ultraToolbar2.DockedRow = 1;
	    ultraToolbar2.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
	    ultraToolbar2.Settings.AllowHiding = Infragistics.Win.DefaultableBoolean.False;
	    ultraToolbar2.Settings.FillEntireRow = Infragistics.Win.DefaultableBoolean.True;
	    ultraToolbar2.Settings.GrabHandleStyle = Infragistics.Win.UltraWinToolbars.GrabHandleStyle.None;
	    ultraToolbar2.Text = "tbTools";
	    ultraToolbar2.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
											      buttonTool1,
											      buttonTool2,
											      buttonTool3});
	    this.Toolbars.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
												     ultraToolbar1,
												     ultraToolbar2});
	    popupMenuTool5.SharedProps.Caption = "&View";
	    popupMenuTool5.SharedProps.Category = "View";
	    popupMenuTool5.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
											       buttonTool4,
											       buttonTool5,
											       buttonTool6});
	    appearance10.Image = 1;
	    buttonTool7.SharedProps.AppearancesSmall.Appearance = appearance10;
	    buttonTool7.SharedProps.Caption = "Out&line";
	    buttonTool7.SharedProps.Category = "View";
	    appearance11.Image = 8;
	    buttonTool8.SharedProps.AppearancesSmall.Appearance = appearance11;
	    buttonTool8.SharedProps.Caption = "&Properties";
	    buttonTool8.SharedProps.Category = "View";
	    appearance12.Image = 2;
	    buttonTool9.SharedProps.AppearancesSmall.Appearance = appearance12;
	    buttonTool9.SharedProps.Caption = "&Output";
	    buttonTool9.SharedProps.Category = "View";
	    popupMenuTool6.SharedProps.Caption = "&File";
	    popupMenuTool6.SharedProps.Category = "File";
	    popupMenuTool6.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
											       buttonTool10,
											       buttonTool11});
	    appearance13.Image = 4;
	    buttonTool12.SharedProps.AppearancesSmall.Appearance = appearance13;
	    buttonTool12.SharedProps.Caption = "&Open";
	    buttonTool12.SharedProps.Category = "File";
	    appearance14.Image = 5;
	    buttonTool13.SharedProps.AppearancesSmall.Appearance = appearance14;
	    buttonTool13.SharedProps.Caption = "E&xit";
	    buttonTool13.SharedProps.Category = "File";
	    popupMenuTool7.SharedProps.Caption = "&Type";
	    popupMenuTool7.SharedProps.Category = "Type";
	    popupMenuTool7.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
											       stateButtonTool1,
											       stateButtonTool2,
											       stateButtonTool3});
	    stateButtonTool4.OptionSetKey = "Type";
	    stateButtonTool4.SharedProps.Caption = "&Schema";
	    stateButtonTool4.SharedProps.Category = "Type";
	    stateButtonTool5.OptionSetKey = "Type";
	    stateButtonTool5.SharedProps.Caption = "&DTD";
	    stateButtonTool5.SharedProps.Category = "Type";
	    stateButtonTool6.OptionSetKey = "Type";
	    stateButtonTool6.SharedProps.Caption = "&None";
	    stateButtonTool6.SharedProps.Category = "Type";
	    popupMenuTool8.SharedProps.Caption = "&Commands";
	    popupMenuTool8.SharedProps.Category = "Commands";
	    popupMenuTool8.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
											       buttonTool14,
											       buttonTool15});
	    appearance15.Image = 11;
	    buttonTool16.SharedProps.AppearancesSmall.Appearance = appearance15;
	    buttonTool16.SharedProps.Caption = "&Generate";
	    buttonTool16.SharedProps.Category = "Commands";
	    buttonTool16.SharedProps.Enabled = false;
	    buttonTool16.SharedProps.ToolTipText = "Generate";
	    appearance16.Image = 16;
	    buttonTool17.SharedProps.AppearancesSmall.Appearance = appearance16;
	    buttonTool17.SharedProps.Caption = "&Validate";
	    buttonTool17.SharedProps.Category = "Commands";
	    buttonTool17.SharedProps.ToolTipText = "Validate";
	    this.Toolbars.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
											      popupMenuTool5,
											      buttonTool7,
											      buttonTool8,
											      buttonTool9,
											      popupMenuTool6,
											      buttonTool12,
											      buttonTool13,
											      popupMenuTool7,
											      stateButtonTool4,
											      stateButtonTool5,
											      stateButtonTool6,
											      popupMenuTool8,
											      buttonTool16,
											      buttonTool17});
	    this.Toolbars.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.Toolbars_ToolClick);
	    // 
	    // _Editor_Toolbars_Dock_Area_Left
	    // 
	    this._Editor_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
	    this._Editor_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(158)), ((System.Byte)(190)), ((System.Byte)(245)));
	    this._Editor_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
	    this._Editor_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
	    this._Editor_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 52);
	    this._Editor_Toolbars_Dock_Area_Left.Name = "_Editor_Toolbars_Dock_Area_Left";
	    this._Editor_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 487);
	    this._Editor_Toolbars_Dock_Area_Left.ToolbarsManager = this.Toolbars;
	    // 
	    // _Editor_Toolbars_Dock_Area_Right
	    // 
	    this._Editor_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
	    this._Editor_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(158)), ((System.Byte)(190)), ((System.Byte)(245)));
	    this._Editor_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
	    this._Editor_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
	    this._Editor_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(868, 52);
	    this._Editor_Toolbars_Dock_Area_Right.Name = "_Editor_Toolbars_Dock_Area_Right";
	    this._Editor_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 487);
	    this._Editor_Toolbars_Dock_Area_Right.ToolbarsManager = this.Toolbars;
	    // 
	    // _Editor_Toolbars_Dock_Area_Top
	    // 
	    this._Editor_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
	    this._Editor_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(158)), ((System.Byte)(190)), ((System.Byte)(245)));
	    this._Editor_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
	    this._Editor_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
	    this._Editor_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
	    this._Editor_Toolbars_Dock_Area_Top.Name = "_Editor_Toolbars_Dock_Area_Top";
	    this._Editor_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(868, 52);
	    this._Editor_Toolbars_Dock_Area_Top.ToolbarsManager = this.Toolbars;
	    // 
	    // _Editor_Toolbars_Dock_Area_Bottom
	    // 
	    this._Editor_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
	    this._Editor_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(158)), ((System.Byte)(190)), ((System.Byte)(245)));
	    this._Editor_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
	    this._Editor_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
	    this._Editor_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 539);
	    this._Editor_Toolbars_Dock_Area_Bottom.Name = "_Editor_Toolbars_Dock_Area_Bottom";
	    this._Editor_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(868, 0);
	    this._Editor_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.Toolbars;
	    // 
	    // sbStatus
	    // 
	    this.sbStatus.Location = new System.Drawing.Point(0, 539);
	    this.sbStatus.Name = "sbStatus";
	    ultraStatusPanel1.Key = "Filename";
	    ultraStatusPanel1.MinWidth = 100;
	    ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
	    ultraStatusPanel2.Key = "Document Type";
	    ultraStatusPanel2.MinWidth = 100;
	    ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
	    ultraStatusPanel3.Key = "Result";
	    ultraStatusPanel3.MinWidth = 100;
	    ultraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
	    this.sbStatus.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] {
													ultraStatusPanel1,
													ultraStatusPanel2,
													ultraStatusPanel3});
	    this.sbStatus.Size = new System.Drawing.Size(868, 23);
	    this.sbStatus.TabIndex = 25;
	    // 
	    // openFileDialog
	    // 
	    this.openFileDialog.Filter = "XML (*.xml)|*.xml";
	    this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
	    // 
	    // ultraDataSource1
	    // 
	    this.ultraDataSource1.Band.ChildBands.AddRange(new object[] {
									    ultraDataBand1,
									    ultraDataBand2});
	    // 
	    // propertyGrid1
	    // 
	    this.propertyGrid1.CommandsBackColor = System.Drawing.SystemColors.Window;
	    this.propertyGrid1.CommandsVisibleIfAvailable = true;
	    this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
	    this.propertyGrid1.LargeButtons = false;
	    this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
	    this.propertyGrid1.Location = new System.Drawing.Point(284, 0);
	    this.propertyGrid1.Name = "propertyGrid1";
	    this.propertyGrid1.Size = new System.Drawing.Size(272, 322);
	    this.propertyGrid1.TabIndex = 20;
	    this.propertyGrid1.Text = "propertyGrid1";
	    this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
	    this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
	    // 
	    // windowDockingArea4
	    // 
	    this.windowDockingArea4.Controls.Add(this.dockableWindow4);
	    this.windowDockingArea4.Dock = System.Windows.Forms.DockStyle.Bottom;
	    this.windowDockingArea4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
	    this.windowDockingArea4.Location = new System.Drawing.Point(0, 394);
	    this.windowDockingArea4.Name = "windowDockingArea4";
	    this.windowDockingArea4.Owner = this.DockManager;
	    this.windowDockingArea4.Size = new System.Drawing.Size(868, 145);
	    this.windowDockingArea4.TabIndex = 26;
	    // 
	    // Editor
	    // 
	    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
	    this.BackColor = System.Drawing.SystemColors.Window;
	    this.ClientSize = new System.Drawing.Size(868, 562);
	    this.Controls.Add(this._EditorAutoHideControl);
	    this.Controls.Add(this.windowDockingArea6);
	    this.Controls.Add(this.windowDockingArea1);
	    this.Controls.Add(this.windowDockingArea4);
	    this.Controls.Add(this._EditorUnpinnedTabAreaTop);
	    this.Controls.Add(this._EditorUnpinnedTabAreaBottom);
	    this.Controls.Add(this._EditorUnpinnedTabAreaLeft);
	    this.Controls.Add(this._EditorUnpinnedTabAreaRight);
	    this.Controls.Add(this._Editor_Toolbars_Dock_Area_Left);
	    this.Controls.Add(this._Editor_Toolbars_Dock_Area_Right);
	    this.Controls.Add(this._Editor_Toolbars_Dock_Area_Top);
	    this.Controls.Add(this._Editor_Toolbars_Dock_Area_Bottom);
	    this.Controls.Add(this.sbStatus);
	    this.Name = "Editor";
	    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
	    this.Text = "DTG Configuration Editor";
	    this.Load += new System.EventHandler(this.Editor_Load);
	    ((System.ComponentModel.ISupportInitialize)(this.tvSchema)).EndInit();
	    this.pnlProperties.ResumeLayout(false);
	    ((System.ComponentModel.ISupportInitialize)(this.dbgImport)).EndInit();
	    ((System.ComponentModel.ISupportInitialize)(this.DockManager)).EndInit();
	    this.windowDockingArea1.ResumeLayout(false);
	    this.dockableWindow2.ResumeLayout(false);
	    this.dockableWindow1.ResumeLayout(false);
	    this.dockableWindow4.ResumeLayout(false);
	    this.windowDockingArea6.ResumeLayout(false);
	    ((System.ComponentModel.ISupportInitialize)(this.Toolbars)).EndInit();
	    ((System.ComponentModel.ISupportInitialize)(this.ultraDataSource1)).EndInit();
	    this.windowDockingArea4.ResumeLayout(false);
	    this.ResumeLayout(false);

	}
	#endregion

	private void Toolbars_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e) {
	    switch (e.Tool.Key) {
		case "Outline": {
		    break;
		}

		case "Properties": {
		    break;
		}

		case "Output": {
		    break;
		}

		case "Open": {
		    openFileDialog.InitialDirectory = sbStatus.Panels["Filename"].Text;
		    openFileDialog.ShowDialog(this);
		    break;
		}

		case "Exit": {
		    break;
		}

		case "Schema": {
		    break;
		}

		case "DTD": {
		    break;
		}

		case "None": {
		    break;
		}

		case "Generate": {
		    break;
		}

		case "Validate": {
		    LoadFile();
		    break;
		}
	    }

	}

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e) {
	    sbStatus.Panels["Filename"].Text = openFileDialog.FileName;
	    LoadFile();
	}

	private void LoadFile() {
	    try {
		Cursor = Cursors.WaitCursor;
		tvSchema.Nodes.Clear();
		XmlParser parser = new XmlParser();
		parser.Parse(sbStatus.Panels["Filename"].Text);
		sbStatus.Panels["Result"].Text = parser.IsValid ? "" : "Document is invalid - fix errors";
		Toolbars.Tools["Generate"].SharedProps.Enabled = parser.IsValid;
		txtOutput.Text = String.Empty;
		foreach(String s in parser.Log) {
		    txtOutput.Text += s + Environment.NewLine;
		}
		LoadTree(parser);
	    } catch (Exception ex) {
		txtOutput.Text = "An error occcurred while loading.\n\n" + ex.ToString();
	    } finally { 
		Cursor = Cursors.Default;
	    }
	}

        private void Editor_Load(object sender, System.EventArgs e) {
	    sbStatus.Panels["Filename"].Text = @"C:\Data\work\UHEAA\UESP\src\";//System.Environment.CurrentDirectory;
	}

	private void LoadTree(XmlParser parser) {
	    tvSchema.Nodes.Clear();
	    
	    UltraTreeNode node;
	    int nodeKey = 0;

	    types = parser.Types;
	    node = tvSchema.Nodes.Add((nodeKey++).ToString(), "Types");
	    node.Tag = types;
	    foreach(TypeElement type in types.Values) {
		UltraTreeNode newNode = node.Nodes.Add((nodeKey++).ToString(), type.Name);
		newNode.Tag = type;
	    }

	    enums = parser.Enums;
	    node = tvSchema.Nodes.Add((nodeKey++).ToString(), "Enums");
	    node.Tag = enums;
	    foreach(EnumElement e in enums) {
		UltraTreeNode enumNode = node.Nodes.Add((nodeKey++).ToString(), e.Name);
		enumNode.Nodes.Add((nodeKey++).ToString(), "Values");
		enumNode.Tag = e;
	    }

	    collections = parser.Collections;
	    node = tvSchema.Nodes.Add((nodeKey++).ToString(), "Collections");
	    node.Tag = collections;
	    foreach(CollectionElement c in collections) {
		UltraTreeNode newNode = node.Nodes.Add((nodeKey++).ToString(), c.Name);
		newNode.Tag = c;
	    }

	    databases = parser.Databases;
	    node = tvSchema.Nodes.Add((nodeKey++).ToString(), "Databases");
	    node.Tag = databases;
	    foreach(DatabaseElement database in databases) {
		UltraTreeNode databaseNode = node.Nodes.Add((nodeKey++).ToString(), database.Name);
		databaseNode.Tag = database;
		foreach(SqlEntityElement sqlentity in database.SqlEntities) {
		    UltraTreeNode sqlEntityNode = databaseNode.Nodes.Add((nodeKey++).ToString(), sqlentity.Name);
		    sqlEntityNode.Tag = sqlentity;
		    if (sqlentity.Constraints.Count>0) {
			UltraTreeNode constraintNode = sqlEntityNode.Nodes.Add((nodeKey++).ToString(), "Constraints");
			foreach (ConstraintElement constraint in sqlentity.Constraints) {
			    UltraTreeNode newNode = constraintNode.Nodes.Add((nodeKey++).ToString(), constraint.Name);
			    newNode.Tag = constraint;
			}
		    }
		    if (sqlentity.Indexes.Count>0) {
			UltraTreeNode indexNode = sqlEntityNode.Nodes.Add((nodeKey++).ToString(), "Indexes");
			foreach (IndexElement index in sqlentity.Indexes) {
			    UltraTreeNode newNode = indexNode.Nodes.Add((nodeKey++).ToString(), index.Name);
			    newNode.Tag = indexNode;
			}
		    }

		}
	    }

	    entities = parser.Entities;
	    node = tvSchema.Nodes.Add((nodeKey++).ToString(), "Entities");
	    node.Tag = entities;
	    foreach(EntityElement entity in entities) {
		UltraTreeNode entityNode = node.Nodes.Add((nodeKey++).ToString(), entity.Name);
		entityNode.Tag = entity;
		if (entity.Finders.Count > 0) {
		    UltraTreeNode findersNode = entityNode.Nodes.Add((nodeKey++).ToString(), "Finders");
		    foreach (FinderElement finder in entity.Finders) {
			UltraTreeNode finderNode = findersNode.Nodes.Add((nodeKey++).ToString(), finder.Name);
			finderNode.Tag = finder;
		    }
		}
	    }
	}

        private void tvSchema_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e) {
	    try {
		UltraTreeNode activeNode = tvSchema.ActiveNode;
		UltraTreeNode rootNode = tvSchema.ActiveNode;

		propertyGrid1.SelectedObject = activeNode.Tag;

		while (rootNode.Level > 0) {
		    rootNode = rootNode.Parent;
		}

		dbgImport.DataSource = null;
		if (rootNode.Text == "Databases") {
		    dbgImport.DataSource = databases;
		} else if (rootNode.Text == "Types") {
		    
		} else if (rootNode.Text == "Enums") {
		    dbgImport.DataSource = enums;
		} else if (rootNode.Text == "Entities") {
		    dbgImport.DataSource = entities;
		} else if (rootNode.Text == "Collections") {
		    dbgImport.DataSource = collections;
		}

		
	    } catch (Exception ex) {
		Console.WriteLine(ex.ToString());
	    }
	}
    }
}
