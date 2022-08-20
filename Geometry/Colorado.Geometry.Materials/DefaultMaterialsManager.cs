using Colorado.Common.Services;
using Colorado.Geometry.Materials.Readers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Colorado.Geometry.Materials
{
    public interface IDefaultMaterialsManager
    {
        IMaterial this[string materialName] { get; }

        IEnumerable<IMaterial> DefaultMaterials { get; }

        void SetLastConfiguratedMaterial(IMaterial material);
    }

    public class DefaultMaterialsManager : IDefaultMaterialsManager
    {
        #region Constants

        private const string defaultMaterialsFileName = @"Content\DefaultMaterials.xml";
        public const string LastConfiguratedMaterialName = "Last configurated";

        #endregion Constants

        #region Private fields

        private readonly IMessageBoxService _messageBoxService;
        private Dictionary<string, IMaterial> _materialNameToMaterialMap;

        #endregion Private fields

        #region Constructor

        public DefaultMaterialsManager(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
            _materialNameToMaterialMap = new Dictionary<string, IMaterial>()
            {
                { Material.Default.Name, Material.Default}
            };

            LoadDefaultMaterials();
        }

        #endregion Constructor

        #region Properties

        public IMaterial this[string materialName]
        {
            get
            {
                return _materialNameToMaterialMap[materialName];
            }
        }

        public IEnumerable<IMaterial> DefaultMaterials => _materialNameToMaterialMap.Values;

        #endregion Properties

        #region Public logic

        public void SetLastConfiguratedMaterial(IMaterial material)
        {
            material.Name = LastConfiguratedMaterialName;
            _materialNameToMaterialMap[LastConfiguratedMaterialName] = material;
        }

        #endregion Public logic

        #region Private logic

        private void LoadDefaultMaterials()
        {
            string defaultMaterialsFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                defaultMaterialsFileName);

            foreach (Material material in MaterialsReader.Read(defaultMaterialsFile, _messageBoxService))
            {
                _materialNameToMaterialMap[material.Name] = material;
            }
        }

        #endregion Private logic
    }
}
