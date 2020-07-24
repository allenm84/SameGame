using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public class MoveVertAnimationItem : AnimationItem
  {
    public override AnimationType Type
    {
      get { return AnimationType.MoveVert; }
    }

    protected override void Apply()
    {
      Target.Y = (float)Current;
    }
  }
}
