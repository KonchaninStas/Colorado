using Colorado.Geometry.Abstractions.BoundingBoxStructures;
using Colorado.MeshStructure;
using Colorado.ModelStructure.Collections;

namespace Colorado.ModelStructure
{
    public interface INode
    {
        INodesList Children { get; }
        IMesh Mesh { get; }
        INode Parent { get; set; }

        IBoundingBox CalculateBoundingBox();
    }

    public class Node : INode
    {
        private readonly NodesList _children;

        private INode _parent;

        public Node(IMesh mesh)
        {
            Mesh = mesh;
            _children = new NodesList(_parent);
        }

        public IMesh Mesh { get; }

        public IBoundingBox CalculateBoundingBox()
        {
            IBoundingBox boundingBox = Mesh.BoundingBox;

            foreach (INode child in Children)
            {
                boundingBox.Add(child.CalculateBoundingBox());
            }

            return boundingBox;
        }

        public INode Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent?.Children.Remove(this);
                _parent = value;
                _parent.Children.Add(this);
                _children.Parent = value;
            }
        }

        public INodesList Children => _children;
    }
}
