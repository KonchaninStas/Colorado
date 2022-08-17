using Colorado.Common.Extensions;

namespace Colorado.Geometry.Structures.Primitives
{
    public class Vector
    {
        #region Private fields

        #endregion Private fields

        #region Constructor

        public Vector(Point startPoint, Point endPoint)
          : this(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y, endPoint.Z - startPoint.Z) { }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            Length = CalculateLength();
            IsZero = X.IsZero() && Y.IsZero() && Z.IsZero();

            FloatArray = new float[] { (float)X, (float)Y, (float)Z, 0 };
        }

        #endregion Constructor

        #region Properties

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Length { get; }

        public Vector UnitVector => new Vector(X / Length, Y / Length, Z / Length);

        public bool IsZero { get; }

        public float[] FloatArray { get; }

        public Vector Inversed => this * -1;

        public static Vector ZeroVector => new Vector(0, 0, 0);

        public static Vector XAxis => new Vector(1, 0, 0);

        public static Vector YAxis => new Vector(0, 1, 0);

        public static Vector ZAxis => new Vector(0, 0, 1);

        #endregion Properties

        #region Public logic

        public Vector CrossProduct(Vector anotherVector)
        {
            double x = Y * anotherVector.Z - anotherVector.Y * Z;
            double y = (X * anotherVector.Z - anotherVector.X * Z) * -1;
            double z = X * anotherVector.Y - anotherVector.X * Y;

            return new Vector(x, y, z);
        }

        public double AngleToVectorInRadians(Vector anotherVector)
        {
            return (float)System.Math.Acos(this.CosToVector(anotherVector));
        }

        public double CosToVector(Vector anotherVector)
        {
            return DotProduct(anotherVector) / (Length * anotherVector.Length);
        }

        public double DotProduct(Vector anotherVector)
        {
            return X * anotherVector.X + Y * anotherVector.Y + Z * anotherVector.Z;
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}, Z = {Z}";
        }

        public Ray ToRay()
        {
            return new Ray(this);
        }

        #endregion Public logic

        #region Operators

        public static Vector operator *(Vector vector, double scaleFactor)
        {
            return new Vector(vector.X * scaleFactor, vector.Y * scaleFactor, vector.Z * scaleFactor);
        }

        public static Vector operator +(Vector leftVector, Vector rightVector)
        {
            return new Vector(leftVector.X + rightVector.X, leftVector.Y + rightVector.Y, leftVector.Z + rightVector.Z);
        }

        #endregion Operators

        #region Private logic

        private double CalculateLength()
        {
            return System.Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        #endregion Private logic
    }
}
