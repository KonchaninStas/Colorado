﻿using Colorado.Application;
using Colorado.Common.UI.WPF.ViewModels.Base;
using Colorado.Documents;
using Colorado.Rendering.Controls.Abstractions;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Managers;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Rendering;
using Colorado.Rendering.Controls.OpenGL.OpenGLRenderingControl.Scene;
using Colorado.Rendering.Controls.WPF;
using System.Linq;

namespace Colorado.Viewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            IProgram program = new Program();

            program.DocumentsManager.OpenDocument(
                program.DocumentsManager.GetDefaultDocumentsNames().FirstOrDefault(d => d.Contains("Star")));

            IRenderingControl renderingControl = new OpenGLRenderingControl(program,
                new OpenGLLightsManager(),
                new OpenGLViewport(new OpenGLCamera(program.DocumentsManager), program.DocumentsManager),
                new OpenGLGeometryRenderer(new OpenGLMaterialsManager()));

            WPFRenderingControl = new WPFRenderingControl(renderingControl);
        }

        public WPFRenderingControl WPFRenderingControl { get; }
    }
}
