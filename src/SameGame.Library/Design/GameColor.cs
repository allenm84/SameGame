using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SameGame
{
  public struct GameColor : IEquatable<GameColor>
  {
    private static class PackUtils
    {
      private static double ClampAndRound(float value, float min, float max)
      {
        if (float.IsNaN(value))
        {
          return 0;
        }
        if (float.IsInfinity(value))
        {
          return (double)((float.IsNegativeInfinity(value) ? min : max));
        }
        if (value < min)
        {
          return (double)min;
        }
        if (value > max)
        {
          return (double)max;
        }
        return Math.Round((double)value);
      }

      public static uint PackSigned(uint bitmask, float value)
      {
        float single = (float)((float)(bitmask >> 1));
        float single1 = -single - 1f;
        return (uint)PackUtils.ClampAndRound(value, single1, single) & bitmask;
      }

      public static uint PackSNorm(uint bitmask, float value)
      {
        float single = (float)((float)(bitmask >> 1));
        value = value * single;
        return (uint)PackUtils.ClampAndRound(value, -single, single) & bitmask;
      }

      public static uint PackUNorm(float bitmask, float value)
      {
        value = value * bitmask;
        return (uint)PackUtils.ClampAndRound(value, 0f, bitmask);
      }

      public static uint PackUnsigned(float bitmask, float value)
      {
        return (uint)PackUtils.ClampAndRound(value, 0f, bitmask);
      }

      public static float UnpackSNorm(uint bitmask, uint value)
      {
        uint num = bitmask + 1 >> 1;
        if ((value & num) == 0)
        {
          value = value & bitmask;
        }
        else
        {
          if ((value & bitmask) == num)
          {
            return -1f;
          }
          value = value | ~bitmask;
        }
        float single = (float)((float)(bitmask >> 1));
        return (float)value / single;
      }

      public static float UnpackUNorm(uint bitmask, uint value)
      {
        value = value & bitmask;
        return (float)((float)value) / (float)((float)bitmask);
      }
    }

    public static GameColor AliceBlue
    {
      get
      {
        return new GameColor(-1808);
      }
    }

    public static GameColor AntiqueWhite
    {
      get
      {
        return new GameColor(-2626566);
      }
    }

    public static GameColor Aqua
    {
      get
      {
        return new GameColor(-256);
      }
    }

    public static GameColor Aquamarine
    {
      get
      {
        return new GameColor(-2818177);
      }
    }

    public static GameColor Azure
    {
      get
      {
        return new GameColor(-16);
      }
    }

    public static GameColor Beige
    {
      get
      {
        return new GameColor(-2296331);
      }
    }

    public static GameColor Bisque
    {
      get
      {
        return new GameColor(-3873537);
      }
    }

    public static GameColor Black
    {
      get
      {
        return new GameColor(-16777216);
      }
    }

    public static GameColor BlanchedAlmond
    {
      get
      {
        return new GameColor(-3281921);
      }
    }

    public static GameColor Blue
    {
      get
      {
        return new GameColor(-65536);
      }
    }

    public static GameColor BlueViolet
    {
      get
      {
        return new GameColor(-1954934);
      }
    }

    public static GameColor Brown
    {
      get
      {
        return new GameColor(-14013787);
      }
    }

    public static GameColor BurlyWood
    {
      get
      {
        return new GameColor(-7882530);
      }
    }

    public static GameColor CadetBlue
    {
      get
      {
        return new GameColor(-6250913);
      }
    }

    public static GameColor Chartreuse
    {
      get
      {
        return new GameColor(-16711809);
      }
    }

    public static GameColor Chocolate
    {
      get
      {
        return new GameColor(-14784046);
      }
    }

    public static GameColor Coral
    {
      get
      {
        return new GameColor(-11501569);
      }
    }

    public static GameColor CornflowerBlue
    {
      get
      {
        return new GameColor(-1206940);
      }
    }

    public static GameColor Cornsilk
    {
      get
      {
        return new GameColor(-2295553);
      }
    }

    public static GameColor Crimson
    {
      get
      {
        return new GameColor(-12839716);
      }
    }

    public static GameColor Cyan
    {
      get
      {
        return new GameColor(-256);
      }
    }

    public static GameColor DarkBlue
    {
      get
      {
        return new GameColor(-7667712);
      }
    }

    public static GameColor DarkCyan
    {
      get
      {
        return new GameColor(-7632128);
      }
    }

    public static GameColor DarkGoldenrod
    {
      get
      {
        return new GameColor(-16021832);
      }
    }

    public static GameColor DarkGray
    {
      get
      {
        return new GameColor(-5658199);
      }
    }

    public static GameColor DarkGreen
    {
      get
      {
        return new GameColor(-16751616);
      }
    }

    public static GameColor DarkKhaki
    {
      get
      {
        return new GameColor(-9717827);
      }
    }

    public static GameColor DarkMagenta
    {
      get
      {
        return new GameColor(-7667573);
      }
    }

    public static GameColor DarkOliveGreen
    {
      get
      {
        return new GameColor(-13669547);
      }
    }

    public static GameColor DarkOrange
    {
      get
      {
        return new GameColor(-16741121);
      }
    }

    public static GameColor DarkOrchid
    {
      get
      {
        return new GameColor(-3394919);
      }
    }

    public static GameColor DarkRed
    {
      get
      {
        return new GameColor(-16777077);
      }
    }

    public static GameColor DarkSalmon
    {
      get
      {
        return new GameColor(-8743191);
      }
    }

    public static GameColor DarkSeaGreen
    {
      get
      {
        return new GameColor(-7619441);
      }
    }

    public static GameColor DarkSlateBlue
    {
      get
      {
        return new GameColor(-7652024);
      }
    }

    public static GameColor DarkSlateGray
    {
      get
      {
        return new GameColor(-11579601);
      }
    }

    public static GameColor DarkTurquoise
    {
      get
      {
        return new GameColor(-3027456);
      }
    }

    public static GameColor DarkViolet
    {
      get
      {
        return new GameColor(-2948972);
      }
    }

    public static GameColor DeepPink
    {
      get
      {
        return new GameColor(-7138049);
      }
    }

    public static GameColor DeepSkyBlue
    {
      get
      {
        return new GameColor(-16640);
      }
    }

    public static GameColor DimGray
    {
      get
      {
        return new GameColor(-9868951);
      }
    }

    public static GameColor DodgerBlue
    {
      get
      {
        return new GameColor(-28642);
      }
    }

    public static GameColor Firebrick
    {
      get
      {
        return new GameColor(-14540110);
      }
    }

    public static GameColor FloralWhite
    {
      get
      {
        return new GameColor(-984321);
      }
    }

    public static GameColor ForestGreen
    {
      get
      {
        return new GameColor(-14513374);
      }
    }

    public static GameColor Fuchsia
    {
      get
      {
        return new GameColor(-65281);
      }
    }

    public static GameColor Gainsboro
    {
      get
      {
        return new GameColor(-2302756);
      }
    }

    public static GameColor GhostWhite
    {
      get
      {
        return new GameColor(-1800);
      }
    }

    public static GameColor Gold
    {
      get
      {
        return new GameColor(-16721921);
      }
    }

    public static GameColor Goldenrod
    {
      get
      {
        return new GameColor(-14637606);
      }
    }

    public static GameColor Gray
    {
      get
      {
        return new GameColor(-8355712);
      }
    }

    public static GameColor Green
    {
      get
      {
        return new GameColor(-16744448);
      }
    }

    public static GameColor GreenYellow
    {
      get
      {
        return new GameColor(-13631571);
      }
    }

    public static GameColor Honeydew
    {
      get
      {
        return new GameColor(-983056);
      }
    }

    public static GameColor HotPink
    {
      get
      {
        return new GameColor(-4953601);
      }
    }

    public static GameColor IndianRed
    {
      get
      {
        return new GameColor(-10724147);
      }
    }

    public static GameColor Indigo
    {
      get
      {
        return new GameColor(-8257461);
      }
    }

    public static GameColor Ivory
    {
      get
      {
        return new GameColor(-983041);
      }
    }

    public static GameColor Khaki
    {
      get
      {
        return new GameColor(-7543056);
      }
    }

    public static GameColor Lavender
    {
      get
      {
        return new GameColor(-334106);
      }
    }

    public static GameColor LavenderBlush
    {
      get
      {
        return new GameColor(-659201);
      }
    }

    public static GameColor LawnGreen
    {
      get
      {
        return new GameColor(-16712580);
      }
    }

    public static GameColor LemonChiffon
    {
      get
      {
        return new GameColor(-3278081);
      }
    }

    public static GameColor LightBlue
    {
      get
      {
        return new GameColor(-1648467);
      }
    }

    public static GameColor LightCoral
    {
      get
      {
        return new GameColor(-8355600);
      }
    }

    public static GameColor LightCyan
    {
      get
      {
        return new GameColor(-32);
      }
    }

    public static GameColor LightGoldenrodYellow
    {
      get
      {
        return new GameColor(-2950406);
      }
    }

    public static GameColor LightGray
    {
      get
      {
        return new GameColor(-2894893);
      }
    }

    public static GameColor LightGreen
    {
      get
      {
        return new GameColor(-7278960);
      }
    }

    public static GameColor LightPink
    {
      get
      {
        return new GameColor(-4081921);
      }
    }

    public static GameColor LightSalmon
    {
      get
      {
        return new GameColor(-8740609);
      }
    }

    public static GameColor LightSeaGreen
    {
      get
      {
        return new GameColor(-5590496);
      }
    }

    public static GameColor LightSkyBlue
    {
      get
      {
        return new GameColor(-340345);
      }
    }

    public static GameColor LightSlateGray
    {
      get
      {
        return new GameColor(-6715273);
      }
    }

    public static GameColor LightSteelBlue
    {
      get
      {
        return new GameColor(-2177872);
      }
    }

    public static GameColor LightYellow
    {
      get
      {
        return new GameColor(-2031617);
      }
    }

    public static GameColor Lime
    {
      get
      {
        return new GameColor(-16711936);
      }
    }

    public static GameColor LimeGreen
    {
      get
      {
        return new GameColor(-13447886);
      }
    }

    public static GameColor Linen
    {
      get
      {
        return new GameColor(-1642246);
      }
    }

    public static GameColor Magenta
    {
      get
      {
        return new GameColor(-65281);
      }
    }

    public static GameColor Maroon
    {
      get
      {
        return new GameColor(-16777088);
      }
    }

    public static GameColor MediumAquamarine
    {
      get
      {
        return new GameColor(-5583514);
      }
    }

    public static GameColor MediumBlue
    {
      get
      {
        return new GameColor(-3342336);
      }
    }

    public static GameColor MediumOrchid
    {
      get
      {
        return new GameColor(-2927174);
      }
    }

    public static GameColor MediumPurple
    {
      get
      {
        return new GameColor(-2396013);
      }
    }

    public static GameColor MediumSeaGreen
    {
      get
      {
        return new GameColor(-9325764);
      }
    }

    public static GameColor MediumSlateBlue
    {
      get
      {
        return new GameColor(-1152901);
      }
    }

    public static GameColor MediumSpringGreen
    {
      get
      {
        return new GameColor(-6620672);
      }
    }

    public static GameColor MediumTurquoise
    {
      get
      {
        return new GameColor(-3354296);
      }
    }

    public static GameColor MediumVioletRed
    {
      get
      {
        return new GameColor(-8055353);
      }
    }

    public static GameColor MidnightBlue
    {
      get
      {
        return new GameColor(-9430759);
      }
    }

    public static GameColor MintCream
    {
      get
      {
        return new GameColor(-327691);
      }
    }

    public static GameColor MistyRose
    {
      get
      {
        return new GameColor(-1972993);
      }
    }

    public static GameColor Moccasin
    {
      get
      {
        return new GameColor(-4856577);
      }
    }

    public static GameColor NavajoWhite
    {
      get
      {
        return new GameColor(-5382401);
      }
    }

    public static GameColor Navy
    {
      get
      {
        return new GameColor(-8388608);
      }
    }

    public static GameColor OldLace
    {
      get
      {
        return new GameColor(-1640963);
      }
    }

    public static GameColor Olive
    {
      get
      {
        return new GameColor(-16744320);
      }
    }

    public static GameColor OliveDrab
    {
      get
      {
        return new GameColor(-14446997);
      }
    }

    public static GameColor Orange
    {
      get
      {
        return new GameColor(-16734721);
      }
    }

    public static GameColor OrangeRed
    {
      get
      {
        return new GameColor(-16759297);
      }
    }

    public static GameColor Orchid
    {
      get
      {
        return new GameColor(-2723622);
      }
    }


    public static GameColor PaleGoldenrod
    {
      get
      {
        return new GameColor(-5576466);
      }
    }

    public static GameColor PaleGreen
    {
      get
      {
        return new GameColor(-6751336);
      }
    }

    public static GameColor PaleTurquoise
    {
      get
      {
        return new GameColor(-1118545);
      }
    }

    public static GameColor PaleVioletRed
    {
      get
      {
        return new GameColor(-7114533);
      }
    }

    public static GameColor PapayaWhip
    {
      get
      {
        return new GameColor(-2756609);
      }
    }

    public static GameColor PeachPuff
    {
      get
      {
        return new GameColor(-4596993);
      }
    }

    public static GameColor Peru
    {
      get
      {
        return new GameColor(-12614195);
      }
    }

    public static GameColor Pink
    {
      get
      {
        return new GameColor(-3424001);
      }
    }

    public static GameColor Plum
    {
      get
      {
        return new GameColor(-2252579);
      }
    }

    public static GameColor PowderBlue
    {
      get
      {
        return new GameColor(-1646416);
      }
    }

    public static GameColor Purple
    {
      get
      {
        return new GameColor(-8388480);
      }
    }

    public static GameColor Red
    {
      get
      {
        return new GameColor(-16776961);
      }
    }

    public static GameColor RosyBrown
    {
      get
      {
        return new GameColor(-7368772);
      }
    }

    public static GameColor RoyalBlue
    {
      get
      {
        return new GameColor(-2004671);
      }
    }

    public static GameColor SaddleBrown
    {
      get
      {
        return new GameColor(-15514229);
      }
    }

    public static GameColor Salmon
    {
      get
      {
        return new GameColor(-9273094);
      }
    }

    public static GameColor SandyBrown
    {
      get
      {
        return new GameColor(-10443532);
      }
    }

    public static GameColor SeaGreen
    {
      get
      {
        return new GameColor(-11039954);
      }
    }

    public static GameColor SeaShell
    {
      get
      {
        return new GameColor(-1116673);
      }
    }

    public static GameColor Sienna
    {
      get
      {
        return new GameColor(-13806944);
      }
    }

    public static GameColor Silver
    {
      get
      {
        return new GameColor(-4144960);
      }
    }

    public static GameColor SkyBlue
    {
      get
      {
        return new GameColor(-1323385);
      }
    }

    public static GameColor SlateBlue
    {
      get
      {
        return new GameColor(-3319190);
      }
    }

    public static GameColor SlateGray
    {
      get
      {
        return new GameColor(-7307152);
      }
    }

    public static GameColor Snow
    {
      get
      {
        return new GameColor(-328961);
      }
    }

    public static GameColor SpringGreen
    {
      get
      {
        return new GameColor(-8388864);
      }
    }

    public static GameColor SteelBlue
    {
      get
      {
        return new GameColor(-4947386);
      }
    }

    public static GameColor Tan
    {
      get
      {
        return new GameColor(-7555886);
      }
    }

    public static GameColor Teal
    {
      get
      {
        return new GameColor(-8355840);
      }
    }

    public static GameColor Thistle
    {
      get
      {
        return new GameColor(-2572328);
      }
    }

    public static GameColor Tomato
    {
      get
      {
        return new GameColor(-12098561);
      }
    }

    public static GameColor Transparent
    {
      get
      {
        return new GameColor(0);
      }
    }

    public static GameColor Turquoise
    {
      get
      {
        return new GameColor(-3088320);
      }
    }

    public static GameColor Violet
    {
      get
      {
        return new GameColor(-1146130);
      }
    }

    public static GameColor Wheat
    {
      get
      {
        return new GameColor(-4989195);
      }
    }

    public static GameColor White
    {
      get
      {
        return new GameColor(-1);
      }
    }

    public static GameColor WhiteSmoke
    {
      get
      {
        return new GameColor(-657931);
      }
    }

    public static GameColor Yellow
    {
      get
      {
        return new GameColor(-16711681);
      }
    }

    public static GameColor YellowGreen
    {
      get
      {
        return new GameColor(-13447782);
      }
    }

    private int packedValue;

    public byte A
    {
      get { return (byte)(this.packedValue >> 24); }
      set { this.packedValue = this.packedValue & 16777215 | value << 24; }
    }

    public byte R
    {
      get { return (byte)this.packedValue; }
      set { this.packedValue = this.packedValue & -256 | value; }
    }

    public byte G
    {
      get { return (byte)(this.packedValue >> 8); }
      set { this.packedValue = this.packedValue & -65281 | value << 8; }
    }

    public byte B
    {
      get { return (byte)(this.packedValue >> 16); }
      set { this.packedValue = this.packedValue & -16711681 | value << 16; }
    }

    private GameColor(int packedValue)
    {
      this.packedValue = packedValue;
    }

    public GameColor(int r, int g, int b)
    {
      if (((r | g | b) & -256) != 0)
      {
        r = GameColor.ClampToByte64((long)r);
        g = GameColor.ClampToByte64((long)g);
        b = GameColor.ClampToByte64((long)b);
      }
      g = g << 8;
      b = b << 16;
      this.packedValue = (int)(r | g | b | -16777216);
    }

    public GameColor(int r, int g, int b, int a)
    {
      if (((r | g | b | a) & -256) != 0)
      {
        r = GameColor.ClampToByte32(r);
        g = GameColor.ClampToByte32(g);
        b = GameColor.ClampToByte32(b);
        a = GameColor.ClampToByte32(a);
      }
      g = g << 8;
      b = b << 16;
      a = a << 24;
      this.packedValue = (int)(r | g | b | a);
    }

    public GameColor(float r, float g, float b)
    {
      this.packedValue = GameColor.PackHelper(r, g, b, 1f);
    }

    public GameColor(float r, float g, float b, float a)
    {
      this.packedValue = GameColor.PackHelper(r, g, b, a);
    }

    private static int ClampToByte32(int value)
    {
      if (value < 0)
      {
        return 0;
      }
      if (value > 255)
      {
        return 255;
      }
      return value;
    }

    private static int ClampToByte64(long value)
    {
      if (value < (long)0)
      {
        return 0;
      }
      if (value > (long)255)
      {
        return 255;
      }
      return (int)value;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is GameColor))
      {
        return false;
      }
      return this.Equals((GameColor)obj);
    }

    public bool Equals(GameColor other)
    {
      return this.packedValue.Equals(other.packedValue);
    }

    public override int GetHashCode()
    {
      return this.packedValue.GetHashCode();
    }

    public static GameColor Multiply(GameColor value, float scale)
    {
      int num;
      GameColor color = new GameColor();
      int num1 = value.packedValue;
      int num2 = (byte)num1;
      int num3 = (byte)(num1 >> 8);
      int num4 = (byte)(num1 >> 16);
      int num5 = (byte)(num1 >> 24);
      scale = scale * 65536f;
      if (scale < 0f)
      {
        num = 0;
      }
      else if (scale <= 16777215f)
      {
        num = (int)scale;
      }
      else
      {
        num = 16777215;
      }
      num2 = num2 * num >> 16;
      num3 = num3 * num >> 16;
      num4 = num4 * num >> 16;
      num5 = num5 * num >> 16;
      if (num2 > 255)
      {
        num2 = 255;
      }
      if (num3 > 255)
      {
        num3 = 255;
      }
      if (num4 > 255)
      {
        num4 = 255;
      }
      if (num5 > 255)
      {
        num5 = 255;
      }
      color.packedValue = num2 | num3 << 8 | num4 << 16 | num5 << 24;
      return color;
    }

    public static bool operator ==(GameColor a, GameColor b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(GameColor a, GameColor b)
    {
      return !a.Equals(b);
    }

    public static GameColor operator *(GameColor value, float scale)
    {
      int num;
      GameColor color = new GameColor();
      int num1 = value.packedValue;
      int num2 = (byte)num1;
      int num3 = (byte)(num1 >> 8);
      int num4 = (byte)(num1 >> 16);
      int num5 = (byte)(num1 >> 24);
      scale = scale * 65536f;
      if (scale < 0f)
      {
        num = 0;
      }
      else if (scale <= 16777215f)
      {
        num = (int)scale;
      }
      else
      {
        num = 16777215;
      }
      num2 = num2 * num >> 16;
      num3 = num3 * num >> 16;
      num4 = num4 * num >> 16;
      num5 = num5 * num >> 16;
      if (num2 > 255)
      {
        num2 = 255;
      }
      if (num3 > 255)
      {
        num3 = 255;
      }
      if (num4 > 255)
      {
        num4 = 255;
      }
      if (num5 > 255)
      {
        num5 = 255;
      }
      color.packedValue = num2 | num3 << 8 | num4 << 16 | num5 << 24;
      return color;
    }

    private static int PackHelper(float vectorX, float vectorY, float vectorZ, float vectorW)
    {
      uint num = PackUtils.PackUNorm(255f, vectorX);
      uint num1 = PackUtils.PackUNorm(255f, vectorY) << 8;
      uint num2 = PackUtils.PackUNorm(255f, vectorZ) << 16;
      uint num3 = PackUtils.PackUNorm(255f, vectorW) << 24;
      return (int)(num | num1 | num2 | num3);
    }

    public override string ToString()
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      object[] r = new object[] { this.R, this.G, this.B, this.A };
      return string.Format(currentCulture, "{{R:{0} G:{1} B:{2} A:{3}}}", r);
    }
  }
}
