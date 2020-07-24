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
      this.pnlScreen = new System.Windows.Forms.Panel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.btnReset = new System.Windows.Forms.Button();
      this.lblGamesWon = new System.Windows.Forms.Label();
      this.lblGamesLost = new System.Windows.Forms.Label();
      this.lblScore = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
      this.tableLayoutPanel1.Controls.Add(this.pnlScreen, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(554, 410);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // pnlScreen
      // 
      this.pnlScreen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pnlScreen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlScreen.Location = new System.Drawing.Point(3, 3);
      this.pnlScreen.Name = "pnlScreen";
      this.pnlScreen.Size = new System.Drawing.Size(388, 404);
      this.pnlScreen.TabIndex = 0;
      this.pnlScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlScreen_Paint);
      this.pnlScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlScreen_MouseClick);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.btnReset, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.lblGamesWon, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.lblGamesLost, 0, 3);
      this.tableLayoutPanel2.Controls.Add(this.lblScore, 0, 4);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(394, 0);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 6;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(160, 410);
      this.tableLayoutPanel2.TabIndex = 1;
      // 
      // btnReset
      // 
      this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.btnReset.Location = new System.Drawing.Point(42, 148);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new System.Drawing.Size(75, 23);
      this.btnReset.TabIndex = 0;
      this.btnReset.Text = "Reset";
      this.btnReset.UseVisualStyleBackColor = true;
      this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
      // 
      // lblGamesWon
      // 
      this.lblGamesWon.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.lblGamesWon.AutoSize = true;
      this.lblGamesWon.Location = new System.Drawing.Point(3, 183);
      this.lblGamesWon.Name = "lblGamesWon";
      this.lblGamesWon.Size = new System.Drawing.Size(77, 13);
      this.lblGamesWon.TabIndex = 1;
      this.lblGamesWon.Text = "Games Won: 0";
      // 
      // lblGamesLost
      // 
      this.lblGamesLost.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.lblGamesLost.AutoSize = true;
      this.lblGamesLost.Location = new System.Drawing.Point(3, 213);
      this.lblGamesLost.Name = "lblGamesLost";
      this.lblGamesLost.Size = new System.Drawing.Size(75, 13);
      this.lblGamesLost.TabIndex = 2;
      this.lblGamesLost.Text = "Games Lost: 0";
      // 
      // lblScore
      // 
      this.lblScore.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.lblScore.AutoSize = true;
      this.lblScore.Location = new System.Drawing.Point(3, 243);
      this.lblScore.Name = "lblScore";
      this.lblScore.Size = new System.Drawing.Size(47, 13);
      this.lblScore.TabIndex = 3;
      this.lblScore.Text = "Score: 0";
      // 
      // GdiRenderForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(554, 410);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GdiRenderForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Same Game";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Panel pnlScreen;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Button btnReset;
    private System.Windows.Forms.Label lblGamesWon;
    private System.Windows.Forms.Label lblGamesLost;
    private System.Windows.Forms.Label lblScore;
  }
}

