using Colorado.Geometry.Structures.BoundingBoxStructures;
using Colorado.Geometry.Structures.Math;
using Colorado.MeshStructure;
using Colorado.ModelStructure.Collections;

namespace Colorado.ModelStructure
{
    public interface INode
    {
        INodesList Children { get; }
        IMesh Mesh { get; }
        INode Parent { get; set; }
        ITransform RelativeTransform { get; }

        void ApplyRelativeTransform(ITransform transform);
        IBoundingBox CalculateBoundingBox();
        ITransform GetAbsoluteTransform();
    }

    public class Node : INode
    {
        private readonly NodesList _children;

        private INode _parent;

        public Node(IMesh mesh)
        {
            Mesh = mesh;
            _children = new NodesList(_parent);
            RelativeTransform = Transform.Identity();
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

        public ITransform RelativeTransform { get; private set; }

        public ITransform GetAbsoluteTransform()
        {
            return Parent == null ? RelativeTransform : Parent.GetAbsoluteTransform().Multiply(RelativeTransform);
        }

        public void ApplyRelativeTransform(ITransform transform)
        {
            RelativeTransform = RelativeTransform.Multiply(transform);
        }
    }
}
