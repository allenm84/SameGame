using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public class Cell : BaseAnimationObject
  {
    static readonly int[][] mods = new int[][]
    {
      new int[] { 0, 1 },
      new int[] { 0, -1 },
      new int[] { 1, 0 },
      new int[] { -1, 0 }
    };

    public CellVisualState State;
    public Color Color;

    private int row, column;

    public Cell()
      : this(RectangleF.Empty)
    {

    }

    public Cell(RectangleF rect)
    {
      X = rect.X;
      Y = rect.Y;
      Width = rect.Width;
      Height = rect.Height;
      State = CellVisualState.Normal;
    }

    public void SetCoordinate(int row, int column)
    {
      this.row = row;
      this.column = column;
    }

    public bool IsNeighbor(Cell b)
    {
      return mods.Any(m => 
        (b.row == row + m[0]) && 
        (b.column == column + m[1]));
    }
  }
}
