using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Common.Extensions;

namespace SameGame
{
  public partial class MainForm : Form
  {
    private GameController controller;
    private int won = 0, lost = 0;

    private static Color ToColor(GameColor color)
    {
      return Color.FromArgb(color.A, color.R, color.G, color.B);
    }

    public MainForm()
    {
      InitializeComponent();
      controller = new GameController(20, 20);
      InitializeBoard();
    }

    private void InitializeBoard()
    {
      float columnPercentage = 100f / controller.Columns;
      float rowPercentage = 100f / controller.Rows;
      int i;

      tblBoard.SuspendLayout();

      tblBoard.ColumnCount = controller.Columns;
      tblBoard.ColumnStyles.Clear();
      for (i = 0; i < controller.Columns; ++i)
      {
        tblBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentage));
      }

      tblBoard.RowCount = controller.Rows;
      tblBoard.RowStyles.Clear();
      for (i = 0; i < controller.Rows; ++i)
      {
        tblBoard.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercentage));
      }

      tblBoard.ResumeLayout(true);
      ReCreateButtons();
    }

    private void ReCreateButtons()
    {
      tblBoard.SuspendDrawing();
      tblBoard.SuspendLayout();

      var buttons = tblBoard.Controls.Cast<Button>().ToArray();
      tblBoard.Controls.Clear();
      foreach (var button in buttons)
      {
        button.Tag = null;
        button.Click -= button_Click;
        button.Dispose();
      }

      int r, c;
      for (r = 0; r < controller.Rows; ++r)
      {
        for (c = 0; c < controller.Columns; ++c)
        {
          var piece = controller[r, c];
          if (piece == null) continue;

          var button = new Button();
          button.UseVisualStyleBackColor = true;
          button.Dock = DockStyle.Fill;
          button.BackColor = ToColor(piece.Color);
          button.Tag = piece;
          button.FlatStyle = FlatStyle.Flat;
          button.Margin = new Padding(0);
          button.Click += button_Click;
          tblBoard.Controls.Add(button, c, r);
        }
      }

      tblBoard.ResumeLayout(true);
      tblBoard.ResumeDrawing();

      UpdateDisplay();
    }

    private void UpdateDisplay()
    {
      lblGamesWon.Text = string.Format("Games Won: {0}", won);
      lblGamesLost.Text = string.Format("Game Lost: {0}", lost);
      lblScore.Text = string.Format("Score: {0}", controller.Score);
    }

    private void button_Click(object sender, EventArgs e)
    {
      var button = sender as Button;
      var piece = button.Tag as GamePiece;
      if (controller.Click(piece))
      {
        if (controller.GameWon) { ++won; }
        else { ++lost; }
      }
      ReCreateButtons();
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      controller.ResetBoard();
      ReCreateButtons();
    }
  }
}
