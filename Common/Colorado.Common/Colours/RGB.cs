using System;
using System.Windows.Media;

namespace Colorado.Common.Colours
{
    public interface IRGB
    {
        byte Blue { get; set; }
        byte Green { get; set; }
        float Intensity { get; set; }
        byte Red { get; set; }

        event EventHandler Changed;

        void CopyValuesFrom(IRGB lastUsedColor);
        IRGB GetCopy();
        Color ToColor();
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
            this(color.R, color.G, color.B, color.A / (float)byte.MaxValue)
        {
        }

        public RGB(float red, float green, float blue, float intensity) :
            this((byte)(red * byte.MaxValue), (byte)(green * byte.MaxValue), (byte)(blue * byte.MaxValue), intensity)
        { }

        public RGB(byte red, byte green, byte blue, float intensity)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Intensity = intensity;
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

        /// <summary>
        /// 0 - fully transparent
        /// 1 - fully visible
        /// </summary>
        public float Intensity { get; set; }

        #endregion Properties

        #region Default colors

        public static IRGB BlackColor => new RGB(0, 0, 0, 1);

        public static IRGB WhiteColor => new RGB(255, 255, 255, 1);

        public static IRGB RedColor => new RGB(255, 0, 0, 1);

        public static IRGB GreenColor => new RGB(0, 255, 0, 1);

        public static IRGB BlueColor => new RGB(0, 0, 255, 1);

        public static IRGB BackgroundDefaultColor => new RGB(204, 204, 204, 1);

        public static IRGB GridDefaultColor => new RGB(126, 126, 126, 0.5f);

        public static IRGB TargetPointDefaultColor => RGB.BlackColor;

        #endregion Default colors

        #region Public logic

        public Color ToColor()
        {
            return Color.FromRgb(Red, Green, Blue);
        }

        public float[] ToFloat4Array()
        {
            return new[] { Red / (float)byte.MaxValue, Green / (float)byte.MaxValue, Blue / (float)byte.MaxValue, Intensity };
        }

        public IRGB GetCopy()
        {
            return new RGB(Red, Green, Blue, Intensity);
        }

        public void CopyValuesFrom(IRGB lastUsedColor)
        {
            Red = lastUsedColor.Red;
            Green = lastUsedColor.Green;
            Blue = lastUsedColor.Blue;
            Intensity = lastUsedColor.Intensity;
        }

        public static IRGB GetRandomColour()
        {
            return new RGB(GetRandomColourValue(), GetRandomColourValue(), GetRandomColourValue(), (float)_rnd.NextDouble());
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
