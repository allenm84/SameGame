using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public abstract class BaseAnimationObject
  {
    public float X, Y, Width, Height;

    public float Bottom { get { return Y + Height; } }
    public float Right { get { return X + Width; } }

    public bool Contains(float x, float y)
    {
      if (x < X) return false;
      if (y < Y) return false;
      if (x > Right) return false;
      if (y > Bottom) return false;
      return true;
    }

    public BaseAnimationObjectSnapshot TakeSnapshot()
    {
      return new BaseAnimationObjectSnapshot(X, Y, Width, Height);
    }
  }
}
