using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.Math;

namespace Colorado.ModelStructure
{
    public interface IModel
    {
        INode RootNode { get; }
        IBoundingBox TotalBoundingBox { get; }

        void ApplyTransform(ITransform transform);

        int TrianglesCount { get; }
    }

    public class Model : IModel
    {
        #region Constructor

        public Model(INode rootNode)
        {
            RootNode = rootNode;
            TotalBoundingBox = RootNode.CalculateBoundingBox();
        }

        #endregion Constructor

        #region Properties

        public INode RootNode { get; }

        public IBoundingBox TotalBoundingBox { get; private set; }

        public int TrianglesCount => RootNode.TrianglesCount;

        #endregion Properties

        #region Public logic

        public void ApplyTransform(ITransform transform)
        {
            RootNode.ApplyRelativeTransform(transform);
            TotalBoundingBox = TotalBoundingBox.ApplyTransform(transform);
        }

        #endregion Public logic
    }
}
