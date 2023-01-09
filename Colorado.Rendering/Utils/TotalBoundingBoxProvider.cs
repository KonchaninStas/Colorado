using Colorado.Documents;
using Colorado.Geometry.Structures.BaseStructures;
using Colorado.Geometry.Structures.BoundingBoxStructures;
using System.Collections.Generic;

namespace Colorado.Rendering.Utils
{
    public interface ITotalBoundingBoxProvider
    {
        IBoundingBox TotalBoundingBox { get; }
        IBoundingBox NodesBoundingBox { get; }

        void AddRenderableObject(IRenderableObject renderableObject);
        void RemoveRenderableObject(IRenderableObject renderableObject);
    }

    public class TotalBoundingBoxProvider : ITotalBoundingBoxProvider
    {
        #region Private fields

        private readonly IDocumentsManager _documentsManager;
        private readonly HashSet<IRenderableObject> _renderableObjects;

        private IBoundingBox _renderableObjectsBoundingBox;

        #endregion Private fields

        #region Constructor

        public TotalBoundingBoxProvider(IDocumentsManager documentsManager)
        {
            _renderableObjects = new HashSet<IRenderableObject>();
            _documentsManager = documentsManager;
            CalculateRenderableObjectsBoundingBox();
        }

        #endregion Constructor

        #region Properties

        public IBoundingBox TotalBoundingBox =>
           _renderableObjectsBoundingBox.Add(_documentsManager.ActiveDocument.Model.TotalBoundingBox);

        public IBoundingBox NodesBoundingBox => _documentsManager.ActiveDocument.Model.TotalBoundingBox;

        #endregion Properties

        #region Public logic

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

        #endregion Public logic

        #region Private logic

        private void CalculateRenderableObjectsBoundingBox()
        {
            _renderableObjectsBoundingBox = BoundingBox.Empty;

            foreach (IRenderableObject renderableObject in _renderableObjects)
            {
                _renderableObjectsBoundingBox = renderableObject.BoundingBox.Add(renderableObject.BoundingBox);
            }
        }

        #endregion Private logic
    }
}
