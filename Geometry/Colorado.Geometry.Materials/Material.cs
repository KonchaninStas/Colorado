using Colorado.Common.Colours;

namespace Colorado.Geometry.Materials
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

    public class Material : IMaterial
    {
        #region Constants

        public const string DefaultMaterialName = "Default";
        public const string BlackMaterialName = "Black";

        #endregion Constants

        #region Private fields

        #endregion Private fields

        #region Constructor

        public Material(IRGB diffuse)
            : this(Default.Name, Default.Ambient, diffuse, Default.Specular, Default.ShininessRadius, Default.Emission) { }

        public Material(string name, IRGB ambient, IRGB diffuse, IRGB specular, float shininess, IRGB emission)
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

        #endregion Constructor

        #region Properties

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

        #endregion Properties

        #region Public logic

        public override string ToString()
        {
            return Name;
        }

        public IMaterial GetCopy()
        {
            return new Material(Name, Ambient.GetCopy(), Diffuse.GetCopy(), Specular.GetCopy(), ShininessRadius, Emission.GetCopy());
        }

        #endregion Public logic
    }
}
