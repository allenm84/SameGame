using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public class MoveHorzAnimationItem : AnimationItem
  {
    public override AnimationType Type
    {
      get { return AnimationType.MoveHorz; }
    }

    protected override void Apply()
    {
      Target.X = (float)Current;
    }
  }
}
