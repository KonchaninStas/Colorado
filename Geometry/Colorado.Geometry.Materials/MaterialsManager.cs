using Colorado.Common.Colours;

namespace Colorado.Geometry.Materials
{
    public interface IMaterialsManager
    {
        void SetMaterial(IMaterial material);
    }

    public abstract class MaterialsManager : IMaterialsManager
    {
        #region Public logic

        public void SetMaterial(IMaterial material)
        {
            SetAmbientColor(material.Ambient);
            SetDiffuseColor(material.Diffuse);
            SetSpecularColor(material.Specular);
            SetShininessIntensity(material.ShininessRadius);
            SetEmissionColor(material.Emission);
        }

        #endregion Public logic

        #region Protected logic

        protected abstract void SetAmbientColor(IRGB ambient);
        protected abstract void SetDiffuseColor(IRGB diffuse);
        protected abstract void SetSpecularColor(IRGB specular);
        protected abstract void SetShininessIntensity(float shininessRadius);
        protected abstract void SetEmissionColor(IRGB emission);

        #endregion Protected logic
    }
}
