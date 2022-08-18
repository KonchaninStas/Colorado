using System;

namespace Colorado.Rendering.Controls.Abstractions.Utils
{
    public interface IFpsCalculator
    {
        int FramesPerSecond { get; }
        double LastFrameRenderingTime { get; }
    }

    public class FpsCalculator : IFpsCalculator
    {
        #region Constants

        private const int defaultFpsValue = 30;

        #endregion Constants

        #region Fields

        private double drawSceneStartTime;

        #endregion Fields

        #region Constructor

        public FpsCalculator(IRenderingControl renderingControl)
        {
            renderingControl.DrawSceneStarted += DrawSceneStartedCallback;
            renderingControl.DrawSceneFinished += DrawSceneFinishedCallback;
        }

        #endregion Constructor

        #region Properties

        public int FramesPerSecond { get; private set; }

        public double LastFrameRenderingTime { get; private set; }

        #endregion Properties

        #region Private logic

        private void DrawSceneStartedCallback(object sender, EventArgs e)
        {
            drawSceneStartTime = ApplicationTimer.SysTime;
        }

        private void DrawSceneFinishedCallback(object sender, EventArgs e)
        {
            LastFrameRenderingTime = ApplicationTimer.SysTime - drawSceneStartTime;
            FramesPerSecond = LastFrameRenderingTime == 0.0 ? defaultFpsValue : (int)(1.0 / LastFrameRenderingTime + 0.5);
            FramesPerSecond = FramesPerSecond > 60 ? 60 : FramesPerSecond;
        }

        #endregion Private logic
    }
}
