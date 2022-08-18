using System.Collections.Generic;

namespace Colorado.ModelStructure.Collections
{
    public interface INodesList : IList<INode>
    {
        new void Add(INode node);
        new bool Remove(INode node);
    }

    public class NodesList : List<INode>, INodesList
    {
        #region Constructor

        internal NodesList(INode parent)
        {
            Parent = parent;
        }

        #endregion Constructor

        #region Properties

        internal INode Parent { get; set; }

        #endregion Properties

        #region Public logic

        public new void Add(INode node)
        {
            node.Parent?.Children.Remove(node);
            base.Add(node);
            node.Parent = Parent;
        }

        public new bool Remove(INode node)
        {
            node.Parent = null;
            return base.Remove(node);
        }

        #endregion Public logic
    }
}
