using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Extensions
{
    public static class LightExtensions
    {
        public static int ToNumber(this LightType lightType)
        {
            switch (lightType)
            {
                case LightType.Light0:
                    return 0;
                case LightType.Light1:
                    return 1;
                case LightType.Light2:
                    return 2;
                case LightType.Light3:
                    return 3;
                case LightType.Light4:
                    return 4;
                case LightType.Light5:
                    return 5;
                case LightType.Light6:
                    return 6;
                case LightType.Light7:
                    return 7;
                default:
                    throw new Exception();
            }
        }

        public static LightType ToLightType(this int lightNumber)
        {
            switch (lightNumber)
            {
                case 0:
                    return LightType.Light0;
                case 1:
                    return LightType.Light1;
                case 2:
                    return LightType.Light2;
                case 3:
                    return LightType.Light3;
                case 4:
                    return LightType.Light4;
                case 5:
                    return LightType.Light5;
                case 6:
                    return LightType.Light6;
                case 7:
                    return LightType.Light7;
                default:
                    throw new Exception();
            }
        }
    }
}
