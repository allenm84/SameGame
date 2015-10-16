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
    static readonly int[][] mods = new int[][]
    {
      new int[] { 0, 1 },
      new int[] { 0, -1 },
      new int[] { 1, 0 },
      new int[] { -1, 0 }
    };

    const int Rows = 32;
    const int Columns = 32;

    private List<Cell> grid = new List<Cell>();
    private CellLookup lookup = new CellLookup();

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
          lookup[r, c] = cell;

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
      value.Pending = true;

      int r, c;
      lookup.GetCoordinate(value, out r, out c);
      GetNeighbors(r, c, value.Color, pending, visited);
      visited.Clear();

      if (pending.Count > 1)
      {
        foreach (var cell in pending)
        {
          animationEngine.Shrink(cell);
          await Task.Yield();
        }
      }
    }

    private void GetNeighbors(int row, int column, Color color, List<Cell> list, HashSet<Cell> visited)
    {
      int r, c;
      foreach (var m in mods)
      {
        r = row + m[0];
        c = column + m[1];

        Cell cell = lookup[r, c];
        if (cell == null)
        {
          continue;
        }

        if (cell.Color != color)
        {
          continue;
        }

        if (!visited.Add(cell))
        {
          continue;
        }

        cell.Pending = true;
        list.Add(cell);
        GetNeighbors(r, c, color, list, visited);
      }
    }

    private void ApplyGravity()
    {
      MoveDown();
      MoveLeft();
    }

    private void MoveDown()
    {
      int r, c, rb;

      for (r = Rows - 2; r > -1; --r)
      {
        rb = r + 1;
        for (c = 0; c < Columns; ++c)
        {
          var cell = lookup[r, c];
          if (cell == null)
          {
            continue;
          }

          if (lookup.Exists(rb, c))
          {
            continue;
          }

          lookup.Move(r, c, rb, c);
          animationEngine.MoveVert(cell, rb * cell.Height);
        }
      }
    }

    private void MoveLeft()
    {
      int r, c, cl;

      for (c = 1; c < Columns; ++c)
      {
        cl = c - 1;
        for (r = 0; r < Rows; ++r)
        {
          var cell = lookup[r, c];
          if (cell == null)
          {
            continue;
          }

          if (lookup.Exists(r, cl))
          {
            continue;
          }

          lookup.Move(r, c, r, cl);
          animationEngine.MoveHorz(cell, cl * cell.Width);
        }
      }
    }

    private void animationEngine_Completed(object sender, AnimationCompletedEventArgs e)
    {
      if (e.Type == AnimationType.Shrink)
      {
        Cell cell = (Cell)e.Target;
        grid.Remove(cell);
        lookup.Remove(cell);
      }
    }

    private void pnlScreen_RenderFrame(object sender, PanelDbRenderEventArgs e)
    {
      RenderFrame(e.Graphics);
    }

    private void pnlScreen_UpdateFrame(object sender, EventArgs e)
    {
      animationEngine.Update();
      ApplyGravity();
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
