using System.Globalization;
using Windows.UI;

namespace IF.Common.Metro.UI.Extensions
{
    public static class ColourEx
    {
        public static Color ColorFromHex(string hex)
        {
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }

        public static string ColorToHexARGB(Color colour)
        {
            string a = colour.A.ToString("X2");
            string r = colour.R.ToString("X2");
            string g = colour.G.ToString("X2");
            string b = colour.B.ToString("X2");

            return string.Format("#{0}{1}{2}{3}", a, r, g, b);
        }

    }
}