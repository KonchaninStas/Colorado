namespace Colorado.Services.Gdi32.Structures
{
    public interface IPixelFormatDescriptor
    {
        byte AccumAlphaBits { get; set; }
        byte AccumBits { get; set; }
        byte AccumBlueBits { get; set; }
        byte AccumGreenBits { get; set; }
        byte AccumRedBits { get; set; }
        byte AlphaBits { get; set; }
        byte AlphaShift { get; set; }
        byte AuxBuffers { get; set; }
        byte BlueBits { get; set; }
        byte BlueShift { get; set; }
        byte ColorBits { get; set; }
        byte DepthBits { get; set; }
        uint dwDamageMask { get; set; }
        uint dwFlags { get; set; }
        uint dwLayerMask { get; set; }
        uint dwVisibleMask { get; set; }
        byte GreenBits { get; set; }
        byte GreenShift { get; set; }
        byte LayerType { get; set; }
        byte PixelType { get; set; }
        byte RedBits { get; set; }
        byte RedShift { get; set; }
        byte Reserved { get; set; }
        ushort Size { get; set; }
        byte StencilBits { get; set; }
        ushort Version { get; set; }
    }

    public class PixelFormatDescriptor : IPixelFormatDescriptor
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
