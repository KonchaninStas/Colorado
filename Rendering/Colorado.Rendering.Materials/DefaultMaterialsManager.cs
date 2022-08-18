using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Colorado.Rendering.Materials
{
    public interface IDefaultMaterialsManager
    {
        IMaterial this[string materialName] { get; }

        IEnumerable<IMaterial> DefaultMaterials { get; }

        void SetLastConfiguratedMaterial(IMaterial material);
    }

    public class DefaultMaterialsManager : IDefaultMaterialsManager
    {
        #region Singelton implementation

        private static DefaultMaterialsManager instance;

        public static DefaultMaterialsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DefaultMaterialsManager();
                }
                return instance;
            }
        }

        #endregion Singelton implementation

        #region Constants

        private const string defaultMaterialsFileName = @"Content\DefaultMaterials.xml";
        public const string LastConfiguratedMaterialName = "Last configurated";

        #endregion Constants

        #region Private fields

        private Dictionary<string, IMaterial> _materialNameToMaterialMap;

        #endregion Private fields

        #region Constructor

        private DefaultMaterialsManager()
        {
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

            if (File.Exists(defaultMaterialsFile))
            {
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(Material[]));
                    using (var fs = new FileStream(defaultMaterialsFile, FileMode.OpenOrCreate))
                    {
                        Material[] materials = (Material[])formatter.Deserialize(fs);

                        foreach (Material material in materials)
                        {
                            _materialNameToMaterialMap[material.Name] = material;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageViewHandler.ShowExceptionMessage(Resources.ViewerTitle, Resources.Error_DefaultMaterialsFileIsNotValid, ex);
                }
            }
            else
            {
                //MessageViewHandler.ShowWarningMessage(Resources.ViewerTitle, Resources.Error_DefaultMaterialsFileDoesNotExist);
            }
        }

        #endregion Private logic
    }
}
