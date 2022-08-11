using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using Colorado.ModelStructure;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal class ModelKeyController : Controller
    {
        public override string Name => nameof(ModelKeyController);

        public override void OnKeyDown(Keys keyCode, IControllerInputData controllerInputData)
        {
            switch (keyCode)
            {
                default:
                    break;
            }
        }

        private void MoveModel(Vector transaltionVector, IModel model)
        {
            model.ApplyTransform(Transform.CreateTranslation(transaltionVector));
        }
    }
}
