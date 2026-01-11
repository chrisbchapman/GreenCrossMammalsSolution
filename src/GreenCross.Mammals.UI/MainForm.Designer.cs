namespace GreenCross.Mammals.UI;

partial class MainForm
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
        menuStrip1 = new MenuStrip();
        importCSVToolStripMenuItem = new ToolStripMenuItem();
        importRecordVeriicationStatusesToolStripMenuItem = new ToolStripMenuItem();
        importLocationsToolStripMenuItem = new ToolStripMenuItem();
        importRecordersToolStripMenuItem = new ToolStripMenuItem();
        importHarvestMouiseRecordsToolStripMenuItem = new ToolStripMenuItem();
        reportsToolStripMenuItem = new ToolStripMenuItem();
        fullListToolStripMenuItem = new ToolStripMenuItem();
        exportCSVsToolStripMenuItem = new ToolStripMenuItem();
        forQGISToolStripMenuItem = new ToolStripMenuItem();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { importCSVToolStripMenuItem, reportsToolStripMenuItem, exportCSVsToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(800, 24);
        menuStrip1.TabIndex = 0;
        menuStrip1.Text = "menuStrip1";
        // 
        // importCSVToolStripMenuItem
        // 
        importCSVToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importRecordVeriicationStatusesToolStripMenuItem, importLocationsToolStripMenuItem, importRecordersToolStripMenuItem, importHarvestMouiseRecordsToolStripMenuItem });
        importCSVToolStripMenuItem.Name = "importCSVToolStripMenuItem";
        importCSVToolStripMenuItem.Size = new Size(84, 20);
        importCSVToolStripMenuItem.Text = "Import CSVs";
        // 
        // importRecordVeriicationStatusesToolStripMenuItem
        // 
        importRecordVeriicationStatusesToolStripMenuItem.Name = "importRecordVeriicationStatusesToolStripMenuItem";
        importRecordVeriicationStatusesToolStripMenuItem.Size = new Size(270, 22);
        importRecordVeriicationStatusesToolStripMenuItem.Text = "Import Record Verification Statuses ...";
        importRecordVeriicationStatusesToolStripMenuItem.Click += ImportRecordVeriicationStatusesToolStripMenuItem_Click;
        // 
        // importLocationsToolStripMenuItem
        // 
        importLocationsToolStripMenuItem.Name = "importLocationsToolStripMenuItem";
        importLocationsToolStripMenuItem.Size = new Size(270, 22);
        importLocationsToolStripMenuItem.Text = "Import Locations";
        importLocationsToolStripMenuItem.Click += ImportLocationsToolStripMenuItem_Click;
        // 
        // importRecordersToolStripMenuItem
        // 
        importRecordersToolStripMenuItem.Name = "importRecordersToolStripMenuItem";
        importRecordersToolStripMenuItem.Size = new Size(270, 22);
        importRecordersToolStripMenuItem.Text = "Import Recorders ...";
        importRecordersToolStripMenuItem.Click += ImportRecordersToolStripMenuItem_Click;
        // 
        // importHarvestMouiseRecordsToolStripMenuItem
        // 
        importHarvestMouiseRecordsToolStripMenuItem.Name = "importHarvestMouiseRecordsToolStripMenuItem";
        importHarvestMouiseRecordsToolStripMenuItem.Size = new Size(270, 22);
        importHarvestMouiseRecordsToolStripMenuItem.Text = "Import Harvest Mouse Records ...";
        importHarvestMouiseRecordsToolStripMenuItem.Click += importHarvestMouiseRecordsToolStripMenuItem_Click;
        // 
        // reportsToolStripMenuItem
        // 
        reportsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fullListToolStripMenuItem });
        reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        reportsToolStripMenuItem.Size = new Size(59, 20);
        reportsToolStripMenuItem.Text = "Reports";
        // 
        // fullListToolStripMenuItem
        // 
        fullListToolStripMenuItem.Name = "fullListToolStripMenuItem";
        fullListToolStripMenuItem.Size = new Size(180, 22);
        fullListToolStripMenuItem.Text = "Full List";
        fullListToolStripMenuItem.Click += FullListToolStripMenuItem_Click;
        // 
        // exportCSVsToolStripMenuItem
        // 
        exportCSVsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { forQGISToolStripMenuItem });
        exportCSVsToolStripMenuItem.Name = "exportCSVsToolStripMenuItem";
        exportCSVsToolStripMenuItem.Size = new Size(81, 20);
        exportCSVsToolStripMenuItem.Text = "Export CSVs";
        // 
        // forQGISToolStripMenuItem
        // 
        forQGISToolStripMenuItem.Name = "forQGISToolStripMenuItem";
        forQGISToolStripMenuItem.Size = new Size(180, 22);
        forQGISToolStripMenuItem.Text = "For QGIS";
        forQGISToolStripMenuItem.Click += ForQGISToolStripMenuItem_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(menuStrip1);
        IsMdiContainer = true;
        MainMenuStrip = menuStrip1;
        Name = "MainForm";
        Text = "Green Cross Mammals App";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
    private ToolStripMenuItem importCSVToolStripMenuItem;
    private ToolStripMenuItem importRecordersToolStripMenuItem;
    private ToolStripMenuItem importHarvestMouiseRecordsToolStripMenuItem;
    private ToolStripMenuItem importRecordVeriicationStatusesToolStripMenuItem;
    private ToolStripMenuItem importLocationsToolStripMenuItem;
    private ToolStripMenuItem reportsToolStripMenuItem;
    private ToolStripMenuItem fullListToolStripMenuItem;
    private ToolStripMenuItem exportCSVsToolStripMenuItem;
    private ToolStripMenuItem forQGISToolStripMenuItem;
}
