﻿using Colorado.Common.Colours;
using System;

namespace Colorado.Rendering.Materials
{
    public interface IMaterial
    {
        IRGB Ambient { get; set; }
        IRGB Diffuse { get; set; }
        IRGB Emission { get; set; }
        string Name { get; set; }
        float ShininessRadius { get; set; }
        IRGB Specular { get; set; }
        float Transparency { get; set; }

        IMaterial GetCopy();
        string ToString();
    }

    [Serializable]
    public class Material : IMaterial
    {
        public const string DefaultMaterialName = "Default";
        public const string BlackMaterialName = "Black";

        public Material()
        { }

        public Material(IRGB diffuse)
            : this(Default.Name, Default.Ambient, diffuse, Default.Specular, Default.ShininessRadius, Default.Emission) { }

        public Material(string name, IRGB ambient, IRGB diffuse, IRGB specular,
           float shininess, IRGB emission)
        {
            Name = name;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;

            if (shininess < 0 || shininess > 128)
            {
                ShininessRadius = 0;
            }
            ShininessRadius = shininess;
            Emission = emission;
        }

        public Material(string name, IRGB ambient, IRGB diffuse, IRGB specular,
           float shininess) : this(name, ambient, diffuse, specular, shininess, Default.Emission) { }

        public Material(IRGB ambient, IRGB diffuse, IRGB specular,
            float shininess, IRGB emission) : this(string.Empty, ambient, diffuse, specular, shininess, emission) { }

        public string Name { get; set; }

        public IRGB Ambient { get; set; }

        public IRGB Diffuse { get; set; }

        public IRGB Specular { get; set; }

        public float ShininessRadius { get; set; }

        public IRGB Emission { get; set; }

        public float Transparency { get; set; }

        public static Material Default
        {
            get
            {
                return new Material(DefaultMaterialName, new RGB(0.2f, 0.2f, 0.2f), new RGB(0.8f, 0.8f, 0.8f),
                   new RGB(0f, 0f, 0f), 0, new RGB(0.0f, 0.0f, 0.0f));
            }
        }

        public static Material Black
        {
            get
            {
                return new Material(BlackMaterialName, new RGB(0f, 0f, 0f), new RGB(0f, 0f, 0f),
                   new RGB(0f, 0f, 0f), 0, new RGB(0.0f, 0.0f, 0.0f));
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public IMaterial GetCopy()
        {
            return new Material(Name, Ambient.GetCopy(), Diffuse.GetCopy(), Specular.GetCopy(), ShininessRadius, Emission.GetCopy());
        }
    }
}
