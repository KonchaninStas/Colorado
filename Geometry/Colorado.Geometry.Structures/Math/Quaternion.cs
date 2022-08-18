using Colorado.Common.Extensions;
using Colorado.Common.Utils;
using Colorado.Geometry.Structures.Primitives;

namespace Colorado.Geometry.Structures.Math
{
    public interface IQuaternion
    {
        double X { get; }
        double Y { get; }
        double Z { get; }
        double W { get; }

        double AngleInRadians { get; }
        Vector Axis { get; }
        bool IsIdentity { get; }

        Vector ApplyToVector(Vector vector);
        EulerAngles GetEulerAngles();
        IQuaternion GetInversed();
        IQuaternion Multiply(IQuaternion rhs);
    }

    public sealed class Quaternion : IQuaternion
    {
        #region Private fields

        private readonly Vector _axis;

        #endregion Private fields

        #region Constructor

        private Quaternion(Vector axis, double w) : this(axis.X, axis.Y, axis.Z, w) { }

        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            _axis = new Vector(x, y, z);
        }

        #endregion Constructor

        #region Properties

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double W { get; }

        public Vector Axis => _axis;

        /// <summary>
        /// Gets the angle of the quaternion.
        /// </summary>
        /// <value>The quaternion's angle.</value>
        public double AngleInRadians
        {
            get
            {
                double length = _axis.Length * 2;
                if (length.IsZero())
                    return 0.0;

                return (float)(2.0 * System.Math.Acos(MathUtils.Instance.Clamp(W, -1f, 1f)));
            }
        }

        public static IQuaternion Identity
        {
            get { return new Quaternion(0, 0, 0, 1); }
        }

        public bool IsIdentity
        {
            get { return X.IsZero() && Y.IsZero() && Z.IsZero() && W.EqualsWithTolerance(1); }
        }

        #endregion Properties

        #region Public logic

        public EulerAngles GetEulerAngles()
        {
            // roll (x-axis rotation)
            double sinr_cosp = 2 * (W * X + Y * Z);
            double cosr_cosp = 1 - 2 * (X * X + Y * Y);
            double roll = System.Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            double pitch = 0;
            double sinp = 2 * (W * Y - Z * X);
            if (System.Math.Abs(sinp) >= 1)
                pitch = (System.Math.PI / 2).CopySign(sinp); // use 90 degrees if out of range
            else
                pitch = System.Math.Asin(sinp);

            // yaw (z-axis rotation)
            double siny_cosp = 2 * (W * Z + X * Y);
            double cosy_cosp = 1 - 2 * (Y * Y + Z * Z);
            double yaw = System.Math.Atan2(siny_cosp, cosy_cosp);
            return new EulerAngles(roll, pitch, yaw);
        }

        public IQuaternion Multiply(IQuaternion rhs)
        {
            return new Quaternion(
               W * rhs.X + X * rhs.W + Y * rhs.Z - Z * rhs.Y,
               W * rhs.Y + Y * rhs.W + Z * rhs.X - X * rhs.Z,
               W * rhs.Z + Z * rhs.W + X * rhs.Y - Y * rhs.X,
               W * rhs.W - X * rhs.X - Y * rhs.Y - Z * rhs.Z);
        }

        public Vector ApplyToVector(Vector vector)
        {
            double x = this.X * 2F;
            double y = this.Y * 2F;
            double z = this.Z * 2F;
            double xx = this.X * x;
            double yy = this.Y * y;
            double zz = this.Z * z;
            double xy = this.X * y;
            double xz = this.X * z;
            double yz = this.Y * z;
            double wx = this.W * x;
            double wy = this.W * y;
            double wz = this.W * z;

            return new Vector(
                (1F - (yy + zz)) * vector.X + (xy - wz) * vector.Y + (xz + wy) * vector.Z,
                (xy + wz) * vector.X + (1F - (xx + zz)) * vector.Y + (yz - wx) * vector.Z,
                (xz - wy) * vector.X + (yz + wx) * vector.Y + (1F - (xx + yy)) * vector.Z);
        }

        public IQuaternion GetInversed()
        {
            double lengthSq = GetLengthSquarted();
            if (lengthSq != 0.0)
            {
                double i = 1.0 / lengthSq;
                return new Quaternion(_axis * -i, W * i);
            }
            return this;
        }

        public static IQuaternion Create(Vector rotationAxis, double rotationAngleInDegrees)
        {
            if (rotationAxis.IsZero)
            {
                return Identity;
            }
            double rotationAngleInRadians = MathUtils.Instance.ConvertDegreesToRadians(rotationAngleInDegrees);
            rotationAngleInRadians *= 0.5;
            rotationAxis = rotationAxis.UnitVector;
            rotationAxis = rotationAxis * System.Math.Sin(rotationAngleInRadians);

            return new Quaternion(rotationAxis.X, rotationAxis.Y, rotationAxis.Z, System.Math.Cos(rotationAngleInRadians)).GetNormalized();
        }

        public static IQuaternion LookRotation(Vector forward, Vector up)
        {
            forward = forward.UnitVector;
            Vector right = up.CrossProduct(forward).UnitVector;
            up = forward.CrossProduct(right);
            double m00 = right.X;
            double m01 = right.Y;
            double m02 = right.Z;
            double m10 = up.X;
            double m11 = up.Y;
            double m12 = up.Z;
            double m20 = forward.X;
            double m21 = forward.Y;
            double m22 = forward.Z;


            double num8 = m00 + m11 + m22;
            if (num8 > 0f)
            {
                double num = System.Math.Sqrt(num8 + 1f);
                double w = num * 0.5f;
                num = 0.5f / num;
                return new Quaternion((m12 - m21) * num, (m20 - m02) * num, (m01 - m10) * num, w);
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                double num7 = System.Math.Sqrt(1f + m00 - m11 - m22);
                double num4 = 0.5f / num7;
                return new Quaternion(0.5f * num7, (m01 + m10) * num4, (m02 + m20) * num4, (m12 - m21) * num4);
            }
            if (m11 > m22)
            {
                double num6 = (double)System.Math.Sqrt(1f + m11 - m00 - m22);
                double num3 = 0.5f / num6;
                return new Quaternion((m10 + m01) * num3, 0.5f * num6, (m21 + m12) * num3, (m20 - m02) * num3);
            }

            double num5 = System.Math.Sqrt(1f + m22 - m00 - m11);
            double num2 = 0.5f / num5;
            return new Quaternion((m20 + m02) * num2, (m21 + m12) * num2, 0.5f * num5, (m01 - m10) * num2);
        }

        #endregion Public logic

        #region Private logic

        private double GetLength()
        {
            return System.Math.Sqrt(GetLengthSquarted());
        }

        private double GetLengthSquarted()
        {
            return X * X + Y * Y + Z * Z + W * W;
        }

        private IQuaternion GetNormalized()
        {
            double scale = 1.0 / GetLength();
            return new Quaternion(_axis * scale, W * scale);
        }

        #endregion Private logic
    }
}
