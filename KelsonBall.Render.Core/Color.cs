using OpenTK.Graphics;
using Render.Core.Extensions;
using System;

namespace Render.Core
{
    public class Color
    {
        private readonly Color4 _color;

        public Color(Color4 color)
        {
            _color = color;
        }

        public Color(int color)
        {
            _color = new Color4(color.GetByte(2), color.GetByte(1), color.GetByte(1), color.GetByte(0));
        }

        public Color(string color)
        {
            if (color.StartsWith("0x"))
                color = color.Substring(2);
            else if (color.StartsWith("#"))
                color = color.Substring(1);
            else
                throw new InvalidOperationException($"Cannot convert this string to a color ({color})");

            byte r, g, b, a;
            switch (color.Length)
            {
                case 2:
                    b = Convert.ToByte(color, 16);
                    _color = new Color4(b, b, b, 0xFF);
                    break;
                case 6:
                    r = Convert.ToByte(color.Substring(0, 2), 16);
                    g = Convert.ToByte(color.Substring(2, 2), 16);
                    b = Convert.ToByte(color.Substring(4, 2), 16);
                    _color = new Color4(r, g, b, 0xFF);
                    break;
                case 8:
                    a = Convert.ToByte(color.Substring(0, 2), 16);
                    r = Convert.ToByte(color.Substring(2, 2), 16);
                    g = Convert.ToByte(color.Substring(4, 2), 16);
                    b = Convert.ToByte(color.Substring(6, 2), 16);
                    _color = new Color4(r, g, b, a);
                    break;
            }
        }

        public byte A => (_color.ToArgb().GetByte(3));
        public double Alpha => _color.A;
        public byte R => (_color.ToArgb().GetByte(2));
        public double Red => _color.R;
        public byte G => (_color.ToArgb().GetByte(1));
        public double Green => _color.G;
        public byte B => (_color.ToArgb().GetByte(0));
        public double Blue => _color.B;

        public double Hue => Color4.ToHsv(_color).X;
        public double Saturation => Color4.ToHsv(_color).Y;
        public double Value => Color4.ToHsv(_color).Z;

        public static implicit operator Color4 (Color color) => color._color;

        public static implicit operator Color (Color4 color) => new Color(color);

        public static implicit operator Color (String hex) => new Color(hex);

        public static class Constants
        {
            public static Color MediumAquamarine => Color4.MediumAquamarine;

            public static Color MediumBlue => Color4.MediumBlue;

            public static Color MediumOrchid => Color4.MediumOrchid;

            public static Color MediumPurple => Color4.MediumPurple;

            public static Color MediumSeaGreen => Color4.MediumSeaGreen;

            public static Color MediumSlateBlue => Color4.MediumSlateBlue;

            public static Color MediumSpringGreen => Color4.MediumSpringGreen;

            public static Color MediumTurquoise => Color4.MediumTurquoise;

            public static Color MintCream => Color4.MintCream;

            public static Color MidnightBlue => Color4.MidnightBlue;

            public static Color Maroon => Color4.Maroon;

            public static Color MistyRose => Color4.MistyRose;

            public static Color Moccasin => Color4.Moccasin;

            public static Color NavajoWhite => Color4.NavajoWhite;

            public static Color Navy => Color4.Navy;

            public static Color OldLace => Color4.OldLace;

            public static Color MediumVioletRed => Color4.MediumVioletRed;

            public static Color Magenta => Color4.Magenta;

            public static Color LightYellow => Color4.LightYellow;

            public static Color LimeGreen => Color4.LimeGreen;

            public static Color LavenderBlush => Color4.LavenderBlush;

            public static Color LawnGreen => Color4.LawnGreen;

            public static Color LemonChiffon => Color4.LemonChiffon;

            public static Color LightBlue => Color4.LightBlue;

            public static Color LightCoral => Color4.LightCoral;

            public static Color LightCyan => Color4.LightCyan;

            public static Color LightGoldenrodYellow => Color4.LightGoldenrodYellow;

            public static Color LightGreen => Color4.LightGreen;

            public static Color LightGray => Color4.LightGray;

            public static Color LightPink => Color4.LightPink;

            public static Color LightSalmon => Color4.LightSalmon;

            public static Color LightSeaGreen => Color4.LightSeaGreen;

            public static Color LightSkyBlue => Color4.LightSkyBlue;

            public static Color LightSlateGray => Color4.LightSlateGray;

            public static Color LightSteelBlue => Color4.LightSteelBlue;

            public static Color Olive => Color4.Olive;

            public static Color Lime => Color4.Lime;

            public static Color Linen => Color4.Linen;

            public static Color OliveDrab => Color4.OliveDrab;

            public static Color Orchid => Color4.Orchid;

            public static Color OrangeRed => Color4.OrangeRed;

            public static Color Silver => Color4.Silver;

            public static Color SkyBlue => Color4.SkyBlue;

            public static Color SlateBlue => Color4.SlateBlue;

            public static Color SlateGray => Color4.SlateGray;

            public static Color Snow => Color4.Snow;

            public static Color SpringGreen => Color4.SpringGreen;

            public static Color SteelBlue => Color4.SteelBlue;

            public static Color Sienna => Color4.Sienna;

            public static Color Tan => Color4.Tan;

