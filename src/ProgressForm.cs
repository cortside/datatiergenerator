using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Summary description for ProgressForm.
    /// </summary>
    public class ProgressForm : System.Windows.Forms.Form {
	private System.Windows.Forms.ProgressBar progressBar;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;

	public ProgressForm() {
	    //
	    // Required for Windows Form Designer support
	    //
	    InitializeComponent();

	    progressBar.Minimum=0;
	    progressBar.Maximum=100;
	    progressBar.Value=0;
	    progressBar.Step=1;
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
	    this.progressBar = new System.Windows.Forms.ProgressBar();
	    this.SuspendLayout();
	    // 
	    // progressBar
	    // 
	    this.progressBar.Location = new System.Drawing.Point(8, 8);
	    this.progressBar.Name = "progressBar";
	    this.progressBar.Size = new System.Drawing.Size(280, 23);
	    this.progressBar.TabIndex = 0;
	    // 
	    // ProgressForm
	    // 
	    this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
	    this.ClientSize = new System.Drawing.Size(292, 38);
	    this.Controls.AddRange(new System.Windows.Forms.Control[] {
									  this.progressBar});
	    this.Name = "ProgressForm";
	    this.Text = "ProgressForm";
	    this.ResumeLayout(false);

	}
	#endregion


	public void Clear() {
	    progressBar.Value=0;
	}

	public void Step() {
	    progressBar.Value ++;
	    Application.DoEvents();
	}

	public Int32 Maximum {
	    get { return progressBar.Maximum; }
	    set { progressBar.Maximum = value; }
	}
	
    }
}
