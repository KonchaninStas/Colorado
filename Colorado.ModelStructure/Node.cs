﻿using Colorado.Geometry.Structures.BoundingBoxStructures;
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

        int TrianglesCount { get; }

        void ApplyRelativeTransform(ITransform transform);
        IBoundingBox CalculateBoundingBox();
        ITransform GetAbsoluteTransform();
    }

    public class Node : INode
    {
        #region Private fields

        private readonly NodesList _children;

        private INode _parent;

        #endregion Private fields

        #region Constructor

        public Node(IMesh mesh)
        {
            Mesh = mesh;
            _children = new NodesList(_parent);
            RelativeTransform = Transform.Identity();
        }

        #endregion Constructor

        #region Properties

        public IMesh Mesh { get; }

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

        public int TrianglesCount
        {
            get
            {
                int trianglesCount = 0;
                if (Mesh != null)
                {
                    trianglesCount += Mesh.Triangles.Count;
                }

                foreach (INode child in Children)
                {
                    trianglesCount += child.TrianglesCount;
                }

                return trianglesCount;
            }
        }

        #endregion Properties

        #region Public logic

        public IBoundingBox CalculateBoundingBox()
        {
            IBoundingBox boundingBox = Mesh.BoundingBox;

            foreach (INode child in Children)
            {
                boundingBox.Add(child.CalculateBoundingBox());
            }

            return boundingBox;
        }



        public ITransform GetAbsoluteTransform()
        {
            return Parent == null ? RelativeTransform : Parent.GetAbsoluteTransform().Multiply(RelativeTransform);
        }

        public void ApplyRelativeTransform(ITransform transform)
        {
            RelativeTransform = RelativeTransform.Multiply(transform);
        }

        #endregion Public logic
    }
}
