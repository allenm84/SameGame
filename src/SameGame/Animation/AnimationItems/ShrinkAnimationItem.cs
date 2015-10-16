using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public class ShrinkAnimationItem : AnimationItem
  {
    public override AnimationType Type
    {
      get { return AnimationType.Shrink; }
    }

    protected override void Apply()
    {
      float scale = ((float)(Current / 100));
      float desiredWidth = Snapshot.Width * scale;
      float desiredHeight = Snapshot.Height * scale;
      Target.Width = desiredWidth;
      Target.Height = desiredHeight;

      float cX = Snapshot.X + (Snapshot.Width / 2);
      float cY = Snapshot.Y + (Snapshot.Height / 2);

      Target.X = cX - (desiredWidth / 2);
      Target.Y = cY - (desiredHeight / 2);
    }
  }
}
