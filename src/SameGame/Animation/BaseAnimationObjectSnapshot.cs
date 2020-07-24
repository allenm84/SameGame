using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SameGame
{
  public struct BaseAnimationObjectSnapshot
  {
    public float X, Y, Width, Height;
    public BaseAnimationObjectSnapshot(float x, float y, float width, float height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }
  }
}
