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
    public CellVisualState State;
    public Color Color;
    public bool Pending;

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
  }
}