            public static Color Thistle => Color4.Thistle;

            public static Color Tomato => Color4.Tomato;

            public static Color Turquoise => Color4.Turquoise;

            public static Color Violet => Color4.Violet;

            public static Color Wheat => Color4.Wheat;

            public static Color White => Color4.White;

            public static Color WhiteSmoke => Color4.WhiteSmoke;

            public static Color Teal => Color4.Teal;

            public static Color SeaShell => Color4.SeaShell;

            public static Color SeaGreen => Color4.SeaGreen;

            public static Color SandyBrown => Color4.SandyBrown;

            public static Color Lavender => Color4.Lavender;

            public static Color PaleGoldenrod => Color4.PaleGoldenrod;

            public static Color PaleGreen => Color4.PaleGreen;

            public static Color PaleTurquoise => Color4.PaleTurquoise;

            public static Color PaleVioletRed => Color4.PaleVioletRed;

            public static Color PapayaWhip => Color4.PapayaWhip;

            public static Color PeachPuff => Color4.PeachPuff;

            public static Color Peru => Color4.Peru;

            public static Color Pink => Color4.Pink;

            public static Color Plum => Color4.Plum;

            public static Color PowderBlue => Color4.PowderBlue;

            public static Color Purple => Color4.Purple;

            public static Color Red => Color4.Red;

            public static Color RosyBrown => Color4.RosyBrown;

            public static Color RoyalBlue => Color4.RoyalBlue;

            public static Color SaddleBrown => Color4.SaddleBrown;

            public static Color Salmon => Color4.Salmon;

            public static Color Orange => Color4.Orange;

            public static Color Khaki => Color4.Khaki;

            public static Color IndianRed => Color4.IndianRed;

            public static Color Indigo => Color4.Indigo;

            public static Color DarkGray => Color4.DarkGray;

            public static Color DarkGoldenrod => Color4.DarkGoldenrod;

            public static Color DarkCyan => Color4.DarkCyan;

            public static Color DarkBlue => Color4.DarkBlue;

            public static Color Cyan => Color4.Cyan;

            public static Color Crimson => Color4.Crimson;

            public static Color Cornsilk => Color4.Cornsilk;

            public static Color CornflowerBlue => Color4.CornflowerBlue;

            public static Color Ivory => Color4.Ivory;

            public static Color Chocolate => Color4.Chocolate;

            public static Color Chartreuse => Color4.Chartreuse;

            public static Color CadetBlue => Color4.CadetBlue;

            public static Color DarkGreen => Color4.DarkGreen;

            public static Color BurlyWood => Color4.BurlyWood;

            public static Color BlueViolet => Color4.BlueViolet;

            public static Color Blue => Color4.Blue;

            public static Color BlanchedAlmond => Color4.BlanchedAlmond;

            public static Color Black => Color4.Black;

            public static Color Bisque => Color4.Bisque;

            public static Color Beige => Color4.Beige;

            public static Color Azure => Color4.Azure;

            public static Color Aquamarine => Color4.Aquamarine;

            public static Color Aqua => Color4.Aqua;

            public static Color AntiqueWhite => Color4.AntiqueWhite;

            public static Color AliceBlue => Color4.AliceBlue;

            public static Color Transparent => Color4.Transparent;

            public static Color Brown => Color4.Brown;

            public static Color DarkKhaki => Color4.DarkKhaki;

            public static Color Coral => Color4.Coral;

            public static Color DarkOliveGreen => Color4.DarkOliveGreen;

            public static Color Yellow => Color4.Yellow;

            public static Color HotPink => Color4.HotPink;

            public static Color DarkMagenta => Color4.DarkMagenta;

            public static Color GreenYellow => Color4.GreenYellow;

            public static Color Green => Color4.Green;

            public static Color Gray => Color4.Gray;

            public static Color Goldenrod => Color4.Goldenrod;

            public static Color Gold => Color4.Gold;

            public static Color GhostWhite => Color4.GhostWhite;

            public static Color Gainsboro => Color4.Gainsboro;

            public static Color Fuchsia => Color4.Fuchsia;

            public static Color ForestGreen => Color4.ForestGreen;

            public static Color FloralWhite => Color4.FloralWhite;

            public static Color Honeydew => Color4.Honeydew;

            public static Color DodgerBlue => Color4.DodgerBlue;

            public static Color Firebrick => Color4.Firebrick;

            public static Color DarkOrange => Color4.DarkOrange;

            public static Color DarkOrchid => Color4.DarkOrchid;

            public static Color DarkRed => Color4.DarkRed;

            public static Color DarkSalmon => Color4.DarkSalmon;

            public static Color DarkSeaGreen => Color4.DarkSeaGreen;

            public static Color YellowGreen => Color4.YellowGreen;

            public static Color DarkSlateGray => Color4.DarkSlateGray;

            public static Color DarkTurquoise => Color4.DarkTurquoise;

            public static Color DarkViolet => Color4.DarkViolet;

            public static Color DeepPink => Color4.DeepPink;

            public static Color DeepSkyBlue => Color4.DeepSkyBlue;

            public static Color DarkSlateBlue => Color4.DarkSlateBlue;

            public static Color DimGray => Color4.DimGray;
        }
    }
}
