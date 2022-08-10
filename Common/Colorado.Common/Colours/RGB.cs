using System;
using System.Windows.Media;

namespace Colorado.Common.Colours
{
    public interface IRGB
    {
        byte Blue { get; set; }
        byte Green { get; set; }
        int Intensity { get; set; }
        byte Red { get; set; }

        event EventHandler Changed;

        void CopyValuesFrom(IRGB lastUsedColor);
        IRGB GetCopy();
        Color ToColor();
        float[] ToFloat3Array();
        float[] ToFloat4Array();
    }

    [Serializable]
    public class RGB : IRGB
    {
        private byte red;
        private byte green;
        private byte blue;

        public RGB() { }

        public RGB(Color color) :
            this(color.R, color.G, color.B)
        {
        }

        public RGB(float red, float green, float blue) :
            this((byte)(red * byte.MaxValue), (byte)(green * byte.MaxValue), (byte)(blue * byte.MaxValue))
        { }

        public RGB(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Intensity = 100;
        }

        public byte Red
        {
            get
            {
                return red;
            }
            set
            {
                red = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public byte Green
        {
            get
            {
                return green;
            }
            set
            {
                green = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public byte Blue
        {
            get
            {
                return blue;
            }
            set
            {
                blue = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public int Intensity { get; set; }

        public static IRGB BlackColor => new RGB(0, 0, 0);

        public static IRGB WhiteColor => new RGB(255, 255, 255);

        public static IRGB RedColor => new RGB(255, 0, 0);

        public static IRGB GreenColor => new RGB(0, 255, 0);

        public static IRGB BlueColor => new RGB(0, 0, 255);

        public static IRGB BackgroundDefaultColor => new RGB(204, 204, 204);

        public static IRGB GridDefaultColor => new RGB(126, 126, 126);

        public static IRGB TargetPointDefaultColor => RGB.BlackColor;

        public event EventHandler Changed;

        public Color ToColor()
        {
            return Color.FromRgb(Red, Green, Blue);
        }

        public float[] ToFloat3Array()
        {
            return new[] { Red / (float)byte.MaxValue, Green / (float)byte.MaxValue, Blue / (float)byte.MaxValue };
        }

        public float[] ToFloat4Array()
        {
            return new[] { Red / (float)byte.MaxValue, Green / (float)byte.MaxValue, Blue / (float)byte.MaxValue, 1 };
        }

        public IRGB GetCopy()
        {
            return new RGB(Red, Green, Blue);
        }

        public void CopyValuesFrom(IRGB lastUsedColor)
        {
            Red = lastUsedColor.Red;
            Green = lastUsedColor.Green;
            Blue = lastUsedColor.Blue;
        }

        public static IRGB GetRandomColour()
        {
            return new RGB(GetRandomColourValue(), GetRandomColourValue(), GetRandomColourValue());
        }

        private static Random rnd = new Random();

        private static byte GetRandomColourValue()
        {
            return (byte)rnd.Next(byte.MinValue, byte.MaxValue);
        }
    }
}
