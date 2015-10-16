using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SameGame
{
  public partial class MainForm : Form
  {
    const int Rows = 32;
    const int Columns = 32;

    private List<Cell> grid = new List<Cell>();
    private Random random = new Random();

    private Dictionary<CellVisualState, Cell> currentStates = new Dictionary<CellVisualState, Cell>();
    private AnimationEngine animationEngine = new AnimationEngine();

    public MainForm()
    {
      InitializeComponent();
      animationEngine.Completed += animationEngine_Completed;
      ResetGrid(Color.Red, Color.Goldenrod);
      pnlScreen.Start();
    }

    private void ResetGrid(params Color[] colors)
    {
      grid.Clear();
      int r, c;

      float gridWidth = pnlScreen.Width;
      float gridHeight = pnlScreen.Height;

      float cellWidth = gridWidth / Columns;
      float cellHeight = gridHeight / Rows;

      RectangleF rect = new RectangleF(0, 0, cellWidth, cellHeight);
      for (r = 0; r < Rows; ++r)
      {
        rect.X = 0;
        for (c = 0; c < Columns; ++c)
        {
          var cell = new Cell(rect);
          cell.Color = colors[random.Next(colors.Length)];
          cell.SetCoordinate(r, c);

          grid.Add(cell);
          rect.X += cellWidth;
        }

        rect.Y += cellHeight;
      }
    }

    private void RenderFrame(Graphics graphics)
    {
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      graphics.Clear(Color.Black);

      Color shade = Color.Empty;
      foreach (var cell in grid)
      {
        switch (cell.State)
        {
          case CellVisualState.Hovered:
            {
              shade = Color.FromArgb(100, Color.White);
              break;
            }
          case CellVisualState.Normal:
            {
              shade = cell.Color;
              break;
            }
          case CellVisualState.Pressed:
            {
              shade = Color.FromArgb(100, Color.LightGray);
              break;
            }
        }

        Color color = Colors.Blend(cell.Color, shade, 0.5f);
        using (var brush = new SolidBrush(color))
        {
          graphics.FillRectangle(brush, cell.X, cell.Y, cell.Width, cell.Height);
          graphics.DrawRectangle(Pens.Black, cell.X, cell.Y, cell.Width, cell.Height);
        }
      }
    }

    private Cell ClearState(CellVisualState state)
    {
      Cell current;
      if (currentStates.TryGetValue(state, out current))
      {
        current.State = CellVisualState.Normal;
        currentStates.Remove(state);
      }
      else
      {
        current = null;
      }
      return current;
    }

    private void SetState(CellVisualState state, float x, float y)
    {
      var desired = grid.LastOrDefault(c => c.Contains(x, y));
      if (desired != null)
      {
        desired.State = state;
        currentStates[state] = desired;
      }
    }

    private void UpdateState(CellVisualState state, float x, float y)
    {
      ClearState(state);
      SetState(state, x, y);
    }

    private async void PerformClick(Cell value)
    {
      Color color = value.Color;

      var pending = new List<Cell>();
      pending.Add(value);

      var visited = new HashSet<Cell>();
      visited.Add(value);

      GetNeighbors(value, pending, visited);
      visited.Clear();

      while (pending.Count > 0)
      {
        var cell = pending[0];
        pending.RemoveAt(0);
        animationEngine.Shrink(cell);
        await Task.Delay(1);
      }
    }

    private void GetNeighbors(Cell cell, List<Cell> list, HashSet<Cell> visited)
    {
      foreach (var candidate in grid.Where(c => c.IsNeighbor(cell)))
      {
        if (candidate.Color != cell.Color)
        {
          continue;
        }

        if (!visited.Add(candidate))
        {
          continue;
        }

        list.Add(candidate);
        GetNeighbors(candidate, list, visited);
      }
    }

    private void animationEngine_Completed(object sender, AnimationCompletedEventArgs e)
    {
      grid.RemoveAll(c => c == e.Target);
    }

    private void pnlScreen_RenderFrame(object sender, PanelDbRenderEventArgs e)
    {
      RenderFrame(e.Graphics);
    }

    private void pnlScreen_UpdateFrame(object sender, EventArgs e)
    {
      animationEngine.Update();
    }

    private void pnlScreen_MouseMove(object sender, MouseEventArgs e)
    {
      if (currentStates.ContainsKey(CellVisualState.Pressed))
      {
        return;
      }

      UpdateState(CellVisualState.Hovered, e.X, e.Y);
    }

    private void pnlScreen_MouseLeave(object sender, EventArgs e)
    {
      ClearState(CellVisualState.Hovered);
    }

    private void pnlScreen_MouseDown(object sender, MouseEventArgs e)
    {
      UpdateState(CellVisualState.Pressed, e.X, e.Y);
    }

    private void pnlScreen_MouseUp(object sender, MouseEventArgs e)
    {
      var cell = ClearState(CellVisualState.Pressed);
      if (cell != null)
      {
        PerformClick(cell);
      }
    }
  }
}
