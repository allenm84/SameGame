using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  [Flags]
  public enum AnimationType
  {
    None = 0,
    Shrink = 1 << 0,
    MoveVert = 1 << 1,
    MoveHorz = 1 << 2,
  }
}
