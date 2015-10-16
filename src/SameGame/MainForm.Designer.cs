namespace SameGame
{
  partial class MainForm
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
      this.pnlScreen = new System.Windows.Forms.PanelDb();
      this.SuspendLayout();
      // 
      // pnlScreen
      // 
      this.pnlScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlScreen.Location = new System.Drawing.Point(10, 10);
      this.pnlScreen.Name = "pnlScreen";
      this.pnlScreen.Size = new System.Drawing.Size(764, 541);
      this.pnlScreen.TabIndex = 0;
      this.pnlScreen.RenderFrame += new System.Windows.Forms.PanelDbRenderEventHandler(this.pnlScreen_RenderFrame);
      this.pnlScreen.UpdateFrame += new System.EventHandler(this.pnlScreen_UpdateFrame);
      this.pnlScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlScreen_MouseDown);
      this.pnlScreen.MouseLeave += new System.EventHandler(this.pnlScreen_MouseLeave);
      this.pnlScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlScreen_MouseMove);
      this.pnlScreen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlScreen_MouseUp);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 561);
      this.Controls.Add(this.pnlScreen);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Same Game";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PanelDb pnlScreen;
  }
}

