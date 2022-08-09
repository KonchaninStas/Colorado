using Colorado.Geometry.Abstractions.BoundingBoxStructures;

namespace Colorado.ModelStructure
{
    public interface IModel
    {
        INode RootNode { get; }
        IBoundingBox TotalBoundingBox { get; }
    }

    public class Model : IModel
    {
        public Model(INode rootNode)
        {
            RootNode = rootNode;
            TotalBoundingBox = RootNode.CalculateBoundingBox();
        }

        public INode RootNode { get; }

        public IBoundingBox TotalBoundingBox { get; }
    }
}
