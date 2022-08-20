using Colorado.Common.Colours;
using System;

namespace Colorado.Geometry.Materials.Readers
{
    [Serializable]
    internal class SerializableMaterial
    {
        #region Constructor

        public SerializableMaterial() { }

        #endregion Constructor

        #region Properties

        public string Name { get; set; }

        public IRGB Ambient { get; set; }

        public IRGB Diffuse { get; set; }

        public IRGB Specular { get; set; }

        public float ShininessRadius { get; set; }

        public IRGB Emission { get; set; }

        public float Transparency { get; set; }

        #endregion Properties

        #region Public logic

        public Material ToMaterial()
        {
            return new Material(Name, Ambient, Diffuse, Specular, ShininessRadius, Emission);
        }

        #endregion Public logic
    }
}
