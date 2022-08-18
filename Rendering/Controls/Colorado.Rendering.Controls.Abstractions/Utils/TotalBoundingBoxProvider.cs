using Colorado.Documents;
using Colorado.Geometry.Structures.BaseStructures;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using System.Collections.Generic;

namespace Colorado.Rendering.Controls.Abstractions.Utils
{
    public interface ITotalBoundingBoxProvider
    {
        IBoundingBox TotalBoundingBox { get; }

        void AddRenderableObject(IRenderableObject renderableObject);
        void RemoveRenderableObject(IRenderableObject renderableObject);
    }

    public class TotalBoundingBoxProvider : ITotalBoundingBoxProvider
    {
        private readonly IDocumentsManager _documentsManager;
        private readonly HashSet<IRenderableObject> _renderableObjects;

        private IBoundingBox _renderableObjectsBoundingBox;

        public TotalBoundingBoxProvider(IDocumentsManager documentsManager)
        {
            _renderableObjects = new HashSet<IRenderableObject>();
            _documentsManager = documentsManager;
            CalculateRenderableObjectsBoundingBox();
        }

        public void AddRenderableObject(IRenderableObject renderableObject)
        {
            _renderableObjects.Add(renderableObject);
            CalculateRenderableObjectsBoundingBox();
        }

        public void RemoveRenderableObject(IRenderableObject renderableObject)
        {
            _renderableObjects.Remove(renderableObject);
            CalculateRenderableObjectsBoundingBox();
        }

        public IBoundingBox TotalBoundingBox =>
            _renderableObjectsBoundingBox.Add(_documentsManager.ActiveDocument.Model.TotalBoundingBox);

        private void CalculateRenderableObjectsBoundingBox()
        {
            _renderableObjectsBoundingBox = BoundingBox.Empty;

            foreach (IRenderableObject renderableObject in _renderableObjects)
            {
                _renderableObjectsBoundingBox = renderableObject.BoundingBox.Add(renderableObject.BoundingBox);
            }
        }
    }
}
