using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public static class Colors
  {
    public static Color Blend(this Color value, Color color, float amount)
    {
      float mu = (1 - amount);
      var a = (byte)((value.A * amount) + color.A * mu);
      var r = (byte)((value.R * amount) + color.R * mu);
      var g = (byte)((value.G * amount) + color.G * mu);
      var b = (byte)((value.B * amount) + color.B * mu);
      return Color.FromArgb(a, r, g, b);
    }

    public static Color Complement(this Color color)
    {
      double max = Math.Max(color.R, Math.Max(color.G, color.B));
      double min = Math.Min(color.R, Math.Min(color.G, color.B));

      double hue = color.GetHue();
      var saturation = (max == 0) ? 0 : 1d - (1d * min / max);
      var value = max / 255d;

      hue += 180.0;
      while (hue > 360.0) { hue -= 360.0; }
      while (hue < 0.0) { hue += 360.0; }

      var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
      var f = hue / 60 - Math.Floor(hue / 60);

      value = value * 255;
      var v = Convert.ToInt32(value);
      var p = Convert.ToInt32(value * (1 - saturation));
      var q = Convert.ToInt32(value * (1 - f * saturation));
      var t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

      var retval = Color.Empty;
      switch (hi)
      {
        case 0:
          retval = Color.FromArgb(255, v, t, p);
          break;
        case 1:
          retval = Color.FromArgb(255, q, v, p);
          break;
        case 2:
          retval = Color.FromArgb(255, p, v, t);
          break;
        case 3:
          retval = Color.FromArgb(255, p, q, v);
          break;
        case 4:
          retval = Color.FromArgb(255, t, p, v);
          break;
        default:
          retval = Color.FromArgb(255, v, p, q);
          break;
      }
      return retval;
    }
  }
}
