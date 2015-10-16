using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public abstract class AnimationItem
  {
    public BaseAnimationObject Target;
    public BaseAnimationObjectSnapshot Snapshot;

    public double Elapsed = 0;
    public double From, To;
    public double Current;
    public double Duration;
    public bool Completed;
    public Equation Equation;

    public abstract AnimationType Type { get; }

    protected abstract void Apply();

    public void Update(double seconds)
    {
      Elapsed += seconds;
      Current = Equation(Elapsed, From, (To - From), Duration);

      if (Elapsed >= Duration)
      {
        Current = To;
        Completed = true;
      }

      Apply();
    }
  }
}
