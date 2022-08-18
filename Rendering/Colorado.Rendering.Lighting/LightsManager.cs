using Colorado.Common.Colours;
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
        #region Private fields

        protected readonly Dictionary<int, ILight> _lightNumberToLightMap;

        #endregion Private fields

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

        public abstract ILight GetDefault(int lightNumber);

        public void EnableLight(int lightNumber)
        {
            if (this[lightNumber] != null)
            {
                EnableLight(this[lightNumber]);
                this[lightNumber].IsEnabled = true;
            }
        }

        public void DisableLight(int lightNumber)
        {
            if (this[lightNumber] != null)
            {
                DisableLight(this[lightNumber]);
                this[lightNumber].IsEnabled = false;
            }
        }

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

        public void DrawLightsSources(double radius)
        {
            if (IsLightingEnabled && DrawLights)
            {
                foreach (ILight light in _lightNumberToLightMap.Values.Where(l => l.IsEnabled))
                {
                    DrawLightPoint(Point.Zero + (light.Direction * (radius == 0 ? 10 : radius)),
                        light.Diffuse, (float)LightSourceDrawDiameter);
                }
            }
        }

        #endregion Public logic

        #region Protected logic

        protected abstract Dictionary<int, ILight> InitLights();

        protected abstract void EnableLight(ILight light);

        protected abstract void DisableLight(ILight light);

        protected abstract void ConfigurateLight(ILight light);

        protected abstract void DrawLightPoint(Point centerPoint, RGB color, double radius);

        #endregion Protected logic
    }
}
