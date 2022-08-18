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
        #region Private fields

        private static Random _rnd = new Random();

        private byte _red;
        private byte _green;
        private byte _blue;

        #endregion Private fields

        #region Constructor

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

        #endregion Constructor

        #region Events

        public event EventHandler Changed;

        #endregion Events

        #region Properties

        public byte Red
        {
            get
            {
                return _red;
            }
            set
            {
                _red = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public byte Green
        {
            get
            {
                return _green;
            }
            set
            {
                _green = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public byte Blue
        {
            get
            {
                return _blue;
            }
            set
            {
                _blue = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        public int Intensity { get; set; }

        #endregion Properties

        #region Default colors

        public static IRGB BlackColor => new RGB(0, 0, 0);

        public static IRGB WhiteColor => new RGB(255, 255, 255);

        public static IRGB RedColor => new RGB(255, 0, 0);

        public static IRGB GreenColor => new RGB(0, 255, 0);

        public static IRGB BlueColor => new RGB(0, 0, 255);

        public static IRGB BackgroundDefaultColor => new RGB(204, 204, 204);

        public static IRGB GridDefaultColor => new RGB(126, 126, 126);

        public static IRGB TargetPointDefaultColor => RGB.BlackColor;

        #endregion Default colors

        #region Public logic

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

        #endregion Public logic

        #region Private logic

        private static byte GetRandomColourValue()
        {
            return (byte)_rnd.Next(byte.MinValue, byte.MaxValue);
        }

        #endregion Private logic
    }
}
