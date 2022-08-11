using Colorado.Common.Colours;

namespace Colorado.Rendering.Materials
{
    public interface IMaterialsManager
    {
        void SetMaterial(IMaterial material);
    }

    public abstract class MaterialsManager : IMaterialsManager
    {
        public void SetMaterial(IMaterial material)
        {
            SetAmbientColor(material.Ambient);
            SetDiffuseColor(material.Diffuse);
            SetSpecularColor(material.Specular);
            SetShininessIntensity(material.ShininessRadius);
            SetEmissionColor(material.Emission);
        }

        protected abstract void SetAmbientColor(IRGB ambient);
        protected abstract void SetDiffuseColor(IRGB diffuse);
        protected abstract void SetSpecularColor(IRGB specular);
        protected abstract void SetShininessIntensity(float shininessRadius);
        protected abstract void SetEmissionColor(IRGB emission);
    }
}
