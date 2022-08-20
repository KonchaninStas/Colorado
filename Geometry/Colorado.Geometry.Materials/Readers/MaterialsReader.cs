using Colorado.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using Strings = Colorado.Resources.Properties.Resources;

namespace Colorado.Geometry.Materials.Readers
{
    internal static class MaterialsReader
    {
        public static IEnumerable<Material> Read(string fileName, IMessageBoxService messageBoxService)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    var formatter = new XmlSerializer(typeof(SerializableMaterial[]));
                    using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        return ((SerializableMaterial[])formatter.Deserialize(fs)).Select(m => m.ToMaterial());
                    }
                }
                catch (Exception ex)
                {
                    messageBoxService.ShowExceptionMessage(Strings.UI_Title, Strings.Error_DefaultMaterialsFileIsNotValid, ex);
                }
            }
            else
            {
                messageBoxService.ShowExceptionMessage(Strings.UI_Title, Strings.Error_DefaultMaterialsFileDoesNotExist);
            }
            return Enumerable.Empty<Material>();
        }
    }
}
