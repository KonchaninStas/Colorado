using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Enumerations;
using Colorado.Rendering.Controls.OpenGL.OpenGLAPI.InternalAPI.General;

namespace Colorado.Rendering.Controls.OpenGL.OpenGLAPI.Wrappers.General
{
    public static class OpenGLGeneralWrapper
    {
        public static void EnableCapability(OpenGLCapability capability)
        {
            OpenGLAPIGeneral.Enable((int)capability);
        }

        public static void DisableCapability(OpenGLCapability capability)
        {
            OpenGLAPIGeneral.Disable((int)capability);
        }

        public static bool IsEnabled(OpenGLCapability capability)
        {
            return OpenGLAPIGeneral.IsEnabled((int)capability);
        }
    }
}
