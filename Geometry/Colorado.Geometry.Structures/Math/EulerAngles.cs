namespace Colorado.Geometry.Structures.Math
{
    public sealed class EulerAngles
    {
        #region Constructor

        public EulerAngles(double roll, double pitch, double yaw)
        {
            Roll = roll;
            Pitch = pitch;
            Yaw = yaw;
        }

        #endregion Constructor

        #region Properties

        public double Roll { get; }

        public double Pitch { get; }

        public double Yaw { get; }

        #endregion Properties
    }
}
