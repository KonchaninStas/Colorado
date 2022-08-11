using Colorado.Geometry.Abstractions.BoundingBoxStructures;
using Colorado.Geometry.Abstractions.Math;

namespace Colorado.ModelStructure
{
    public interface IModel
    {
        INode RootNode { get; }
        IBoundingBox TotalBoundingBox { get; }

        void ApplyTransform(ITransform transform);
    }

    public class Model : IModel
    {
        public Model(INode rootNode)
        {
            RootNode = rootNode;
            TotalBoundingBox = RootNode.CalculateBoundingBox();
        }

        public INode RootNode { get; }

        public IBoundingBox TotalBoundingBox { get; private set; }

        public void ApplyTransform(ITransform transform)
        {
            RootNode.ApplyRelativeTransform(transform);
            TotalBoundingBox = TotalBoundingBox.ApplyTransform(transform);
        }
    }
}
