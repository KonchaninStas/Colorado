using Colorado.Common.Colours;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations.Materials;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.Materials;
using System;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.Materials
{
    public static class OpenGLMaterialWrapper
    {
        #region Public logic

        public static void SetBlendFunction()
        {
            OpenGLMaterialsAPI.SetBlendFunction((int)SourceBlendingFactor.Alpha,
                (int)DestinationBlendingFactor.OneMinusSourceAlpha);
        }

        /// <summary>
        ///  (0.2, 0.2, 0.2, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="materialColorType"></param>
        /// <param name="value"></param>
        public static void SetAmbientColor(IRGB ambientColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Ambient, ambientColor);
        }

        /// <summary>
        /// (0.8, 0.8, 0.8, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="diffuseColor"></param>
        public static void SetDiffuseColor(IRGB diffuseColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Diffuse, diffuseColor);
        }

        /// <summary>
        /// (0.0, 0.0, 0.0, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="specularColor"></param>
        public static void SetSpecularColor(IRGB specularColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Specular, specularColor);
        }

        /// <summary>
        /// 0
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="shininessIntensityValue"></param>
        public static void SetShininessIntensity(float shininessIntensityValue)
        {
            if (shininessIntensityValue < 0 || shininessIntensityValue > 128)
            {
                throw new Exception();
            }
            SetMaterialValue(FaceSide.FrontAndBack, MaterialColorType.Shininess, shininessIntensityValue);
        }

        /// <summary>
        ///  (0.0, 0.0, 0.0, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="emissionColor"></param>
        public static void SetEmissionColor(IRGB emissionColor)
        {
            SetMaterialValues(FaceSide.FrontAndBack, MaterialColorType.Emission, emissionColor);
        }

        /// <summary>
        ///  (0.2, 0.2, 0.2, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="materialColorType"></param>
        /// <param name="value"></param>
        public static void SetAmbientColor(FaceSide faceSide, IRGB ambientColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Ambient, ambientColor);
        }

        /// <summary>
        /// (0.8, 0.8, 0.8, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="diffuseColor"></param>
        public static void SetDiffuseColor(FaceSide faceSide, IRGB diffuseColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Diffuse, diffuseColor);
        }

        /// <summary>
        /// (0.0, 0.0, 0.0, 1.0)
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="specularColor"></param>
        public static void SetSpecularColor(FaceSide faceSide, IRGB specularColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Specular, specularColor);
        }

        /// <summary>
        /// 0
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="shininessIntensityValue"></param>
        public static void SetShininessIntensity(FaceSide faceSide, float shininessIntensityValue)
        {
            if (shininessIntensityValue < 0 || shininessIntensityValue > 128)
            {
                //throw new Exception(Resources.Error_ShininessValueIsOutOfRange);
            }
            SetMaterialValue(faceSide, MaterialColorType.Shininess, 128 - shininessIntensityValue);
        }

        /// <summary>
        ///  (0.0, 0.0, 0.0, 1.0).
        /// </summary>
        /// <param name="faceSide"></param>
        /// <param name="emissionColor"></param>
        public static void SetEmissionColor(FaceSide faceSide, IRGB emissionColor)
        {
            SetMaterialValues(faceSide, MaterialColorType.Emission, emissionColor);
        }

        #endregion Public logic

        #region Private logic

        private static void SetMaterialValue(FaceSide faceSide,
            MaterialColorType materialColorType, float value)
        {
            OpenGLMaterialsAPI.Materialf((int)faceSide, (int)materialColorType, value);
        }

        private static void SetMaterialValues(FaceSide faceSide,
            MaterialColorType materialColorType, IRGB color)
        {
            SetMaterialValues(faceSide, materialColorType, color.ToFloat4Array());
        }

        private static void SetMaterialValues(FaceSide faceSide,
            MaterialColorType materialColorType, float[] values)
        {
            OpenGLMaterialsAPI.Materialfv((int)faceSide, (int)materialColorType, values);
        }

        #endregion Private logic
    }
}
