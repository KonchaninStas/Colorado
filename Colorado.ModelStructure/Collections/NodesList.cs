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
        internal NodesList(INode parent)
        {
            Parent = parent;
        }

        internal INode Parent { get; set; }

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
    }
}
