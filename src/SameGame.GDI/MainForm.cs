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
      pnlScreen.SetDoubleBuffered(true);
      pnlScreen.SetResizeRedraw(true);
    }

    private void UpdateDisplay()
    {
      lblGamesWon.Text = string.Format("Games Won: {0}", won);
      lblGamesLost.Text = string.Format("Game Lost: {0}", lost);
      lblScore.Text = string.Format("Score: {0}", controller.Score);
    }

    private void GetBoardSize(out SizeF client, out SizeF block)
    {
      client = pnlScreen.ClientSize;
      block = client;
      block.Width /= controller.Columns;
      block.Height /= controller.Rows;
    }

    private Brush GetBrush(GameColor gameColor, Dictionary<int, Brush> brushes)
    {
      Brush brush;
      Color color = ToColor(gameColor);

      int key = color.ToArgb();
      if (!brushes.TryGetValue(key, out brush))
      {
        brush = new SolidBrush(color);
        brushes[key] = brush;
      }
      return brush;
    }

    private void pnlScreen_Paint(object sender, PaintEventArgs e)
    {
      UpdateDisplay();

      var graphics = e.Graphics;
      graphics.Clear(Color.SlateBlue);

      SizeF client, block;
      GetBoardSize(out client, out block);

      var brushes = new Dictionary<int, Brush>();
      var rect = RectangleF.Empty;
      rect.Size = block;

      int r, c;
      for (r = 0; r < controller.Rows; ++r)
      {
        for (c = 0; c < controller.Columns; ++c)
        {
          var piece = controller[r, c];
          if (piece != null)
          {
            graphics.FillRectangle(GetBrush(piece.Color, brushes), rect);
            graphics.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height);
          }
          rect.X += block.Width;
        }
        rect.Y += block.Height;
        rect.X = 0;
      }

      foreach (var kvp in brushes)
      {
        kvp.Value.Dispose();
      }
      brushes.Clear();
    }

    private void pnlScreen_MouseClick(object sender, MouseEventArgs e)
    {
      if ((e.Button & System.Windows.Forms.MouseButtons.Left) != 0)
      {
        SizeF client, block;
        GetBoardSize(out client, out block);

        PointF pt = e.Location;

        var rect = RectangleF.Empty;
        rect.Size = block;

        int r, c;
        for (r = 0; r < controller.Rows; ++r)
        {
          for (c = 0; c < controller.Columns; ++c)
          {
            var piece = controller[r, c];
            if (piece != null && rect.Contains(pt))
            {
              if (controller.Click(r, c))
              {
                if (controller.GameWon) { ++won; }
                else { ++lost; }
              }
            }
            rect.X += block.Width;
          }
          rect.Y += block.Height;
          rect.X = 0;
        }

        pnlScreen.Invalidate();
      }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
      controller.ResetBoard();
      pnlScreen.Invalidate();
    }
  }
}
