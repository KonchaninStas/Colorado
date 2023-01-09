using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Rendering.Controls.Abstractions.Scene;
using Colorado.Rendering.Lighting;
using Colorado.Tools.SunPositionTool;
using System;
using System.Device.Location;

namespace Colorado.Viewer.Controls.TimeAndLocationSettings
{
    public interface ITimeAndLocationSettingsViewModel
    {
        DateTime CurrentDateTime { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }

    public class TimeAndLocationSettingsViewModel : ViewModelBase, ITimeAndLocationSettingsViewModel
    {
        private readonly ILightsManager _lightsManager;
        private readonly ICamera _camera;

        public TimeAndLocationSettingsViewModel(ILightsManager lightsManager, ICamera camera)
       {
            _lightsManager = lightsManager;
            _camera = camera;
            CurrentDateTime = DateTime.Now;

            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.Start(); //started watcher
            GeoCoordinate coord = watcher.Position.Location;
            if (!watcher.Position.Location.IsUnknown)
            {
                Latitude = coord.Latitude;
                Longitude = coord.Longitude;
            }
            else
            {
                Latitude = 50.450001;
                Longitude = 30.523333;
            }
            UpdateLight();
        }

        private DateTime _currentDateTime;
        public DateTime CurrentDateTime
        {
            get
            {
                return _currentDateTime;
            }
            set
            {
                _currentDateTime = value;
                OnPropertyChanged(nameof(CurrentDateTime));
                UpdateLight();
            }
        }

        private double _latitude;
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
                OnPropertyChanged(nameof(Latitude));
                UpdateLight();
            }
        }

        private double _longitude;
        public double Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
                OnPropertyChanged(nameof(Longitude));
                UpdateLight();
            }
        }

        private void UpdateLight()
        {
            SunPosition sunPosition = SunPositionProvider.Instance.CalculateSunPosition(_currentDateTime, Latitude, Longitude);
            _lightsManager[0].AltitudeAngleInDegrees = sunPosition.Altitude;
            _lightsManager[0].AzimuthAngleInDegrees = sunPosition.Azimuth;

            _camera.Refresh();
        }
    }
}
