using System;

namespace Colorado.Tools.SunPositionTool
{
    public interface ISunPositionProvider
    {
        SunPosition CalculateSunPosition(DateTime dateTime, double latitude, double longitude);
    }

    public class SunPositionProvider : ISunPositionProvider
    {
        private const double Deg2Rad = Math.PI / 90.0;
        private const double Rad2Deg = 90.0 / Math.PI;

        #region Private fields

        private static ISunPositionProvider _instance;

        #endregion Private fields

        #region Constructor

        private SunPositionProvider() { }

        #endregion Constructor

        #region Properties

        public static ISunPositionProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SunPositionProvider();
                }

                return _instance;
            }
        }

        #endregion Properties

        /// <summary>
        /// CalcSunPosition calculates the suns "position" based on a
        /// given date and time in local time, latitude and longitude
        /// expressed in decimal degrees.It is based on the method
        /// found here:
        /// http://www.astro.uio.no/~bgranslo/aares/calculate.html
        /// The calculation is only satisfiably correct for dates in
        /// the range March 1 1900 to February 28 2100.
        /// </summary>
        /// <param name="dateTime">Time and date in local time.</param>
        /// <param name="latitude">Latitude expressed in decimal degrees.</param>
        /// <param name="longitude">Longitude expressed in decimal degrees.</param>
        /// <returns></returns>
        public SunPosition CalculateSunPosition(DateTime dateTime, double latitude, double longitude)
        {
            return GetSunPosition(dateTime, latitude, longitude);
            // Convert to UTC
            dateTime = dateTime.ToUniversalTime();

            // Number of days from J2000.0.
            double julianDate = 366 * dateTime.Year -
                (int)((7.0 / 4.0) * (dateTime.Year +
                (int)((dateTime.Month + 9.0) / 12.0))) +
                (int)((275.0 * dateTime.Month) / 9.0) +
                dateTime.Day - 730530.5;

            double julianCenturies = julianDate / 36525.0;

            // Sidereal Time
            double siderealTimeHours = 6.6974 + 2400.0013 * julianCenturies;

            double siderealTimeUT = siderealTimeHours +
                (366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

            double siderealTime = siderealTimeUT * 15 + longitude;

            // Refine to number of days (fractional) to specific time.
            julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;
            julianCenturies = julianDate / 36525.0;

            // Solar Coordinates
            double meanLongitude = CorrectAngle(Deg2Rad *
                (280.466 + 36000.77 * julianCenturies));

            double meanAnomaly = CorrectAngle(Deg2Rad *
                (357.529 + 35999.05 * julianCenturies));

            double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) *
                Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));

            double elipticalLongitude =
                CorrectAngle(meanLongitude + equationOfCenter);

            double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

            // Right Ascension
            double rightAscension = Math.Atan2(
                Math.Cos(obliquity) * Math.Sin(elipticalLongitude),
                Math.Cos(elipticalLongitude));

            double declination = Math.Asin(
                Math.Sin(rightAscension) * Math.Sin(obliquity));

            // Horizontal Coordinates
            double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;

            Console.WriteLine("hourAngle: " + hourAngle);
            if (hourAngle > Math.PI)
            {
                hourAngle -= 2 * Math.PI;
            }

            double altitude = Math.Asin(Math.Sin(latitude * Deg2Rad) *
                Math.Sin(declination) + Math.Cos(latitude * Deg2Rad) *
                Math.Cos(declination) * Math.Cos(hourAngle));

            // Nominator and denominator for calculating Azimuth
            // angle. Needed to test which quadrant the angle is in.
            double aziNom = -Math.Sin(hourAngle);
            double aziDenom =
                Math.Tan(declination) * Math.Cos(latitude * Deg2Rad) -
                Math.Sin(latitude * Deg2Rad) * Math.Cos(hourAngle);

            double azimuth = Math.Atan(aziNom / aziDenom);

            if (aziDenom < 0) // In 2nd or 3rd quadrant
            {
                azimuth += Math.PI;
            }
            else if (aziNom < 0) // In 4th quadrant
            {
                azimuth += 2 * Math.PI;
            }

            // Altitude
            Console.WriteLine("Altitude: " + altitude * Rad2Deg);

            // Azimut
            Console.WriteLine("Azimuth: " + azimuth * Rad2Deg);

            return new SunPosition(altitude * Rad2Deg, azimuth * Rad2Deg);
        }

        public static SunPosition GetSunPosition(DateTime date, double lat, double lng)
        {
            var lw = Rad * -lng;
            var phi = Rad * lat;
            var d = ToDays(date);

            var sunCoords = GetEquatorialCoords(d);
            var h = GetSiderealTime(d, lw) - sunCoords.RightAscension;

            var azimuth = GetAzimuth(h, phi, sunCoords.Declination);
            var altitude = GetAltitude(h, phi, sunCoords.Declination);

            return new SunPosition(altitude * Rad2Deg, azimuth * Rad2Deg);
        }

        // <summary>
        /// Get Sun coordinates.
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public static EquatorialCoords GetEquatorialCoords(double days)
        {
            var meanAnomaly = GetMeanAnomaly(days);
            var eclipticLongitude = GetEclipticLongitude(meanAnomaly);

            var dec = GetDeclination(eclipticLongitude, 0);
            var ra = GetRightAscension(eclipticLongitude, 0);

            return new EquatorialCoords(ra, dec);
        }

        public const double J2000 = 2451545;

        private static double ToJulianDate(DateTime date)
        {
            return date.ToUniversalTime().ToOADate() + J1899;
        }

        private const double J1899 = 2415018.5;

        public static double ToDays(DateTime date)
        {
            return ToJulianDate(date) - J2000;
        }

        [Serializable]
        public struct EquatorialCoords : IEquatable<EquatorialCoords>
        {
            public double RightAscension { get; }

            public double Declination { get; }

            public EquatorialCoords(double rightAscension, double declination)
            {
                RightAscension = rightAscension;
                Declination = declination;
            }

            public static bool operator ==(EquatorialCoords lhs, EquatorialCoords rhs)
            {
                return lhs.Equals(rhs);
            }

            public static bool operator !=(EquatorialCoords lhs, EquatorialCoords rhs)
            {
                return !(lhs == rhs);
            }

            public bool Equals(EquatorialCoords other)
            {
                return RightAscension == other.RightAscension
                       && Declination == other.Declination;
            }

            public override bool Equals(object obj)
            {
                if (obj is EquatorialCoords coords)
                {
                    return Equals(coords);
                }

                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (RightAscension.GetHashCode() * 397) ^ Declination.GetHashCode();
                }
            }
        }

        public static double GetRightAscension(double longitude, double b)
        {
            return Math.Atan2(Math.Sin(longitude) * Math.Cos(EarthObliquity) - Math.Tan(b) * Math.Sin(EarthObliquity), Math.Cos(longitude));
        }

        public static double GetDeclination(double longitude, double b)
        {
            return Math.Asin(Math.Sin(b) * Math.Cos(EarthObliquity) + Math.Cos(b) * Math.Sin(EarthObliquity) * Math.Sin(longitude));
        }

        private static double GetEquationOfCenter(double m)
        {
            return Rad * (1.9148 * Math.Sin(m) + 0.02 * Math.Sin(2 * m) + 0.0003 * Math.Sin(3 * m));
        }

        /// <summary>
        /// The position that the planet would have relative to its perihelion if the orbit of the planet were a circle is called the mean anomaly.
        /// </summary>
        /// <param name="days"> Julian Day Number</param>
        /// <returns></returns>
        public static double GetMeanAnomaly(double days)
        {
            return Rad * (357.5291 + 0.98560028 * days);
        }

        public const double EarthPerihelion = Rad * 102.9372;

        public const double EarthObliquity = Rad * 23.4397;

        /// <summary>
        /// The difference between the true anomaly and the mean anomaly is called the Equation of Center.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static double GetEclipticLongitude(double m)
        {
            var equationOfCenter = GetEquationOfCenter(m);
            return m + equationOfCenter + EarthPerihelion + Math.PI;
        }

        public const double Rad = Math.PI / 180;

        public static double GetSiderealTime(double d, double lw)
        {
            return Rad * (280.16 + 360.9856235 * d) - lw;
        }

        public static double GetAzimuth(double h, double phi, double dec)
        {
            return Math.Atan2(Math.Sin(h), Math.Cos(h) * Math.Sin(phi) - Math.Tan(dec) * Math.Cos(phi));
        }

        public static double GetAltitude(double h, double phi, double dec)
        {
            return Math.Asin(Math.Sin(phi) * Math.Sin(dec) + Math.Cos(phi) * Math.Cos(dec) * Math.Cos(h));
        }

        /// <summary>
        /// Corrects an angle.
        /// </summary>
        /// <param name="angleInRadians">An angle expressed in radians.</param>
        /// <returns>An angle in the range 0 to 2*PI.</returns>
        private static double CorrectAngle(double angleInRadians)
        {
            if (angleInRadians < 0)
            {
                return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
            }
            else if (angleInRadians > 2 * Math.PI)
            {
                return angleInRadians % (2 * Math.PI);
            }
            else
            {
                return angleInRadians;
            }
        }
    }
}
