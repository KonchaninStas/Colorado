using System.Runtime.InteropServices;

namespace Colorado.Common.WindowsLibrariesWrappers.Gdi32.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class PixelFormatDescriptor
    {
        public byte AccumAlphaBits { get; set; }

        public byte AccumBits { get; set; }

        public byte AccumBlueBits { get; set; }

        public byte AccumGreenBits { get; set; }

        public byte AccumRedBits { get; set; }

        public byte AlphaBits { get; set; }

        public byte AlphaShift { get; set; }

        public byte AuxBuffers { get; set; }

        public byte BlueBits { get; set; }

        public byte BlueShift { get; set; }

        public byte ColorBits { get; set; }

        public byte DepthBits { get; set; }

        public uint dwDamageMask { get; set; }

        public uint dwFlags { get; set; }

        public uint dwLayerMask { get; set; }

        public uint dwVisibleMask { get; set; }

        public byte GreenBits { get; set; }

        public byte GreenShift { get; set; }

        public byte LayerType { get; set; }

        public byte PixelType { get; set; }

        public byte RedBits { get; set; }

        public byte RedShift { get; set; }

        public byte Reserved { get; set; }

        public ushort Size { get; set; }

        public byte StencilBits { get; set; }

        public ushort Version { get; set; }
    }
}
