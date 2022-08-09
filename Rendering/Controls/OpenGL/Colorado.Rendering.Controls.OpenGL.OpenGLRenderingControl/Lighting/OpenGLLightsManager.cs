using Colorado.Common.Colours;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Extensions;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Lighting;
using Colorado.Rendering.Lighting;
using Colorado.Rendering.Lighting.Structures;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Lighting
{
    public class OpenGLLightsManager : LightsManager
    {
        public override void DisableLight(int lightNumber)
        {
            OpenGLLightingWrapper.DisableLight(lightNumber.ToLightType());
        }

        public override void EnableLight(int lightNumber)
        {
            OpenGLLightingWrapper.EnableLight(lightNumber.ToLightType());
        }

        public override ILight GetDefault(int lightNumber)
        {
            return new Light(
                lightNumber,
                new RGB(0f, 0f, 0f),
                lightNumber == LightType.Light0.ToNumber() ? new RGB(1f, 1f, 1f) : new RGB(0f, 0f, 0f),
                lightNumber == LightType.Light0.ToNumber() ? new RGB(1f, 1f, 1f) : new RGB(0f, 0f, 0f),
                0, 90);
        }

        protected override void ConfigurateLight(ILight light)
        {
            LightType lightType = light.Number.ToLightType();

            OpenGLLightingWrapper.EnableLight(lightType);
            OpenGLLightingWrapper.SetLightParameter(lightType, LightParameter.ConstantAttenuation, 1);
            OpenGLLightingWrapper.SetLightParameter(lightType, LightParameter.LinearAttenuation, 0);
            OpenGLLightingWrapper.SetLightParameter(lightType, LightParameter.QuadraticAttenuation, 0);

            OpenGLLightingWrapper.SetAmbientColor(lightType, light.Ambient);
            OpenGLLightingWrapper.SetDiffuseColor(lightType, light.Diffuse);
            OpenGLLightingWrapper.SetSpecularColor(lightType, light.Specular);
            OpenGLLightingWrapper.SetLigthDirection(lightType, light.Direction);
        }

        public override void DisableLighting()
        {
            OpenGLLightingWrapper.DisableLighting();
        }

        protected override void DrawLightPoint(IPoint centerPoint, RGB color, double radius)
        {
            throw new System.NotImplementedException();
        }

        public override void EnableLighting()
        {
            OpenGLLightingWrapper.EnableLighting();
        }

        protected override Dictionary<int, ILight> InitLights()
        {
            return new Dictionary<int, ILight>()
            {
                { 0, GetDefault(0) },
                { 1, GetDefault(1) },
                { 2, GetDefault(2) },
                { 3, GetDefault(3) },
                { 4, GetDefault(4) },
                { 5, GetDefault(5) },
                { 6, GetDefault(6) },
                { 7, GetDefault(7) },
            };
        }
    }
}
