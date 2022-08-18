using Colorado.Documents;
using Colorado.Rendering.Controls.Abstractions.Utils;

namespace Colorado.Rendering.Controls.Abstractions.Statistics
{
    public interface IRenderingControlStatistics
    {
        double FPS { get; }
        int TrianglesCount { get; }
    }

    public class RenderingControlStatistics : IRenderingControlStatistics
    {
        #region Private fields

        private readonly IDocumentsManager _documentsManager;
        private readonly IFpsCalculator _fpsCalculator;

        #endregion Private fields

        #region Constructor

        public RenderingControlStatistics(IDocumentsManager documentsManager, IFpsCalculator fpsCalculator)
        {
            _documentsManager = documentsManager;
            _fpsCalculator = fpsCalculator;
        }

        #endregion Constructor

        #region Properties

        public int TrianglesCount => _documentsManager.ActiveDocument.Model.TrianglesCount;

        public double FPS => _fpsCalculator.FramesPerSecond;

        #endregion Properties
    }
}
