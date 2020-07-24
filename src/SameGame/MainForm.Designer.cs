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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.lblScore = new System.Windows.Forms.Label();
      this.btnReset = new System.Windows.Forms.Button();
      this.btnNew = new System.Windows.Forms.Button();
      this.pnlScreen = new System.Windows.Forms.PanelDb();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 5;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
      this.tableLayoutPanel1.Controls.Add(this.pnlScreen, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblScore, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.btnReset, 3, 0);
      this.tableLayoutPanel1.Controls.Add(this.btnNew, 4, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 11);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 539);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(49, 14);
      this.label1.TabIndex = 1;
      this.label1.Text = "Score:";
      // 
      // lblScore
      // 
      this.lblScore.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.lblScore.AutoSize = true;
      this.lblScore.Location = new System.Drawing.Point(138, 8);
      this.lblScore.Name = "lblScore";
      this.lblScore.Size = new System.Drawing.Size(14, 14);
      this.lblScore.TabIndex = 2;
      this.lblScore.Text = "0";
      // 
      // btnReset
      // 
      this.btnReset.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnReset.Location = new System.Drawing.Point(603, 3);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new System.Drawing.Size(74, 24);
      this.btnReset.TabIndex = 3;
      this.btnReset.Text = "Reset";
      this.btnReset.UseVisualStyleBackColor = true;
      // 
      // btnNew
      // 
      this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnNew.Location = new System.Drawing.Point(683, 3);
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(74, 24);
      this.btnNew.TabIndex = 4;
      this.btnNew.Text = "New";
      this.btnNew.UseVisualStyleBackColor = true;
      // 
      // pnlScreen
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.pnlScreen, 5);
      this.pnlScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlScreen.Location = new System.Drawing.Point(3, 33);
      this.pnlScreen.Name = "pnlScreen";
      this.pnlScreen.Size = new System.Drawing.Size(754, 503);
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
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 561);
      this.Controls.Add(this.tableLayoutPanel1);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Same Game";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PanelDb pnlScreen;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblScore;
    private System.Windows.Forms.Button btnReset;
    private System.Windows.Forms.Button btnNew;
  }
}

