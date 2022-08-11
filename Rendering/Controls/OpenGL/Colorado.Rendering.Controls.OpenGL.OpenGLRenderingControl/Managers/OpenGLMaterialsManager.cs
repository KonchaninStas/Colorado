using Colorado.Common.Colours;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Materials;
using Colorado.Rendering.Materials;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Managers
{
    public class OpenGLMaterialsManager : MaterialsManager
    {
        protected override void SetAmbientColor(IRGB ambient) => OpenGLMaterialWrapper.SetAmbientColor(ambient);

        protected override void SetDiffuseColor(IRGB diffuse) => OpenGLMaterialWrapper.SetDiffuseColor(diffuse);

        protected override void SetEmissionColor(IRGB emission) => OpenGLMaterialWrapper.SetEmissionColor(emission);

        protected override void SetShininessIntensity(float shininessRadius) => OpenGLMaterialWrapper.SetShininessIntensity(shininessRadius);

        protected override void SetSpecularColor(IRGB specular) => OpenGLMaterialWrapper.SetSpecularColor(specular);
    }
}
