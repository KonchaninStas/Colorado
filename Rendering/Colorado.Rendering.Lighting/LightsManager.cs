using Colorado.Common.Colours;
using Colorado.Geometry.Abstractions.Primitives;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Lighting.Structures;
using System.Collections.Generic;
using System.Linq;

namespace Colorado.Rendering.Lighting
{
    public interface ILightsManager
    {
        ILight this[int lightNumber] { get; }

        bool DrawLights { get; set; }
        bool IsLightingEnabled { get; set; }
        IEnumerable<ILight> Lights { get; }
        double LightSourceDrawDiameter { get; set; }

        void ConfigureEnabledLights();
        void DisableLight(int lightNumber);
        void DisableLighting();
        void DrawLightsSources(double radius);
        void EnableLight(int lightNumber);
        void EnableLighting();
        ILight GetDefault(int lightNumber);
    }

    public abstract class LightsManager : ILightsManager
    {
        protected readonly Dictionary<int, ILight> _lightNumberToLightMap;

        public abstract ILight GetDefault(int lightNumber);

        #region Constructor

        public LightsManager()
        {
            _lightNumberToLightMap = InitLights();
            EnableLight(0);
            IsLightingEnabled = true;
            DrawLights = true;
            LightSourceDrawDiameter = 20;
        }

        #endregion Constructor

        #region Properties

        public ILight this[int lightNumber]
        {
            get
            {
                return _lightNumberToLightMap.TryGetValue(lightNumber, out ILight light) ? light : null;
            }
        }

        public IEnumerable<ILight> Lights => _lightNumberToLightMap.Values;

        public bool IsLightingEnabled { get; set; }

        public bool DrawLights { get; set; }

        public double LightSourceDrawDiameter { get; set; }

        #endregion Properties

        #region Public logic

        protected abstract Dictionary<int, ILight> InitLights();

        public abstract void EnableLight(int lightNumber);

        public abstract void DisableLight(int lightNumber);

        protected abstract void ConfigurateLight(ILight light);

        public abstract void EnableLighting();

        public abstract void DisableLighting();

        public void ConfigureEnabledLights()
        {
            if (IsLightingEnabled)
            {
                EnableLighting();
                foreach (ILight light in _lightNumberToLightMap.Values.Where(l => l.IsEnabled))
                {
                    ConfigurateLight(light);
                }
            }
            else
            {
                DisableLighting();
            }
        }

        protected abstract void DrawLightPoint(IPoint centerPoint, RGB color, double radius);

        public void DrawLightsSources(double radius)
        {
            if (IsLightingEnabled && DrawLights)
            {
                foreach (ILight light in _lightNumberToLightMap.Values.Where(l => l.IsEnabled))
                {
                    DrawLightPoint(Point.Zero.Plus(light.Direction.Multiply(radius == 0 ? 10 : radius)),
                        light.Diffuse, (float)LightSourceDrawDiameter);
                }
            }
        }

        #endregion Public logic
    }
}
