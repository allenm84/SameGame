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
    private Dictionary<BaseAnimationObject, int> targets = new Dictionary<BaseAnimationObject, int>();

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
      UpdateCount(item, 1);
    }

    private void UpdateCount(BaseAnimationObject item, int delta)
    {
      int count;
      if (!targets.TryGetValue(item, out count))
      {
        if (delta <= 0)
        {
          return;
        }

        count = 0;
        targets[item] = count;
      }

      count += delta;
      if (count <= 0)
      {
        targets.Remove(item);
      }
      else
      {
        targets[item] = count;
      }
    }

    public bool IsAnimating(BaseAnimationObject item)
    {
      return targets.ContainsKey(item);
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
          UpdateCount(animation.Target, -1);
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
