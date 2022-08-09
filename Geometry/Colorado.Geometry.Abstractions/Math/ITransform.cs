using Colorado.Geometry.Abstractions.Primitives;

namespace Colorado.Geometry.Abstractions.Math
{
    public interface ITransform
    {
        double this[int index] { get; set; }
        double this[int row, int column] { get; set; }

        double[] Array { get; }
        double Scale { get; }
        IVector Translation { get; set; }

        IPoint ApplyToPoint(IPoint point);
        IVector ApplyToVector(IVector vector);
        ITransform Clone();
        ITransform GetInverted();
        double[] GetOpenGLArray();
        ITransform GetRotationTransform();
        bool IsIdentity();
        ITransform Multiply(ITransform anotherOne);
        void ScaleTranslation(double value);
        void SetTranslation(IPoint point);
        IQuaternion ToQuaternion();
        string ToString();
        void Translate(IVector vector);
    }
}
