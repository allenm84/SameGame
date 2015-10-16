using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public delegate void AnimationCompletedEventHandler(object sender, AnimationCompletedEventArgs e);

  public class AnimationCompletedEventArgs : EventArgs
  {
    public BaseAnimationObject Target { get; private set; }
    public AnimationCompletedEventArgs(BaseAnimationObject target)
    {
      Target = target;
    }
  }

  public class AnimationEngine
  {
    private DateTime last;
    private List<AnimationItem> animations = new List<AnimationItem>();

    public event AnimationCompletedEventHandler Completed;

    public AnimationEngine()
    {

    }

    public void Shrink(BaseAnimationObject item)
    {
      var animation = new ShrinkAnimationItem();
      animation.From = 100;
      animation.To = 10;
      animation.Duration = TimeSpan.FromMilliseconds(400).TotalSeconds;
      animation.Equation = Equations.BounceEaseIn;
      animation.Target = item;
      animation.Snapshot = item.TakeSnapshot();
      animations.Add(animation);
    }

    public void Update()
    {
      DateTime now = DateTime.Now;
      TimeSpan elapsed = (now - last);

      animations.ForEach(i => i.Update(elapsed.TotalSeconds));
      for (int i = animations.Count - 1; i > -1; --i)
      {
        var animation = animations[i];
        if (animation.Completed)
        {
          animations.RemoveAt(i);
          FireCompleted(animation.Target);
        }
      }

      last = now;
    }

    private void FireCompleted(BaseAnimationObject item)
    {
      var completed = Completed;
      if (completed != null)
      {
        completed(this, new AnimationCompletedEventArgs(item));
      }
    }
  }
}
