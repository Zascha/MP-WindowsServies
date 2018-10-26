using System.Configuration;

namespace MP.WindowsServices.Common.ConfigurationHelper
{
    internal interface IConfigurationCollectionElement
    {
        object Key { get; }
    }

    internal class ObservableFolderElement : ConfigurationElement, IConfigurationCollectionElement
    {
        [ConfigurationProperty("path", IsKey = true, IsRequired = true)]
        public string Path => (string)base["path"];

        public object Key => Path;
    }
}
