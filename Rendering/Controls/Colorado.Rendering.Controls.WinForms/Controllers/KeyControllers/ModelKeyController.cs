using Colorado.Documents.ModelStructure;
using Colorado.Geometry.Structures.Math;
using Colorado.Geometry.Structures.Primitives;
using Colorado.Rendering.Controls.WinForms.Controllers.Data;
using System.Windows.Forms;

namespace Colorado.Rendering.Controls.WinForms.Controllers.KeyControllers
{
    internal sealed class ModelKeyController : Controller
    {
        #region Properties

        public override string Name => nameof(ModelKeyController);

        #endregion Properties

        #region Public logic

        public override void OnKeyDown(Keys keyCode, IControllerInputData controllerInputData)
        {
            switch (keyCode)
            {
                default:
                    break;
            }
        }

        #endregion Public logic

        #region Private logic

        private void MoveModel(Vector transaltionVector, IModel model)
        {
            model.ApplyTransform(Transform.CreateTranslation(transaltionVector));
        }

        #endregion Private logic
    }
}
