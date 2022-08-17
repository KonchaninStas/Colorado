using Colorado.Common.Colours;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.General;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.Lighting;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Lighting
{
    public static class OpenGLLightingWrapper
    {
        public static void EnableLight(LightType lightType)
        {
            OpenGLAPIGeneral.Enable((int)lightType);
        }

        public static void DisableLight(LightType lightType)
        {
            OpenGLAPIGeneral.Disable((int)lightType);
        }

        public static void SetLightParameter(LightType lightType, LightParameter lightParameter, double value)
        {
            OpenGLLightingAPI.Lightf((int)lightType, (int)lightParameter, (float)value);
        }

        /// <summary>
        ///  (0.0, 0.0, 0.0, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="materialColorType"></param>
        /// <param name="value"></param>
        public static void SetAmbientColor(LightType lightType, RGB ambientColor)
        {
            SetLightParameter(lightType, LightColorType.Ambient, ambientColor);
        }

        /// <summary>
        /// (1.0, 1.0, 1.0, 1.0) for LIGHT0 and (0.0, 0.0, 0.0, 1.0) for other ones.
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="diffuseColor"></param>
        public static void SetDiffuseColor(LightType lightType, RGB diffuseColor)
        {
            SetLightParameter(lightType, LightColorType.Diffuse, diffuseColor);
        }

        /// <summary>
        /// (1.0, 1.0, 1.0, 1.0) for LIGHT0 and (0.0, 0.0, 0.0, 1.0) for other ones.
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="specularColor"></param>
        public static void SetSpecularColor(LightType lightType, RGB specularColor)
        {
            SetLightParameter(lightType, LightColorType.Specular, specularColor);
        }

        public static void SetLigthDirection(LightType lightType, Vector lightDirection)
        {
            OpenGLLightingAPI.Lightfv((int)lightType, (int)LightParameter.Position,
                lightDirection.FloatArray);
        }

        private static void SetLightParameter(LightType lightType, LightColorType lightColorType,
            RGB color)
        {
            OpenGLLightingAPI.Lightfv((int)lightType, (int)lightColorType, color.ToFloat4Array());
        }

        public static void EnableLighting()
        {
            OpenGLGeneralWrapper.EnableCapability(OpenGLCapability.Lighting);
        }

        public static void DisableLighting()
        {
            OpenGLGeneralWrapper.DisableCapability(OpenGLCapability.Lighting);
        }

        public static bool IsLightingEnabled()
        {
            return OpenGLGeneralWrapper.IsEnabled(OpenGLCapability.Lighting);
        }

        public static void SetLightModel(LightModel lightModel, bool enable)
        {
            OpenGLLightingAPI.LightModeli((int)lightModel, enable ? 1 : 0);
        }

        /// <summary>
        /// (0.2, 0.2, 0.2,1.0)
        /// </summary>
        /// <param name="ambientColor"></param>
        public static void SetLightModelAmbientColor(RGB ambientColor)
        {
            OpenGLLightingAPI.LightModelfv((int)LightModel.Ambient,
                ambientColor.ToFloat4Array());
        }
    }
}
